namespace Panelak.Database
{
    using Panelak.Sql;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Instance of a connection to a relational database source.
    /// </summary>
    public interface IConnectionLink
    {
        /// <summary>
        /// Checks whether the table exists in the database
        /// </summary>
        /// <param name="tableIdentifier">Table identifier</param>
        /// <returns>True if table is found</returns>
        bool CheckIfTableExists(ISqlTableIdentifier tableIdentifier);

        /// <summary>
        /// Gets a name of the first column of given table
        /// </summary>
        /// <param name="table">Table identifier</param>
        /// <returns>Name of the first column</returns>
        string GetFirstColumnName(ISqlTableIdentifier table);

        /// <summary>
        /// Gets a number of rows for given SQL query
        /// </summary>
        /// <param name="sqlQuery">SQL query</param>
        /// <returns>Number of rows</returns>
        int GetTotalRecords(ISqlSelectQuery sqlQuery);

        /// <summary>
        /// Attempts to find a primary key for a given table.
        /// </summary>
        /// <param name="table">Table identifier</param>
        /// <param name="primaryKey">Name of the primary key</param>
        /// <returns>True if primary key was identified</returns>
        bool TryGetPrimaryKeyForTable(ISqlTableIdentifier table, out string primaryKey);

        /// <summary>
        /// Builds a driver-specific SQL query for determining the number of rows for given driver-independent SQL query
        /// </summary>
        /// <param name="sqlQuery">Driver-independent SQL query</param>
        /// <returns>Driver-specific SQL query</returns>
        IParameterizedQuery BuildCountQuery(ISqlSelectQuery sqlQuery);

        /// <summary>
        /// Builds a driver-specific SQL query from driver-independent SQL query
        /// </summary>
        /// <param name="sqlQuery">Driver-independent SQL query</param>
        /// <returns>Driver-specific SQL query</returns>
        IParameterizedQuery BuildQuery(ISqlSelectQuery sqlQuery);
        
        /// <summary>
        /// Returns a <see cref="DataSet"/> from given query
        /// </summary>
        /// <param name="query">Driver-independent query with parameters</param>
        /// <returns>DataSet of the query</returns>
        DataSet GetDataTable(IParameterizedQuery query);

        /// <summary>
        /// Returns a <see cref="DataSet"/> from given query
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <returns>DataSet of the query</returns>
        DataSet GetDataTable(string query);

        /// <summary>
        /// Returns the enumeration of rows as DTO models. Assumes names of parameters in the string in curly brackets e.g. {parameter}.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">Parameterized query model</param>
        /// <returns>Rows as IEnumerable of DTO</returns>
        IEnumerable<T> GetResult<T>(IParameterizedQuery query) where T : new();

        /// <summary>
        /// Returns the enumeration of rows as DTO models.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">SQL query string</param>
        /// <returns>Rows as IEnumerable of DTO</returns>
        IEnumerable<T> GetResult<T>(string query) where T : new();

        /// <summary>
        /// Returns the enumeration of rows as DTO models for a query with an ID parameter. Assumes names of parameters in the string in curly brackets e.g. {parameter}.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">SQL query string</param>
        /// <param name="idParam">ID parameter of the query {id}</param>
        /// <returns>Rows as IEnumerable of DTO</returns>
        IEnumerable<T> GetResult<T>(string query, int idParam) where T : new();

        /// <summary>
        /// Returns the enumeration of rows as DTO models for a query with an ID parameter. Assumes names of parameters in the string in curly brackets e.g. {parameter}.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">SQL query string</param>
        /// <param name="queryParams">Array of anonymous objects with Name and Value properties</param>
        /// <returns>Rows as IEnumerable of DTO</returns>
        IEnumerable<T> GetResult<T>(string query, object queryParams) where T : new();

        /// <summary>
        /// Returns the first row of the result cast to a DTO for a query that has different SQL representation in various RDBMS.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">Dictionary of SQL queries for various RDBMS.</param>
        /// <param name="idParam">ID parameter of the query {id}</param>
        /// <returns>First row as DTO</returns>
        T GetRow<T>(IDictionary<DatabaseType, string> query, int idParam) where T : new();

        /// <summary>
        /// Returns the first row of the result cast to a DTO.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="parametrizedQuery">SQL query with parameters model</param>
        /// <returns>First row as DTO</returns>
        T GetRow<T>(IParameterizedQuery parametrizedQuery) where T : new();

        /// <summary>
        /// Returns the first row of the result cast to a DTO.
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="query">SQL query string</param>
        /// <param name="idParam">ID parameter of the query {id}</param>
        /// <returns>First row as DTO</returns>
        T GetRow<T>(string query, int idParam) where T : new();

        /// <summary>
        /// Returns the value of the first column in the first row of the result cast to a given scalar type.
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="query">SQL query with parameters model</param>
        /// <returns>Value of the first column in the first row</returns>
        T GetScalar<T>(IParameterizedQuery query);

        /// <summary>
        /// Returns the value of the first column in the first row of the result cast to a given scalar type.
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="query">SQL query string</param>
        /// <returns>Value of the first column in the first row</returns>
        T GetScalar<T>(string query);

        /// <summary>
        /// Returns the value of the first column in the first row of the result cast to a given scalar type.
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="query">SQL query string</param>
        /// <param name="queryParams">Array of anonymous objects with Name and Value properties</param>
        /// <returns>Value of the first column in the first row</returns>
        T GetScalar<T>(string query, object queryParams);

        /// <summary>
        /// Returns the value of the first column in the first row of the result cast to a given scalar type.
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="query">SQL query string</param>
        /// <param name="idParam">ID parameter of the query {id}</param>
        /// <returns>Value of the first column in the first row</returns>
        T GetScalar<T>(string query, int idParam);
    }
}
