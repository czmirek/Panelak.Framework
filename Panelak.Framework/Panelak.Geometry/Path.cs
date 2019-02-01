namespace Panelak.Geometry
{
    using System.Collections.Generic;

    /// <summary>
    /// Path geometry consisting of multiple lines.
    /// </summary>
    public class Path : Geometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="lines">List of line geometries.</param>
        public Path(IList<Line> lines) => Lines = lines;

        /// <summary>
        /// Gets the list of line geometries
        /// </summary>
        public IList<Line> Lines { get; }
    }
}
