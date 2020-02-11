namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// Thrown when attempting to parse condition expression in the <see cref="SqlConditionParser"/> with sub queries
    /// which is not supported.
    /// </summary>
    public class SubqueriesNotAllowedException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubqueriesNotAllowedException"/> class.
        /// </summary>
        public SubqueriesNotAllowedException() : base("Selected query contains subqueries. Replace subqueries with a single view and try again.")
        {
        }
    }
}
