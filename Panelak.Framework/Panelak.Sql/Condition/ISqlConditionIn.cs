namespace Panelak.Sql
{
    using System.Collections.Generic;

    /// <summary>
    /// IN operator expression in the form [Column] IN ([value], [value], ...)
    /// </summary>
    public interface ISqlConditionIn : ISqlConditionColumnComparisonExpression
    {
        /// <summary>
        /// Gets a list of values contained on the right side of the IN operator
        /// </summary>
        IEnumerable<string> Values { get; }
    }
}