namespace Panelak.Geometry
{
    /// <summary>
    /// Represents a circular arc defined by three points.
    /// </summary>
    public class CircularCurve : Line
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircularCurve"/> class.
        /// </summary>
        /// <param name="start">Starting point</param>
        /// <param name="end">Ending point</param>
        /// <param name="arcPoint">Middle arc point</param>
        public CircularCurve(Point start, Point end, Point arcPoint)
            : base(start, end) => ArcPoint = arcPoint;

        /// <summary>
        /// Gets the middle arc point
        /// </summary>
        public Point ArcPoint { get; }
    }
}
