namespace odac.client.x86.Console
{
    using Newtonsoft.Json;
    using Oracle.DataAccess.Client;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// PL/SQL query
    /// </summary>
    public class OracleQuery
    {
        /// <summary>
        /// Returns a result as JSON from the Oracle database 
        /// for given <paramref name="query"/>, <paramref name="connectionString"/>
        /// and query <paramref name="parameters"/>.
        /// </summary>
        /// <param name="connectionString">Oracle database connection string</param>
        /// <param name="query">SQL query</param>
        /// <param name="parameters">Optional SQL query parameters</param>
        /// <returns></returns>
        public string GetResult(string connectionString, string query, string parameters)
        {
            var parametersDict = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(parameters))
                parametersDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(parameters);

            var result = new Result();

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();

                using (OracleCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    foreach (var item in parametersDict)
                        command.Parameters.Add(item.Key, item.Value);

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        var columnNames = new List<string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);
                            columnNames.Add(columnName);
                            result.Columns.Add(columnName);
                        }

                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                object value = reader.GetValue(i);
                                row.Add(columnNames[i], value);
                            }
                            result.Data.Add(row);
                        }
                    }
                }
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}
