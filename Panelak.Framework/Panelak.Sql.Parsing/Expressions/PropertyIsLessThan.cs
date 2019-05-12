namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// SQL operator less than (&lt;)
    /// </summary>
    public class PropertyIsLessThan : ColumnComparisonExpression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyIsLessThan"/> class.
        /// </summary>
        /// <param name="column">Column identifier</param>
        /// <param name="literal">Value literal</param>
        public PropertyIsLessThan(string column, string literal) : base(column, "<", literal)
        {
        }
    }
}
