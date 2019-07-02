namespace Panelak.Database.Oracle
{
    using System.Linq;
    using Panelak.Sql;
    using Microsoft.Extensions.Logging;
    using global::Oracle.ManagedDataAccess.Client;
    using System.Data.Common;
    using OracleSourceConnection = global::Oracle.ManagedDataAccess.Client.OracleConnection;
    using System.Collections;
    using System.Data;
    using System.Reflection;
    using System;
    using global::Oracle.ManagedDataAccess.Types;

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
        /// Runs a procedure with given name, input and output params
        /// </summary>
        /// <param name="procedure">Name of procedure</param>
        /// <param name="inputParams">Input parameters</param>
        /// <param name="outputParams">Output parameters</param>
        public void ExecuteProcedure(string procedure, object inputParams, object outputParams)
        {
            Log.LogTrace($"Executing procedure {procedure}");

            using (var db = GetConnection() as OracleSourceConnection)
            {
                OracleCommand command = db.CreateCommand();
                command.CommandText = procedure;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                
                ProcessParams(inputParams, command, ParameterDirection.Input);
                ProcessParams(outputParams, command, ParameterDirection.Output);

                command.ExecuteNonQuery();

                ProcessOutputParams(outputParams, command);
            }
        }

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


        /// <summary>
        /// Applies the object parameters <paramref name="queryParams"/> into parameters for command in <paramref name="command"/>.
        /// </summary>
        /// <param name="queryParams">Query parameters object</param>
        /// <param name="command">The command <see cref="OracleCommand"/></param>
        private void ProcessParams(object queryParams, OracleCommand command, ParameterDirection direction = ParameterDirection.Input)
        {
            if (queryParams == null)
                return;

            if (queryParams is IEnumerable paramCollection)
            {
                foreach (object param in paramCollection)
                    ProcessParam(param, command, direction);
            }
            else
                ProcessParam(queryParams, command, direction);
        }

        /// <summary>
        /// Converts a anonymous object parameter with Name and Value
        /// into a SQL command parameter.
        /// </summary>
        /// <param name="param">Anonymous object with Name and Value parameters</param>
        /// <param name="command">Command from which to create the parameter and to which to append it</param>
        private void ProcessParam(object param, OracleCommand command, ParameterDirection direction = ParameterDirection.Input)
        {
            PropertyInfo[] propInfos = param.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (PropertyInfo propInfo in propInfos)
            {
                Log.LogTrace($"OracleConnection: Processing parameter {propInfo.Name}");

                OracleParameter dbParam = command.CreateParameter();
                dbParam.Direction = direction;
                dbParam.ParameterName = propInfo.Name;

                if (direction == ParameterDirection.Input)
                {
                    dbParam.Value = propInfo.GetValue(param);
                    Log.LogTrace($"OracleConnection: Input parameter: {propInfo.Name} = {dbParam.Value}");
                }
                
                OracleDbTypeAttribute dbTypeAttr = propInfo.GetCustomAttribute<OracleDbTypeAttribute>();
                if (dbTypeAttr != null)
                {
                    dbParam.OracleDbType = dbTypeAttr.OracleDbType;
                    Log.LogTrace($"OracleConnection: DbType: {propInfo.Name} = {dbParam.OracleDbType}");
                    if (dbTypeAttr.Size != null)
                    {
                        dbParam.Size = dbTypeAttr.Size.Value;
                        Log.LogTrace($"OracleConnection: DbType Size: {propInfo.Name} = {dbParam.Size}");
                    }
                }
                
                command.Parameters.Add(dbParam);
            }
        }

        /// <summary>
        /// Performs assigning of output parameters to the user defined object by reflection
        /// </summary>
        /// <param name="outputParams">Output parameters object</param>
        /// <param name="command">Executed command</param>
        private void ProcessOutputParams(object outputParams, OracleCommand command)
        {
            PropertyInfo[] propInfos = outputParams.GetType().GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (OracleParameter parameter in command.Parameters)
            {
                if (parameter.Direction != ParameterDirection.Output)
                    continue;

                PropertyInfo prop = propInfos.FirstOrDefault(p => p.Name == parameter.ParameterName);

                Log.LogTrace($"OracleConnection: Processing output param: {parameter.ParameterName}");

                if (prop != null)
                {
                    if (parameter.Value is OracleDecimal oracleDecimal)
                    {
                        if (oracleDecimal.IsNull)
                            continue;

                        if (prop.PropertyType.Equals(typeof(byte)))
                            prop.SetValue(outputParams, oracleDecimal.ToInt16());
                        else if (prop.PropertyType.Equals(typeof(int)))
                            prop.SetValue(outputParams, oracleDecimal.ToInt32());
                        else if (prop.PropertyType.Equals(typeof(long)))
                            prop.SetValue(outputParams, oracleDecimal.ToInt64());
                        else
                            throw new InvalidOperationException($"Cannot assign OracleDecimal to property with type {prop.PropertyType.Name}");
                    }
                    else if (parameter.Value is OracleString oracleString)
                    {
                        if (oracleString.IsNull)
                            continue;

                        prop.SetValue(outputParams, oracleString.Value);
                    }
                    else
                        throw new NotImplementedException($"Parameter type {parameter.Value.GetType().Name} is not yet supported");
                }
                else
                    Log.LogTrace($"OracleConnection: No property found for output param {parameter.ParameterName}");
            }
        }
    }
}
