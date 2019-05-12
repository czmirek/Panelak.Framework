namespace Panelak.Database.SqlServer
{
    using System;
    using System.Data.SqlClient;
    using Panelak.Sql;

    /// <summary>
    /// SQL Server condition string builder
    /// </summary>
    public class SqlServerConditionStringBuilder : SqlConditionStringBuilder
    {
        /// <summary>
        /// System SQL command builder
        /// </summary>
        private readonly SqlCommandBuilder builder = new SqlCommandBuilder();

        /// <summary>
        /// Returns SQL Server prefixed parameter name
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>SQL Server prefixed parameter name</returns>
        protected override string GetParamName(string name) => $"@{name}";

        /// <summary>
        /// Returns SQL Server quoted column identifier
        /// </summary>
        /// <param name="identifier">Column identifier</param>
        /// <returns>SQL Server quoted column identifier</returns>
        protected override string GetQuotedIdentifier(string identifier) => builder.QuoteIdentifier(identifier);

        /// <summary>
        /// Returns SQL Server spatial expression
        /// </summary>
        /// <param name="spatialExpression">Spatial expression</param>
        /// <returns>SQL Server expression</returns>
        protected override string GetSpatialExpression(ISqlConditionSpatialExpression spatialExpression)
        {
            switch (spatialExpression)
            {
                case ISqlConditionOverlaps overlaps:
                    return $"{GetQuotedIdentifier(overlaps.Column)}.STOverlaps(geometry::STGeomFromText('{overlaps.Geometry.GeometryToSqlServerString()}'))";

                default:
                    throw new NotImplementedException($"{GetType().Name} cannot convert the spatial expression of type {spatialExpression.GetType().Name} to SQL string expression");
            }
        }
    }
}
