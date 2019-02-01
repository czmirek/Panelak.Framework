namespace Panelak.Sql
{
    /// <summary>
    /// IS BETWEEN expression in the form [Column] IS BETWEEN [LowerBoundary] AND [UpperBoundary]
    /// </summary>
    public interface ISqlConditionIsBetween : ISqlConditionExpression
    {
        /// <summary>
        /// Gets a column identifier
        /// </summary>
        string Column { get; }

        /// <summary>
        /// Gets an upper boundary value
        /// </summary>
        string UpperBoundary { get; }

        /// <summary>
        /// Gets a lower boundary value
        /// </summary>
        string LowerBoundary { get; }
    }
}