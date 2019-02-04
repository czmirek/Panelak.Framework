namespace Panelak.Database.Test
{
    using Panelak.Database.OracleConsole;
    using Panelak.Geometry;
    using Microsoft.Extensions.Logging.Debug;
    using NUnit.Framework;

    /// <summary>
    /// Tests for converting SDO_GEOMETRY models to RDBMS-independent geometry models
    /// </summary>
    [TestFixture]
    public class SdoGeometryConverterTest
    {
        private readonly DebugLogger log;

        public SdoGeometryConverterTest() => log = new DebugLogger("logger");

        [Test]
        public void PointTest()
        {
            var converter = new SdoGeometryConverter();
            var sdo = new SdoGeometry()
            {
                SdoSRID = null,
                SdoGtype = 2001,
                SdoPoint = new SdoPoint() { X = 12, Y = 14, Z = null },
                ElemArray = null,
                OrdinatesArray = null
            };

            Geometry geometry = converter.GeometryFromSdo(sdo);
            Assert.IsInstanceOf<Point>(geometry);

            var ogeom = geometry as Point;
            TestPoint(12, 14, ogeom);
        }

        [Test]
        public void CompoundLineTest()
        {
            var converter = new SdoGeometryConverter();
            var sdo = new SdoGeometry()
            {
                SdoSRID = null,
                SdoGtype = 2002,
                SdoPoint = null,
                ElemArray = new decimal[] { 1, 4, 2, 1, 2, 1, 3, 2, 2 },
                OrdinatesArray = new decimal[] { 10, 10, 10, 14, 6, 10, 14, 10 }
            };

            Geometry geometry = converter.GeometryFromSdo(sdo);
            Assert.IsInstanceOf<Path>(geometry);

            var ogeom = geometry as Path;
            Assert.AreEqual(2, ogeom.Lines.Count);

            TestPoint(10, 10, ogeom.Lines[0].Start);
            TestPoint(10, 14, ogeom.Lines[0].End);

            Assert.IsInstanceOf<CircularCurve>(ogeom.Lines[1]);
            var ogeomCirc = ogeom.Lines[1] as CircularCurve;

            TestPoint(10, 14, ogeomCirc.Start);
            TestPoint(6, 10, ogeomCirc.Middle);
            TestPoint(14, 10, ogeomCirc.End);
        }

        [Test]
        public void CompountPolygonTest()
        {
            var converter = new SdoGeometryConverter();
            var sdo = new SdoGeometry()
            {
                SdoSRID = null,
                SdoGtype = 2003,
                SdoPoint = null,
                ElemArray = new decimal[] { 1, 1005, 2, 1, 2, 1, 5, 2, 2 },
                OrdinatesArray = new decimal[] { 6, 10, 10, 1, 14, 10, 10, 14, 6, 10 }
            };

            Geometry geometry = converter.GeometryFromSdo(sdo);

            Assert.IsInstanceOf<Path>(geometry);

            var ogeom = geometry as Path;
            Assert.AreEqual(3, ogeom.Lines.Count);

            TestPoint(6, 10, ogeom.Lines[0].Start);
            TestPoint(10, 1, ogeom.Lines[0].End);

            TestPoint(10, 1, ogeom.Lines[1].Start);
            TestPoint(14, 10, ogeom.Lines[1].End);

            Assert.IsInstanceOf<CircularCurve>(ogeom.Lines[2]);
            var ogeomCirc = ogeom.Lines[2] as CircularCurve;

            TestPoint(14, 10, ogeomCirc.Start);
            TestPoint(10, 14, ogeomCirc.Middle);
            TestPoint(6, 10, ogeomCirc.End);
        }

        private void TestPoint(double x, double y, Point point)
        {
            Assert.AreEqual(x, point.X);
            Assert.AreEqual(y, point.Y);
        }
    }
}
