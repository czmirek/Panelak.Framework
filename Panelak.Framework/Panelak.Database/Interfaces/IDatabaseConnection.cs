namespace Panelak.Database
{
    /// <summary>
    /// Information about a specific database connection
    /// </summary>
    public interface IDatabaseConnection
    {
        /// <summary>
        /// Gets the database type such as SqlServer or Oracle
        /// </summary>
        DatabaseType DatabaseType { get; }

        /// <summary>
        /// Gets the connection string to the database
        /// </summary>
        string ConnectionString { get; }
    }
}
