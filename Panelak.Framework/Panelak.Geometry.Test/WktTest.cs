namespace Tests
{
    using Panelak.Geometry;
    using System.Linq;
    using NUnit.Framework;
    using System.Collections.Generic;

    public class WktTest
    {
        [Test]
        public void Point_Test()
        {
            var wktConverter = new WktConverter();
            Geometry geom = wktConverter.FromWkt(null, "POINT(1 2)").FirstOrDefault();
            Geometry geom2 = wktConverter.FromWkt(null, "POINT(-7330.778696 4662.77960)").FirstOrDefault();
            Assert.IsInstanceOf<Point>(geom);
            Assert.IsInstanceOf<Point>(geom2);

            var p = geom as Point;
            var p2= geom2 as Point;

            Assert.IsNull(p.Srid);
            AssertPoint(1, 2, p);

            AssertPoint(-7330.778696, 4662.77960, p2);
        }

        [Test]
        public void LinestringTest()
        {
            var wktConverter = new WktConverter();
            Geometry geom = wktConverter.FromWkt(null, "LINESTRING (1 1, 3 3, 2 4, 2 0)").FirstOrDefault();
            Assert.IsInstanceOf<Path>(geom);

            var p = geom as Path;

            Assert.IsNull(p.Srid);
            Assert.AreEqual(3, p.Lines.Count);
            AssertLine(1, 1, 3, 3, p.Lines[0]);
            AssertLine(3, 3, 2, 4, p.Lines[1]);
            AssertLine(2, 4, 2, 0, p.Lines[2]);
        }

        [Test]
        public void PolygonTest()
        {
            var wktConverter = new WktConverter();
            Geometry geom = wktConverter.FromWkt(null, "POLYGON((1 1, 3 3, 3 1, 1 1))").FirstOrDefault();
            Assert.IsInstanceOf<Polygon>(geom);

            var p = geom as Polygon;
            Assert.IsNull(p.Srid);
            Assert.AreEqual(3, p.Lines.Count);
            AssertLine(1, 1, 3, 3, p.Lines[0]);
            AssertLine(3, 3, 3, 1, p.Lines[1]);
            AssertLine(3, 1, 1, 1, p.Lines[2]);
        }


        [Test]
        public void MultipolygonTest()
        {
            var wktConverter = new WktConverter();
            var geometries = wktConverter.FromWkt(null, "MULTIPOLYGON(((0 -0.7856336, 0 3, 3 3, 3 0, 0 -0.7856336), (1 1, 1 2, 2 1, 1 1)), ((9 9, 9 10, 10 9, 9 9)))")
                                         .ToList();

            Assert.IsInstanceOf<Polygon>(geometries[0]);
            Assert.IsInstanceOf<Polygon>(geometries[1]);
            Assert.IsInstanceOf<Polygon>(geometries[2]);

            var p1 = geometries[0] as Polygon;
            var p2 = geometries[1] as Polygon;
            var p3 = geometries[2] as Polygon;

            Assert.AreEqual(4, p1.Lines.Count);
            Assert.AreEqual(3, p2.Lines.Count);
            Assert.AreEqual(3, p3.Lines.Count);
            AssertLine(0, -0.7856336, 0, 3, p1.Lines[0]);
            AssertLine(0, 3, 3, 3, p1.Lines[1]);
            AssertLine(3, 3, 3, 0, p1.Lines[2]);
            AssertLine(3, 0, 0, -0.7856336, p1.Lines[3]);

            AssertLine(1, 1, 1, 2, p2.Lines[0]);
            AssertLine(1, 2, 2, 1, p2.Lines[1]);
            AssertLine(2, 1, 1, 1, p2.Lines[2]);

            AssertLine(9, 9, 9, 10, p3.Lines[0]);
            AssertLine(9, 10, 10, 9, p3.Lines[1]);
            AssertLine(10, 9, 9, 9, p3.Lines[2]);
        }

        public void AssertPoint(double x, double y, Point p)
        {
            Assert.AreEqual(x, p.X);
            Assert.AreEqual(y, p.Y);
        }

        public void AssertLine(double sx, double sy, double ex, double ey, Line l)
        {
            AssertPoint(sx, sy, l.Start);
            AssertPoint(ex, ey, l.End);
        }
    }
}
