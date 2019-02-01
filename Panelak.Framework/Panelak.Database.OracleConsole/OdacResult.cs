namespace Panelak.Database.OracleConsole
{
    using System.Collections.Generic;

    /// <summary>
    /// JSON serialized result returned by the executable
    /// </summary>
    public class OdacResult
    {
        /// <summary>
        /// Gets a list of columns in the result
        /// </summary>
        public List<string> Columns { get; } = new List<string>();

        /// <summary>
        /// Gets a result table
        /// </summary>
        public List<Dictionary<string, object>> Data { get; } = new List<Dictionary<string, object>>();
    }
}
