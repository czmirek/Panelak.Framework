namespace Panelak.Database.Oracle
{
    using Panelak.Sql;
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Extensions with methods to convert <see cref="ISqlGeometry"/> types to Oracle DB SDO_GEOMETRY types.
    /// </summary>
    public static class OracleSqlGeometryExtensions
    {
        /// <summary>
        /// Number format with decimal dot separator.
        /// </summary>
        private static readonly NumberFormatInfo numberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        /// <summary>
        /// Returns the SDO_GEOMETRY string of given <see cref="ISqlGeometry"/>.
        /// </summary>
        /// <param name="sqlGeometry">SQL geometry model</param>
        /// <returns>SDO_GEOMETRY SQL string</returns>
        public static string GeometryToString(this ISqlGeometry sqlGeometry)
        {
            switch (sqlGeometry) 
            {
                case ISqlPoint point:
                    return point.PointToOracleSqlString();
                case ISqlLineString lineString:
                    return lineString.LineStringToOracleSqlString();
                case ISqlPolygon polygon:
                    return polygon.PolygonToOracleSqlString();
                default:
                    throw new NotImplementedException($"Cannot convert {sqlGeometry.GetType().Name} to SQL string expression.");
            }
        }

        /// <summary>
        /// Returns the SDO_GEOMETRY string of <see cref="ISqlPoint"/>.
        /// </summary>
        /// <param name="sqlPoint">SQL point model</param>
        /// <returns>SDO_GEOMETRY SQL string for point</returns>
        public static string PointToOracleSqlString(this ISqlPoint sqlPoint) 
            => $"SDO_GEOMETRY(2001, NULL, SDO_POINT_TYPE({PointString(sqlPoint)}, NULL), NULL, NULL)";

        /// <summary>
        /// Returns the SDO_GEOMETRY string of <see cref="ISqlLineString"/>.
        /// </summary>
        /// <param name="sqlLineString">SQL line string model</param>
        /// <returns>SDO_GEOMETRY SQL string for line string</returns>
        public static string LineStringToOracleSqlString(this ISqlLineString sqlLineString)
        {
            string points = String.Join(", ", sqlLineString.Points.Select(p => PointString(p)));
            return $"SDO_GEOMETRY(2002, NULL, NULL, SDO_ELEM_INFO(1, 2, 1), SDO_ORDINATE_ARRAY({points}))";
        }

        /// <summary>
        /// Returns the SDO_GEOMETRY string of <see cref="ISqlPolygon"/>.
        /// </summary>
        /// <param name="sqlPolygon">SQL polygon model</param>
        /// <returns>SDO_GEOMETRY SQL string for polygon (1003, 1 = simple exterior)</returns>
        public static string PolygonToOracleSqlString(this ISqlPolygon sqlPolygon)
        {
            string firstAndLast = PointString(sqlPolygon.FirstAndLastPoint);
            string points = String.Join(", ", sqlPolygon.InBetweenPoints.Select(p => PointString(p)));
            points = String.Join(", ", new string[] 
            {
                firstAndLast,
                points,
                firstAndLast
            });
            return $"SDO_GEOMETRY(2003, NULL, NULL, SDO_ELEM_INFO(1, 1003, 1), SDO_ORDINATE_ARRAY({points}))";
        }

        /// <summary>
        /// Helper method returning comma separated digits of the point
        /// </summary>
        /// <param name="point">SQL point model</param>
        /// <returns>Comma separated points</returns>
        private static string PointString(ISqlPoint point) => $"{point.X.ToString(numberFormat)}, {point.Y.ToString(numberFormat)}";
        
    }
}
