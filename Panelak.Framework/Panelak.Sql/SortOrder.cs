namespace Panelak.Sql
{
    /// <summary>
    /// Sort order of the SQL query (e.g. ASC, DESC)
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// The default. No sort order is specified.
        /// </summary>
        Unspecified = -1,
        
        /// <summary>
        /// Rows are sorted in ascending order.
        /// </summary>
        Ascending = 0,
        
        /// <summary>
        /// Rows are sorted in descending order.
        /// </summary>
        Descending = 1
    }
}