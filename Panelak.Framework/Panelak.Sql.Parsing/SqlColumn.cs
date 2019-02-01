namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// Single column in a SQL SELECT expression
    /// </summary>
    internal class SqlColumn : ISqlColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlColumn"/> class.
        /// </summary>
        /// <param name="expression">Column identifier</param>
        /// <param name="alias">Optional alias of the column</param>
        public SqlColumn(string expression, string alias = null)
        {
            Expression = expression;
            ExpressionAlias = alias;
            TrimmedAlias = alias?.Trim('\'', '"', ' ');

            if (expression.Contains("rownum") || expression.Contains("||rownum"))
                Unquoted = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the column is the primary key of the table
        /// </summary>
        public bool IsPrimaryKey { get; set; } = false;

        /// <summary>
        /// Gets the column expression or identifier
        /// </summary>
        public string Expression { get; private set; }

        /// <summary>
        /// Gets the alias of the expression
        /// </summary>
        public string ExpressionAlias { get; private set; }

        /// <summary>
        /// Gets the alias trimmed of apostrophes and spaces
        /// </summary>
        public string TrimmedAlias { get; private set; }

        /// <summary>
        /// Gets a value indicating whether expression is not to be quoted in the resulting query
        /// </summary>
        public bool Unquoted { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the column is to be included in the result
        /// </summary>
        public bool Include { get; set; } = true;

        /// <summary>
        /// Gets or sets the SortOrder
        /// Gets or sets whether the column is used for sorting
        /// </summary>
        public SortOrder SortOrder { get; set; } = SortOrder.Unspecified;
    }
}
