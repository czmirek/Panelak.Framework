namespace Panelak.Sql
{
    /// <summary>
    /// IS NULL expression in the form [Column] IS NULL
    /// </summary>
    public interface ISqlConditionIsNull : ISqlConditionExpression
    {
        /// <summary>
        /// Gets a column identifier
        /// </summary>
        string Column { get; }
    }
}