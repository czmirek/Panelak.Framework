namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// SQL operator LIKE
    /// </summary>
    public class PropertyIsLike : ColumnComparisonExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyIsLike"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="literal">Value literal</param>
        public PropertyIsLike(string column, string literal) : base(column, "LIKE", literal)
        {
        }
    }
}
