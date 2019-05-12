namespace Panelak.Sql.Parsing
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Abstraction for SQL operators in the form [Column] [Operator] [Literal].
    /// </summary>
    [DebuggerDisplay("{Column} {Operator} {Literal}")]
    public abstract class ColumnComparisonExpression : ISqlConditionColumnComparisonExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnComparisonExpression" /> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="operator">SQL comparison operator</param>
        /// <param name="literal">Literal value</param>
        protected ColumnComparisonExpression(string column, string @operator, string literal)
        {
            Column = column ?? throw new ArgumentNullException(nameof(column));
            Operator = @operator;
            Literal = literal ?? throw new ArgumentNullException(nameof(literal));
        }

        /// <summary>
        /// Gets a column identifier on the left side of the SQL operator
        /// </summary>
        public string Column { get; }

        /// <summary>
        /// Gets an SQL comparison operator
        /// </summary>
        public string Operator { get; }

        /// <summary>
        /// Gets a literal value on the right side of the SQL operator
        /// </summary>
        public string Literal { get; }
    }
}
