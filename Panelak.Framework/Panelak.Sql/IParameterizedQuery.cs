namespace Panelak.Sql
{
    using System.Collections.Generic;

    /// <summary>
    /// RDBMS independent model of SQL query with parameters
    /// </summary>
    public interface IParameterizedQuery
    {
        /// <summary>
        /// Gets an SQL of the query
        /// </summary>
        string QueryText { get; }

        /// <summary>
        /// Gets parameters of the query
        /// </summary>
        IEnumerable<ISqlParameter> SqlParameters { get; }
    }
}
