namespace Panelak.Sql
{
    using Panelak.Geometry;

    /// <summary>
    /// Condition expression for comparing a <see cref="Column"/> inside a spatial expression
    /// against given <see cref="Geometry"/> such as Overlaps, Withing etc.
    /// </summary>
    public interface ISqlConditionSpatialExpression : ISqlConditionExpression
    {
        /// <summary>
        /// Gets the column of the spaitial expression
        /// </summary>
        string Column { get; }

        /// <summary>
        /// Gets the geometry which is used in the spatial expression
        /// </summary>
        IGeometry Geometry { get; }
    }
}
