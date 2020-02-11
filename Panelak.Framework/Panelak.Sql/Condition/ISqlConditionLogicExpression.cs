namespace Panelak.Sql
{
    /// <summary>
    /// Expression of SQL logical operator
    /// </summary>
    public interface ISqlConditionLogicExpression : ISqlConditionExpression
    {
        /// <summary>
        /// Gets an SQL logical operator such as AND, OR, NOT
        /// </summary>
        string Operator { get; }
    }
}