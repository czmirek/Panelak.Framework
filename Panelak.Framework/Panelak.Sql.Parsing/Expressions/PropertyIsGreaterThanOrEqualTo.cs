namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// SQL operator greater than or equal to (&gt;=)
    /// </summary>
    public class PropertyIsGreaterThanOrEqualTo : ComparisonExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyIsGreaterThanOrEqualTo"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="literal">Value literal</param>
        public PropertyIsGreaterThanOrEqualTo(string column, string literal) : base(column, ">=", literal)
        {
        }
    }
}
