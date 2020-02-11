namespace Panelak.Sql
{
    /// <summary>
    /// RDBMS independent SQL parameter of a SQL query
    /// </summary>
    public interface ISqlParameter
    {
        /// <summary>
        /// Gets name of the parameter
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets value of the parameter
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Gets a value indicating whether the parameter affects number of returned results
        /// </summary>
        bool IsFilteringParameter { get; }
    }
}