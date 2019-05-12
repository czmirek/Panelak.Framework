namespace Panelak.Database
{
    using Panelak.Geometry;
    using Microsoft.SqlServer.Types;
    using System;
    using System.Data;

    /// <summary>
    /// Default implementation of the SQL results in the <see cref="IDataReader"/> to the DTO in a type parameter.
    /// </summary>
    public class SqlServerDtoMapper : DefaultDtoMapper
    {
        /// <summary>
        /// SQL Server DTO mapper accounting for SQL
        /// </summary>
        /// <param name="dataValue">Data value</param>
        /// <param name="dataValueType">Data value type</param>
        /// <param name="customMap">Converted value</param>
        /// <returns>True if mapping was successful</returns>
        public override bool TryCustomMap(object dataValue, Type dataValueType, out object customMap)
        {
            customMap = null;

            if (!dataValueType.Equals(typeof(SqlGeometry))) 
                return false;

            var geom = dataValue as SqlGeometry;
            int? csid = geom.STSrid.Value;

            string wkt = geom.STAsText().ToSqlString().Value;
            var converter = new WktConverter();
            customMap = converter.FromWkt(csid, wkt);
            return true;
        }
    }
}
