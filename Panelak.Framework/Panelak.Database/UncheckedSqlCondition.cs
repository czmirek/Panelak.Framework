namespace Panelak.Database
{
    using Panelak.Sql;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a SQL condition model with identifiers and parameters but not yet
    /// checked against a given SQL connection.
    /// </summary>
    internal class UncheckedSqlCondition : IUncheckedSqlCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UncheckedSqlCondition"/> class.
        /// </summary>
        /// <param name="sqlConditionString">SQL condition string</param>
        /// <param name="uncheckedIdentifiers">Unchecked column identifiers</param>
        /// <param name="parameters">Parameters of the SQL condition string</param>
        public UncheckedSqlCondition(string sqlConditionString,
            IEnumerable<string> uncheckedIdentifiers,
            IEnumerable<ISqlParameter> parameters)
        {
            SqlConditionString = sqlConditionString ?? throw new ArgumentNullException(nameof(sqlConditionString));
            UncheckedIdentifiers = uncheckedIdentifiers ?? throw new ArgumentNullException(nameof(uncheckedIdentifiers));
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        /// <summary>
        /// Gets the SQL condition string
        /// </summary>
        public string SqlConditionString { get; }

        /// <summary>
        /// Gets the enumeration of unchecked column identifiers
        /// </summary>
        public IEnumerable<string> UncheckedIdentifiers { get; }

        /// <summary>
        /// Gets the enumeration of SQL query parameters
        /// </summary>
        public IEnumerable<ISqlParameter> Parameters { get; }
    }
}
