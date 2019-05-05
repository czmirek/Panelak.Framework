namespace Panelak.Geometry
{
    using System.Linq;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Single polygon geometry
    /// </summary>
    public struct Polygon : IGeometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        public Polygon(IList<ILine> lines)
        {
            Lines = lines ?? throw new ArgumentNullException(nameof(lines));
            Srid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        /// <param name="srid">Coordinate system ID</param>
        public Polygon(IList<ILine> lines, int? srid)
        {
            Lines = lines ?? throw new ArgumentNullException(nameof(lines));
            Srid = srid;
        }

        /// <summary>
        /// Gets the list of line geometries for this polygon
        /// </summary>
        public IList<ILine> Lines { get; }

        /// <summary>
        /// Gets the coordinate system ID
        /// </summary>
        public int? Srid { get; }

        /// <summary>
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString() => String.Join(", ", Lines.Select(l => l.ToString()));
    }
}
