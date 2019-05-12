namespace Panelak.Database.SqlServer
{
    using Panelak.Sql;
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Extensions with methods to convert <see cref="ISqlGeometry"/> types to SQL Server spatial types.
    /// </summary>
    public static class SqlServerGeometryExtensions
    {
        /// <summary>
        /// Number format with decimal dot separator.
        /// </summary>
        private static readonly NumberFormatInfo numberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        /// <summary>
        /// Returns the SQL Server spatial string of given <see cref="ISqlGeometry"/>.
        /// </summary>
        /// <param name="sqlGeometry">SQL geometry model</param>
        /// <returns>SQL Server string</returns>
        public static string GeometryToSqlServerString(this ISqlGeometry sqlGeometry)
        {
            switch (sqlGeometry) 
            {
                case ISqlPoint point:
                    return point.PointToSqlServerString();
                case ISqlLineString lineString:
                    return lineString.LineStringToSqlServerString();
                case ISqlPolygon polygon:
                    return polygon.PolygonToSqlServerString();
                default:
                    throw new NotImplementedException($"Cannot convert {sqlGeometry.GetType().Name} to SQL string expression.");
            }
        }

        /// <summary>
        /// Returns the Sql Server POINT of <see cref="ISqlPoint"/>.
        /// </summary>
        /// <param name="sqlPoint">SQL point model</param>
        /// <returns>Sql Server POINT string for point</returns>
        public static string PointToSqlServerString(this ISqlPoint sqlPoint) 
            => $"POINT({sqlPoint.X.ToString(numberFormat)} {sqlPoint.Y.ToString(numberFormat)})";

        /// <summary>
        /// Returns the Sql Server LINESTRING string of <see cref="ISqlLineString"/>.
        /// </summary>
        /// <param name="sqlLineString">SQL line string model</param>
        /// <returns>Sql Server LINESTRING</returns>
        public static string LineStringToSqlServerString(this ISqlLineString sqlLineString)
        {
            string points = String.Join(" ", sqlLineString.Points.Select(p => p.PointToSqlServerString()));
            return $"LINESTRING({points})";
        }

        /// <summary>
        /// Returns the Sql Server POLYGON string of <see cref="ISqlLineString"/>.
        /// </summary>
        /// <param name="sqlPolygon">SQL polygon model</param>
        /// <returns>Sql Server POLYGON</returns>
        public static string PolygonToSqlServerString(this ISqlPolygon sqlPolygon)
        {
            string firstAndLast = PointString(sqlPolygon.FirstAndLastPoint);
            string points = String.Join(", ", sqlPolygon.InBetweenPoints.Select(p => PointString(p)));
            points = String.Join(", ", new string[] 
            {
                firstAndLast,
                points,
                firstAndLast
            });
            return $"POLYGON(({points}))";
        }

        /// <summary>
        /// Helper method returning space separated digits of the point
        /// </summary>
        /// <param name="point">SQL point model</param>
        /// <returns>Space separated points</returns>
        private static string PointString(ISqlPoint sqlPoint) => $"{sqlPoint.X.ToString(numberFormat)} {sqlPoint.Y.ToString(numberFormat)}";
    }
}
