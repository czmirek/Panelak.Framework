namespace Panelak.Geometry
{
    /// <summary>
    /// Represents a straight line between two points
    /// </summary>
    public readonly struct StraightLine : ILine
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
            Csid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StraightLine"/> class.
        /// </summary>
        /// <param name="start">The start <see cref="Point"/></param>
        /// <param name="end">The end <see cref="Point"/></param>
        /// <param name="csid">Coordinate system ID</param>
        public StraightLine(Point start, Point end, int? csid)
        {
            Start = start;
            End = end;
            Csid = csid;
        }

        /// <summary>
        /// Gets the start point
        /// </summary>
        public readonly Point Start;

        /// <summary>
        /// Gets the end point
        /// </summary>
        public readonly Point End;

        /// <summary>
        /// Gets the coordinate system ID
        /// </summary>
        public readonly int? Csid;

        /// <summary>
        /// Gets the coordinate system identification
        /// </summary>
        public int? GetCsid() => Csid;

        /// <summary>
        /// Gets the start point
        /// </summary>
        public Point GetStartPoint() => Start;

        /// <summary>
        /// Gets the end point
        /// </summary>
        public Point GetEndPoint() => End;
    }
}
