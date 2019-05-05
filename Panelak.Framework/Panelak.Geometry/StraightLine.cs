namespace Panelak.Geometry
{
    /// <summary>
    /// Represents a straight line between two points
    /// </summary>
    public struct StraightLine : ILine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StraightLine"/> class.
        /// </summary>
        /// <param name="start">The start <see cref="Point"/></param>
        /// <param name="end">The end <see cref="Point"/></param>
        public StraightLine(Point start, Point end)
        {
            Start = start;
            End = end;
            Srid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StraightLine"/> class.
        /// </summary>
        /// <param name="start">The start <see cref="Point"/></param>
        /// <param name="end">The end <see cref="Point"/></param>
        /// <param name="srid">Coordinate system ID</param>
        public StraightLine(Point start, Point end, int? srid)
        {
            Start = start;
            End = end;
            Srid = srid;
        }

        /// <summary>
        /// Gets the start point
        /// </summary>
        public Point Start { get; }

        /// <summary>
        /// Gets the end point
        /// </summary>
        public Point End { get; }

        /// <summary>
        /// Gets the coordinate system ID
        /// </summary>
        public int? Srid { get; }
    }
}
