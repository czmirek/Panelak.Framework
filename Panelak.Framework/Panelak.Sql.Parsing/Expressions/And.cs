namespace Panelak.Sql.Parsing
{
    using ISqlExpr = ISqlConditionExpression;

    /// <summary>
    /// SQL operator AND
    /// </summary>
    public class And : LeftRightSideExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="And"/> class.
        /// </summary>
        /// <param name="left">Expression on the left side of the AND operator</param>
        /// <param name="right">Expression on the right side of the AND operator</param>
        public And(ISqlExpr left, ISqlExpr right) : base("AND", left, right)
        {
        }
    }
}