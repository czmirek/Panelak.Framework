namespace Panelak.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Path geometry consisting of multiple lines.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public readonly struct Path : IGeometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        public Path(IList<ILine> lines)
        {
            Lines = new ReadOnlyCollection<ILine>(lines ?? throw new ArgumentNullException(nameof(lines)));
            Csid = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        /// <param name="csid">Coordinate system ID</param>
        public Path(IList<ILine> lines, int? csid)
        {
            Lines = new ReadOnlyCollection<ILine>(lines ?? throw new ArgumentNullException(nameof(lines)));
            Csid = csid;
        }

        /// <summary>
        /// List of line geometries
        /// </summary>
        public readonly ReadOnlyCollection<ILine> Lines;

        /// <summary>
        /// Coordinate system ID
        /// </summary>
        public readonly int? Csid;

        /// <summary>
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString() => String.Join(", ", Lines.Select(l => l.ToString()));

        /// <summary>
        /// Gets the coordinate system identification
        /// </summary>
        public int? GetCsid() => Csid;
    }
}
