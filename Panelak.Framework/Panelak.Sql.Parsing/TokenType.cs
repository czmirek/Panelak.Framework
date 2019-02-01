namespace Panelak.Sql.Parsing
{
    /// <summary>
    /// SQL token classifications
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// Column identifier: [column], column
        /// </summary>
        Identifier,

        /// <summary>
        /// Value literal: 'literal', '%literal%', literal
        /// </summary>
        Literal,

        /// <summary>
        /// Opening round bracket
        /// </summary>
        OpeningRoundBracket,

        /// <summary>
        /// Closing round bracket
        /// </summary>
        ClosingRoundBracket,

        /// <summary>
        /// Equal sign
        /// </summary>
        Equal,

        /// <summary>
        /// Not equal sign
        /// </summary>
        NotEqual,

        /// <summary>
        /// Greater than sign
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Lesser than sign
        /// </summary>
        LesserThan,

        /// <summary>
        /// Greater than or equal sign
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Lesser than or equal sign
        /// </summary>
        LesserThanOrEqual,

        /// <summary>
        /// NOT operator
        /// </summary>
        Not,

        /// <summary>
        /// LIKE operator
        /// </summary>
        Like,

        /// <summary>
        /// NOT LIKE operator
        /// </summary>
        NotLike,

        /// <summary>
        /// AND operator
        /// </summary>
        And,

        /// <summary>
        /// OR operator
        /// </summary>
        Or,

        /// <summary>
        /// IS NULL comparison operator
        /// </summary>
        IsNull,

        /// <summary>
        /// IS NOT NULL comparison operator
        /// </summary>
        IsNotNull,

        /// <summary>
        /// IN operator
        /// </summary>
        In
    }
}
