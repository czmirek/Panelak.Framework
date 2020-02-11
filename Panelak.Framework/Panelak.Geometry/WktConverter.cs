namespace Panelak.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Converts WKT (Well-Known Text) to geometry models.
    /// Supports only 2D models, other dimensions are ignored.
    /// </summary>
    public class WktConverter
    {
        /// <summary>
        /// Default number format
        /// </summary>
        private readonly NumberFormatInfo numberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };

        /// <summary>
        /// Converts the WKT string into a single geometry model or returns the first geometry model
        /// in the list of geometries defined in WKT.
        /// </summary>
        /// <param name="csid">Coordinate system identification</param>
        /// <param name="wktString">Well Known Text string</param>
        /// <returns>Single or first geometry</returns>
        public IGeometry FromWkt(int? csid, string wktString) => FromWktToGeomCollection(csid, wktString).FirstOrDefault();

        /// <summary>
        /// Converts the WKT string into a given Geometry.
        /// </summary>
        /// <param name="csid">Coordinate system identification</param>
        /// <param name="wktString">Well Known Text string</param>
        /// <returns>The <see cref="Geometry"/></returns>
        public IEnumerable<IGeometry> FromWktToGeomCollection(int? csid, string wktString)
        {
            var pointMatches = new Regex(@"\((?<points>[\d\s\-\,\.]+)\)");
            MatchCollection matches = pointMatches.Matches(wktString);

            wktString = wktString.ToUpperInvariant().Trim();

            if (wktString.StartsWith("POINT"))
            {
                string regexValue = matches[0].Groups["points"].Value;
                string[] values = regexValue.Split(' ');

                double x = Double.Parse(values[0], numberFormat);
                double y = Double.Parse(values[1], numberFormat);

                return new IGeometry[] { new Point(x, y) };
            }

            if (wktString.StartsWith("LINESTRING"))
            {
                string values = matches[0].Groups["points"].Value;
                List<ILine> lines = CreateLines(csid, values);

                return new IGeometry[] { new Path(lines.AsReadOnly()) };
            }

            if (wktString.StartsWith("POLYGON") || wktString.StartsWith("MULTIPOLYGON"))
            {
                var polygons = new List<Polygon>();

                foreach (Match match in matches)
                {
                    string points = match.Groups["points"].Value;
                    List<ILine> lines = CreateLines(csid, points);

                    var poly = new Polygon(lines.AsReadOnly());
                    polygons.Add(poly);
                }

                return polygons.AsReadOnly().Select(p => p as IGeometry);
            }

            throw new InvalidOperationException($"Unable to convert WKT string to a geometry model: {wktString.Substring(0, Math.Max(20, wktString.Length))}");
        }

        /// <summary>
        /// Converts the string containing individual points to the list of line geometries.
        /// </summary>
        /// <param name="csid">Coordinate system ID</param>
        /// <param name="pointValues">WKT points</param>
        /// <returns>List of line geometries</returns>
        private List<ILine> CreateLines(int? csid, string pointValues)
        {
            var lines = new List<ILine>();
            string[] points = pointValues.Split(',');

            var numPoints = points.Select(s =>
            {
                string[] xyStr = s.Trim(' ').Split(' ');

                double x = Double.Parse(xyStr[0], numberFormat);
                double y = Double.Parse(xyStr[1], numberFormat);
                return (x, y);
            }).ToList();

            for (int i = 0; i < numPoints.Count - 1; i++)
            {
                double sx = numPoints[i].x;
                double sy = numPoints[i].y;

                double ex = numPoints[i + 1].x;
                double ey = numPoints[i + 1].y;

                var line = new StraightLine(new Point(sx, sy), new Point(ex, ey));
                lines.Add(line);
            }

            return lines;
        }
    }
}
