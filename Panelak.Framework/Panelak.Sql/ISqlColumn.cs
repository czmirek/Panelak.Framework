namespace Panelak.Sql
{
    /// <summary>
    /// RDBMS independent single column in a SQL SELECT expression
    /// </summary>
    public interface ISqlColumn : ISqlSortColumn
    {
        /// <summary>
        /// Gets a value indicating whether the column is considered a primary key
        /// </summary>
        bool IsPrimaryKey { get; }

        /// <summary>
        /// Gets an alias of the expression
        /// </summary>
        string ExpressionAlias { get; }
        
        /// <summary>
        /// Gets a name of the column without identifier quotes
        /// </summary>
        string TrimmedAlias { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the column is to be included in the result
        /// </summary>
        bool Include { get; set; }

        /// <summary>
        /// Gets a value indicating whether expression is not to be quoted in the resulting query
        /// </summary>
        //bool Unquoted { get; }

        /// <summary>
        /// Gets or sets the sort order of the column
        /// </summary>
        new SortOrder SortOrder { get; set; }

        /// <summary>
        /// Detects if the column should be queried id DB but not attached to the result set
        /// </summary>
        bool Visible { get; set; }
    }
}
