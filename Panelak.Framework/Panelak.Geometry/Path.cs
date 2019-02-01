namespace Panelak.Geometry
{
    using System;
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

        /// <summary>
        /// Returns string representation of points in geometric object
        /// </summary>
        /// <returns>String representation of points in geometric object</returns>
        public override string ToString()
        {
            string str = "";
            foreach (Line line in Lines)
                str += line.ToString() + Environment.NewLine;

            return str;
        }
    }
}
