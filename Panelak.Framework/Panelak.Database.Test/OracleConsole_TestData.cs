namespace Panelak.Database.Test
{
    using Panelak.Database.OracleConsole;
    using Panelak.Database.Test.Model;
    using Microsoft.Extensions.Logging.Debug;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class OracleConsole_TestData
    {
        private readonly DebugLogger log;
        private OdacProcess odacProcess = new OdacProcess();

        public OracleConsole_TestData() => log = new DebugLogger("logger");

        [Test]
        public void SimpleQueryTest()
        {
            var connection = new OracleConsoleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=xe)));User Id=system;Password=oracle", log);
            IEnumerable<OracleTypesTestTable> data = connection.GetResult<OracleTypesTestTable>("SELECT * FROM SimpleTypesTestTable");
            Assert.AreEqual(2, data.Count());
        }

        [Test]
        public void SpatialTypesTest()
        {
            var connection = new OracleConsoleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=xe)));User Id=system;Password=oracle", log);
            IEnumerable<OracleGeometryTable> data = connection.GetResult<OracleGeometryTable>("SELECT ID, sdo_geometry_notnull FROM GeometryTypesTestTable");
            Assert.AreEqual(6, data.Count());
        }
        
        [OneTimeSetUp]
        public void Init() => odacProcess.Start();

        [OneTimeTearDown]
        public void Dispose() => odacProcess.End();
    }
}
