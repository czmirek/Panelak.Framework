namespace Panelak.Sql
{
    /// <summary>
    /// RDBMS independent single column for sorting in a SQL SELECT expression
    /// </summary>
    public interface ISqlSortColumn
    {
        /// <summary>
        /// Gets a column expression or identifier
        /// </summary>
        string Expression { get; }

        /// <summary>
        /// Gets the column sort order
        /// </summary>
        SortOrder SortOrder { get; }
    }
}