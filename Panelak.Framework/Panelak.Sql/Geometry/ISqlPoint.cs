namespace Panelak.Sql
{
    public interface ISqlPoint : ISqlGeometry
    {
        double X { get; }
        double Y { get; }
    }
}
