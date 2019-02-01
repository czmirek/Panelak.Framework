namespace Panelak.Database.OracleConsole
{
    /// <summary>
    /// Input message sent to the named pipe in JSON format
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets a value indicating whether the executable should close.
        /// </summary>
        public bool IsExit { get; set; }

        /// <summary>
        /// Gets or sets the SQL query text.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the SQL query parameters.
        /// </summary>
        public string Parameters { get; set; }
    }
}
