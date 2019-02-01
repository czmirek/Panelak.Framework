namespace Panelak.Sql.Parsing.Test
{
    using Microsoft.Extensions.Logging.Debug;
    using NUnit.Framework;
    using System;

    public class SqlConditionBuilderTest
    {
        private readonly DebugLogger log;

        public SqlConditionBuilderTest() => log = new DebugLogger("logger");

        [Test(Description = "Tests simple X operator Y expressions")]
        [TestOf(typeof(SqlConditionBuilder))]
        [TestCase(typeof(PropertyIsEqualTo), "A=1", "A", "1")]
        [TestCase(typeof(PropertyIsEqualTo), "A = 1", "A", "1")]
        [TestCase(typeof(PropertyIsEqualTo), "    A       =        1    ", "A", "1")]
        [TestCase(typeof(PropertyIsEqualTo), "[A] =    12345678", "A", "12345678")]
        [TestCase(typeof(PropertyIsEqualTo), "[A] = 'STRING WITH SPACES'", "A", "STRING WITH SPACES")]
        [TestCase(typeof(PropertyIsEqualTo), "[A A A] = 'ŽLUŤOUČKÝ KŮŇ ÚPĚL ĎÁBELSKÉ ÓDY'", "A A A", "ŽLUŤOUČKÝ KŮŇ ÚPĚL ĎÁBELSKÉ ÓDY")]
        [TestCase(typeof(PropertyIsGreaterThan), "B > 2", "B", "2")]
        [TestCase(typeof(PropertyIsGreaterThanOrEqualTo), "C >= 3", "C", "3")]
        [TestCase(typeof(PropertyIsLessThan), "[d] < 4", "d", "4")]
        [TestCase(typeof(PropertyIsLessThanOrEqualTo), "EEEE <= 4.77910", "EEEE", "4.77910")]
        [TestCase(typeof(PropertyIsLike), "SomeText LIKE 'Some String%'", "SomeText", "Some String%")]
        [TestCase(typeof(PropertyIsNotLike), "[Some Different Text] NOT LIKE 'ŽLUŤOUČKÝ KŮŇ'", "Some Different Text", "ŽLUŤOUČKÝ KŮŇ")]
        [TestCase(typeof(PropertyIsNotEqualTo), "Asdf != 'text'", "Asdf", "text")]

        //todo: support for IN expression
        //[TestCase(typeof(PropertyIsIn), "SomeColumn IN (1, 2, 3, 4, 5)", "SomeColumn", "(1, 2, 3, 4, 5)")]
        public void TestComparisonExpressions(Type comparisonType, string conditionString, string column, string literal)
        {
            
            var builder = new SqlConditionBuilder(log);
            ISqlConditionExpression condition = builder.Build(conditionString);

            Assert.IsNotNull(condition);
            
            TestComparisonType(comparisonType, condition, column, literal);
        }

        [Test(Description = "Tests simple 'expr GATE expr' expressions")]
        [TestCase(typeof(And), "a > 1 AND b = 3")]
        [TestCase(typeof(Or), "a = 1 OR b = 3")]
        public void LR_Test(Type logicGate, string conditionString)
        {
            var builder = new SqlConditionBuilder(log);
            ISqlConditionExpression condition = builder.Build(conditionString);

            Assert.IsNotNull(condition);
            
            Assert.IsInstanceOf<LeftRightSideExpression>(condition);
            Assert.IsInstanceOf(logicGate, condition);
        }

        [Test]
        [TestCase(typeof(Not), "NOT a > 5")]
        public void R_Test(Type gate, string conditionString)
        {
            var builder = new SqlConditionBuilder(log);
            ISqlConditionExpression condition = builder.Build(conditionString);

            Assert.IsNotNull(condition);
            
            Assert.IsInstanceOf<RightSideExpression>(condition);
            Assert.IsInstanceOf(gate, condition);
        }

        private void TestComparisonType(Type comparisonType, ISqlConditionExpression comparisonExpression, string column, string literal)
        {
            Assert.IsInstanceOf<ComparisonExpression>(comparisonExpression);
            Assert.IsInstanceOf(comparisonType, comparisonExpression);

            var compExpr = comparisonExpression as ComparisonExpression;

            Assert.AreEqual(compExpr.Column, column);
            Assert.AreEqual(compExpr.Literal, literal);
        }

        /// <summary>
        /// Complex expression to test
        /// Indented by the correct expression tree considering
        /// operator precedence.
        /// </summary>
        private const string ComplexExpression = @"

    A = 1 

AND 
        ([B] > 3 OR [C] NOT LIKE '%string%' AND (D <= 15 AND [ŘEŘICHA A MEZERY] > 99.04)) 

        OR 
            column IS NULL
        OR 
            F LIKE 'smthing%'
AND 
    
        NOT (B < 74 AND NOT (CC >= 77.10 OR II = 1))";
    }
}
