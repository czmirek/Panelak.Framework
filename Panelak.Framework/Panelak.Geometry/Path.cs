namespace Panelak.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Path geometry consisting of multiple lines.
    /// </summary>
    public struct Path : IGeometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        public Path(IList<ILine> lines)
        {
            Lines = lines ?? throw new ArgumentNullException(nameof(lines));
            Srid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        /// <param name="srid">Coordinate system ID</param>
        public Path(IList<ILine> lines, int? srid)
        {
            Lines = lines ?? throw new ArgumentNullException(nameof(lines));
            Srid = srid;
        }

        /// <summary>
        /// Gets the list of line geometries
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
