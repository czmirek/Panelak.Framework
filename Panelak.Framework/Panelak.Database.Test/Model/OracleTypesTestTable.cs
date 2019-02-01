namespace Panelak.Database.Test.Model
{
    using System;

#pragma warning disable IDE1006 // Naming Styles
    public class OracleTypesTestTable
    { 
        public decimal ID { get; set; }

        public string varchar2_notnull { get; set; }
        public string varchar2_null { get; set; }

        public string char_notnull { get; set; }
        public string char_null { get; set; }

        public DateTime date_notnull { get; set; }
        public DateTime? date_null { get; set; }

        public DateTime timestamp_notnull { get; set; }
        public DateTime? timestamp_null { get; set; }

        public int number80_notnull { get; set; }
        public int? number80_null { get; set; }

        public decimal float_notnull { get; set; }
        public decimal? float_null { get; set; }

        public string clob_notnull { get; set; }
        public string clob_null { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
}
