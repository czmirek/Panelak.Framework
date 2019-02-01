//Source: https://stackoverflow.com/questions/29160849/how-to-declare-mdsys-sdo-geometry-in-c-sharp
using Oracle.DataAccess.Types;
using System;

#pragma warning disable 1591

namespace odac.client.x86.Console
{
    public abstract class OracleArrayTypeFactoryBase<T> : IOracleArrayTypeFactory
    {
        public Array CreateArray(int numElems)
        {
            return new T[numElems];
        }

        public Array CreateStatusArray(int numElems)
        {
            return new OracleUdtStatus[numElems];
        }
    }
}
