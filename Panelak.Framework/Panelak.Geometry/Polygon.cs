namespace Panelak.Geometry
{
    using System.Linq;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Single polygon geometry
    /// </summary>
    public class Polygon : Geometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        public Polygon(IList<Line> lines) => Lines = lines ?? throw new ArgumentNullException(nameof(lines));

        /// <summary>
        /// Gets the list of line geometries for this polygon
        /// </summary>
        public IList<Line> Lines { get; }

        /// <summary>
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString() => String.Join(", ", Lines.Select(l => l.ToString()));
    }
}
