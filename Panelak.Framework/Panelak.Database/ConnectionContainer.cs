namespace Panelak.Database
{
    using System;
    using ConnectionDictionary = System.Collections.Generic.Dictionary<string, IConnectionLink>;

    /// <summary>
    /// Collection of database connections from IQS configuration.
    /// </summary>
    public class ConnectionContainer : IConnectionLinkContainer
    {
        /// <summary>
        /// Connection dictionary
        /// </summary>
        private ConnectionDictionary connections = new ConnectionDictionary();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionContainer"/> class.
        /// </summary>
        /// <param name="connections">The connection dictionary</param>
        public ConnectionContainer(ConnectionDictionary connections) => this.connections = connections ?? throw new ArgumentNullException(nameof(connections));

        /// <summary>
        /// Returns a connection for the given connection key in the IQS configuration.
        /// </summary>
        /// <param name="key">Connection key</param>
        /// <returns>Database connection</returns>
        public IConnectionLink this[string key] => connections.ContainsKey(key) ? connections[key] : null;

        /// <summary>
        /// Checks whether the connection with given key exists
        /// </summary>
        /// <param name="connectionKey">Connection key</param>
        /// <returns>True if connection exists, false otherwise</returns>
        public bool ConnectionExists(string connectionKey) => connections.ContainsKey(connectionKey);
    }
}
