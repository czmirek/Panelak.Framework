namespace Panelak.Database.OracleConsole
{
    using System.Collections.Generic;

    /// <summary>
    /// JSON serialized result returned by the executable. Data are of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of the result row.</typeparam>
    public class OdacResult<T>
    {
        /// <summary>
        /// Gets a list of column names
        /// </summary>
        public List<string> Columns { get; } = new List<string>();

        /// <summary>
        /// Gets a result mapped to a specific type in <typeparamref name="T"/>.
        /// </summary>
        public List<T> Data { get; } = new List<T>();
    }
}
