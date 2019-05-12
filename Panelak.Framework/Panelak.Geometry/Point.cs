namespace Panelak.Geometry
{
    /// <summary>
    /// Two dimensional point geometry
    /// </summary>
    public readonly struct Point : IGeometry
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
            Csid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="csid">Coordinate system ID</param>
        public Point(double x, double y, int? csid)
        {
            X = x;
            Y = y;
            Csid = csid;
        }

        /// <summary>
        /// X coordinate
        /// </summary>
        public readonly double X;

        /// <summary>
        /// Y coordinate
        /// </summary>
        public readonly double Y;

        /// <summary>
        /// Coordinate system ID
        /// </summary>
        public readonly int? Csid;

        /// <summary>
        /// Gets the coordinate system identification
        /// </summary>
        public int? GetCsid() => Csid;

        /// <summary>
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString() => $"{X} {Y}";
    }
}
