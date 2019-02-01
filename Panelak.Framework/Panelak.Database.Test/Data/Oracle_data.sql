declare
   c int;
   sql_stmt varchar2(3000);
begin
   select count(*) into c from user_tables where table_name = upper('SimpleTypesTestTable');
   if c = 1 then
      execute immediate 'drop table SimpleTypesTestTable';
   end if;
   
   sql_stmt := 'CREATE TABLE SimpleTypesTestTable
   (
		ID NUMBER PRIMARY KEY,
		varchar2_notnull VARCHAR2(255) NOT NULL,
		varchar2_null VARCHAR2(255) NULL,
		char_notnull CHAR(1 BYTE) NOT NULL,
		char_null CHAR(1 BYTE) NULL,
		date_notnull DATE NOT NULL,
		date_null DATE NULL,
		timestamp_notnull TIMESTAMP NOT NULL,
		timestamp_null TIMESTAMP NULL,
		number80_notnull NUMBER(8, 0) NOT NULL,
		number80_null NUMBER(8, 0) NULL,
		float_notnull FLOAT NOT NULL,
		float_null FLOAT NULL,
		clob_notnull CLOB NOT NULL,
		clob_null CLOB NULL
   )';
   
   EXECUTE IMMEDIATE sql_stmt;   
   INSERT INTO SimpleTypesTestTable VALUES (1, 'test string', null, 'c', null, TO_DATE('2003/05/03 21:02:44', 'yyyy/mm/dd hh24:mi:ss'), null, CURRENT_TIMESTAMP, NULL, 123, null, 123.123, null, 'str', null);
   INSERT INTO SimpleTypesTestTable VALUES (2, 'test string', 'test string', 'c', 'c', TO_DATE('2003/05/03 21:02:44', 'yyyy/mm/dd hh24:mi:ss'), TO_DATE('2003/05/03 21:02:44', 'yyyy/mm/dd hh24:mi:ss'), CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 123, 123, 123.123, 123.123, 'str', 'str');

   select count(*) into c from user_tables where table_name = upper('GeometryTypesTestTable');
   if c = 1 then
      execute immediate 'drop table GeometryTypesTestTable';
   end if;
   
   sql_stmt := 'CREATE TABLE GeometryTypesTestTable
	(
		ID int PRIMARY KEY,
		sdo_geometry_notnull SDO_GEOMETRY not null,
		sdo_geometry_null SDO_GEOMETRY null,
		sdo_geometry_sql nvarchar2(2000) not null
	)';
	
	EXECUTE IMMEDIATE sql_stmt;

	--NULL column with NULL value
	--point
	INSERT INTO GeometryTypesTestTable VALUES (1, SDO_GEOMETRY(2001, NULL, MDSYS.SDO_POINT_TYPE(12, 14, NULL), NULL,NULL), NULL, 'MDSYS.SDO_GEOMETRY(2001, NULL, MDSYS.SDO_POINT_TYPE(12, 14, NULL), NULL,NULL)');
	
	--line
	INSERT INTO GeometryTypesTestTable VALUES (2, SDO_GEOMETRY(2002,NULL,NULL,SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), SDO_ORDINATE_ARRAY(10,10, 10,14, 6,10, 14,10)), NULL, 'SDO_GEOMETRY(2002,NULL,NULL,SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), SDO_ORDINATE_ARRAY(10,10, 10,14, 6,10, 14,10))');

	--polygon
	INSERT INTO GeometryTypesTestTable VALUES (3, SDO_GEOMETRY(2003, NULL, NULL, SDO_ELEM_INFO_ARRAY(1,1005,2, 1,2,1, 5,2,2), SDO_ORDINATE_ARRAY(6,10, 10,1, 14,10, 10,14, 6,10)), NULL, 'SDO_GEOMETRY(2003, NULL, NULL, SDO_ELEM_INFO_ARRAY(1,1005,2, 1,2,1, 5,2,2), SDO_ORDINATE_ARRAY(6,10, 10,1, 14,10, 10,14, 6,10))');
	  
	-- NULL column with same value as not null
	--point
	INSERT INTO GeometryTypesTestTable VALUES (4, SDO_GEOMETRY(2001, NULL, MDSYS.SDO_POINT_TYPE(12, 14, NULL), NULL,NULL), SDO_GEOMETRY(2001, NULL, MDSYS.SDO_POINT_TYPE(12, 14, NULL), NULL,NULL), 'MDSYS.SDO_GEOMETRY(2001, NULL, MDSYS.SDO_POINT_TYPE(12, 14, NULL), NULL,NULL)');
	
	--line
	INSERT INTO GeometryTypesTestTable VALUES (5, SDO_GEOMETRY(2002,NULL,NULL,SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), SDO_ORDINATE_ARRAY(10,10, 10,14, 6,10, 14,10)), SDO_GEOMETRY(2002,NULL,NULL,SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), SDO_ORDINATE_ARRAY(10,10, 10,14, 6,10, 14,10)), 'SDO_GEOMETRY(2002,NULL,NULL,SDO_ELEM_INFO_ARRAY(1,4,2, 1,2,1, 3,2,2), SDO_ORDINATE_ARRAY(10,10, 10,14, 6,10, 14,10))');

	--polygon
	INSERT INTO GeometryTypesTestTable VALUES (6, SDO_GEOMETRY(2003, NULL, NULL, SDO_ELEM_INFO_ARRAY(1,1005,2, 1,2,1, 5,2,2), SDO_ORDINATE_ARRAY(6,10, 10,1, 14,10, 10,14, 6,10)), SDO_GEOMETRY(2003, NULL, NULL, SDO_ELEM_INFO_ARRAY(1,1005,2, 1,2,1, 5,2,2), SDO_ORDINATE_ARRAY(6,10, 10,1, 14,10, 10,14, 6,10)), 'SDO_GEOMETRY(2003, NULL, NULL, SDO_ELEM_INFO_ARRAY(1,1005,2, 1,2,1, 5,2,2), SDO_ORDINATE_ARRAY(6,10, 10,1, 14,10, 10,14, 6,10))');
end;