namespace Panelak.Database.OracleConsole
{
    using Newtonsoft.Json;

    /// <summary>
    /// Sends query to the named pipe
    /// </summary>
    public class OdacPipeQueryMessage : OdacPipe
    {
        /// <summary>
        /// Oracle connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// SQL query text
        /// </summary>
        private readonly string query;

        /// <summary>
        /// SQL query comma separated parameters
        /// </summary>
        private readonly string parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="OdacPipeQueryMessage"/> class.
        /// </summary>
        /// <param name="connectionString">Oracle connection string</param>
        /// <param name="query">SQL query text</param>
        /// <param name="parameters">SQL query parameters</param>
        public OdacPipeQueryMessage(string connectionString, string query, string parameters) : base()
        {
            this.connectionString = connectionString;
            this.query = query;
            this.parameters = parameters;
        }

        /// <summary>
        /// Creates a new message object, sends it to the named pipe and retrieves the
        /// JSON serialized result.
        /// </summary>
        /// <returns>JSON result</returns>
        public string GetResult()
        {
            var msg = new Message()
            {
                IsExit = false,
                ConnectionString = connectionString,
                Query = query,
                Parameters = parameters
            };

            string serialized = JsonConvert.SerializeObject(msg);
            Writer.WriteLine(serialized);
            Writer.Flush();
            return Reader.ReadLine();
        }
    }
}
