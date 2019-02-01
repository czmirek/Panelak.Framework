namespace Panelak.Sql
{
    /// <summary>
    /// IS NOT NULL expression in the form [Column] IS NOT NULL
    /// </summary>
    public interface ISqlConditionIsNotNull : ISqlConditionExpression
    {
        /// <summary>
        /// Gets a column identifier
        /// </summary>
        string Column { get; }
    }
}