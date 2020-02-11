namespace Panelak.Geometry
{
    /// <summary>
    /// Line geometry shape consisting of two points
    /// </summary>
    public interface ILine : IGeometry
    {
        /// <summary>
        /// Gets the start point
        /// </summary>
        Point GetStartPoint();

        /// <summary>
        /// Gets the end point
        /// </summary>
        Point GetEndPoint();
    }
}
