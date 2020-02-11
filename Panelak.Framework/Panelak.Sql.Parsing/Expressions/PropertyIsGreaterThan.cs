namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// SQL operator greater than (&gt;)
    /// </summary>
    public class PropertyIsGreaterThan : ColumnComparisonExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyIsGreaterThan"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="literal">Value literal</param>
        public PropertyIsGreaterThan(string column, string literal) : base(column, ">", literal)
        {
        }
    }
}
