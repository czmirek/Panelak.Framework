namespace Tests
{
    using NUnit.Framework;
    using Panelak.Geometry;
    using System.Linq;

    public class WktTest
    {
        [Test]
        public void Point_Test()
        {
            var wktConverter = new WktConverter();
            IGeometry geom = wktConverter.FromWkt(null, "POINT(1 2)");
            IGeometry geom2 = wktConverter.FromWkt(null, "POINT(-7330.778696 4662.77960)");
            Assert.IsInstanceOf<Point>(geom);
            Assert.IsInstanceOf<Point>(geom2);

            var p = (Point)geom;
            var p2= (Point)geom2;

            Assert.IsNull(p.Csid);
            AssertPoint(1, 2, p);

            AssertPoint(-7330.778696, 4662.77960, p2);
        }

        [Test]
        public void LinestringTest()
        {
            var wktConverter = new WktConverter();
            IGeometry geom = wktConverter.FromWkt(null, "LINESTRING (1 1, 3 3, 2 4, 2 0)");
            Assert.IsInstanceOf<Path>(geom);

            var p = (Path)geom;

            Assert.IsNull(p.Csid);
            Assert.AreEqual(3, p.Lines.Count);
            AssertLine(1, 1, 3, 3, p.Lines[0]);
            AssertLine(3, 3, 2, 4, p.Lines[1]);
            AssertLine(2, 4, 2, 0, p.Lines[2]);
        }

        [Test]
        public void PolygonTest()
        {
            var wktConverter = new WktConverter();
            IGeometry geom = wktConverter.FromWkt(null, "POLYGON((1 1, 3 3, 3 1, 1 1))");
            Assert.IsInstanceOf<Polygon>(geom);

            var p = (Polygon)geom;
            Assert.IsNull(p.Csid);
            Assert.AreEqual(3, p.Lines.Count);
            AssertLine(1, 1, 3, 3, p.Lines[0]);
            AssertLine(3, 3, 3, 1, p.Lines[1]);
            AssertLine(3, 1, 1, 1, p.Lines[2]);
        }


        [Test]
        public void MultipolygonTest()
        {
            var wktConverter = new WktConverter();
            var geometries = wktConverter.FromWktToGeomCollection(null, "MULTIPOLYGON(((0 -0.7856336, 0 3, 3 3, 3 0, 0 -0.7856336), (1 1, 1 2, 2 1, 1 1)), ((9 9, 9 10, 10 9, 9 9)))")
                                         .ToList();

            Assert.IsInstanceOf<Polygon>(geometries[0]);
            Assert.IsInstanceOf<Polygon>(geometries[1]);
            Assert.IsInstanceOf<Polygon>(geometries[2]);

            var p1 = (Polygon)geometries[0];
            var p2 = (Polygon)geometries[1];
            var p3 = (Polygon)geometries[2];

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

        public void AssertLine(double sx, double sy, double ex, double ey, ILine l)
        {
            AssertPoint(sx, sy, l.GetStartPoint());
            AssertPoint(ex, ey, l.GetEndPoint());
        }
    }
}
