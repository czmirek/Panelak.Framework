namespace Panelak.Sql.Parsing
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// SQL IS NULL expression in the format [Column] IS NOT NULL
    /// </summary>
    [DebuggerDisplay("{Column} IS NOT NULL")]
    public class IsNotNull : ISqlConditionIsNotNull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsNotNull"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        public IsNotNull(string column)
        {
            this.Column = column ?? throw new ArgumentNullException(nameof(column));
        }

        /// <summary>
        /// Gets the column identifier in the expression
        /// </summary>
        public string Column { get; }
    }
}
