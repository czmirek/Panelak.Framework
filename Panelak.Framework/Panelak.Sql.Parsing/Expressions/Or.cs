namespace Panelak.Sql.Parsing
{
    using ISqlExpr = ISqlConditionExpression;

    /// <summary>
    /// SQL operator OR
    /// </summary>
    public class Or : LeftRightSideExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Or"/> class.
        /// </summary>
        /// <param name="left">Expression on the left side of the OR operator</param>
        /// <param name="right">Expression on the right side of the OR operator</param>
        public Or(ISqlExpr left, ISqlExpr right) : base("OR", left, right)
        {
        }
    }
}
