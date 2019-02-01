namespace Panelak.Sql.Parsing
{
    using System;

    /// <summary>
    /// SQL parameter of a SQL query
    /// </summary>
    internal class SqlParameter : ISqlParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParameter"/> class.
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="value">Parameter value</param>
        /// <param name="isFilteringParameter">Whether the parameter is used for filtering</param>
        public SqlParameter(string name, object value, bool isFilteringParameter)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            IsFilteringParameter = isFilteringParameter;
        }

        /// <summary>
        /// Gets the name of the parameter
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value of the parameter
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the parameter affects number of returned results
        /// </summary>
        public bool IsFilteringParameter { get; private set; }
    }
}
