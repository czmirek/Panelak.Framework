namespace Panelak.Sql
{
    using System.Collections.Generic;

    /// <summary>
    /// RDBMS independent SQL SELECT query model
    /// </summary>
    public interface ISqlSelectQuery
    {
        /// <summary>
        /// Gets or sets the identifier of the table in the FROM clause
        /// </summary>
        ISqlTableIdentifier TableIdentifier { get; set; }

        /// <summary>
        /// Gets SQL columns in the SELECT clause. Sub queries are not supported.
        /// </summary>
        IEnumerable<ISqlColumn> Columns { get; }

        /// <summary>
        /// Gets or sets a value indicating whether column aliases are to be excluded from the query
        /// </summary>
        bool ExcludeAliases { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pagination is not considered when building this SQL query
        /// </summary>
        bool NoPagination { get; set; }

        /// <summary>
        /// Gets or sets page for pagination purposes. Starts at 1.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets the size of the page for pagination purposes.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Gets SQL parameters entering into the RDBMS with this query.
        /// </summary>
        IEnumerable<ISqlParameter> Parameters { get; }

        /// <summary>
        /// Gets a binary tree representation of an SQL condition
        /// </summary>
        ISqlConditionExpression Condition { get; }

        /// <summary>
        /// Applies sorting in fallback manner. If no sorting is specified in the query, default
        /// column sorting is used.
        /// </summary>
        /// <param name="defaultSortColumnName">Column used for default sorting</param>
        /// <param name="defaultSortColumnSortOrder">Sort order of default column</param>
        /// <param name="inputSortByColumns">Columns with sorting from the input</param>
        void ApplySorting(string defaultSortColumnName, SortOrder defaultSortColumnSortOrder, IEnumerable<ISqlSortColumn> inputSortByColumns);

        /// <summary>
        /// Inserts a new column to the column list
        /// </summary>
        /// <param name="name">Column identifier</param>
        /// <param name="include">Whether the column must to be included in the query</param>
        /// <param name="sortOrder">Column sort order</param>
        /// <param name="visible">If the column will be visible in the result set</param>
        void InsertColumn(string name, bool include, SortOrder sortOrder, string alias = null, bool visible = true);


        /// <summary>
        /// Removes a column by name from the list
        /// </summary>
        /// <param name="columnName">Name of the column to remove</param>
        void RemoveColumn(string columnName);

        /// <summary>
        /// Sets a selected column as a primary key and unsets the primary key flag from all other columns.
        /// </summary>
        /// <param name="primaryKeyColumnName">Column identifier of the primary key</param>
        void SetPrimaryKey(string primaryKeyColumnName);

        /// <summary>
        /// Appends a new condition expression after "AND"
        /// </summary>
        /// <param name="expression">SQL expression</param>
        void And(ISqlConditionExpression expression);

        /// <summary>
        /// Appends "AND column IN (1,2,3...)" to the condition expression tree
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="values">Values of the IN operator</param>
        void AndIn(string column, IEnumerable<string> values);

        /// <summary>
        /// Parses <paramref name="expression"/> and appends it to the condition expression tree
        /// </summary>
        /// <param name="expression">SQL expression</param>
        void AndString(string expression);
    }
}
