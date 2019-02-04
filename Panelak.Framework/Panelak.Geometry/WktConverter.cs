namespace Panelak.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Converts WKT (Well-Known Text) to geometry models
    /// </summary>
    public class WktConverter
    {
        /// <summary>
        /// Converts the WKT string into a given Geometry.
        /// </summary>
        /// <param name="srid">Coordinate system identification</param>
        /// <param name="wktString">Well Known Text string</param>
        /// <returns>The <see cref="Geometry"/></returns>
        public Geometry FromWkt(int? srid, string wktString)
        {
            wktString = wktString.ToUpperInvariant().Trim();
            
            //todo bug
            if (wktString.StartsWith("POINT"))
            {
                string[] values = wktString
                         .Substring(wktString.IndexOf('(') + 1, wktString.Length - "POINT".Length - 2)
                         .Split(' ');

                double x = Double.Parse(values[0]);
                double y = Double.Parse(values[1]);

                return new Point(x, y);
            }

            if (wktString.StartsWith("LINESTRING"))
            {
                string values = wktString.Substring(wktString.IndexOf('(') + 1, wktString.Length - "LINESTRING".Length - 2);
                List<Line> lines = CreateLines(srid, values);

                return new Path(lines.AsReadOnly());
            }

            if (wktString.StartsWith("POLYGON"))
            {
                int openPar = wktString.IndexOf('(', wktString.IndexOf('(') + 1);
                int closePar = wktString.IndexOf(')');
                int length = closePar - openPar;

                string values = wktString.Substring(openPar + 1, length - 1);

                return new Path(CreateLines(srid, values));
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the string containing individual points to the list of line geometries.
        /// </summary>
        /// <param name="srid">Coordinate system ID</param>
        /// <param name="pointValues">WKT points</param>
        /// <returns>List of line geometries</returns>
        private List<Line> CreateLines(int? srid, string pointValues)
        {
            var lines = new List<Line>();
            string[] points = pointValues.Split(',');

            var numPoints = points.Select(s =>
            {
                string[] xyStr = s.Trim(' ').Split(' ');

                double x = Double.Parse(xyStr[0]);
                double y = Double.Parse(xyStr[1]);
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
