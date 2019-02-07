namespace Panelak.Database.Oracle
{
    using Panelak.Sql;
    using Microsoft.Extensions.Logging;
    using global::Oracle.ManagedDataAccess.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;

    /// <summary>
    /// SQL builder for building Oracle SQL queries
    /// </summary>
    public class OracleSqlBuilder : SqlBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OracleSqlBuilder"/> class.
        /// </summary>
        /// <param name="connection">Oracle connection</param>
        /// <param name="log">Logger instance</param>
        public OracleSqlBuilder(OracleConnection connection, ILogger log)
            : base(connection, new OracleConditionStringBuilder(), new OracleCommandBuilder(), log)
        {
        }

        /// <summary>
        /// Creates an Oracle parameterized query with an SQL text from given <see cref="ISqlSelectQuery"/>
        /// </summary>
        /// <param name="sqlQuery">SQL query model</param>
        /// <returns>Parameterized query model</returns>
        public override IParameterizedQuery BuildQuery(ISqlSelectQuery sqlQuery)
        {
            Logger.LogTrace("Building an Oracle SQL query");

            string columns = GetColumns(sqlQuery);
            string table = QuoteIdentifier(sqlQuery.TableIdentifier.Table);

            string conditions;
            IEnumerable<ISqlParameter> parameters;
            if (sqlQuery.Condition != null)
            {
                IUncheckedSqlCondition uncheckedCondition = ConditionStringBuilder.Build(sqlQuery.Condition);
                conditions = uncheckedCondition.SqlConditionString;
                parameters = sqlQuery.Parameters.Union(uncheckedCondition.Parameters);

                if (!Connection.CheckIfTableExists(sqlQuery.TableIdentifier))
                    throw new InvalidOperationException($"Table {sqlQuery.TableIdentifier.Table} does not exist or is not accessible.");

                foreach (string column in uncheckedCondition.UncheckedIdentifiers)
                {
                    if (!Connection.CheckIfColumnExists(sqlQuery.TableIdentifier, column))
                        throw new InvalidOperationException($"Column {sqlQuery.TableIdentifier.Table}.{column} does not exist or is not accessible.");
                }
            }
            else
            {
                parameters = sqlQuery.Parameters;
                conditions = "1 = 1";
            }

            if (sqlQuery.NoPagination)
            {
                string noPaginationQuery = $"SELECT {columns} FROM {table} WHERE({conditions})";
                return new OracleDbQuery(noPaginationQuery, sqlQuery.Parameters);
            }

            var orderByColumns = sqlQuery.Columns.Where(col => col.SortOrder != SortOrder.Unspecified)
                                         .Select(col => QuoteIdentifier(col.Expression) + " " + (col.SortOrder == SortOrder.Ascending ? "ASC" : "DESC"))
                                         .ToList();

            orderByColumns.Reverse();

            if (!orderByColumns.Any())
                throw new InvalidOperationException("At least one column for sorting must be specified for pagination to work");

            string orderby = String.Join(", ", orderByColumns);

            int offset, limit;
            if (sqlQuery.Page == 1)
            {
                offset = 1;
                limit = sqlQuery.PageSize;
            }
            else
            {
                offset = sqlQuery.Page * sqlQuery.PageSize;
                limit = offset + sqlQuery.PageSize - 1;
            }

            string query = $@"SELECT {columns} 
                                FROM (SELECT {columns}, ROW_NUMBER() OVER (ORDER BY {orderby}) rn 
                                        FROM {table} 
                                       WHERE ({conditions}))
                               WHERE rn BETWEEN {offset} AND {limit} ORDER BY rn";

            return new OracleDbQuery(query, parameters);
        }

        /// <summary>
        /// Returns string representation of Oracle quoted columns
        /// </summary>
        /// <param name="sqlQuery">SQL query model</param>
        /// <returns>Joined string of SQL SELECT columns</returns>
        private string GetColumns(ISqlSelectQuery sqlQuery)
        {
            IEnumerable<ISqlColumn> cols = sqlQuery.Columns.Where(col => col.Include);
            string columns = String.Join(", ", cols.Select(col =>
            {
                string columnExpression = col.Unquoted ? col.Expression : QuoteIdentifier(col.Expression);

                if (!sqlQuery.ExcludeAliases && !String.IsNullOrEmpty(col.ExpressionAlias))
                    columnExpression += " " + QuoteIdentifier(col.ExpressionAlias);

                return columnExpression;
            }));
            return columns;
        }
    }
}
