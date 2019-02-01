namespace Panelak.Database.OracleConsole
{
    using Panelak.Database.Oracle;
    using Panelak.Sql;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Oracle Connection for the Oracle ODAC console application.
    /// </summary>
    public class OracleConsoleConnection : OracleConnection
    {
        /// <summary>
        /// Regex used for replacement of RDBMS-independent parameters used in SQL queries in the format {parameter}.
        /// </summary>
        private static readonly Regex QueryParametersRegex = new Regex("\\{(\\w+)\\}", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        /// <summary>
        /// JSON settings for converting the result to DTO
        /// </summary>
        private readonly JsonSerializerSettings jsonSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleConsoleConnection"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="logger">Logger instance</param>
        public OracleConsoleConnection(string connectionString, ILogger logger) 
            : base(connectionString, logger) 
            => jsonSettings = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter>() { new SdoGeometryJsonConverter() }
        };

        /// <summary>
        /// Returns a result as a RDBMS-specific <see cref="DataSet"/> for a parameterized query.
        /// Individual parameters, in present, are converted into the relevant RDMBS-specific <see cref="DbParameter"/>.
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <returns>RDBMS-specific <see cref="DataSet"/>.</returns>
        public new DataSet GetDataTable(IParameterizedQuery query)
        {
            LogQuery(query);

            string queryText = TranslateQueryParameters(query.QueryText);
            string parameters = ProcessParams(query.SqlParameters);

            string result = ReadOdac(queryText, parameters);
            OdacResult data = JsonConvert.DeserializeObject<OdacResult>(result, jsonSettings);

            var dataSet = new DataSet();
            dataSet.Tables.Add("result");
            foreach (string column in data.Columns)
                dataSet.Tables[0].Columns.Add(column);

            foreach (Dictionary<string, object> row in data.Data)
            {
                DataRow dataSetRow = dataSet.Tables[0].NewRow();
                foreach (KeyValuePair<string, object> item in row)
                    dataSetRow[item.Key] = item.Value;

                dataSet.Tables[0].Rows.Add(dataSetRow);
            }

            return dataSet;
        }

        /// <summary>
        /// Returns a result enumeration from given <paramref name="query"/> into a list of a given DTO (<typeparamref name="T"/>).
        /// </summary>
        /// <typeparam name="T">Type of the DTO</typeparam>
        /// <param name="query">Query returning a result</param>
        /// <returns>DTO enumeration</returns>
        public new IEnumerable<T> GetResult<T>(string query) where T : new()
        {
            LogQuery(query);
            return ReadOdacResult<T>(query, null);
        }

        /// <summary>
        /// Returns a result enumeration from given parameterized query into a list of a given DTO.
        /// </summary>
        /// <typeparam name="T">Type of the DTO</typeparam>
        /// <param name="query">Parameterized query</param>
        /// <returns>DTO enumeration</returns>
        public new IEnumerable<T> GetResult<T>(IParameterizedQuery query) where T : new()
        {
            LogQuery(query);

            string queryText = TranslateQueryParameters(query.QueryText);
            string parameters = ProcessParams(query.SqlParameters);
            return ReadOdacResult<T>(queryText, parameters);
        }

        /// <summary>
        /// Returns a result enumeration from given <paramref name="query"/> into a list of a given DTO (<typeparamref name="T"/>)
        /// and mapping parameters into the query from <paramref name="queryParams"/>.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="queryParams">Query parameters object</param>
        /// <returns>Enumeration of DTOs from the result.</returns>
        public new IEnumerable<T> GetResult<T>(string query, object queryParams) where T : new()
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);
            string parameters = ProcessParams(queryParams);
            return ReadOdacResult<T>(query, parameters);
        }

        /// <summary>
        /// Returns a result enumeration for a given query with a single ID parameter.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">Value of the ID parameter</param>
        /// <returns>Enumeration of DTOs from the result.</returns>
        public new IEnumerable<T> GetResult<T>(string query, int idParam) where T : new() => GetResult<T>(query, new { id = idParam });

        /// <summary>
        /// Returns a value from the first row of the first column in the query result.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query</param>
        /// <returns>Scalar value</returns>
        public new T GetScalar<T>(string query)
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);
            return ReadOdacScalar<T>(query, null);
        }

        /// <summary>
        /// Returns a value from the first row of the first column in the query result using parameters.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query with RDBMS-independent parameters.</param>
        /// <param name="queryParams">Query parameters</param>
        /// <returns>Scalar value</returns>
        public new T GetScalar<T>(string query, object queryParams)
        {
            LogQuery(query);

            query = TranslateQueryParameters(query);
            string parameters = ProcessParams(queryParams);
            return ReadOdacScalar<T>(query, parameters);
        }

        /// <summary>
        /// Returns a value from the first row of the first column in the query result.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">ID parameter included in the query</param>
        /// <returns>Scalar value</returns>
        public new T GetScalar<T>(string query, int idParam) => GetScalar<T>(query, new { id = idParam });

        /// <summary>
        /// Returns a value from the first row of the first column in the query result.
        /// </summary>
        /// <typeparam name="T">Type of the scalar</typeparam>
        /// <param name="query">SQL query with parameters</param>
        /// <returns>Scalar value</returns>
        public new T GetScalar<T>(IParameterizedQuery query)
        {
            LogQuery(query);

            string queryText = TranslateQueryParameters(query.QueryText);
            string parameters = ProcessParams(query.SqlParameters);
            return ReadOdacScalar<T>(queryText, parameters);
        }

        /// <summary>
        /// Returns a single row for the query in given <see cref="DatabaseType"/>.
        /// </summary>
        /// <typeparam name="T">DTO of the result</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">ID parameter</param>
        /// <returns>DTO of the first row, if present, otherwise null.</returns>
        public new T GetRow<T>(IDictionary<DatabaseType, string> query, int idParam) where T : new() => GetRow<T>(query[DatabaseType], idParam);

        /// <summary>
        /// Returns a single row for the query with an ID parameter.
        /// </summary>
        /// <typeparam name="T">DTO of the row.</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="idParam">ID parameter</param>
        /// <returns>DTO result</returns>
        public new T GetRow<T>(string query, int idParam) where T : new()
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
        public new T GetRow<T>(IParameterizedQuery parameterizedQuery) where T : new() => GetResult<T>(parameterizedQuery).FirstOrDefault();

        /// <summary>
        /// Returns the Oracle managed connection for given connection string
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns>Oracle managed connection</returns>
        protected override DbConnection GetConnectionProtected(string connectionString)
            => throw new InvalidOperationException("No connection in OracleConsole provider.");

        /// <summary>
        /// Returns the Oracle managed data adapter for given command
        /// </summary>
        /// <param name="command">Oracle command to enter the data adapter</param>
        /// <returns>The oracle data adapter</returns>
        protected override DataAdapter GetDataAdapterForCommand(DbCommand command)
            => throw new InvalidOperationException("No data adapter in OracleConsole provider.");

        /// <summary>
        /// Reads the result from ODAC console and maps the JSON to DTO in <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">SQL query</param>
        /// <param name="parameters">SQL query parameters</param>
        /// <returns>Result enumeration</returns>
        private IEnumerable<T> ReadOdacResult<T>(string query, string parameters) where T : new()
        {
            string result = ReadOdac(query, parameters);
            OdacResult<T> odacResult = JsonConvert.DeserializeObject<OdacResult<T>>(result, jsonSettings);
            return odacResult.Data.AsReadOnly();
        }

        /// <summary>
        /// Reads the result from ODAC console and returns the value of the first column in the first row
        /// </summary>
        /// <typeparam name="T">Scalar type</typeparam>
        /// <param name="query">SQL query text</param>
        /// <param name="parameters">SQL query parameters</param>
        /// <returns>Scalar value</returns>
        private T ReadOdacScalar<T>(string query, string parameters)
        {
            string result = ReadOdac(query, parameters);
            OdacResult odacResult = JsonConvert.DeserializeObject<OdacResult>(result, jsonSettings);
            var dtoList = new List<T>();
            return (T)odacResult.Data.FirstOrDefault()?.FirstOrDefault().Value;
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

        /// <summary>
        /// Converts SQL parameters into a JSON string representation
        /// </summary>
        /// <param name="sqlParameters">SQL parameters</param>
        /// <returns>JSON serialized parameters</returns>
        private string ProcessParams(IEnumerable<ISqlParameter> sqlParameters)
        {
            var paramsDict = sqlParameters.ToDictionary(k => k.Name, v => v.Value);
            return JsonConvert.SerializeObject(paramsDict);
        }

        /// <summary>
        /// Converts anonymous object parameters into a serialized JSON string
        /// </summary>
        /// <param name="queryParams">Anonymous object parameters</param>
        /// <returns>JSON serialized parameters</returns>
        private string ProcessParams(object queryParams)
        {
            var paramsDict = new Dictionary<string, object>();

            if (queryParams is IEnumerable paramCollection)
            {
                foreach (object param in paramCollection)
                {
                    PropertyInfo[] propInfos = param.GetType().GetProperties();
                    foreach (PropertyInfo propInfo in propInfos)
                    {
                        string name = propInfo.Name;
                        object value = propInfo.GetValue(param);
                        paramsDict.Add(name, value);

                        Log.LogTrace($"{name} = {value}");
                    }
                }
            }
            else
            {
                PropertyInfo[] propInfos = queryParams.GetType().GetProperties();
                foreach (PropertyInfo propInfo in propInfos)
                {
                    string name = propInfo.Name;
                    object value = propInfo.GetValue(queryParams);
                    paramsDict.Add(name, value);

                    Log.LogTrace($"{name} = {value}");
                }
            }

            return JsonConvert.SerializeObject(paramsDict);
        }

        /// <summary>
        /// Sends the query to the named pipe and returns the string result in JSON.
        /// </summary>
        /// <param name="queryText">SQL query text</param>
        /// <param name="parameters">Serialized parameters</param>
        /// <returns>The string result</returns>
        private string ReadOdac(string queryText, string parameters)
        {
            using (var odac = new OdacPipeQueryMessage(ConnectionString, queryText, parameters))
                return odac.GetResult();
        }
    }
}
