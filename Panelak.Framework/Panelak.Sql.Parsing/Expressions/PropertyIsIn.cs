namespace Panelak.Sql.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Default implementation of the IN expression in the format Column IN ([value], [value], ...)
    /// </summary>
    public class PropertyIsIn : ISqlConditionIn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyIsIn"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="values">Enumeration of values</param>
        public PropertyIsIn(string column, IEnumerable<string> values)
        {
            Column = column ?? throw new ArgumentNullException(nameof(column));
            Operator = "IN";
            Values = values ?? throw new ArgumentNullException(nameof(values));
            Literal = String.Join(", ", values.Select(s => s.ToString()));
        }

        /// <summary>
        /// Gets the column identifier
        /// </summary>
        public string Column { get; }

        /// <summary>
        /// Gets the IN literal
        /// </summary>
        public string Operator { get; }

        /// <summary>
        /// Gets the enumeration of values on the right side of the IN operator
        /// </summary>
        public IEnumerable<string> Values { get; }

        /// <summary>
        /// Gets the string representation of the right side of the IN operator
        /// </summary>
        public string Literal { get; }
    }
}
