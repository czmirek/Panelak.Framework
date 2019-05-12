namespace Panelak.Database.Oracle
{
    using Panelak.Geometry;
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Converter with methods to convert geometry models into Oracle PL/SQL SDO_GEOMETRY models
    /// </summary>
    public class GeometryToOracleSqlStringConverter
    {
        /// <summary>
        /// Number format with decimal dot separator.
        /// </summary>
        private readonly NumberFormatInfo numberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        /// <summary>
        /// Returns the SDO_GEOMETRY string of given <see cref="ISqlGeometry"/>.
        /// </summary>
        /// <param name="sqlGeometry">SQL geometry model</param>
        /// <returns>SDO_GEOMETRY SQL string</returns>
        public string GeometryToString(IGeometry geometry)
        {
            switch (geometry)
            {
                case Point point:
                    return PointToOracleSqlString(point);
                case Path path:
                    return PathToOracleSqlString(path);
                case Polygon polygon:
                    return PolygonToOracleSqlString(polygon);
                default:
                    throw new NotImplementedException($"Cannot convert {geometry.GetType().Name} to SQL string expression.");
            }
        }

        /// <summary>
        /// Returns the SDO_GEOMETRY string of <see cref="ISqlPoint"/>.
        /// </summary>
        /// <param name="sqlPoint">SQL point model</param>
        /// <returns>SDO_GEOMETRY SQL string for point</returns>
        public string PointToOracleSqlString(Point point)
            => $"SDO_GEOMETRY(2001, NULL, SDO_POINT_TYPE({PointString(point)}, NULL), NULL, NULL)";

        /// <summary>
        /// Returns the SDO_GEOMETRY string of <see cref="ISqlLineString"/>.
        /// </summary>
        /// <param name="sqlLineString">SQL line string model</param>
        /// <returns>SDO_GEOMETRY SQL string for line string</returns>
        public string PathToOracleSqlString(Path path)
        {
            string points = String.Join(", ", path.Lines.Select(l => $"{PointString(l.GetStartPoint())}, {PointString(l.GetEndPoint())}"));
            return $"SDO_GEOMETRY(2002, NULL, NULL, SDO_ELEM_INFO(1, 2, 1), SDO_ORDINATE_ARRAY({points}))";
        }

        /// <summary>
        /// Returns the SDO_GEOMETRY string of <see cref="ISqlPolygon"/>.
        /// </summary>
        /// <param name="sqlPolygon">SQL polygon model</param>
        /// <returns>SDO_GEOMETRY SQL string for polygon (1003, 1 = simple exterior)</returns>
        public string PolygonToOracleSqlString(Polygon polygon)
        {
            string points = String.Join(", ", polygon.Lines.Select(l => $"{PointString(l.GetStartPoint())}, {PointString(l.GetEndPoint())}"));
            return $"SDO_GEOMETRY(2003, NULL, NULL, SDO_ELEM_INFO(1, 1003, 1), SDO_ORDINATE_ARRAY({points}))";
        }

        /// <summary>
        /// Helper method returning comma separated digits of the point
        /// </summary>
        /// <param name="point">SQL point model</param>
        /// <returns>Comma separated points</returns>
        private string PointString(Point point) => $"{point.X.ToString(numberFormat)}, {point.Y.ToString(numberFormat)}";
    }
}
