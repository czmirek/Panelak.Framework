# Creates an Oracle XE 11g container

container_name="oracledb"
script_dir="/var/opt/oracle/testdb"

docker pull wnameless/oracle-xe-11g
docker run --name $container_name -d -p 1521:1521 -p 8080:8080 wnameless/oracle-xe-11g
sleep 20
docker exec -it $container_name mkdir -p $script_dir
docker cp "Oracle_data.sql" "${container_name}:${script_dir}"
docker exec -it $container_name bash -c "exit | ORACLE_HOME=/u01/app/oracle/product/11.2.0/xe /u01/app/oracle/product/11.2.0/xe/bin/sqlplus -L -Sszste system/oracle@xe @$script_dir/Oracle_data.sql"