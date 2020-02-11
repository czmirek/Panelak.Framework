namespace Panelak.Database.SqlServer
{
    using Panelak.Sql;
    using Panelak.Sql.Condition;
    using System;
    using System.Data.SqlClient;

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
        /// Converter from geometry models to Sql Server SQL
        /// </summary>
        private readonly GeometryToSqlServerStringConverter geometryToSqlServer = new GeometryToSqlServerStringConverter();

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
                    return $"{GetQuotedIdentifier(overlaps.Column)}.STOverlaps(geometry::STGeomFromText('{geometryToSqlServer.GeometryToSqlServerString(overlaps.Geometry)}',0))=1";
                case ISqlConditionIntersects intersects:
                    return $"{GetQuotedIdentifier(intersects.Column)}.STIntersects(geometry::STGeomFromText('{geometryToSqlServer.GeometryToSqlServerString(intersects.Geometry)}',0))=1";
                default:
                    throw new NotImplementedException($"{GetType().Name} cannot convert the spatial expression of type {spatialExpression.GetType().Name} to SQL string expression");
            }
        }
    }
}
