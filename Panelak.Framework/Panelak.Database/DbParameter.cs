namespace Panelak.Database
{
    using Panelak.Sql;
    using System;

    /// <summary>
    /// Local database parameter implementation
    /// </summary>
    internal class DbParameter : ISqlParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbParameter"/> class.
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        /// <param name="isFilteringParameter">Whether the parameter filters the result</param>
        public DbParameter(string name, object value, bool isFilteringParameter)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            IsFilteringParameter = isFilteringParameter;
        }

        /// <summary>
        /// Gets the name of SQL parameter
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value of SQL parameter
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the parameter filters the result
        /// </summary>
        public bool IsFilteringParameter { get; private set; }
    }
}
