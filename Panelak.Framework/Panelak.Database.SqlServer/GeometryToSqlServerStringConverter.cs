namespace Panelak.Database.SqlServer
{
    using Panelak.Geometry;
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Converter with methods to convert geometry models into Sql Server WKTs
    /// </summary>
    public class GeometryToSqlServerStringConverter
    {
        /// <summary>
        /// Number format with decimal dot separator.
        /// </summary>
        private readonly NumberFormatInfo numberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        /// <summary>
        /// Returns the SQL Server spatial string of given <see cref="IGeometry"/>.
        /// </summary>
        /// <param name="sqlGeometry">SQL geometry model</param>
        /// <returns>SQL Server string</returns>
        public string GeometryToSqlServerString(IGeometry sqlGeometry)
        {
            switch (sqlGeometry)
            {
                case Point point:
                    return PointToSqlServerString(point);
                case Path path:
                    return LineStringToSqlServerString(path);
                case Polygon polygon:
                    return PolygonToSqlServerString(polygon);
                default:
                    throw new NotImplementedException($"Cannot convert {sqlGeometry.GetType().Name} to SQL string expression.");
            }
        }

        /// <summary>
        /// Returns the Sql Server POINT of <see cref="Point"/>.
        /// </summary>
        /// <param name="sqlPoint">SQL point model</param>
        /// <returns>Sql Server POINT string for point</returns>
        public string PointToSqlServerString(Point sqlPoint) 
            => $"POINT({sqlPoint.X.ToString(numberFormat)} {sqlPoint.Y.ToString(numberFormat)})";

        /// <summary>
        /// Returns the Sql Server LINESTRING string of <see cref="Path"/>.
        /// </summary>
        /// <param name="path">SQL line string model</param>
        /// <returns>Sql Server LINESTRING</returns>
        public string LineStringToSqlServerString(Path path)
        {
            string points = String.Join(" ", path.Lines.Select(l => $"{PointString(l.GetStartPoint())} {PointString(l.GetEndPoint())}"));
            return $"LINESTRING({points})";
        }

        /// <summary>
        /// Returns the Sql Server POLYGON string of <see cref="Path"/>.
        /// </summary>
        /// <param name="polygon">SQL polygon model</param>
        /// <returns>Sql Server POLYGON</returns>
        public string PolygonToSqlServerString(Polygon polygon)
        {
            string points = String.Join(" ", polygon.Lines.Select(l => $"{PointString(l.GetStartPoint())} {PointString(l.GetEndPoint())}"));
            return $"POLYGON(({points}))";
        }

        /// <summary>
        /// Helper method returning space separated digits of the point
        /// </summary>
        /// <param name="point">SQL point model</param>
        /// <returns>Space separated points</returns>
        private string PointString(Point point) => $"{point.X.ToString(numberFormat)} {point.Y.ToString(numberFormat)}";
    }
}
