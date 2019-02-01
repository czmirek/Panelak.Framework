namespace Panelak.Database.SqlServer
{
    using Panelak.Sql;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>
    /// SQL builder for building SQL Server queries
    /// </summary>
    public class MSSQLBuilder : SqlBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQLBuilder"/> class.
        /// </summary>
        /// <param name="connection">SQL Server connection</param>
        /// <param name="log">Logger instance</param>
        public MSSQLBuilder(MSSQLConnection connection, ILogger log)
            : base(connection, new MSSQLConditionStringBuilder(), new SqlCommandBuilder(), log)
        {
        }

        /// <summary>
        /// Creates an SQL Server parameterized query with an SQL text from given <see cref="ISqlSelectQuery"/>
        /// </summary>
        /// <param name="sqlQuery">SQL query model</param>
        /// <returns>Parameterized query model</returns>
        public override IParameterizedQuery BuildQuery(ISqlSelectQuery sqlQuery)
        {
            (string columns, string aliasColumns) = GetColumns(sqlQuery);
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
                return new MSSQLDbQuery(noPaginationQuery, parameters);
            }

            var orderByColumns = sqlQuery.Columns.Where(col => col.SortOrder != Sql.SortOrder.Unspecified)
                                                          .Select(col => QuoteIdentifier(col.Expression) + " " + (col.SortOrder == Sql.SortOrder.Ascending ? "ASC" : "DESC"))
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

            string query = $@"SELECT {aliasColumns} 
                                FROM (SELECT {columns}, ROW_NUMBER() OVER (ORDER BY {orderby}) rn 
                                        FROM {table}
                                       WHERE ({conditions})) tbl
                               WHERE tbl.rn BETWEEN {offset} AND {limit} ORDER BY tbl.rn";

            return new MSSQLDbQuery(query, parameters);
        }

        /// <summary>
        /// Returns string representation of SQL Server quoted columns
        /// </summary>
        /// <param name="sqlQuery">SQL query model</param>
        /// <returns>Joined string of SQL SELECT columns</returns>
        private (string columns, string aliasColumns) GetColumns(ISqlSelectQuery sqlQuery)
        {
            IEnumerable<ISqlColumn> cols = sqlQuery.Columns.Where(col => col.Include);

            string columns = String.Join(", ", cols.Select(col =>
            {
                string columnExpression = QuoteIdentifier(col.Expression);

                if (!sqlQuery.ExcludeAliases && col.ExpressionAlias != null)
                    columnExpression += " " + QuoteIdentifier(col.ExpressionAlias);

                return columnExpression;
            }));

            string aliasColumns = String.Join(", ", cols.Select(col =>
            {
                string columnExpression = QuoteIdentifier("tbl") + "." + QuoteIdentifier(col.Expression);

                if (!sqlQuery.ExcludeAliases && col.ExpressionAlias != null)
                    columnExpression = QuoteIdentifier("tbl") + "." + QuoteIdentifier(col.ExpressionAlias);

                return columnExpression;
            }));

            return (columns, aliasColumns);
        }
    }
}
