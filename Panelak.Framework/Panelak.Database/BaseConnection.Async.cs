namespace Panelak.Database
{
    using Panelak.Sql;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Connection driver for basic database task.
    /// </summary>
    public partial class BaseConnection
    {
        /// <summary>
        /// Opens a given <see cref="DbConnection"/>. This can be overridden in a specific driver.
        /// </summary>
        /// <param name="connection">Database connection</param>
        protected virtual async Task InitializeAsync(DbConnection connection) => await connection.OpenAsync();

        /// <summary>
        /// Returns an opened connection which is considered prepared and ready for use for a given RDBMS.
        /// </summary>
        /// <returns>RDBMS-ready opened connection</returns>
        protected async Task<DbConnection> GetConnectionAsync()
        {
            DbConnection connection = GetConnectionProtected(ConnectionString);
            await InitializeAsync(connection);
            return connection;
        }

        /// <summary>
        /// Returns a result enumeration from given <paramref name="query"/> into a list of a given DTO (<typeparamref name="T"/>).
        /// </summary>
        /// <typeparam name="T">Type of the DTO</typeparam>
        /// <param name="query">Query returning a result</param>
        /// <returns>DTO enumeration</returns>
        public async Task<IEnumerable<T>> GetResultAsync<T>(string query) where T : new()
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);
            using (DbConnection db = await GetConnectionAsync())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = query;
                IDataReader dataReader = await command.ExecuteReaderAsync();
                return mapper.MapToDto<T>(dataReader, DatabaseType);
            }
        }
        

        /// <summary>
        /// Returns a result enumeration from given parameterized query into a list of a given DTO.
        /// </summary>
        /// <typeparam name="T">Type of the DTO</typeparam>
        /// <param name="query">Parameterized query</param>
        /// <returns>DTO enumeration</returns>
        public async Task<IEnumerable<T>> GetResultAsync<T>(IParameterizedQuery query) where T : new()
        {
            LogQuery(query);

            string queryText = TranslateQueryParameters(query.QueryText);

            using (DbConnection db = await GetConnectionAsync())
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

                IDataReader dataReader = await command.ExecuteReaderAsync();
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
        public async Task<IEnumerable<T>> GetResultAsync<T>(string query, object queryParams) where T : new()
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);

            using (DbConnection db = await GetConnectionAsync())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = query;

                ProcessParams(queryParams, command);

                IDataReader dataReader = await command.ExecuteReaderAsync();

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
        public async Task<IEnumerable<T>> GetResultAsync<T>(string query, int idParam) where T : new() => await GetResultAsync <T>(query, new { id = idParam });
        
        
        /// <summary>
        /// Returns a value from the first row of the first column in the query result.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query</param>
        /// <returns>Scalar value</returns>
        public async Task<T> GetScalarAsync<T>(string query)
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);

            using (DbConnection db = await GetConnectionAsync())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = query;

                return (T)await command.ExecuteScalarAsync();
            }
        }

        /// <summary>
        /// Returns a value from the first row of the first column in the query result using parameters.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query with RDBMS-independent parameters.</param>
        /// <param name="queryParams">Query parameters</param>
        /// <returns>Scalar value</returns>
        public async Task<T> GetScalarAsync<T>(string query, object queryParams)
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);

            using (DbConnection db = await GetConnectionAsync())
            {
                DbCommand command = db.CreateCommand();
                command.CommandText = query;
                ProcessParams(queryParams, command);

                return (T)await command.ExecuteScalarAsync();
            }
        }

        /// <summary>
        /// Returns a value from the first row of the first column in the query result.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">ID parameter included in the query</param>
        /// <returns>Scalar value</returns>
        public async Task<T> GetScalarAsync<T>(string query, int idParam) => await GetScalarAsync<T>(query, new { id = idParam });

        /// <summary>
        /// Returns a value from the first row of the first column in the query result.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query with parameters</param>
        /// <returns>Scalar value</returns>
        public async Task<T> GetScalarAsync<T>(IParameterizedQuery query)
        {
            LogQuery(query);

            string queryText = TranslateQueryParameters(query.QueryText);

            using (DbConnection db = await GetConnectionAsync())
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

                return (T)await command.ExecuteScalarAsync();
            }
        }

        /// <summary>
        /// Returns a single row for the query in given <see cref="DatabaseType"/>.
        /// </summary>
        /// <typeparam name="T">DTO of the result</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">ID parameter</param>
        /// <returns>DTO of the first row, if present, otherwise null.</returns>
        public async Task<T> GetRowAsync<T>(IDictionary<DatabaseType, string> query, int idParam) where T : new() => await GetRowAsync<T>(query[DatabaseType], idParam);


        /// <summary>
        /// Returns a single row for the query with an ID parameter.
        /// </summary>
        /// <typeparam name="T">DTO of the row.</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">ID parameter</param>
        /// <returns>DTO result</returns>
        public async Task<T> GetRowAsync<T>(string query, int idParam) where T : new()
        {
            IEnumerable<T> s = await GetResultAsync<T>(query, new { id = idParam });
            return s.FirstOrDefault();
        }
        
        /// <summary>
        /// Returns a single row for a parameterized query.
        /// </summary>
        /// <typeparam name="T">DTO of the row</typeparam>
        /// <param name="parameterizedQuery">Parameterized query</param>
        /// <returns>DTO result</returns>
        public async Task<T> GetRowAsync<T>(IParameterizedQuery parameterizedQuery) where T : new() => (await GetResultAsync<T>(parameterizedQuery)).FirstOrDefault();

        /// <summary>
        /// Returns number of records for given table identifier. May be RDBMS-specific.
        /// </summary>
        /// <param name="sqlQuery">The SQL QUERY model</param>
        /// <returns>Number of records</returns>
        public virtual async Task<int> GetTotalRecordsAsync(ISqlSelectQuery sqlQuery)
        {
            IParameterizedQuery countQuery = BuildCountQuery(sqlQuery);
            return await GetScalarAsync<int>(countQuery);
        }
    }
}
