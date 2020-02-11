namespace Panelak.Database
{
    /// <summary>
    /// Container container of connection links.
    /// </summary>
    public interface IConnectionLinkContainer
    {
        /// <summary>
        /// Returns a connection link for given connection key
        /// </summary>
        /// <param name="connectionKey">Connection key</param>
        /// <returns>Connection link for given connection key</returns>
        IConnectionLink this[string connectionKey] { get; }

        /// <summary>
        /// Checks whether the connection with given key exists
        /// </summary>
        /// <param name="connectionKey">Connection key</param>
        /// <returns>True if connection with this connection key exists</returns>
        bool ConnectionExists(string connectionKey);
    }
}
