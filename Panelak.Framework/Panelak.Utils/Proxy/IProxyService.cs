namespace Panelak.Utils
{
    using System.Net;

    /// <summary>
    /// Provides the <see cref="IWebProxy"/>.
    /// </summary>
    public interface IProxyService
    {
        /// <summary>
        /// Returns the current proxy configuration.
        /// </summary>
        /// <returns>Proxy configuration</returns>
        IWebProxy GetProxy();
    }
}
