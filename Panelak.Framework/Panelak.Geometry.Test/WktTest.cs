namespace Tests
{
    using Panelak.Geometry;
    using NUnit.Framework;

    public class WktTest
    {
        [Test]
        public void Point_Test()
        {
            var wktConverter = new WktConverter();
            Geometry geom = wktConverter.FromWkt(null, "POINT(1 2)");
            Assert.IsInstanceOf<Point>(geom);

            var p = geom as Point;

            Assert.IsNull(p.Srid);
            AssertPoint(1, 2, p);
        }

        [Test]
        public void LinestringTest()
        {
            var wktConverter = new WktConverter();
            Geometry geom = wktConverter.FromWkt(null, "LINESTRING (1 1, 3 3, 2 4, 2 0)");
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
            Geometry geom = wktConverter.FromWkt(null, "POLYGON((1 1, 3 3, 3 1, 1 1))");
            Assert.IsInstanceOf<Path>(geom);

            var p = geom as Path;
            Assert.IsNull(p.Srid);
            Assert.AreEqual(3, p.Lines.Count);
            AssertLine(1, 1, 3, 3, p.Lines[0]);
            AssertLine(3, 3, 3, 1, p.Lines[1]);
            AssertLine(3, 1, 1, 1, p.Lines[2]);
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
