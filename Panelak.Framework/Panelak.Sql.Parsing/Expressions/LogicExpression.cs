namespace Panelak.Sql.Parsing
{
    using System.Diagnostics;

    /// <summary>
    /// Abstraction for the SQL logic operator expression
    /// </summary>
    [DebuggerDisplay("{Operator}")]
    public abstract class LogicExpression : ISqlConditionLogicExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogicExpression"/> class.
        /// </summary>
        /// <param name="operator">SQL logical operator</param>
        public LogicExpression(string @operator)
        {
            this.Operator = @operator;
        }

        /// <summary>
        /// Gets the operator literal
        /// </summary>
        public string Operator { get; }
    }
}