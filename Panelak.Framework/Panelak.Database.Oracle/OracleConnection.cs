namespace Panelak.Database.Oracle
{
    using Panelak.Sql;
    using Microsoft.Extensions.Logging;
    using global::Oracle.ManagedDataAccess.Client;
    using System.Data.Common;

    /// <summary>
    /// Oracle specific connection driver
    /// </summary>
    public class OracleConnection : BaseConnection
    {
        /// <summary>
        /// Oracle SQL builder
        /// </summary>
        private readonly OracleSqlBuilder sqlBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleConnection"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="mapper">DTO mapper</param>
        /// <param name="logger">Logger instance</param>
        public OracleConnection(string connectionString, IDtoMapper mapper, ILogger logger)
            : base(connectionString, mapper, logger) 
            => sqlBuilder = new OracleSqlBuilder(this, logger);

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleConnection"/> class with
        /// a default DTO mapper.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="logger">Logger instance</param>
        public OracleConnection(string connectionString, ILogger logger)
            : base(connectionString, new DefaultDtoMapper(), logger)
            => sqlBuilder = new OracleSqlBuilder(this, logger);

        /// <summary>
        /// Gets the Oracle database type
        /// </summary>
        protected override DatabaseType DatabaseType => DatabaseType.Oracle;

        /// <summary>
        /// Attempts to return a primary key for given Oracle table.
        /// </summary>
        /// <param name="table">Table identifier</param>
        /// <param name="primaryKey">Primary key identifier</param>
        /// <returns>True whether the attempt to find a primary key succeeded</returns>
        public override bool TryGetPrimaryKeyForTableAsync(ISqlTableIdentifier table, out string primaryKey)
        {
            primaryKey = GetPrimaryKeyForTableAsync(table);
            return primaryKey != null;
        }

        /// <summary>
        /// Returns the name of the first column in given oracle table
        /// </summary>
        /// <param name="table">Table identifier</param>
        /// <returns>Name of the first column</returns>
        public override string GetFirstColumnNameAsync(ISqlTableIdentifier table) 
                      => GetScalar<string>(@"SELECT column_name 
                                               FROM ALL_TAB_COLUMNS cols 
                                              WHERE UPPER(cols.table_name) = UPPER({tableName}) 
                                           ORDER BY COLUMN_ID",
                                      new { tableName = table.Table });

        /// <summary>
        /// Returns a total number of records in a given table.
        /// </summary>
        /// <param name="sqlQuery">SQL SELECT query model</param>
        /// <returns>Number of total records</returns>
        public override int GetTotalRecords(ISqlSelectQuery sqlQuery)
        {
            IParameterizedQuery countQuery = BuildCountQuery(sqlQuery);
            return (int)GetScalar<decimal>(countQuery);
        }

        /// <summary>
        /// Checks if table exists in Oracle database
        /// </summary>
        /// <param name="tableIdentifier">Table identifier model</param>
        /// <returns>True if table exists</returns>
        public override bool CheckIfTableExists(ISqlTableIdentifier tableIdentifier)
        {
            int count = (int)GetScalar<decimal>(@"SELECT SUM(tbl.cnt) 
                                                   FROM (
                                                         SELECT COUNT(*) cnt FROM user_tables WHERE UPPER(table_name) = UPPER({tableName})
                                                         UNION
                                                         SELECT COUNT(*) cnt FROM all_views WHERE UPPER(view_name) = UPPER({tableName})
                                                        ) tbl", new { tableName = tableIdentifier.Table });

            return count > 0;
        }

        /// <summary>
        /// Checks if column exists in Oracle database
        /// </summary>
        /// <param name="tableIdentifier">Table identifier</param>
        /// <param name="column">Column identifier</param>
        /// <returns>True if column exists</returns>
        public override bool CheckIfColumnExists(ISqlTableIdentifier tableIdentifier, string column)
        {
            string query = @"SELECT COUNT(*) FROM all_tab_cols WHERE table_name = {tableName} AND column_name = {columnName}";
            object[] parameters = new object[] { new { tableName = tableIdentifier.Table }, new { columnName = column } };
            int count = (int)GetScalar<decimal>(query, parameters);
            return count > 0;
        }

        /// <summary>
        /// Converts SQL SELECT query model into the Oracle SQL parameterized query
        /// </summary>
        /// <param name="sqlQuery">SQL SELECT query model</param>
        /// <returns>Parameterized Oracle query</returns>
        public override IParameterizedQuery BuildQuery(ISqlSelectQuery sqlQuery)
            => sqlBuilder.BuildQuery(sqlQuery);

        /// <summary>
        /// Builds a COUNT query for checking the number of rows from a given SQL SELECT query model.
        /// </summary>
        /// <param name="sqlQuery">SQL SELECT query model</param>
        /// <returns>Parameterized Oracle query</returns>
        public override IParameterizedQuery BuildCountQuery(ISqlSelectQuery sqlQuery)
            => sqlBuilder.BuildCountQuery(sqlQuery.TableIdentifier, sqlQuery.Condition);

        /// <summary>
        /// Returns the Oracle managed connection for given connection string
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Oracle managed connection</returns>
        protected override DbConnection GetConnectionProtected(string connectionString) => new global::Oracle.ManagedDataAccess.Client.OracleConnection(connectionString);

        /// <summary>
        /// Returns the Oracle managed data adapter for given command
        /// </summary>
        /// <param name="command">Oracle command to enter the data adapter</param>
        /// <returns>The oracle data adapter</returns>
        protected override DataAdapter GetDataAdapterForCommand(DbCommand command) => new OracleDataAdapter(command as OracleCommand);

        /// <summary>
        /// Returns the parameter token used to prefix the parameters in queries with regexes
        /// </summary>
        /// <returns>The parameter token</returns>
        protected override string GetParameterToken() => ":$1";

        /// <summary>
        /// Returns the primary key for given table
        /// </summary>
        /// <param name="table">Table identifier</param>
        /// <returns>Primary key</returns>
        private string GetPrimaryKeyForTableAsync(ISqlTableIdentifier table)
               => GetScalar<string>(@"SELECT column_name 
                                        FROM all_constraints cons, all_cons_columns cols 
                                       WHERE UPPER(cols.table_name) = UPPER({tableName})
                                         AND cons.constraint_type = 'P' 
                                         AND cons.constraint_name = cols.constraint_name 
                                    ORDER BY cols.table_name, cols.position",
                                     new[] { new { tableName = table.Table } });
    }
}
