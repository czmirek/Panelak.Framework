using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using Panelak.Database.Oracle;
using System;

namespace Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var lf = new LoggerFactory();
#pragma warning disable CS0618 // Type or member is obsolete
            lf.AddConsole(LogLevel.Trace);
#pragma warning restore CS0618 // Type or member is obsolete
            ILogger logger = lf.CreateLogger<Program>();
            logger.LogTrace("test");

            var connection = new Panelak.Database.Oracle.OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=PRA-SGIV-SLNT04)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=SLNTFOW)));User ID=PVV_EDIT;Password=slntfow;", logger);
            var input = new INGR_PVV_PKG.Request.InputPrams()
            {
                vAttribute = "<ROW><ID></ID><USER_ID>xx</USER_ID><DESCRIPTION>xxx</DESCRIPTION><NAME>name</NAME></ROW>",
                bData = null,
                cAoi = null
            };
            var output = new INGR_PVV_PKG.Request.OutputParams();

            connection.ExecuteProcedure("INGR_PVV_PKG.Request", input, output);

            Console.WriteLine($"Output: {output.err_code} , {output.err_desc}, {output.nId}");
            Console.ReadLine();
        }
    }

    internal class INGR_PVV_PKG
    {
        internal class Request
        {
            internal class InputPrams
            {
                [OracleDbType(OracleDbType.Varchar2)]
                internal string vAttribute { get; set; }

                [OracleDbType(OracleDbType.Blob)]
                internal byte[] bData { get; set; }

                [OracleDbType(OracleDbType.Clob)]
                internal string cAoi { get; set; }
            }
            internal class OutputParams
            {
                [OracleDbType(OracleDbType.Int32)]
                internal int nId { get; set; }

                [OracleDbType(OracleDbType.Int32)]
                internal long err_code { get; set; }

                [OracleDbType(OracleDbType.Varchar2, 4000)]
                internal string err_desc { get; set; }
            }
        }
    }
}
