using Panelak.Geometry;

namespace Panelak.Database.Test.Model
{
#pragma warning disable IDE1006 // Naming Styles
    
    public class MSSQLGeometryTable
    {
        public int ID { get; set; }
        public IGeometry geometry_notnull { get; set; }
        public IGeometry geometry_null { get; set; }
        public string geometry_sql { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
}
