namespace Panelak.Database.SqlServer
{
    using Panelak.Sql;

    /// <summary>
    /// Sql Server SQL parameter of a SQL query
    /// </summary>
    internal class SqlServerDbParameter : ISqlParameter
    {
        /// <summary>
        /// Gets name of the parameter
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets value of the parameter
        /// </summary>
        public object Value { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the parameter affects number of returned results
        /// </summary>
        public bool IsFilteringParameter { get; internal set; }
    }
}
