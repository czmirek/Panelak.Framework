namespace Panelak.Sql
{
    /// <summary>
    /// RDBMS independent table identifier from the FROM clause of the SQL query
    /// </summary>
    public interface ISqlTableIdentifier
    {
        /// <summary>
        /// Gets the connection key
        /// </summary>
        string ConnectionKey { get; }

        /// <summary>
        /// Gets the database identifier
        /// </summary>
        string Database { get; }

        /// <summary>
        /// Gets the table name
        /// </summary>
        string Table { get; }

        /// <summary>
        /// Gets the schema or owner name
        /// </summary>
        string Schema { get; }
    }
}