namespace Panelak.Geometry
{
    /// <summary>
    /// Two dimensional point geometry
    /// </summary>
    public struct Point : IGeometry
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
            Srid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="srid">Coordinate system ID</param>
        public Point(double x, double y, int? srid)
        {
            X = x;
            Y = y;
            Srid = srid;
        }

        /// <summary>
        /// Gets the X
        /// </summary>
        public double X { get; }

        /// <summary>
        /// Gets the Y
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Gets the Srid
        /// </summary>
        public int? Srid { get; }

        /// <summary>
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString() => $"{X} {Y}";
    }
}
