using Panelak.Geometry;

namespace Panelak.Database.Test.Model
{
#pragma warning disable IDE1006 // Naming Styles
    public class OracleGeometryTable
    {
        public decimal ID { get; set; }
        public Geometry.Geometry sdo_geometry_notnull { get; set; }
        public Geometry.Geometry sdo_geometry_null { get; set; }
        public string sdo_geometry_sql { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
}
