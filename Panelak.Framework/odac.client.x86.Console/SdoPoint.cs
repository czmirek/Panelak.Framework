﻿//Source: https://stackoverflow.com/questions/29160849/how-to-declare-mdsys-sdo-geometry-in-c-sharp
#pragma warning disable 1591

namespace odac.client.x86.Console
{
    using Oracle.DataAccess.Types;
    using System;

    [Serializable]
    [OracleCustomTypeMapping("MDSYS.SDO_POINT_TYPE")]
    public class SdoPoint : OracleCustomTypeBase<SdoPoint>
    {
        [OracleObjectMapping("X")]
        public decimal? X { get; set; }

        [OracleObjectMapping("Y")]
        public decimal? Y { get; set; }

        [OracleObjectMapping("Z")]
        public decimal? Z { get; set; }

        public override void MapFromCustomObject()
        {
            SetValue("X", X);
            SetValue("Y", Y);
            SetValue("Z", Z);
        }
        public override void MapToCustomObject()
        {
            X = GetValue<decimal?>("X");
            Y = GetValue<decimal?>("Y");
            Z = GetValue<decimal?>("Z");
        }
    }
}
