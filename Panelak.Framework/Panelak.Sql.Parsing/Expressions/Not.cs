namespace Panelak.Sql.Parsing
{
    using ISqlExpr = ISqlConditionExpression;

    /// <summary>
    /// SQL NOT logical operator
    /// </summary>
    public class Not : RightSideExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Not"/> class.
        /// </summary>
        /// <param name="right">Expression on the right side of the NOT operator</param>
        public Not(ISqlExpr right) : base("NOT", right)
        {
        }
    }
}