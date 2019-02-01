namespace Panelak.Sql.Parsing
{
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LeftRightExprFactory = System.Func<ISqlConditionExpression, ISqlConditionExpression, object>;
    using RightExprFactory = System.Func<ISqlConditionExpression, object>;

    /// <summary>
    /// Builds a SQL condition binary tree out of parsed SQL tokens
    /// </summary>
    public class SqlConditionParser
    {
        /// <summary>
        /// Logger reference
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Recursion index when braces are used
        /// </summary>
        private int nestedLevel = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlConditionParser"/> class.
        /// </summary>
        /// <param name="logger">Instance for logging</param>
        public SqlConditionParser(ILogger logger) => this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <summary>
        /// Parses tokens into binary tree expression
        /// </summary>
        /// <param name="tokens">The tokens<see cref="IEnumerable{SqlToken}"/></param>
        /// <returns>The <see cref="ISqlConditionExpression"/></returns>
        internal ISqlConditionExpression Parse(IEnumerable<SqlToken> tokens)
        {
            logger.LogTrace("Building a binary tree out of SQL tokens");

            var tokenList = tokens.ToList();
            ParseState state = ParseState.ExpressionStart;

            SqlToken expressionLeftSide = null;
            SqlToken expressionOperator = null;
            var parsedExpressions = new List<object>();

            logger.LogTrace("Parsing expressions...");

            // state machine used to merge tokens into comparison expressions
            foreach (SqlToken token in tokenList)
            {
                switch (state)
                {
                    case ParseState.ExpressionStart:
                        switch (token.TokenType)
                        {
                            case TokenType.Not:
                            case TokenType.OpeningRoundBracket:
                            case TokenType.ClosingRoundBracket:
                            case TokenType.And:
                            case TokenType.Or:
                                parsedExpressions.Add(token);
                                break;
                            case TokenType.Identifier:
                                expressionLeftSide = token;
                                state = ParseState.LeftSideIdentifier;
                                break;
                            default:
                                throw new InvalidSqlParserStateException();
                        }

                        break;

                    case ParseState.LeftSideIdentifier:
                        switch (token.TokenType)
                        {
                            case TokenType.Equal:
                            case TokenType.NotEqual:
                            case TokenType.GreaterThan:
                            case TokenType.LesserThan:
                            case TokenType.GreaterThanOrEqual:
                            case TokenType.LesserThanOrEqual:
                            case TokenType.Like:
                            case TokenType.NotLike:
                            case TokenType.In:
                                expressionOperator = token;
                                state = ParseState.TwoSidedOperator;
                                break;
                            case TokenType.IsNull:
                                parsedExpressions.Add(new IsNull(expressionLeftSide.Token));
                                state = ParseState.ExpressionStart;
                                break;
                            case TokenType.IsNotNull:
                                parsedExpressions.Add(new IsNotNull(expressionLeftSide.Token));
                                state = ParseState.ExpressionStart;
                                break;
                            default:
                                throw new InvalidSqlParserStateException();
                        }

                        break;

                    case ParseState.TwoSidedOperator:
                        switch (token.TokenType)
                        {
                            case TokenType.Literal:
                                switch (expressionOperator.TokenType)
                                {
                                    case TokenType.Equal:
                                        parsedExpressions.Add(new PropertyIsEqualTo(expressionLeftSide.Token, token.Token));
                                        state = ParseState.ExpressionStart;
                                        break;
                                    case TokenType.NotEqual:
                                        parsedExpressions.Add(new PropertyIsNotEqualTo(expressionLeftSide.Token, token.Token));
                                        state = ParseState.ExpressionStart;
                                        break;
                                    case TokenType.GreaterThan:
                                        parsedExpressions.Add(new PropertyIsGreaterThan(expressionLeftSide.Token, token.Token));
                                        state = ParseState.ExpressionStart;
                                        break;
                                    case TokenType.LesserThan:
                                        parsedExpressions.Add(new PropertyIsLessThan(expressionLeftSide.Token, token.Token));
                                        state = ParseState.ExpressionStart;
                                        break;
                                    case TokenType.GreaterThanOrEqual:
                                        parsedExpressions.Add(new PropertyIsGreaterThanOrEqualTo(expressionLeftSide.Token, token.Token));
                                        state = ParseState.ExpressionStart;
                                        break;
                                    case TokenType.LesserThanOrEqual:
                                        parsedExpressions.Add(new PropertyIsLessThanOrEqualTo(expressionLeftSide.Token, token.Token));
                                        state = ParseState.ExpressionStart;
                                        break;
                                    case TokenType.Like:
                                        parsedExpressions.Add(new PropertyIsLike(expressionLeftSide.Token, token.Token));
                                        state = ParseState.ExpressionStart;
                                        break;
                                    case TokenType.NotLike:
                                        parsedExpressions.Add(new PropertyIsNotLike(expressionLeftSide.Token, token.Token));
                                        state = ParseState.ExpressionStart;
                                        break;
                                    case TokenType.Not:
                                        parsedExpressions.Add(token);
                                        state = ParseState.ExpressionStart;
                                        break;
                                    default:
                                        throw new InvalidSqlParserStateException();
                                }

                                break;
                            default:
                                throw new InvalidSqlParserStateException();
                        }

                        break;
                }
            }

            // merge logical operators
            ISqlConditionExpression expressions = ParseOperators(parsedExpressions);

            return expressions;
        }

        /// <summary>
        /// Parses logical operators
        /// </summary>
        /// <param name="parsedExpressions">List of already parsed expressions</param>
        /// <returns>Binary tree expression</returns>
        private ISqlConditionExpression ParseOperators(List<object> parsedExpressions)
        {
            nestedLevel++;

            logger.LogTrace($"Parsing binary tree level {nestedLevel}");

            // expressions of current level are in the expression stack
            var exprStack = new Stack<(int level, object expr)>();
            var expressions = new List<ISqlConditionExpression>();
            int level = 0;

            // browse recursively expressions in brackets if there are any
            foreach (object parsed in parsedExpressions)
            {
                switch (parsed)
                {
                    case SqlToken token when token.TokenType == TokenType.OpeningRoundBracket:
                        level++;
                        break;

                    case SqlToken token when token.TokenType == TokenType.ClosingRoundBracket:

                        if (level < 1)
                            throw new InvalidOperationException("Parentheses mismatch");

                        var sublist = new List<object>();
                        while (exprStack.Count > 0 && exprStack.Peek().level == level)
                            sublist.Add(exprStack.Pop().expr);

                        if (sublist.Count > 0)
                        {
                            ISqlConditionExpression subExpressions = ParseOperators(sublist);
                            exprStack.Push((level - 1, subExpressions));
                        }

                        level--;
                        break;

                    default:
                        exprStack.Push((level, parsed));
                        break;
                }
            }

            var topLevelExprList = exprStack.Select(es => es.expr).ToList();

            if (nestedLevel == 1)
                topLevelExprList.Reverse();

            // operator parsing by precedence: NOT, AND, OR. This applies to most popular RDBMS systems.
            RightOperator(topLevelExprList, TokenType.Not, right => new Not(right));
            LeftRightOperator(topLevelExprList, TokenType.And, (left, right) => new And(left, right));
            LeftRightOperator(topLevelExprList, TokenType.Or, (left, right) => new Or(left, right));

            if (topLevelExprList.Count > 1)
                throw new InvalidOperationException("Top level should contain only one expression item");

            nestedLevel--;

            return topLevelExprList[0] as ISqlConditionExpression;
        }

        /// <summary>
        /// Parses a logical operator with right expression
        /// </summary>
        /// <param name="exprList">List of expressions</param>
        /// <param name="tokenType">The operator token type</param>
        /// <param name="exprFac">Factory for creating the expression instance</param>
        private void RightOperator(List<object> exprList, TokenType tokenType, RightExprFactory exprFac)
        {
            var isNotToken = new Predicate<object>(e => e is SqlToken sqlToken && sqlToken.TokenType == tokenType);
            int index;
            while ((index = exprList.FindIndex(isNotToken)) > -1)
            {
                ISqlConditionExpression rightSideExpr = GetExpression(exprList[index + 1]);
                exprList[index] = exprFac(rightSideExpr);
                exprList.RemoveAt(index + 1);
            }
        }

        /// <summary>
        /// Parses a logical operator with left and right expressions
        /// </summary>
        /// <param name="exprList">List of expressions</param>
        /// <param name="tokenType">The operator token type</param>
        /// <param name="exprFac">Factory for creating the expression instance</param>
        private void LeftRightOperator(List<object> exprList, TokenType tokenType, LeftRightExprFactory exprFac)
        {
            var isToken = new Predicate<object>(e => e is SqlToken sqlToken && sqlToken.TokenType == tokenType);
            int index;

            while ((index = exprList.FindIndex(isToken)) > -1)
            {
                ISqlConditionExpression leftSideExpr = GetExpression(exprList[index - 1]);
                ISqlConditionExpression rightSideExpr = GetExpression(exprList[index + 1]);

                exprList[index] = exprFac(leftSideExpr, rightSideExpr);
                exprList.RemoveAt(index + 1);
                exprList.RemoveAt(index - 1);
            }
        }

        /// <summary>
        /// Casts object to <see cref="ISqlConditionExpression"/> with a type check
        /// </summary>
        /// <param name="expression">The expression object</param>
        /// <returns>Expression object returned as condition expression</returns>
        private ISqlConditionExpression GetExpression(object expression)
        {
            if (!(expression is ISqlConditionExpression exprCast))
                throw new InvalidOperationException();

            return exprCast;
        }
    }
}
