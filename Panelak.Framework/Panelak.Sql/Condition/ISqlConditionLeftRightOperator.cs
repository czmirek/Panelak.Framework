namespace Panelak.Sql
{
    /// <summary>
    /// Logical operator expression with left and right side expression (e.g. AND, OR)
    /// </summary>
    public interface ISqlConditionLeftRightOperator : ISqlConditionLogicExpression
    {
        /// <summary>
        /// Gets an expression on the left side of the logical operator
        /// </summary>
        ISqlConditionExpression LeftExpression { get; }

        /// <summary>
        /// Gets an expression on the right side of the logical operator
        /// </summary>
        ISqlConditionExpression RightExpression { get; }
    }
}