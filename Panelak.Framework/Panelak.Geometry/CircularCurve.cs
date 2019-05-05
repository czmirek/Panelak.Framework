namespace Panelak.Geometry
{
    /// <summary>
    /// Represents a circular arc defined by three points.
    /// </summary>
    public struct CircularCurve : ILine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircularCurve"/> class.
        /// </summary>
        /// <param name="start">Starting point</param>
        /// <param name="end">Ending point</param>
        /// <param name="middle">Middle arc point</param>
        public CircularCurve(Point start, Point middle, Point end)
        {
            Start = start;
            Middle = middle;
            End = end;
            Srid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularCurve"/> class.
        /// </summary>
        /// <param name="start">Starting point</param>
        /// <param name="end">Ending point</param>
        /// <param name="middle">Middle arc point</param>
        /// <param name="srid">Coordinate system ID</param>
        public CircularCurve(Point start, Point middle, Point end, int? srid)
        {
            Start = start;
            Middle = middle;
            End = end;
            Srid = srid;
        }

        /// <summary>
        /// Gets the middle arc point
        /// </summary>
        public Point Middle { get; }

        /// <summary>
        /// Gets the starting arc point
        /// </summary>
        public Point Start { get; }

        /// <summary>
        /// Gets the ending arc point
        /// </summary>
        public Point End { get; }

        /// <summary>
        /// Gets the coordinate system ID
        /// </summary>
        public int? Srid { get; }

        /// <summary>
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString() => $"{Start.ToString()} {Middle.ToString()} {End.ToString()}";
    }
}
