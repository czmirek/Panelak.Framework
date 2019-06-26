namespace Panelak.Utils
{
    /// <summary>
    /// Options for proxy service
    /// </summary>
    public class ProxyOptions
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ProxyOptions"/>.
        /// </summary>
        public ProxyOptions()
        {
        }

        /// <summary>
        /// Gets the URL of the proxy
        /// </summary>
        public string ProxyUrl { get; set; }
    }
}
