namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// SQL operator equal to (=)
    /// </summary>
    public class PropertyIsEqualTo : ComparisonExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyIsEqualTo"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="literal">Value literal</param>
        public PropertyIsEqualTo(string column, string literal) : base(column, "=", literal)
        {
        }
    }
}
