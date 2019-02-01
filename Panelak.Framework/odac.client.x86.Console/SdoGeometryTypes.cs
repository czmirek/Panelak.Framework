//Source: https://stackoverflow.com/questions/29160849/how-to-declare-mdsys-sdo-geometry-in-c-sharp
#pragma warning disable 1591

namespace odac.client.x86.Console
{
    using System;

    [Serializable]
    public static class SdoGeometryTypes
    {
        /// <summary>
        /// Oracle Documentation for SDO_ETYPE - SIMPLE
        /// Point//Line//Polygon//exterior counterclockwise - polygon ring = 1003//interior clockwise  polygon ring = 2003
        /// </summary>
        public enum EtypeSimple { Point = 1, Line = 2, Polygon = 3, PolygonExterior = 1003, PolygonInterior = 2003 }
       
        /// <summary>
        ///  Oracle Documentation for SDO_ETYPE - COMPOUND
        /// 1005: exterior polygon ring (must be specified in counterclockwise order)
        /// 2005: interior polygon ring (must be specified in clockwise order)
        /// </summary>
        public enum EtypeCompound { FourDigit = 4, PolygonExterior = 1005, PolygonInterior = 2005 }
        
        /// <summary>
        /// Oracle Documentation for SDO_GTYPE.
        /// This represents the last two digits in a GTYPE, where the first item is dimension(ality) and the second is LRS
        /// </summary>
        public enum Gtype { UnknownGeometry = 00, Point = 01, Line = 02, Curve = 02, Polygon = 03, Collection = 04, Multipoint = 05, Multiline = 06, Multicurve = 06, Multipolygon = 07 }
        public enum Dimension { Dim2D = 2, Dim3D = 3, LRSDim3 = 3, LRSDim4 = 4 }
    }
}
