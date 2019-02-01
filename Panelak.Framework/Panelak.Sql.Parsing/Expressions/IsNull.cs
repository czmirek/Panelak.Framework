namespace Panelak.Sql.Parsing
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// SQL IS NULL expression in the format [Column] IS NULL
    /// </summary>
    [DebuggerDisplay("{Column} IS NULL")]
    public class IsNull : ISqlConditionIsNull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsNull"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        public IsNull(string column)
        {
            this.Column = column ?? throw new ArgumentNullException(nameof(column));
        }

        /// <summary>
        /// Gets the column identifier in the expression
        /// </summary>
        public string Column { get; }
    }
}
