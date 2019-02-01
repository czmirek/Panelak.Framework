namespace Panelak.Sql.Parsing.Test
{
    using Microsoft.Extensions.Logging.Debug;
    using NUnit.Framework;
    using System.Linq;

    public class SqlConditionTokenizerTest
    {
        private readonly DebugLogger log;

        public SqlConditionTokenizerTest() => log = new DebugLogger("logger");

        [Test]
        public void Tokenize_Complex_Condition_Test()
        {
            const string expression = @"

    A = 1 
        AND 
    ([B] > 3 OR [C] NOT LIKE '%string%' AND (D <= 15 AND [ŘEŘICHA A MEZERY] > 99.04)) 

OR 

    column IS NULL

OR 

    F LIKE 'smthing%'
        AND 
    NOT (B < 74 AND NOT (CC >= 77.10 OR II=1))";

            var tokenizer = new SqlConditionTokenizer(log);
            var t = tokenizer.Tokenize(expression).ToList();
            int i = 0;

            t[i++].IsId("A");
            t[i++].IsOp("=", TokenType.Equal);
            t[i++].IsLt("1");
            t[i++].IsOp("AND", TokenType.And);
            t[i++].IsOBr();
            t[i++].IsId("B");
            t[i++].IsOp(">", TokenType.GreaterThan);
            t[i++].IsLt("3");
            t[i++].IsOp("OR", TokenType.Or);
            t[i++].IsId("C");
            t[i++].IsOp("NOT LIKE", TokenType.NotLike);
            t[i++].IsLt("%string%");
            t[i++].IsOp("AND", TokenType.And);
            t[i++].IsOBr();
            t[i++].IsId("D");
            t[i++].IsOp("<=", TokenType.LesserThanOrEqual);
            t[i++].IsLt("15");
            t[i++].IsOp("AND", TokenType.And);
            t[i++].IsId("ŘEŘICHA A MEZERY");
            t[i++].IsOp(">", TokenType.GreaterThan);
            t[i++].IsLt("99.04");
            t[i++].IsCBr();
            t[i++].IsCBr();
            t[i++].IsOp("OR", TokenType.Or);
            t[i++].IsId("column");
            t[i++].IsOp("IS NULL", TokenType.IsNull);
            t[i++].IsOp("OR", TokenType.Or);
            t[i++].IsId("F");
            t[i++].IsOp("LIKE", TokenType.Like);
            t[i++].IsLt("smthing%");
            t[i++].IsOp("AND", TokenType.And);
            t[i++].IsOp("NOT", TokenType.Not);
            t[i++].IsOBr();
            t[i++].IsId("B");
            t[i++].IsOp("<", TokenType.LesserThan);
            t[i++].IsLt("74");
            t[i++].IsOp("AND", TokenType.And);
            t[i++].IsOp("NOT", TokenType.Not);
            t[i++].IsOBr();
            t[i++].IsId("CC");
            t[i++].IsOp(">=", TokenType.GreaterThanOrEqual);
            t[i++].IsLt("77.10");
            t[i++].IsOp("OR", TokenType.Or);
            t[i++].IsId("II");
            t[i++].IsOp("=", TokenType.Equal);
            t[i++].IsLt("1");
            t[i++].IsCBr();
            t[i++].IsCBr();
        }
    }


    public static class SqlTokenExtension
    {
        public static void IsLt(this SqlToken token, string expectedValue)
        {
            Assert.AreEqual(token.TokenType, TokenType.Literal);
            Assert.AreEqual(token.Token, expectedValue);
        }

        public static void IsId(this SqlToken token, string expectedValue)
        {
            Assert.AreEqual(token.TokenType, TokenType.Identifier);
            Assert.AreEqual(token.Token, expectedValue);
        }

        public static void IsOp(this SqlToken token, string expectedValue, TokenType operatorTokenType)
        {
            Assert.AreEqual(token.TokenType, operatorTokenType);
            Assert.AreEqual(token.Token, expectedValue);
        }

        public static void IsOBr(this SqlToken token)
        {
            Assert.AreEqual(token.TokenType, TokenType.OpeningRoundBracket);
            Assert.AreEqual(token.Token, "(");
        }

        public static void IsCBr(this SqlToken token)
        {
            Assert.AreEqual(token.TokenType, TokenType.ClosingRoundBracket);
            Assert.AreEqual(token.Token, ")");
        }
    }
}
