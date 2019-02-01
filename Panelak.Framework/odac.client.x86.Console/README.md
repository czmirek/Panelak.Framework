# odac.client.x86.Console 

This is a .NET Framework console application which listens on a [named pipe](https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-use-named-pipes-for-network-interprocess-communication) **```odac.client.x86.pipe```** where it accepts a JSON serialized object with a connection string, query and query parameters for the Oracle Database and then returns the result as JSON string.

The goal of this project is to bridge missing support of UDTs - especially the SDO_GEOMETRY type - in .NET Core projects. The official *Oracle.ManagedDataAccess.Core* as of 30th January 2019 did not have support for UDTs.

The console application references the older **[odac.client.x86.dll](https://www.nuget.org/packages/odac.client.x86/)** which supports mapping UDT columns to C# classes with attributes.

See the [OracleConsole project](https://github.com/Panelak/Panelak.Framework/tree/master/Panelak.Database.OracleConsole) which is an ORM which includes the console executables and communicates with it through the named pipe.
