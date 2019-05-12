namespace Panelak.Database.Test
{
    using Panelak.Database.SqlServer;
    using Panelak.Sql;
    using Panelak.Sql.Parsing;
    using Microsoft.Extensions.Logging.Debug;
    using NUnit.Framework;

    public class ComplexSqlParsingTests
    {
        private readonly DebugLogger log;

        public ComplexSqlParsingTests() => log = new DebugLogger("logger");

        private const string Expression = @"  A = 1        AND ([B] > 3        OR  [C] NOT LIKE '%string%' AND (  D <= 15       AND [ŘEŘICHA A MEZERY] > 99.04   ))   OR   column IS NULL  OR   F  LIKE 'smthing%' AND  NOT (  B < 74       AND  NOT (  CC >= 77.10    OR   II = 1))";
        private const string Expected = "((([A] = @param_0 AND ([B] > @param_1 OR ([C] NOT LIKE @param_2   AND ([D] <= @param_3 AND [ŘEŘICHA A MEZERY] > @param_4)))) OR [column] IS NULL) OR ([F] LIKE @param_5   AND (NOT ([B] < @param_6 AND (NOT ([CC] >= @param_7 OR [II] = @param_8))))))";
        private const string ExpZeros = "((([A] = 0        AND ([B] > 0        OR ([C] NOT LIKE 0          AND ([D] <= 0        AND [ŘEŘICHA A MEZERY] > 0       )))) OR [column] IS NULL) OR ([F] LIKE 0          AND (NOT ([B] < 0        AND (NOT ([CC] >= 0        OR [II] = 0       ))))))";

        [Test]
        [TestCase(TestName = "Complex Sql > btree test")]
        public void Complex_Sql_Tree_Parsing_Test()
        {
            var binaryTreeBuilder = new SqlConditionBuilder(log);
            ISqlConditionExpression conditionBinaryTree = binaryTreeBuilder.Build(Expression);

            var sqlStringBuilder = new SqlServerConditionStringBuilder();
            IUncheckedSqlCondition uncheckedSqlCondition = sqlStringBuilder.Build(conditionBinaryTree);

            Assert.AreEqual(Expected, uncheckedSqlCondition.SqlConditionString);
        }

        [Test]
        [TestCase(TestName = "Sql > btree > sql test")]
        public void Reversed_Parse_Test()
        {
            var binaryTreeBuilder = new SqlConditionBuilder(log);
            ISqlConditionExpression conditionBinaryTree = binaryTreeBuilder.Build(ExpZeros);

            var sqlStringBuilder = new SqlServerConditionStringBuilder();
            IUncheckedSqlCondition uncheckedSqlCondition = sqlStringBuilder.Build(conditionBinaryTree);

            Assert.AreEqual(Expected, uncheckedSqlCondition.SqlConditionString);
        }
    }
}
