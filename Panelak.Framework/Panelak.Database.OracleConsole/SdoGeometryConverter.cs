namespace Panelak.Database.OracleConsole
{
    using Panelak.Geometry;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements conversion from SDO_GEOMETRY model into geometry models for drawing
    /// Only 2D geometries are supported
    /// </summary>
    public class SdoGeometryConverter
    {
        /// <summary>
        /// Converts SDO_GEOMETRY model into a general geometry model for drawing.
        /// </summary>
        /// <param name="sdoGeometry">Oracle SDO_GEOMETRY model</param>
        /// <returns>RDBMS independent geometry model</returns>
        public Geometry GeometryFromSdo(SdoGeometry sdoGeometry)
        {
            int? srid = (int?)sdoGeometry.SdoSRID;
            int gtype = (int)sdoGeometry.SdoGtype;

            // point
            if (gtype == 2001)
            {
                return new Point((double)sdoGeometry.SdoPoint.X, (double)sdoGeometry.SdoPoint.Y);
            }

            // line, curve, polygon
            if (gtype == 2002 || gtype == 2003)
            {
                var path = new List<Line>();

                for (int i = 0; i < sdoGeometry.ElemArray.Length; i = i + 3)
                {
                    int offset = (int)sdoGeometry.ElemArray[i];
                    int etype = (int)sdoGeometry.ElemArray[i + 1];
                    int interpretation = (int)sdoGeometry.ElemArray[i + 2];

                    // ignore compound type definitions, not needed for drawing
                    if (etype == 4 || etype == 1005 || etype == 2005 || etype == 1006 || etype == 2006)
                        continue;

                    int lastOffset = sdoGeometry.OrdinatesArray.Length - 1;

                    if (sdoGeometry.ElemArray.Length > i + 3)
                        lastOffset = (int)sdoGeometry.ElemArray[i + 3];

                    for (int r = offset - 1; r < lastOffset - 1; r++)
                    {
                        if (etype == 2 && interpretation == 1)
                        {
                            // straight line string
                            double startX = (double)sdoGeometry.OrdinatesArray[r];
                            double startY = (double)sdoGeometry.OrdinatesArray[r + 1];
                            double endX = (double)sdoGeometry.OrdinatesArray[r + 2];
                            double endY = (double)sdoGeometry.OrdinatesArray[r + 3];

                            var start = new Point(startX, startY);
                            var end = new Point(endX, endY);

                            var line = new Line(start, end);

                            r += 1;

                            path.Add(line);
                        }
                        else if (etype == 2 && interpretation == 2) 
                        {
                            // circular arc string
                            double startX = (double)sdoGeometry.OrdinatesArray[r];
                            double startY = (double)sdoGeometry.OrdinatesArray[r + 1];
                            double middleX = (double)sdoGeometry.OrdinatesArray[r + 2];
                            double middleY = (double)sdoGeometry.OrdinatesArray[r + 3];
                            double endX = (double)sdoGeometry.OrdinatesArray[r + 4];
                            double endY = (double)sdoGeometry.OrdinatesArray[r + 5];

                            var start = new Point(startX, startY);
                            var middle = new Point(middleX, middleY);
                            var end = new Point(endX, endY);

                            var curve = new CircularCurve(start, end, middle);

                            path.Add(curve);

                            r += 3;
                        }
                    }
                }

                return new Path(path.AsReadOnly());
            }

            throw new NotImplementedException($"Oracle Geometry not yet supported ({sdoGeometry.SdoGtype})");
        }
    }
}
