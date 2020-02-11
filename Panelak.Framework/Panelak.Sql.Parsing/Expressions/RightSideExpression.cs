namespace Panelak.Sql.Parsing
{
    using System;
    using ISqlExpr = ISqlConditionExpression;

    /// <summary>
    /// Abstraction for the SQL logic operator with only right side next to the operator (such as NOT)
    /// </summary>
    public abstract class RightSideExpression : LogicExpression, ISqlConditionRightOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RightSideExpression"/> class.
        /// </summary>
        /// <param name="operator">The operator literal</param>
        /// <param name="rightSideExpression">Expression on the right side of the logical operator</param>
        public RightSideExpression(string @operator, ISqlExpr rightSideExpression) : base(@operator)
        {
            this.RightExpression = rightSideExpression ?? throw new ArgumentNullException(nameof(rightSideExpression));
        }

        /// <summary>
        /// Gets the expression on the right side of the logical operator
        /// </summary>
        public ISqlExpr RightExpression { get; }
    }
}
