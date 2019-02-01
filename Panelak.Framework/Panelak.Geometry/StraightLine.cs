namespace Panelak.Geometry
{
    /// <summary>
    /// Represents a straight line between two points
    /// </summary>
    public class StraightLine : Line
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StraightLine"/> class.
        /// </summary>
        /// <param name="start">The start <see cref="Point"/></param>
        /// <param name="end">The end <see cref="Point"/></param>
        public StraightLine(Point start, Point end) : base(start, end)
        {
        }
    }
}
