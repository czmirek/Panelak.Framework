namespace Panelak.Drawing
{
    using Panelak.Geometry;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using Path = Geometry.Path;
    using Point = Geometry.Point;

    /// <summary>
    /// Drawing canvas for geometry instances
    /// </summary>
    public class GeometryCanvas
    {
        /// <summary>
        /// Size of the circle diameter representing a geometry <see cref="Point"/>
        /// </summary>
        private const float PointDiameter = 2;

        /// <summary>
        /// Radius of the circle representing a geometry <see cref="Point"/>
        /// </summary>
        private const float PointRadius = PointDiameter / 2;

        /// <summary>
        /// Drawing pen brush width
        /// </summary>
        private const float BrushWidth = 1;

        /// <summary>
        /// Canvas width
        /// </summary>
        private readonly int canvasWidth;

        /// <summary>
        /// Canvas height
        /// </summary>
        private readonly int canvasHeight;

        /// <summary>
        /// Canvas background color
        /// </summary>
        private readonly Color backgroundColor = Color.White;

        /// <summary>
        /// Brush color for geometry shapes
        /// </summary>
        private readonly Color brushColor = Color.Black;

        /// <summary>
        /// Drawing pen
        /// </summary>
        private readonly Pen drawingPen;

        /// <summary>
        /// List of geometric shapes in the canvas
        /// </summary>
        private List<Geometry> shapes = new List<Geometry>();

        /// <summary>
        /// <see cref="System.Drawing.Graphics"/> instance for drawing.
        /// </summary>
        private Graphics graphics = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeometryCanvas"/> class.
        /// </summary>
        /// <param name="width">Width of the canvas in pixels</param>
        /// <param name="height">Height of the canvas in pixels</param>
        public GeometryCanvas(int width, int height)
        {
            canvasWidth = width;
            canvasHeight = height;

            drawingPen = new Pen(brushColor, BrushWidth);
        }

        /// <summary>
        /// Adds a geometry shape to the canvas
        /// </summary>
        /// <param name="geometry">Geometry shape to be drawn</param>
        public void AddShape(Geometry geometry) => shapes.Add(geometry);

        /// <summary>
        /// Returns a PNG picture
        /// </summary>
        /// <returns>PNG picture in byte array</returns>
        public byte[] CreatePng() => CreateImage(ImageFormat.Png);

        /// <summary>
        /// Returns a JPEG picture
        /// </summary>
        /// <returns>JPEG picture in byte array</returns>
        public byte[] CreateJpg() => CreateImage(ImageFormat.Jpeg);
        
        /// <summary>
        /// Creates a picture from current shapes in given format
        /// </summary>
        /// <param name="imageFormat">Image format</param>
        /// <returns>Picture in byte array</returns>
        private byte[] CreateImage(ImageFormat imageFormat)
        {
            using (var bitmap = new Bitmap(canvasWidth, canvasHeight))
            using (graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(backgroundColor);

                foreach (Geometry geometry in shapes)
                {
                    if (geometry is Point point)
                        DrawPoint(point);

                    if (geometry is Line line)
                        DrawLine(line);

                    if (geometry is Path path)
                        DrawPath(path);

                    if (geometry is Polygon polygon)
                        DrawPolygon(polygon);
                }

                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }

        /// <summary>
        /// Returns a line perpendicular to the line. The start and end
        /// of this line is at the borders of the canvas.
        /// </summary>
        /// <param name="line">Input line</param>
        /// <returns>Perpendicular line to the input line</returns>
        private Line CreatePerpendicularLineInBox(Line line)
        {
            Point a = line.Start;
            Point b = line.End;

            var ab_mid = new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
            
            double ab_slope = (b.Y - a.Y) / (b.X - a.X);

            if (ab_slope == 0)
                return new StraightLine(new Point(ab_mid.X, canvasHeight), new Point(ab_mid.X, 0));

            double invertedSlope;

            if (Double.IsInfinity(ab_slope))
                invertedSlope = 0;
            else
                invertedSlope = -1 / ab_slope;

            double y_intercept = (-invertedSlope * ab_mid.X) + ab_mid.Y;

            //coordinates where line intersects with the left side of bounding box
            double left_y = y_intercept;
            double left_x = 0;
            var left = new Point(left_x, left_y);

            //coordinates where line intersects with the top side of bounding box
            double top_x = (canvasHeight / invertedSlope) - (y_intercept / invertedSlope);
            double top_y = canvasHeight;
            var top = new Point(top_x, top_y);

            //coordinates where line intersects with the right side of bounding box
            double right_y = (invertedSlope * canvasWidth) + y_intercept;
            double right_x = canvasWidth;
            var right = new Point(right_x, right_y);

            //coordinates where line intersects with the bottom side of bounding box
            double bottom_x = -y_intercept / invertedSlope;
            double bottom_y = 0;
            var bottom = new Point(bottom_x, bottom_y);
            
            Point startPoint = null, endPoint = null;

            if(left_y >= 0 && left_y <= canvasHeight)
            {
                startPoint = new Point(left_x, left_y);

                if (top_x >= 0 && top_x <= canvasWidth)
                    endPoint = top;
                else if (right_y >= 0 && right_y <= canvasHeight)
                    endPoint = right;
                else if (bottom_x >= 0 && bottom_x <= canvasWidth)
                    endPoint = bottom;
            }
            else if(top_x >= 0 && top_x <= canvasWidth)
            {
                startPoint = new Point(top_x, top_y);

                if (right_y >= 0 && right_y <= canvasHeight)
                    endPoint = right;
                else if (bottom_x >= 0 && bottom_x <= canvasWidth)
                    endPoint = bottom;
                else if (left_y >= 0 && left_y <= canvasHeight)
                    endPoint = left;
            }
            else if (right_y >= 0 && right_y <= canvasHeight)
            {
                startPoint = new Point(right_x, right_y);

                if (bottom_x >= 0 && bottom_x <= canvasWidth)
                    endPoint = bottom;
                else if (left_y >= 0 && left_y <= canvasHeight)
                    endPoint = left;
                else if (top_x >= 0 && top_x <= canvasWidth)
                    endPoint = top;
            }
            else if (bottom_x >= 0 && bottom_x <= canvasWidth)
            {
                startPoint = new Point(bottom_x, bottom_y);

                if (left_y >= 0 && left_y <= canvasHeight)
                    endPoint = left;
                else if (top_x >= 0 && top_x <= canvasWidth)
                    endPoint = top;
                else if (right_y >= 0 && right_y <= canvasHeight)
                    endPoint = right;
            }
            

            return new StraightLine(startPoint, endPoint);
        }

        /// <summary>
        /// Returns a point where two input lines intersect.
        /// </summary>
        /// <param name="p1">First line</param>
        /// <param name="p2">Second line</param>
        /// <returns>Intersection point</returns>
        private Point FindIntersection(Line p1, Line p2)
        {
            Point s1 = p1.Start;
            Point e1 = p1.End;

            Point s2 = p2.Start;
            Point e2 = p2.End;

            double a1 = e1.Y - s1.Y;
            double b1 = s1.X - e1.X;
            double c1 = (a1 * s1.X) + (b1 * s1.Y);

            double a2 = e2.Y - s2.Y;
            double b2 = s2.X - e2.X;
            double c2 = (a2 * s2.X) + (b2 * s2.Y);

            double delta = (a1 * b2) - (a2 * b1);

            double x = ((b2 * c1) - (b1 * c2)) / delta;
            double y = ((a1 * c2) - (a2 * c1)) / delta;

            return new Point(x, y);
        }

        /// <summary>
        /// Returns an angle of the point on circle <paramref name="onCircle"/>
        /// with the <paramref name="center"/>. The angle is measured in degrees 
        /// clockwise from the x-axis.
        /// </summary>
        /// <param name="center">Center of the circle</param>
        /// <param name="onCircle">Point on the circle</param>
        /// <returns>Angle measured in degrees clockwise from the x-axis.</returns>
        private double GetAngle(Point center, Point onCircle)
        {
            double radian = Math.Atan2(onCircle.Y - center.Y, onCircle.X - center.X);
            double angle = radian * (180 / Math.PI);

            if (angle < 0)
                angle += 360;

            angle = 360 - angle;

            return angle;
        }

        /// <summary>
        /// Draws a circular curve geometry shape.
        /// </summary>
        /// <param name="circularCurve">Circular curve geometry shape.</param>
        private void DrawCircularCurve(CircularCurve circularCurve)
        {
            if ((circularCurve.Start.X == circularCurve.Middle.X && circularCurve.Start.X == circularCurve.End.X)
             || (circularCurve.Start.Y == circularCurve.Middle.Y && circularCurve.Start.Y == circularCurve.End.Y))
            {
                DrawLine(new StraightLine(circularCurve.Start, circularCurve.End));
            }

            Point a = circularCurve.Start;
            Point b = circularCurve.Middle;
            Point c = circularCurve.End;

            Line p1 = CreatePerpendicularLineInBox(new StraightLine(a, b));
            Line p2 = CreatePerpendicularLineInBox(new StraightLine(b, c));

            Point intersection = FindIntersection(p1, p2);

            double radius = Math.Sqrt(Math.Pow(intersection.X - a.X, 2) + Math.Pow(intersection.Y - a.Y, 2));
            double a_angle = GetAngle(intersection, a);
            double b_angle = GetAngle(intersection, b);
            double c_angle = GetAngle(intersection, c);

            double startAngle = 0, sweepAngle = 0;

            if (a_angle < b_angle && b_angle < c_angle)
            {
                startAngle = a_angle;
                sweepAngle = c_angle - a_angle;
            }
            else if (a_angle < c_angle && c_angle < b_angle)
            {
                startAngle = 0;
                sweepAngle = 360;
            }
            else if (b_angle < a_angle && a_angle < c_angle)
            {
                startAngle = c_angle;
                sweepAngle = 360 - c_angle + a_angle;
            }
            else if (b_angle < c_angle && c_angle < a_angle)
            {
                startAngle = a_angle;
                sweepAngle = 360 - a_angle + c_angle;
            }
            else if (c_angle < a_angle && a_angle < b_angle)
            {
                startAngle = a_angle;
                sweepAngle = 360 - a_angle + c_angle;
            }
            else if (c_angle < b_angle && b_angle < c_angle)
            {
                startAngle = 0;
                sweepAngle = 360;
            }

            double box_x = intersection.X - radius;
            double box_y = intersection.Y + radius;
            double width = radius * 2;
            double height = width;

            graphics.DrawArc(
                pen: drawingPen,
                x: (float)box_x,
                y: canvasHeight - (float)box_y,
                width: (float)width,
                height: (float)height,
                startAngle: (float)startAngle,
                sweepAngle: (float)sweepAngle);
        }

        /// <summary>
        /// Draws a path geometry
        /// </summary>
        /// <param name="path">Path geometry</param>
        private void DrawPath(Path path)
        {
            foreach (Line line in path.Lines)
                DrawLine(line);
        }

        /// <summary>
        /// Draws a line geometry
        /// </summary>
        /// <param name="line">Line geometry</param>
        private void DrawLine(Line line)
        {
            if (line is CircularCurve circularCurve)
                DrawCircularCurve(circularCurve);
            else
                DrawStraightLine(line);
        }

        /// <summary>
        /// Draws a straight line geometry
        /// </summary>
        /// <param name="line">Straight line geometry</param>
        private void DrawStraightLine(Line line)
        {
            float x1 = (float)line.Start.X;
            float y1 = canvasHeight - (float)line.Start.Y;

            float x2 = (float)line.End.X;
            float y2 = canvasHeight - (float)line.End.Y;

            graphics.DrawLine(drawingPen, x1, y1, x2, y2);
        }

        /// <summary>
        /// Draws a point geometry
        /// </summary>
        /// <param name="p">Point geometry</param>
        private void DrawPoint(Point p)
        {
            float x = (float)p.X - PointRadius;
            float y = canvasHeight - (float)p.Y - PointRadius;

            graphics.DrawEllipse(drawingPen, x, y, PointDiameter, PointDiameter);
        }

        /// <summary>
        /// Draws a polygon geometry
        /// </summary>
        /// <param name="polygon">Polygon geometry</param>
        private void DrawPolygon(Polygon polygon)
        {
            foreach (Line line in polygon.Lines)
                DrawLine(line);
        }
    }
}
