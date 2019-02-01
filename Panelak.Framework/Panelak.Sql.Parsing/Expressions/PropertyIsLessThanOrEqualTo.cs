namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// SQL operator less than or equal to (&lt;=)
    /// </summary>
    public class PropertyIsLessThanOrEqualTo : ComparisonExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyIsLessThanOrEqualTo"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="literal">Value literal</param>
        public PropertyIsLessThanOrEqualTo(string column, string literal) : base(column, "<=", literal)
        {
        }
    }
}
