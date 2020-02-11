-- 
If(db_id(N'TestData') IS NULL)
BEGIN
    CREATE DATABASE TestData
END
GO

USE TestData
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SimpleTypesTestTable'))
   DROP TABLE SimpleTypesTestTable;

CREATE TABLE SimpleTypesTestTable
(
    ID int PRIMARY KEY NOT NULL,
    bigint_notnull bigint not null,
    bigint_null bigint null,
    int_notnull int not null,
    int_null int null,
    smallint_notnull smallint not null,
    smallint_null smallint null,
    tinyint_notnull tinyint not null,
    tinyint_null tinyint null,
    bit_notnull bit not null,
    bit_null bit null,
    money_notnull money not null,
    money_null money null,
    float_notnull float not null,
    float_null float null,
    real_notnull real not null,
    real_null real null,
    numeric_8_0_notnull numeric(8,0) not null,
    numeric_8_0_null numeric(8,0) null,
    date_notnull date not null,
    date_null date null,
    datetime_notnull datetime not null,
    datetime_null datetime null,
    datetime2_notnull datetime2 not null,
    datetime2_null datetime2 null,
    datetimeoffset_notnull datetimeoffset not null,
    datetimeoffset_null datetimeoffset null,
    time_notnull time not null,
    time_null time null,
    char_notnull char not null,
    char_null char null,
    varchar_notnull varchar(max) not null,
    varchar_null varchar(max) null,
    text_notnull text not null,
    text_null text null,
    nchar_notnull nchar not null,
    nchar_null nchar null,
    nvarchar_notnull nvarchar(max) not null,
    nvarchar_null nvarchar(max) null,
    ntext_notnull ntext not null,
    ntext_null ntext null
);

INSERT INTO SimpleTypesTestTable VALUES (1,123,123,123,123,123,123,123,123,1,1,123.12,123.12,123.123,123.123,123.123,123.123,123,123,
'20120618 10:34:09 AM','20120618 10:34:09 AM','20120618 10:34:09 AM','20120618 10:34:09 AM', '20120618 10:34:09 AM',
'20120618 10:34:09 AM','20131114 08:54:00 +10:00','20131114 08:54:00 +10:00','12:03:20','12:03:20',
'c','c','test string','test string','test string','test string','c','c','test string','test string','test string','test string');

INSERT INTO SimpleTypesTestTable VALUES (2,78,null,78,null,78,null,78,null,1,null,78.12,null,78.78,null,78.78,null,78,null,
'20120618 10:34:09 AM',null,'20120618 10:34:09 AM',null, '20120618 10:34:09 AM', null,
'20131114 08:54:00 +10:00',null,'12:03:20',null,
'c',null,'test string',null,'test string',null,'c',null,'test string',null,'test string',null);

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'GeometryTestTable'))
    DROP TABLE GeometryTestTable;

CREATE TABLE GeometryTestTable
(
    ID int PRIMARY KEY NOT NULL,
    geometry_notnull geometry not null,
    geometry_null geometry null,
    geometry_sql nvarchar(max) not null
);

INSERT INTO GeometryTestTable VALUES (1, geometry::STGeomFromText('POINT (3 4)', 0), null, 'POINT (3 4)');
INSERT INTO GeometryTestTable VALUES (2, geometry::STGeomFromText('LINESTRING(1 1, 2 4, 3 9)', 0), null, 'LINESTRING(1 1, 2 4, 3 9)');
INSERT INTO GeometryTestTable VALUES (3, geometry::STPolyFromText('POLYGON((1 1, 3 3, 3 1, 1 1))', 10), null, 'POLYGON((1 1, 3 3, 3 1, 1 1))');
INSERT INTO GeometryTestTable VALUES (4, geometry::STGeomFromText('POINT (3 4)', 0), geometry::STGeomFromText('POINT (3 4)', 0), 'POINT (3 4)');
INSERT INTO GeometryTestTable VALUES (5, geometry::STGeomFromText('LINESTRING(1 1, 2 4, 3 9)', 0), geometry::STGeomFromText('LINESTRING(1 1, 2 4, 3 9)', 0), 'LINESTRING(1 1, 2 4, 3 9)');
INSERT INTO GeometryTestTable VALUES (6, geometry::STPolyFromText('POLYGON((1 1, 3 3, 3 1, 1 1))', 10), geometry::STPolyFromText('POLYGON((1 1, 3 3, 3 1, 1 1))', 10), 'POLYGON((1 1, 3 3, 3 1, 1 1))');