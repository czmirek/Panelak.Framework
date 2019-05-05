namespace Panelak.Geometry
{
    /// <summary>
    /// Geometry shape
    /// </summary>
    public interface IGeometry
    {
        /// <summary>
        /// Gets the coordinate system identification
        /// </summary>
        int? Srid { get; }
    }
}
