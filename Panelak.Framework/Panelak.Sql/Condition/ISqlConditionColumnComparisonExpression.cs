namespace Panelak.Sql
{
    /// <summary>
    /// SQL comparison expression of the form COLUMN OPERATOR LITERAL (e.g. NumberColumn = 3 or StringColumn LIKE 'text').
    /// Note that column must be on the left side of the operator and literal on the right side of the operator.
    /// </summary>
    public interface ISqlConditionColumnComparisonExpression : ISqlConditionExpression
    {
        /// <summary>
        /// Gets a string representation of the operator.
        /// </summary>
        string Operator { get; }

        /// <summary>
        /// Gets a column identifier on the left side of the operator
        /// </summary>
        string Column { get; }

        /// <summary>
        /// Gets a literal value on the right side of the operator.
        /// </summary>
        string Literal { get; }
    }
}
