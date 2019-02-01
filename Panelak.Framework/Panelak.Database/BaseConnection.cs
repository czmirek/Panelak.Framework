namespace Panelak.Database
{
    using Panelak.Sql;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Connection driver for basic database task.
    /// </summary>
    public abstract class BaseConnection : IConnectionLink
    {
        /// <summary>
        /// Connection string used in this connection.
        /// </summary>
        protected readonly string ConnectionString;

        /// <summary>
        /// Regex used for replacement of RDBMS-independent parameters used in SQL queries in the format {parameter}.
        /// This is then replaced into RDBMS-specific implementation in the <see cref="GetParameterToken"/> method.
        /// </summary>
        private static readonly Regex QueryParametersRegex = new Regex("\\{(\\w+)\\}", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        /// <summary>
        /// Mapper used for mapping results to DTOs in this connection.
        /// </summary>
        private readonly IDtoMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConnection"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="dtoMapper">The DTO mapper</param>
        /// <param name="log">The logger instance</param>
        public BaseConnection(string connectionString, IDtoMapper dtoMapper, ILogger log)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            mapper = dtoMapper ?? throw new ArgumentNullException(nameof(dtoMapper));
            Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Gets the logger instance
        /// </summary>
        protected ILogger Log { get; }

        /// <summary>
        /// Gets the RDBMS type
        /// </summary>
        protected abstract DatabaseType DatabaseType { get; }

        /// <summary>
        /// Returns a DataSet for given SQL query
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <returns>Data set result</returns>
        public DataSet GetDataTable(string query) => GetDataTable(new DbQuery(query, Enumerable.Empty<ISqlParameter>()));

        /// <summary>
        /// Attempts to find and return the primary key found for given table. Implementation is RDBMS specific.
        /// </summary>
        /// <param name="table">Identification of the table where primary key is to be found</param>
        /// <param name="primaryKey">Primary key is returned in this out parameter.</param>
        /// <returns>True if primary key was identified, false if not.</returns>
        public abstract bool TryGetPrimaryKeyForTableAsync(ISqlTableIdentifier table, out string primaryKey);

        /// <summary>
        /// Returns a first column in given table. Implementation is RDBMS specific.
        /// </summary>
        /// <param name="table">Table in which the first column is to be found</param>
        /// <returns>Name of the first column</returns>
        public abstract string GetFirstColumnNameAsync(ISqlTableIdentifier table);

        /// <summary>
        /// RDBMS specific check for table existence
        /// </summary>
        /// <param name="tableIdentifier">Table identifier model</param>
        /// <returns>True if the table exists</returns>
        public abstract bool CheckIfTableExists(ISqlTableIdentifier tableIdentifier);

        /// <summary>
        /// RDBMS specific check for column existence
        /// </summary>
        /// <param name="tableIdentifier">Table identifier model</param>
        /// <param name="column">Column identifier</param>
        /// <returns>True if column exists</returns>
        public abstract bool CheckIfColumnExists(ISqlTableIdentifier tableIdentifier, string column);

        /// <summary>
        /// Build a parameterized query model out of the SQL SELECT query model
        /// </summary>
        /// <param name="sqlQuery">SQL SELECT query model</param>
        /// <returns>Parameterized query model</returns>
        public abstract IParameterizedQuery BuildQuery(ISqlSelectQuery sqlQuery);

        /// <summary>
        /// Converts a SQL query into a parameterized query used for determining row count
        /// </summary>
        /// <param name="sqlQuery">SQL SELECT COUNT query model</param>
        /// <returns>Parameterized COUNT query</returns>
        public abstract IParameterizedQuery BuildCountQuery(ISqlSelectQuery sqlQuery);

        /// <summary>
        /// Returns a result as a RDBMS-specific <see cref="DataSet"/> for a parameterized query.
        /// Individual parameters, in present, are converted into the relevant RDMBS-specific <see cref="DbParameter"/>.
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <returns>RDBMS-specific <see cref="DataSet"/>.</returns>
        public DataSet GetDataTable(IParameterizedQuery query)
        {
            LogQuery(query);

            string queryText = TranslateQueryParameters(query.QueryText);

            using (DbConnection db = GetConnection())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = queryText;

                foreach (ISqlParameter param in query.SqlParameters)
                {
                    System.Data.Common.DbParameter dbParam = command.CreateParameter();
                    dbParam.ParameterName = param.Name;
                    dbParam.Value = param.Value;
                    command.Parameters.Add(dbParam);
                }

                DataAdapter dataAdapter = GetDataAdapterForCommand(command);
                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }

        /// <summary>
        /// Returns a result enumeration from given <paramref name="query"/> into a list of a given DTO (<typeparamref name="T"/>).
        /// </summary>
        /// <typeparam name="T">Type of the DTO</typeparam>
        /// <param name="query">Query returning a result</param>
        /// <returns>DTO enumeration</returns>
        public IEnumerable<T> GetResult<T>(string query) where T : new()
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);
            using (DbConnection db = GetConnection())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = query;
                IDataReader dataReader = command.ExecuteReader();
                return mapper.MapToDto<T>(dataReader, DatabaseType);
            }
        }

        /// <summary>
        /// Returns a result enumeration from given parameterized query into a list of a given DTO.
        /// </summary>
        /// <typeparam name="T">Type of the DTO</typeparam>
        /// <param name="query">Parameterized query</param>
        /// <returns>DTO enumeration</returns>
        public IEnumerable<T> GetResult<T>(IParameterizedQuery query) where T : new()
        {
            LogQuery(query);

            string queryText = TranslateQueryParameters(query.QueryText);

            using (DbConnection db = GetConnection())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = queryText;

                foreach (ISqlParameter param in query.SqlParameters)
                {
                    System.Data.Common.DbParameter dbParam = command.CreateParameter();
                    dbParam.ParameterName = param.Name;
                    dbParam.Value = param.Value;
                    command.Parameters.Add(dbParam);
                }

                IDataReader dataReader = command.ExecuteReader();
                return mapper.MapToDto<T>(dataReader, DatabaseType);
            }
        }

        /// <summary>
        /// Returns a result enumeration from given <paramref name="query"/> into a list of a given DTO (<typeparamref name="T"/>)
        /// and mapping parameters into the query from <paramref name="queryParams"/>.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="queryParams">Query parameters object</param>
        /// <returns>Enumeration of DTOs from the result.</returns>
        public IEnumerable<T> GetResult<T>(string query, object queryParams) where T : new()
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);

            using (DbConnection db = GetConnection())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = query;

                ProcessParams(queryParams, command);

                IDataReader dataReader = command.ExecuteReader();

                return mapper.MapToDto<T>(dataReader, DatabaseType);
            }
        }

        /// <summary>
        /// Returns a result enumeration for a given query with a single ID parameter.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">Value of the ID parameter</param>
        /// <returns>Enumeration of DTOs from the result.</returns>
        public IEnumerable<T> GetResult<T>(string query, int idParam) where T : new() => GetResult<T>(query, new { id = idParam });

        /// <summary>
        /// Returns a value from the first row of the first column in the query result.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query</param>
        /// <returns>Scalar value</returns>
        public T GetScalar<T>(string query)
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);

            using (DbConnection db = GetConnection())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = query;

                return (T)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Returns a value from the first row of the first column in the query result using parameters.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query with RDBMS-independent parameters.</param>
        /// <param name="queryParams">Query parameters</param>
        /// <returns>Scalar value</returns>
        public T GetScalar<T>(string query, object queryParams)
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);

            using (DbConnection db = GetConnection())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = query;
                ProcessParams(queryParams, command);

                return (T)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Returns a value from the first row of the first column in the query result.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">ID parameter included in the query</param>
        /// <returns>Scalar value</returns>
        public T GetScalar<T>(string query, int idParam) => GetScalar<T>(query, new { id = idParam });

        /// <summary>
        /// Returns a value from the first row of the first column in the query result.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query with parameters</param>
        /// <returns>Scalar value</returns>
        public T GetScalar<T>(IParameterizedQuery query)
        {
            LogQuery(query);

            string queryText = TranslateQueryParameters(query.QueryText);

            using (DbConnection db = GetConnection())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = queryText;

                foreach (ISqlParameter param in query.SqlParameters)
                {
                    System.Data.Common.DbParameter dbParam = command.CreateParameter();
                    dbParam.ParameterName = param.Name;
                    dbParam.Value = param.Value;
                    command.Parameters.Add(dbParam);
                }

                return (T)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Returns a single row for the query in given <see cref="DatabaseType"/>.
        /// </summary>
        /// <typeparam name="T">DTO of the result</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">ID parameter</param>
        /// <returns>DTO of the first row, if present, otherwise null.</returns>
        public T GetRow<T>(IDictionary<DatabaseType, string> query, int idParam) where T : new() => GetRow<T>(query[DatabaseType], idParam);

        /// <summary>
        /// Returns a single row for the query with an ID parameter.
        /// </summary>
        /// <typeparam name="T">DTO of the row.</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">ID parameter</param>
        /// <returns>DTO result</returns>
        public T GetRow<T>(string query, int idParam) where T : new()
        {
            IEnumerable<T> s = GetResult<T>(query, new { id = idParam });
            return s.FirstOrDefault();
        }

        /// <summary>
        /// Returns a single row for a parameterized query.
        /// </summary>
        /// <typeparam name="T">DTO of the row</typeparam>
        /// <param name="parameterizedQuery">Parameterized query</param>
        /// <returns>DTO result</returns>
        public T GetRow<T>(IParameterizedQuery parameterizedQuery) where T : new() => GetResult<T>(parameterizedQuery).FirstOrDefault();

        /// <summary>
        /// Returns number of records for given table identifier. May be RDBMS-specific.
        /// </summary>
        /// <param name="sqlQuery">The SQL QUERY model</param>
        /// <returns>Number of records</returns>
        public virtual int GetTotalRecords(ISqlSelectQuery sqlQuery)
        {
            IParameterizedQuery countQuery = BuildCountQuery(sqlQuery);
            return GetScalar<int>(countQuery);
        }

        /// <summary>
        /// Opens a given <see cref="DbConnection"/>. This can be overridden in a specific driver.
        /// </summary>
        /// <param name="connection">Database connection</param>
        protected virtual void Initialize(DbConnection connection) => connection.Open();

        /// <summary>
        /// Returns a RDBMS specific <see cref="DataAdapter"/> for given command.
        /// </summary>
        /// <param name="command">Command on which the data adapter is created.</param>
        /// <returns>RDBMS specific data adapter</returns>
        protected abstract DataAdapter GetDataAdapterForCommand(DbCommand command);

        /// <summary>
        /// Returns a RDBMS-specific implementation of the <see cref="DbConnection"/>.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>RDBMS-specific <see cref="DbConnection"/></returns>
        protected abstract DbConnection GetConnectionProtected(string connectionString);

        /// <summary>
        /// Returns a RDMBS-specific string implementation of SQL parameter. 
        /// Use the string literal "$1" as the name of the parameter.
        /// </summary>
        /// <returns>Parameter token</returns>
        protected abstract string GetParameterToken();

        /// <summary>
        /// Returns an opened connection which is considered prepared and ready for use for a given RDBMS.
        /// </summary>
        /// <returns>RDBMS-ready opened connection</returns>
        private DbConnection GetConnection()
        {
            DbConnection connection = GetConnectionProtected(ConnectionString);
            Initialize(connection);
            return connection;
        }

        /// <summary>
        /// Applies the object parameters <paramref name="queryParams"/> into parameters for command in <paramref name="command"/>.
        /// </summary>
        /// <param name="queryParams">Query parameters object</param>
        /// <param name="command">The command<see cref="DbCommand"/></param>
        private void ProcessParams(object queryParams, DbCommand command)
        {
            if (queryParams is IEnumerable paramCollection)
            {
                foreach (object param in paramCollection)
                    ProcessParam(param, command);
            }
            else
                ProcessParam(queryParams, command);
        }

        /// <summary>
        /// Converts a anonymous object parameter with Name and Value
        /// into a SQL command parameter.
        /// </summary>
        /// <param name="param">Anonymous object with Name and Value parameters</param>
        /// <param name="command">Command from which to create the parameter and to which to append it</param>
        private void ProcessParam(object param, DbCommand command)
        {
            PropertyInfo[] propInfos = param.GetType().GetProperties();

            foreach (PropertyInfo propInfo in propInfos)
            {
                System.Data.Common.DbParameter dbParam = command.CreateParameter();
                dbParam.ParameterName = propInfo.Name;
                dbParam.Value = propInfo.GetValue(param);
                command.Parameters.Add(dbParam);

                Log.LogTrace($"{dbParam.ParameterName} = {dbParam.Value.ToString()}");
            }
        }

        /// <summary>
        /// Replaces RDMBS independent parameters {parameter} into RDBMS-specific parameters (e.g. @parameter in SQL Server).
        /// </summary>
        /// <param name="query">SQL query string with RDBMS independent parameters</param>
        /// <returns>SQL query string with RDBMS specific parameters</returns>
        private string TranslateQueryParameters(string query) => QueryParametersRegex.Replace(query, GetParameterToken());

        /// <summary>
        /// Logs a query string
        /// </summary>
        /// <param name="query">Query string</param>
        private void LogQuery(string query) => Log.LogTrace($"SQL QUERY: {query}");

        /// <summary>
        /// Logs parameterized query
        /// </summary>
        /// <param name="query">Parameterized query model</param>
        private void LogQuery(IParameterizedQuery query)
        {
            LogQuery(query.QueryText);
            Log.LogTrace($"Parameters:");

            foreach (ISqlParameter p in query.SqlParameters)
                Log.LogTrace($"{p.Name} = {p.Value}");
        }
    }
}
