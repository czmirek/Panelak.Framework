namespace Panelak.Database
{
    using Panelak.Sql;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;

    /// <summary>
    /// Builder for constructing SQL queries out of SQL SELECT query models.
    /// </summary>
    public abstract class SqlBuilder : ISqlBuilder
    {
        /// <summary>
        /// Connection to which the query is supposed to be called
        /// </summary>
        protected readonly BaseConnection Connection;

        /// <summary>
        /// SQL condition string builder
        /// </summary>
        protected readonly SqlConditionStringBuilder ConditionStringBuilder;

        /// <summary>
        /// System command builder for using the correct identifier quoting function
        /// </summary>
        protected readonly DbCommandBuilder DbCommandBuilder;

        /// <summary>
        /// Logger instance
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlBuilder"/> class.
        /// </summary>
        /// <param name="connection">Connection on which the query is to be executed</param>
        /// <param name="conditionStringBuilder">Condition string builder</param>
        /// <param name="dbCommandBuilder">System command builder</param>
        /// <param name="logger">Logger instance</param>
        public SqlBuilder(BaseConnection connection,
            SqlConditionStringBuilder conditionStringBuilder,
            DbCommandBuilder dbCommandBuilder,
            ILogger logger)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            ConditionStringBuilder = conditionStringBuilder ?? throw new ArgumentNullException(nameof(conditionStringBuilder));
            DbCommandBuilder = dbCommandBuilder ?? throw new ArgumentNullException(nameof(dbCommandBuilder));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// The BuildCountQuery
        /// </summary>
        /// <param name="table">The table<see cref="ISqlTableIdentifier"/></param>
        /// <param name="filter">The filter<see cref="ISqlConditionExpression"/></param>
        /// <returns>The <see cref="IParameterizedQuery"/></returns>
        public IParameterizedQuery BuildCountQuery(ISqlTableIdentifier table, ISqlConditionExpression filter)
        {
            string conditions;
            IEnumerable<ISqlParameter> parameters;
            if (filter != null)
            {
                IUncheckedSqlCondition uncheckedCondition = ConditionStringBuilder.Build(filter);
                conditions = uncheckedCondition.SqlConditionString;
                parameters = uncheckedCondition.Parameters;

                foreach (string column in uncheckedCondition.UncheckedIdentifiers)
                {
                    Logger.LogTrace($"Checking existence of {table.Table}.{column}");

                    if (!Connection.CheckIfColumnExists(table, column))
                        throw new InvalidOperationException($"Column {table.Table}.{column} does not exist or is not accessible.");
                }
            }
            else
            {
                conditions = "1 = 1";
                parameters = Enumerable.Empty<ISqlParameter>();
            }

            string tableStr = DbCommandBuilder.QuoteIdentifier(table.Table);
            string sql = $"SELECT COUNT(*) FROM {tableStr} WHERE ({conditions})";

            return new DbQuery(sql, parameters);
        }

        /// <summary>
        /// Constructs a parameterized query model out of the SQL SELECT query model.
        /// </summary>
        /// <param name="sqlQuery">SQL SELECT query model</param>
        /// <returns>Parameterized query model</returns>
        public abstract IParameterizedQuery BuildQuery(ISqlSelectQuery sqlQuery);

        /// <summary>
        /// Quotes the identifier using RDBMS specific method
        /// </summary>
        /// <param name="str">Identifier string</param>
        /// <returns>Quoted identifier string</returns>
        protected string QuoteIdentifier(string str) => DbCommandBuilder.QuoteIdentifier(str);
    }
}
