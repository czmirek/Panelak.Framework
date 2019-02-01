namespace Panelak.Database.SqlServer
{
    using Panelak.Sql;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Local implementation of SQL query with parameters
    /// </summary>
    internal class MSSQLDbQuery : IParameterizedQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQLDbQuery"/> class.
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="enumerable">SQL parameters</param>
        public MSSQLDbQuery(string query, IEnumerable<ISqlParameter> enumerable)
        {
            QueryText = String.IsNullOrEmpty(query) ? throw new ArgumentNullException(nameof(query)) : query;
            SqlParameters = enumerable ?? throw new ArgumentNullException(nameof(enumerable));
        }

        /// <summary>
        /// Gets the SQL query string
        /// </summary>
        public string QueryText { get; }

        /// <summary>
        /// Gets the SQL query parameters
        /// </summary>
        public IEnumerable<ISqlParameter> SqlParameters { get; }
    }
}
