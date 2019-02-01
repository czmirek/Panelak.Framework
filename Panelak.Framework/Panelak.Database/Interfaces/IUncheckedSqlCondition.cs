namespace Panelak.Database
{
    using Panelak.Sql;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an SQL condition expression model with identified columns and parameters 
    /// which has not yet been checked against the database
    /// </summary>
    public interface IUncheckedSqlCondition
    {
        /// <summary>
        /// Gets the SQL condition string
        /// </summary>
        string SqlConditionString { get; }

        /// <summary>
        /// Gets the unchecked columns identifiers
        /// </summary>
        IEnumerable<string> UncheckedIdentifiers { get; }

        /// <summary>
        /// Gets the query parameters
        /// </summary>
        IEnumerable<ISqlParameter> Parameters { get; }
    }
}
