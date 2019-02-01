namespace Panelak.Sql.Parsing
{
	using Microsoft.Extensions.Logging;
	using System;
	using System.Collections.Generic;
	using System.Text.RegularExpressions;

	/// <summary>
	/// Converts SQL condition expression in string to individual tokens using regular expressions 
	/// which can be later fed into the binary tree builder.
	/// </summary>
	public class SqlConditionTokenizer
	{
		/// <summary>
		/// Regex used for parsing a column identifier
		/// </summary>
		private static readonly Regex IdentifierRx = new Regex(@"(^(?!\d)\w+)", RegexOptions.IgnoreCase);

		/// <summary>
		/// Regex used for parsing a bracketed column identifier (for MSSQL)
		/// </summary>
		private static readonly Regex BracketedIdentifier = new Regex(@"(^\[[\w\p{L}\s]+\])", RegexOptions.IgnoreCase);

		/// <summary>
		/// Regex used for parsing string literals
		/// </summary>
		private static readonly Regex LiteralRx = new Regex(@"(^\'(?:\%?)[\w\p{L}\s""\(\)]+(?:\%?)\')");

		/// <summary>
		/// Regex used for parsing list of values next to the IN operator
		/// </summary>
		private static readonly Regex InRx = new Regex(@"^\(.+\)");

		/// <summary>
		/// Regex used for parsing numeric literals
		/// </summary>
		private static readonly Regex NumericLiteralRx = new Regex(@"(^\d+(?:\.\d+)?)");

		/// <summary>
		/// Regex used for parsing SQL operators
		/// </summary>
		private static readonly Regex OperatorRx = new Regex(@"(?:^(\=|\<\=|\>\=|\!\=|\>|\<|NOT LIKE|NOT|LIKE|AND|OR|IS NULL|IS NOT NULL))", RegexOptions.IgnoreCase);

		/// <summary>
		/// Logger instance
		/// </summary>
		private readonly ILogger log;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlConditionTokenizer"/> class.
		/// </summary>
		/// <param name="log">Logging instance</param>
		public SqlConditionTokenizer(ILogger log) => this.log = log ?? throw new ArgumentNullException(nameof(log));

		/// <summary>
		/// Converts string expression into an enumeration of tokens
		/// </summary>
		/// <param name="expression">SQL string expression</param>
		/// <returns>Enumeration of tokens</returns>
		public IEnumerable<SqlToken> Tokenize(string expression)
		{
			log.LogTrace($"Tokenizing expression {expression}");

			var tokens = new List<SqlToken>();

			for (int i = 0; i < expression.Length; i++)
			{
				if (Char.IsWhiteSpace(expression[i]))
					continue;

				string currentExpr = expression.Substring(i);

				Match literalMatch = LiteralRx.Match(currentExpr);
				if (literalMatch.Success)
				{
					tokens.Add(new SqlToken(literalMatch.Value.Trim('\''), TokenType.Literal));
					i = i + literalMatch.Value.Length - 1;
					continue;
				}

				Match numLiteralMatch = NumericLiteralRx.Match(currentExpr);
				if (numLiteralMatch.Success)
				{
					tokens.Add(new SqlToken(numLiteralMatch.Value, TokenType.Literal));
					i = i + numLiteralMatch.Value.Length - 1;
					continue;
				}

				Match operatorMatch = OperatorRx.Match(currentExpr);
				if (operatorMatch.Success)
				{
					TokenType tt;
					switch (operatorMatch.Value.ToLowerInvariant())
					{
						case "=":
							tt = TokenType.Equal;
							break;
						case "!=":
							tt = TokenType.NotEqual;
							break;
						case ">":
							tt = TokenType.GreaterThan;
							break;
						case "<":
							tt = TokenType.LesserThan;
							break;
						case "<=":
							tt = TokenType.LesserThanOrEqual;
							break;
						case ">=":
							tt = TokenType.GreaterThanOrEqual;
							break;
						case "and":
							tt = TokenType.And;
							break;
						case "or":
							tt = TokenType.Or;
							break;
						case "not":
							tt = TokenType.Not;
							break;
						case "like":
							tt = TokenType.Like;
							break;
						case "not like":
							tt = TokenType.NotLike;
							break;
						case "is null":
							tt = TokenType.IsNull;
							break;
						case "is not null":
							tt = TokenType.IsNotNull;
							break;
						case "in":
							tt = TokenType.In;
							break;
						default:
							throw new NotImplementedException();
					}

					tokens.Add(new SqlToken(operatorMatch.Value, tt));
					i = i + operatorMatch.Value.Length - 1;
					continue;
				}

				if (expression[i] == '(')
				{
					tokens.Add(new SqlToken(expression[i].ToString(), TokenType.OpeningRoundBracket));
					continue;
				}

				if (expression[i] == ')')
				{
					tokens.Add(new SqlToken(expression[i].ToString(), TokenType.ClosingRoundBracket));
					continue;
				}

				Match identifierMatch = IdentifierRx.Match(currentExpr);
				if (identifierMatch.Success)
				{
					tokens.Add(new SqlToken(identifierMatch.Value, TokenType.Identifier));
					i = i + identifierMatch.Value.Length - 1;
					continue;
				}

				Match bracketedIdentifierMatch = BracketedIdentifier.Match(currentExpr);
				if (bracketedIdentifierMatch.Success)
				{
					tokens.Add(new SqlToken(bracketedIdentifierMatch.Value.Trim('[', ']'), TokenType.Identifier));
					i = i + bracketedIdentifierMatch.Value.Length - 1;
					continue;
				}

				Match inMatch = InRx.Match(currentExpr);
				if (inMatch.Success && tokens[tokens.Count - 1].TokenType != TokenType.In)
					throw new InvalidOperationException("IN must be followed by a list of values");
				else
					tokens.Add(new SqlToken(inMatch.Value, TokenType.Literal));
			}

			log.LogTrace($"Tokens: {tokens.Count}");

			return tokens.AsReadOnly();
		}
	}
}
