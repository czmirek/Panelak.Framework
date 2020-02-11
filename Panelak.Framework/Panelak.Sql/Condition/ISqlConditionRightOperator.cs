namespace Panelak.Sql
{
    /// <summary>
    /// Logical operator expression with only right side expression (e.g. NOT)
    /// </summary>
    public interface ISqlConditionRightOperator : ISqlConditionLogicExpression
    {
        /// <summary>
        /// Gets an expression on the right side of the logical operator
        /// </summary>
        ISqlConditionExpression RightExpression { get; }
    }
}