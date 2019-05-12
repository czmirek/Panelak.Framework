using System.Diagnostics;

namespace Panelak.Geometry
{
    /// <summary>
    /// Represents a circular arc defined by three points.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public readonly struct CircularCurve : ILine
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
            Csid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularCurve"/> class.
        /// </summary>
        /// <param name="start">Starting point</param>
        /// <param name="end">Ending point</param>
        /// <param name="middle">Middle arc point</param>
        /// <param name="csid">Coordinate system ID</param>
        public CircularCurve(Point start, Point middle, Point end, int? csid)
        {
            Start = start;
            Middle = middle;
            End = end;
            Csid = csid;
        }

        /// <summary>
        /// Gets the middle arc point
        /// </summary>
        public readonly Point Middle;

        /// <summary>
        /// Gets the starting arc point
        /// </summary>
        public readonly Point Start;

        /// <summary>
        /// Gets the ending arc point
        /// </summary>
        public readonly Point End;

        /// <summary>
        /// Gets the coordinate system ID
        /// </summary>
        public readonly int? Csid;

        /// <summary>
        /// Returns the start point
        /// </summary>
        /// <returns>The start point</returns>
        public Point GetStartPoint() => Start;

        /// <summary>
        /// Returns the end point
        /// </summary>
        /// <returns>The end point</returns>
        public Point GetEndPoint() => End;

        /// <summary>
        /// Gets the coordinate system identification
        /// </summary>
        public int? GetCsid() => Csid;

        /// <summary>
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString() => $"{Start.ToString()} {Middle.ToString()} {End.ToString()}";
    }
}
