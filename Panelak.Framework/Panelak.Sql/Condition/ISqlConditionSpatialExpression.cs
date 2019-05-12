namespace Panelak.Sql
{
    public interface ISqlConditionSpatialExpression : ISqlConditionExpression
    {
        string Column { get; }
        ISqlGeometry Geometry { get; }
    }
}
