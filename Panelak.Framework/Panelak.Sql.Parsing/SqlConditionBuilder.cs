namespace Panelak.Sql.Parsing
{
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    /// <summary>
    /// Builder of a binary tree representation of simplified SQL condition expression
    /// </summary>
    public class SqlConditionBuilder
    {
        /// <summary>
        /// Logger reference
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlConditionBuilder"/> class.
        /// </summary>
        /// <param name="logger">Instance for logging</param>
        public SqlConditionBuilder(ILogger logger) => this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

        /// <summary>
        /// Builds a binary tree out of SQL string with a condition expression
        /// </summary>
        /// <param name="expression">SQL string with condition expression</param>
        /// <returns>Binary tree of the expression</returns>
        public ISqlConditionExpression Build(string expression)
        {
            expression = expression.Trim();
            var lexer = new SqlConditionTokenizer(logger);
            var parser = new SqlConditionParser(logger);

            IEnumerable<SqlToken> tokens = lexer.Tokenize(expression);
            return parser.Parse(tokens);
        }
    }
}
