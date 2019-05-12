namespace Panelak.Sql.Parsing
{
    using System;
    using System.Diagnostics;


    /// <summary>
    /// Unsafe, unchecked raw SQL expression. Use only for appending manually parsed and checked SQL or SQL 
    /// not comming from unsafe sources.
    /// </summary>
    [DebuggerDisplay("{RawSqlValue}")]
    public class RawExpression : ISqlConditionRawExpression
    {
        /// <summary>
        /// Raw SQL string
        /// </summary>
        public string RawSqlString { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RawExpression" /> class.
        /// </summary>
        /// <param name="rawSqlString">SQL string</param>
        public RawExpression(string rawSqlString) => RawSqlString = rawSqlString ?? throw new ArgumentNullException(nameof(rawSqlString));
    }
}
