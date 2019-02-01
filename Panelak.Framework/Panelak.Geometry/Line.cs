namespace Panelak.Geometry
{
    /// <summary>
    /// Line geometry shape
    /// </summary>
    public class Line : Geometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="start">The start <see cref="Point"/></param>
        /// <param name="end">The end <see cref="Point"/></param>
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Gets the start point
        /// </summary>
        public Point Start { get; }

        /// <summary>
        /// Gets the end point
        /// </summary>
        public Point End { get; }
    }
}
