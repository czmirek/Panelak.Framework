namespace Panelak.Sql.Parsing
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// SQL token model
    /// </summary>
    [DebuggerDisplay("{Token}: {TokenType}")]
    public class SqlToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlToken"/> class.
        /// </summary>
        /// <param name="token">The token literal</param>
        /// <param name="tokenType">Token type</param>
        internal SqlToken(string token, TokenType tokenType)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            TokenType = tokenType;
        }

        /// <summary>
        /// Gets the token literal
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Gets the token classification
        /// </summary>
        public TokenType TokenType { get; }
    }
}
