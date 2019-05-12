namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// SQL operator NOT LIKE
    /// </summary>
    public class PropertyIsNotLike : ColumnComparisonExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyIsNotLike"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="literal">Value literal</param>
        public PropertyIsNotLike(string column, string literal) : base(column, "NOT LIKE", literal)
        {
        }
    }
}
