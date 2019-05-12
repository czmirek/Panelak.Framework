namespace Panelak.Database.Test.Model
{
    using System;

#pragma warning disable IDE1006 // Naming Styles
    public class SqlServerTypesTestTable
    { 
        public int ID { get; set; }
        public long bigint_notnull { get; set; }
        public long? bigint_null { get; set; }
        public int int_notnull { get; set; }
        public int? int_null { get; set; }
        public short smallint_notnull { get; set; }
        public short? smallint_null { get; set; }
        public byte tinyint_notnull { get; set; }
        public byte? tinyint_null { get; set; }
        public bool bit_notnull { get; set; }
        public bool? bit_null { get; set; }
        public decimal money_notnull { get; set; }
        public decimal? money_null { get; set; }
        public double float_notnull { get; set; }
        public double? float_null { get; set; }
        public float real_notnull { get; set; }
        public float? real_null { get; set; }
        public decimal numeric_8_0_notnull { get; set; }
        public decimal? numeric_8_0_null { get; set; }
        public DateTime date_notnull { get; set; }
        public DateTime? date_null { get; set; }
        public DateTime datetime_notnull { get; set; }
        public DateTime? datetime_null { get; set; }
        public DateTime datetime2_notnull { get; set; }
        public DateTime? datetime2_null { get; set; }
        public DateTimeOffset datetimeoffset_notnull { get; set; }
        public DateTimeOffset? datetimeoffset_null { get; set; }
        public TimeSpan time_notnull { get; set; }
        public TimeSpan? time_null { get; set; }
        public string char_notnull { get; set; }
        public string char_null { get; set; }
        public string varchar_notnull { get; set; }
        public string varchar_null { get; set; }
        public string text_notnull { get; set; }
        public string text_null { get; set; }
        public string nchar_notnull { get; set; }
        public string nchar_null { get; set; }
        public string nvarchar_notnull { get; set; }
        public string nvarchar_null { get; set; }
        public string ntext_notnull { get; set; }
        public string ntext_null { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
}
