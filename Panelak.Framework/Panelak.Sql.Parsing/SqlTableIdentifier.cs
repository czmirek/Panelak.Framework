namespace Panelak.Sql.Parsing
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Parses the table identifier used in by convention (expressed in Regex).
    /// </summary>
    internal class SqlTableIdentifier : ISqlTableIdentifier
    {
        /// <summary>
        /// Parses the database name, schema, table and connection key from the table identifier.
        /// </summary>
        private static Regex parser = new Regex(@"(?:(?<database>\w+)\.)?(?:(?<schema>\w+)\.)?(?<table>\w+)(?:@(?<key>\w+))?");

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableIdentifier"/> class.
        /// </summary>
        /// <param name="tableIdentifier">Table identifier</param>
        public SqlTableIdentifier(string tableIdentifier)
        {
            Match match = parser.Match(tableIdentifier);

            Database = match.Groups["database"]?.Value;
            Schema = match.Groups["schema"].Value;
            Table = match.Groups["table"].Value;
            ConnectionKey = match.Groups["key"]?.Value;
        }

        /// <summary>
        /// Gets the database identifier. Can be null.
        /// </summary>
        public string Database { get; private set; }

        /// <summary>
        /// Gets the schema or owner name. Can be null.
        /// </summary>
        public string Schema { get; private set; }

        /// <summary>
        /// Gets the table name. Cannot be null.
        /// </summary>
        public string Table { get; private set; }

        /// <summary>
        /// Gets the connection key from configuration. Can be null.
        /// </summary>
        public string ConnectionKey { get; private set; }
    }
}
