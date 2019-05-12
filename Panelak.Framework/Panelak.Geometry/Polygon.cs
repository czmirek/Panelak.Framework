namespace Panelak.Geometry
{
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Polygon geometry
    /// </summary>
    public readonly struct Polygon : IGeometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        public Polygon(IList<ILine> lines)
        {
            Lines = new ReadOnlyCollection<ILine>(lines ?? throw new ArgumentNullException(nameof(lines)));
            Csid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        /// <param name="csid">Coordinate system ID</param>
        public Polygon(IList<ILine> lines, int? csid)
        {
            Lines = new ReadOnlyCollection<ILine>(lines ?? throw new ArgumentNullException(nameof(lines)));
            Csid = csid;
        }

        /// <summary>
        /// List of line geometries for this polygon
        /// </summary>
        public readonly ReadOnlyCollection<ILine> Lines;

        /// <summary>
        /// Gets the coordinate system ID
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
        public override string ToString() => String.Join(", ", Lines.Select(l => l.ToString()));
    }
}
