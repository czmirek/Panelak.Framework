namespace Panelak.Database.Oracle
{
    using global::Oracle.ManagedDataAccess.Client;
    using Panelak.Sql;
    using System;

    /// <summary>
    /// Oracle condition string builder
    /// </summary>
    public class OracleConditionStringBuilder : SqlConditionStringBuilder
    {
        /// <summary>
        /// Managed Oracle command builder
        /// </summary>
        private readonly OracleCommandBuilder builder = new OracleCommandBuilder();

        /// <summary>
        /// Converter from geometry models to Oracle PL/SQL
        /// </summary>
        private readonly GeometryToOracleSqlStringConverter geometryToOracle = new GeometryToOracleSqlStringConverter();

        /// <summary>
        /// Returns Oracle prefixed parameter name
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Oracle prefixed parameter name</returns>
        protected override string GetParamName(string name) => $":{name}";

        /// <summary>
        /// Returns Oracle quoted column identifier
        /// </summary>
        /// <param name="identifier">Column identifier</param>
        /// <returns>Oracle quoted column identifier</returns>
        protected override string GetQuotedIdentifier(string identifier) => builder.QuoteIdentifier(identifier);

        /// <summary>
        /// Returns Oracle SQL spatial expression
        /// </summary>
        /// <param name="spatialExpression">Spatial expression</param>
        /// <returns>Oracle SQL expression</returns>
        protected override string GetSpatialExpression(ISqlConditionSpatialExpression spatialExpression)
        {
            switch (spatialExpression)
            {
                case ISqlConditionOverlaps overlaps:
                    return $"SDO_OVERLAPS({GetQuotedIdentifier(overlaps.Column)}, {geometryToOracle.GeometryToString(overlaps.Geometry)}) = 'TRUE'";
                default:
                    throw new NotImplementedException($"{GetType().Name} cannot convert the spatial expression of type {spatialExpression.GetType().Name} to SQL string expression");
            }
        }
    }
}
