devenv Panelak.Framework.sln -Project "odac.client.x86.Console" /Build "Release|x86"
XCOPY .\odac.client.x86.Console\bin\x86\Release\*.* .\Panelak.Database.OracleConsole\odac.client.x86.console\ /Y /H /K
