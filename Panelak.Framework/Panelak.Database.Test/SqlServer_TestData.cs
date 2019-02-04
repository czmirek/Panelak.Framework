namespace Panelak.Database.Test
{
    using Panelak.Database.SqlServer;
    using Panelak.Database.Test.Model;
    using Panelak.Sql.Parsing;
    using Microsoft.Extensions.Logging.Debug;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    public class SqlServer_TestData
    {
        private readonly DebugLogger log;

        public SqlServer_TestData() => log = new DebugLogger("logger");

        [Test]
        public void SimpleQueryTest()
        {
            var connection = new MSSQLConnection("Server=127.0.0.1,1401;Database=TestData;User Id=sa;Password=s3cur3*P44sw0rd;", log);
            IEnumerable<MSSQLTypesTestTable> data = connection.GetResult<MSSQLTypesTestTable>("SELECT * FROM SimpleTypesTestTable");
            Assert.AreEqual(2, data.Count());
        }

        [Test]
        public void SpatialTypesTest()
        {
            var connection = new MSSQLConnection("Server=127.0.0.1,1401;Database=TestData;User Id=sa;Password=s3cur3*P44sw0rd;", log);
            IEnumerable<MSSQLGeometryTable> data = connection.GetResult<MSSQLGeometryTable>("SELECT * FROM GeometryTestTable");
            Assert.AreEqual(3, data.Count());
        }

        [Test]
        public void SpatialFilterTest()
        {
            var connection = new MSSQLConnection("Server=127.0.0.1;Database=INGR_QUERYSERVQADATA;User Id=sa;Password=Heslo12345Heslo;", log);
            var parser = new SqlParser(log);
            Sql.ISqlSelectQuery parsedQuery = parser.ParseSqlQuery("SELECT G3E_GEOMETRY_SPA geometry_notnull FROM IQS_FID_A33");
            parsedQuery.AndGeomInBbox("G3E_GEOMETRY_SPA", -781853, -1148484, -781855, -1148486);
            parsedQuery.NoPagination = true;
            Sql.IParameterizedQuery parametrizedQuery = connection.BuildQuery(parsedQuery);

            IEnumerable<MSSQLGeometryTable> data = connection.GetResult<MSSQLGeometryTable>(parametrizedQuery);
            //IEnumerable<MSSQLGeometryTable> data = connection.GetResult<MSSQLGeometryTable>("SELECT * FROM GeometryTestTable");
            //Assert.AreEqual(3, data.Count());
        }
    }
}
