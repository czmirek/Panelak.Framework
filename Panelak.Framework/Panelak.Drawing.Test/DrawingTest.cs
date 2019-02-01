namespace Tests
{
    using Panelak.Drawing;
    using Panelak.Geometry;
    using NUnit.Framework;
    using System.IO;

    public class DrawingTest
    {
        [Test]
        public void DrawPoint()
        {
            var c = new GeometryCanvas(20, 20);
            c.AddShape(new Point(10, 10));
            byte[] bytes = c.CreatePng();

            File.WriteAllBytes("point_image.png", bytes);
        }

        [Test]
        public void DrawCircularCurve()
        {
            var c = new GeometryCanvas(150, 150);
            //c.AddShape(new Line(new Point(10, 10), new Point(140, 140)));
            c.AddShape(new CircularCurve(new Point(100, 140), new Point(140, 100), new Point(60, 100)));
            byte[] bytes = c.CreatePng();

            File.WriteAllBytes("point_image.png", bytes);
        }
    }
}
