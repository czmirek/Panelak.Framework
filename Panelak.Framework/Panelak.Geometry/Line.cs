namespace Panelak.Geometry
{
    using System;

    /// <summary>
    /// Line geometry shape consisting of two points
    /// </summary>
    public interface ILine : IGeometry
    {
        /// <summary>
        /// Gets the start point
        /// </summary>
        Point Start { get; }

        /// <summary>
        /// Gets the end point
        /// </summary>
        Point End { get; }
    }
}
