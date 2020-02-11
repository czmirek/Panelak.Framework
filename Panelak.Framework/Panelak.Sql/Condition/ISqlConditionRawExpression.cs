namespace Panelak.Sql
{
    /// <summary>
    /// Unparsed SQL comparison expression with string entering the condition.
    /// Use only for appending manually parsed and checked SQL or SQL from safe sources.
    /// </summary>
    public interface ISqlConditionRawExpression : ISqlConditionExpression
    {
        /// <summary>
        /// SQL expression string
        /// </summary>
        string RawSqlString { get; }
    }
}
