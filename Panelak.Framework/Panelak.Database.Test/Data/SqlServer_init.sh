# Creates a Sql Server container and creates the Northwind database
# Northwind database SQL script source: https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs
# Tested on virtual machine with Ubuntu 18.04

container_name="testdb"
password="s3cur3*P44sw0rd"
script_dir="/var/opt/SqlServer/northwind"
sqlfile="SqlServer_data.sql"

docker pull mcr.microsoft.com/SqlServer/server:2017-latest
docker run -e "ACCEPT_EULA=Y" -e "SqlServer_SA_PASSWORD=${password}" --name $container_name -p 1401:1433 -v sql1data:/var/opt/SqlServer -d mcr.microsoft.com/SqlServer/server:2017-latest

#must wait some time for Sql Server to finish its startup procedures before running Northwind scripts
sleep 30s

docker exec -it $container_name mkdir -p $script_dir
docker cp $sqlfile "${container_name}:${script_dir}"
docker exec -it $container_name /opt/SqlServer-tools/bin/sqlcmd -S localhost -U SA -P $password -i "${script_dir}/${sqlfile}"
