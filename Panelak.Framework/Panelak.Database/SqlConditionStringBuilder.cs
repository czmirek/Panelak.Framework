namespace Panelak.Database
{
    using Panelak.Sql;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="SqlConditionStringBuilder" />
    /// </summary>
    public abstract class SqlConditionStringBuilder
    {
        /// <summary>
        /// Defines the identifiers
        /// </summary>
        private HashSet<string> identifiers;

        /// <summary>
        /// Defines the parameters
        /// </summary>
        private List<DbParameter> parameters;

        /// <summary>
        /// Internal counter of query parameters used for suffixing parameters
        /// </summary>
        private int paramCounter;

        /// <summary>
        /// Builds an unchecked SQL condition model out of binary tree expression
        /// </summary>
        /// <param name="expression">Condition expression binary tree</param>
        /// <returns>Unchecked SQL condition model</returns>
        public IUncheckedSqlCondition Build(ISqlConditionExpression expression)
        {
            identifiers = new HashSet<string>();
            parameters = new List<DbParameter>();
            paramCounter = 0;
            string sqlString = BuildString(expression);

            return new UncheckedSqlCondition(sqlString, identifiers, parameters);
        }

        /// <summary>
        /// Obtains a name of the parameter with a RDBMS specified prefix
        /// </summary>
        /// <param name="name">Parameter string</param>
        /// <returns>Prefixed parameter string</returns>
        protected abstract string GetParamName(string name);

        /// <summary>
        /// Obtains a name of the identifier quoted in RDBMS specific quotes
        /// </summary>
        /// <param name="identifier">Identifier string</param>
        /// <returns>Quoted identifier string</returns>
        protected abstract string GetQuotedIdentifier(string identifier);

        /// <summary>
        /// Creates a RDBMS specific spatial expression.
        /// </summary>
        /// <param name="spatialExpression">Soatuak exoressuib</param>
        /// <returns>RDBMS specific SQL expression</returns>
        protected abstract string GetSpatialExpression(ISqlConditionSpatialExpression spatialExpression);

        /// <summary>
        /// Build the condition SQL string out of the binary tree expression
        /// </summary>
        /// <param name="expression">Binary tree expression condition</param>
        /// <returns>Constructed SQL string</returns>
        private string BuildString(ISqlConditionExpression expression)
        {
            var sqlCondition = new StringBuilder();

            switch (expression)
            {
                case ISqlConditionIn comp:
                    {
                        identifiers.Add(comp.Column);

                        string identifier = GetQuotedIdentifier(comp.Column);
                        string[] literals = comp.Literal.Split(',');

                        var strParams = new List<string>();

                        for (int i = 0; i < literals.Length; i++)
                        {
                            string parameterName = GetParamName($"param_{paramCounter}_{i}");
                            strParams.Add(parameterName);

                            parameters.Add(new DbParameter(parameterName, literals[i], true));
                        }

                        string joinedParams = String.Join(",", strParams);
                        sqlCondition.Append($"{identifier} {comp.Operator} ({joinedParams})");

                        break;
                    }

                case ISqlConditionRawExpression comp:
                    {
                        sqlCondition.Append($"{comp.RawSqlString}");
                        break;
                    }

                case ISqlConditionColumnComparisonExpression comp:
                    {
                        string identifier = GetQuotedIdentifier(comp.Column);
                        string paramName = GetParamName($"param_{paramCounter++}");

                        parameters.Add(new DbParameter(paramName, comp.Literal, true));
                        identifiers.Add(comp.Column);

                        sqlCondition.Append($"{identifier} {comp.Operator} {paramName}");

                        break;
                    }

                case ISqlConditionIsNull isNull:
                    {
                        string identifier = GetQuotedIdentifier(isNull.Column);
                        identifiers.Add(identifier);
                        sqlCondition.Append($"{identifier} IS NULL");
                    }

                    break;

                case ISqlConditionIsNotNull isNotNull:
                    {
                        string identifier = GetQuotedIdentifier(isNotNull.Column);
                        identifiers.Add(identifier);
                        sqlCondition.Append($"{identifier} IS NOT NULL");
                    }

                    break;

                case ISqlConditionRightOperator rightOp:
                    {
                        string subExpr = BuildString(rightOp.RightExpression);
                        sqlCondition.Append($"({rightOp.Operator} {subExpr})");

                        break;
                    }

                case ISqlConditionLeftRightOperator lrOp:
                    {
                        string leftExpr = BuildString(lrOp.LeftExpression);
                        string rightExpr = BuildString(lrOp.RightExpression);
                        sqlCondition.Append($"({leftExpr} {lrOp.Operator} {rightExpr})");
                        break;
                    }

                case ISqlConditionSpatialExpression spatialExpression:
                    {
                        string sqlStr = GetSpatialExpression(spatialExpression);
                        sqlCondition.Append(sqlStr);
                        break;
                    }

                default:
                    throw new NotImplementedException("Unable to parse condition expression");
            }

            return sqlCondition.ToString();
        }
    }
}
