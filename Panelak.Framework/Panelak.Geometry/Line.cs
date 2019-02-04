namespace Panelak.Geometry
{
    using System;

    /// <summary>
    /// Line geometry shape consisting of two points
    /// </summary>
    public abstract class Line : Geometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="start">The start <see cref="Point"/></param>
        /// <param name="end">The end <see cref="Point"/></param>
        public Line(Point start, Point end)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));
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
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString() => $"{Start.ToString()} {End.ToString()}";
    }
}
