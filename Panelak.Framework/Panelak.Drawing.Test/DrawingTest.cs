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
            c.AddShape(new CircularCurve(new Point(30, 30), new Point(40, 10), new Point(50, 30)));
            byte[] bytes = c.CreatePng();

            File.WriteAllBytes("point_image.png", bytes);
        }

        [Test]
        public void SomeTest()
        {
            var c = new GeometryCanvas(100, 100);
            //c.AddShape(new StraightLine(new Point(1375.5, 250), new Point(1475.5, 350)));
            //c.AddShape(new StraightLine(new Point(1375.5, 250), new Point(1475.5, 350)));
            c.AddShape(new CircularCurve(new Point(30, 30), new Point(95, 30), new Point(30, 50)));

            c.CreatePng();
            byte[] bytes = c.CreatePng();

            File.WriteAllBytes("point_image.png", bytes);
        }
    }
}
