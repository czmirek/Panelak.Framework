namespace Panelak.Sql
{
    using System.Collections.Generic;
    public interface ISqlLineString : ISqlGeometry
    {
        IEnumerable<ISqlPoint> Points { get; }
    }
}
