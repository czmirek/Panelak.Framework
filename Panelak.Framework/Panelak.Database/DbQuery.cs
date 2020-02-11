namespace Panelak.Database
{
    using Panelak.Sql;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Local implementation of SQL query with parameters
    /// </summary>
    internal class DbQuery : IParameterizedQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbQuery"/> class.
        /// </summary>
        /// <param name="query">The query string</param>
        /// <param name="enumerable">Parameters enumeration</param>
        public DbQuery(string query, IEnumerable<ISqlParameter> enumerable)
        {
            QueryText = String.IsNullOrEmpty(query) ? throw new ArgumentNullException(nameof(query)) : query;
            SqlParameters = enumerable ?? throw new ArgumentNullException(nameof(enumerable));
        }

        /// <summary>
        /// Gets the SQL query text
        /// </summary>
        public string QueryText { get; }

        /// <summary>
        /// Gets the SQL parameters
        /// </summary>
        public IEnumerable<ISqlParameter> SqlParameters { get; }
    }
}
