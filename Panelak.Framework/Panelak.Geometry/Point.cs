namespace Panelak.Geometry
{
    /// <summary>
    /// Two dimensional point geometry
    /// </summary>
    public class Point : Geometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the X
        /// </summary>
        public double X { get; }

        /// <summary>
        /// Gets the Y
        /// </summary>
        public double Y { get; }
    }
}
