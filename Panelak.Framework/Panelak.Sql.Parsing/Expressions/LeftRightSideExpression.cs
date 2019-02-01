namespace Panelak.Sql.Parsing
{
    using System;
    using ISqlExpr = ISqlConditionExpression;

    /// <summary>
    /// Abstraction for the SQL logic operator with left and right side expressions (such as AND, OR)
    /// </summary>
    public abstract class LeftRightSideExpression : LogicExpression, ISqlConditionLeftRightOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeftRightSideExpression"/> class.
        /// </summary>
        /// <param name="operator">SQL logical operator</param>
        /// <param name="leftExpression">Expression on the left side of the operator</param>
        /// <param name="rightExpression">Expression on the right side of the operator</param>
        public LeftRightSideExpression(string @operator, ISqlExpr leftExpression, ISqlExpr rightExpression)
            : base(@operator)
        {
            this.LeftExpression = leftExpression ?? throw new ArgumentNullException(nameof(leftExpression));
            this.RightExpression = rightExpression ?? throw new ArgumentNullException(nameof(rightExpression));
        }

        /// <summary>
        /// Gets an expression on the left side of the logical operator
        /// </summary>
        public ISqlExpr LeftExpression { get; }

        /// <summary>
        /// Gets an expression on the right side of the logical operator
        /// </summary>
        public ISqlExpr RightExpression { get; }
    }
}
