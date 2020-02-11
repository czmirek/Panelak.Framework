namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// SQL operator not equal to (!=)
    /// </summary>
    public class PropertyIsNotEqualTo : ColumnComparisonExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyIsNotEqualTo"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="literal">Value literal</param>
        public PropertyIsNotEqualTo(string column, string literal) : base(column, "!=", literal)
        {
        }
    }
}
