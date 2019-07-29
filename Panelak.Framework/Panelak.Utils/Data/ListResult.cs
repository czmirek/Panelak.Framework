namespace Panelak.Utils
{
    using System.Collections.Generic;

    /// <summary>
    /// Container for all or filtered data and for the complete amount of that data.
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    public class ListResult<T>
    {
        /// <summary>
        /// Number of all rows (e.g. in a database) 
        /// </summary>
        public int RowsTotal { get; set; }

        /// <summary>
        /// Enumeration of rows, may be filtered.
        /// </summary>
        public IEnumerable<T> Rows { get; set; }
    }
}
