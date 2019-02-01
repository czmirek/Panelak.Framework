namespace Panelak.Database.SqlServer
{
    using Panelak.Sql;
    using Microsoft.Extensions.Logging;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Reflection;

    /// <summary>
    /// SQL Server specific connection driver
    /// </summary>
    public class MSSQLConnection : BaseConnection
    {
        /// <summary>
        /// SQL Server query builder
        /// </summary>
        private readonly MSSQLBuilder sqlBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQLConnection"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="mapper">DTO mapper</param>
        /// <param name="logger">Logger instance</param>
        public MSSQLConnection(string connectionString, IDtoMapper mapper, ILogger logger)
            : base(connectionString, mapper, logger) 
            => sqlBuilder = new MSSQLBuilder(this, logger);

        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQLConnection"/> class with
        /// a default DTO mapper.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="logger">Logger instance</param>
        public MSSQLConnection(string connectionString, ILogger logger)
            : base(connectionString, new MSSQLDtoMapper(), logger) 
            => sqlBuilder = new MSSQLBuilder(this, logger);

        /// <summary>
        /// Gets the SQL Server database type
        /// </summary>
        protected override DatabaseType DatabaseType => DatabaseType.SQLServer;

        /// <summary>
        /// Attempts to return a primary key for given SQL Server table.
        /// </summary>
        /// <param name="tableIdentifier">Table identifier</param>
        /// <param name="primaryKey">Primary key identifier</param>
        /// <returns>True whether the attempt to find a primary key succeeded</returns>
        public override bool TryGetPrimaryKeyForTableAsync(ISqlTableIdentifier tableIdentifier, out string primaryKey)
        {
            primaryKey = GetPrimaryKeyForTableAsync(tableIdentifier);
            return primaryKey != null;
        }

        /// <summary>
        /// Returns the name of the first column in given SQL Server table
        /// </summary>
        /// <param name="tableIdentifier">Table identifier</param>
        /// <returns>Name of the first column</returns>
        public override string GetFirstColumnNameAsync(ISqlTableIdentifier tableIdentifier)
        {
            object[] parameters = new object[] { new { tableName = tableIdentifier.Table }, new { schema = tableIdentifier.Schema } };
            string query = @"SELECT TOP 1 COLUMN_NAME 
                                               FROM INFORMATION_SCHEMA.COLUMNS
                                              WHERE UPPER(TABLE_NAME) = UPPER({tableName}) AND UPPER(TABLE_SCHEMA) = UPPER({schema})
                                           ORDER BY ORDINAL_POSITION";

            return GetScalar<string>(query, parameters);
        }

        /// <summary>
        /// Checks if table exists in SQL Server database
        /// </summary>
        /// <param name="tableIdentifier">Table identifier model</param>
        /// <returns>True if table exists</returns>
        public override bool CheckIfTableExists(ISqlTableIdentifier tableIdentifier)
        {
            int count = GetScalar<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE UPPER(TABLE_NAME) = UPPER({table})", 
                new { table = tableIdentifier.Table });

            return count > 0;
        }

        /// <summary>
        /// Checks if column exists in SQL Server database
        /// </summary>
        /// <param name="tableIdentifier">Table identifier</param>
        /// <param name="columnIdentifier">Column identifier</param>
        /// <returns>True if column exists</returns>
        public override bool CheckIfColumnExists(ISqlTableIdentifier tableIdentifier, string columnIdentifier)
        {
            object[] parameters = new object[] { new { tableName = tableIdentifier.Table }, new { columnName = columnIdentifier } };
            string query = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = {tableName} AND COLUMN_NAME = {columnName}";
            int count = GetScalar<int>(query, parameters);
            return count > 0;
        }

        /// <summary>
        /// Converts SQL SELECT query model into the SQL Server parameterized query
        /// </summary>
        /// <param name="sqlQuery">SQL SELECT query model</param>
        /// <returns>Parameterized SQL Server query</returns>
        public override IParameterizedQuery BuildQuery(ISqlSelectQuery sqlQuery)
            => sqlBuilder.BuildQuery(sqlQuery);

        /// <summary>
        /// Builds a COUNT query for checking the number of rows from a given SQL SELECT query model.
        /// </summary>
        /// <param name="sqlQuery">SQL SELECT query model</param>
        /// <returns>Parameterized SQL Server query</returns>
        public override IParameterizedQuery BuildCountQuery(ISqlSelectQuery sqlQuery)
            => sqlBuilder.BuildCountQuery(sqlQuery.TableIdentifier, sqlQuery.Condition);

        /// <summary>
        /// Returns the parameter token used to prefix the parameters in queries with regexes
        /// </summary>
        /// <returns>The parameter token</returns>
        protected override string GetParameterToken() => "@$1";

        /// <summary>
        /// Returns the SQL Server managed connection for given connection string
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>SQL Server managed connection</returns>
        protected override DbConnection GetConnectionProtected(string connectionString) => new SqlConnection(connectionString);

        /// <summary>
        /// Returns the SQL Server managed data adapter for given command
        /// </summary>
        /// <param name="command">SQL Server command to enter the data adapter</param>
        /// <returns>The SQL Server data adapter</returns>
        protected override DataAdapter GetDataAdapterForCommand(DbCommand command) => new SqlDataAdapter(command as SqlCommand);

        /// <summary>
        /// Returns the primary key for given table
        /// </summary>
        /// <param name="tableIdentifier">Table identifier</param>
        /// <returns>Primary key</returns>
        private string GetPrimaryKeyForTableAsync(ISqlTableIdentifier tableIdentifier)
                      => GetScalar<string>(@"SELECT COLUMN_NAME
                                               FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                                              WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1
                                                AND UPPER(TABLE_NAME) = UPPER({tableName})", 
                                                new { tableName = tableIdentifier.Table });
    }
}
