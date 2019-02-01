namespace Panelak.Database.OracleConsole
{
    /// <summary>
    /// Oracle SDO_GEOMETRY model.
    /// </summary>
    public class SdoGeometry
    {
        /// <summary>
        /// Gets or sets the SDO_GTYPE
        /// </summary>
        public decimal? SdoGtype { get; set; }

        /// <summary>
        /// Gets or sets the SDO_SRID
        /// </summary>
        public decimal? SdoSRID { get; set; }

        /// <summary>
        /// Gets or sets the SDO_POINT
        /// </summary>
        public SdoPoint SdoPoint { get; set; }

        /// <summary>
        /// Gets or sets the SDO_ELEM_INFO 
        /// </summary>
        public decimal[] ElemArray { get; set; }

        /// <summary>
        /// Gets or sets the SDO_ORDINATES 
        /// </summary>
        public decimal[] OrdinatesArray { get; set; }
    }
}
