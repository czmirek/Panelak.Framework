namespace Panelak.Sql
{
    using System.Collections.Generic;
    public interface ISqlPolygon : ISqlGeometry
    {
        ISqlPoint FirstAndLastPoint { get; }
        IList<ISqlPoint> InBetweenPoints { get; }
    }
}
