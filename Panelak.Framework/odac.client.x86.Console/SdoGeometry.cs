#pragma warning disable 1591
//Source: https://stackoverflow.com/questions/29160849/how-to-declare-mdsys-sdo-geometry-in-c-sharp
namespace odac.client.x86.Console
{
    using Oracle.DataAccess.Types;
    using System;

    [Serializable]
    [OracleCustomTypeMapping("MDSYS.SDO_GEOMETRY")]
    public class SdoGeometry : OracleCustomTypeBase<SdoGeometry>
    {
        private enum OracleObjectColumns { SDO_GTYPE, SDO_SRID, SDO_POINT, SDO_ELEM_INFO, SDO_ORDINATES }

        [OracleObjectMapping(0)]
        public decimal? SdoGtype { get; set; }

        [OracleObjectMapping(1)]
        public decimal? SdoSRID { get; set; }

        
        [OracleObjectMapping(2)]
        public SdoPoint SdoPoint { get; set; }

        [OracleObjectMapping(3)]
        public decimal[] ElemArray { get; set; }
        
        [OracleObjectMapping(4)]
        public decimal[] OrdinatesArray { get; set; }

        [OracleCustomTypeMapping("MDSYS.SDO_ELEM_INFO_ARRAY")]
        public class ElemArrayFactory : OracleArrayTypeFactoryBase<decimal> { }

        [OracleCustomTypeMapping("MDSYS.SDO_ORDINATE_ARRAY")]
        public class OrdinatesArrayFactory : OracleArrayTypeFactoryBase<decimal> { }

        public override void MapFromCustomObject()
        {
            SetValue((int)OracleObjectColumns.SDO_GTYPE, SdoGtype);
            SetValue((int)OracleObjectColumns.SDO_SRID, SdoSRID);
            SetValue((int)OracleObjectColumns.SDO_POINT, SdoPoint);
            SetValue((int)OracleObjectColumns.SDO_ELEM_INFO, ElemArray);
            SetValue((int)OracleObjectColumns.SDO_ORDINATES, OrdinatesArray);
        }

        public override void MapToCustomObject()
        {
            SdoGtype = GetValue<decimal?>((int)OracleObjectColumns.SDO_GTYPE);
            SdoSRID = GetValue<decimal?>((int)OracleObjectColumns.SDO_SRID);
            SdoPoint = GetValue<SdoPoint>((int)OracleObjectColumns.SDO_POINT);
            ElemArray = GetValue<decimal[]>((int)OracleObjectColumns.SDO_ELEM_INFO);
            OrdinatesArray = GetValue<decimal[]>((int)OracleObjectColumns.SDO_ORDINATES);
        }
    }
}
