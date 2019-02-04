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
        /// <param name="middle">Middle arc point</param>
        public CircularCurve(Point start, Point middle, Point end)
            : base(start, end) => Middle = middle;

        /// <summary>
        /// Gets the middle arc point
        /// </summary>
        public Point Middle { get; }

        /// <summary>
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString() => $"{Start.ToString()} {Middle.ToString()} {End.ToString()}";
    }
}
