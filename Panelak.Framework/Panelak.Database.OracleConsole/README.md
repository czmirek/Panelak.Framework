# ORM for the odac.client.x86.Console

This is a simple ORM which communicates with the odac.client.x86.Console executable on the named pipe.

## How it works with SDO_GEOMETRY

See the tests here: https://github.com/Panelak/Panelak.Framework/blob/master/Panelak.Database.Test/OracleConsole_TestData.cs

First you need to launch the .NET Framework executable. This is fully in your control and it's up to you where and when you start it.

```csharp
OdacProcess odacProcess = new OdacProcess();
odacProcess.Start();
```

Of course you can end the executable simply like this.

```csharp
odacProcess.End();
```

Without the ODAC executable, OracleConsoleConnection is not going to work.

Creating the connection is simple.


```csharp
var connection = new OracleConsoleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=xe)));User Id=system;Password=oracle", log);
```

Query a database with SDO_GEOMETRY column types and map the result to a DTO.

```csharp
IEnumerable<OracleGeometryTable> data = connection.GetResult<OracleGeometryTable>("SELECT ID, sdo_geometry_notnull FROM GeometryTypesTestTable");
```

See the (OracleGeometryTable)[https://github.com/Panelak/Panelak.Framework/blob/master/Panelak.Database.Test/Model/OracleGeometryTable.cs].
