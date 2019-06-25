namespace Panelak.Utils
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Win32;
    using System;
    using System.Net;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Service for obtaining proxy configuration.
    /// </summary>
    public class ProxyService : IProxyService
    {
        /// <summary>
        /// Logger service
        /// </summary>
        private readonly ILogger<ProxyService> logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ProxyService"/>.
        /// </summary>
        /// <param name="logger">Logger service</param>
        public ProxyService(ILogger<ProxyService> logger) 
            => this.logger = logger;

        /// <summary>
        /// Returns the proxy configured in the operating system.
        /// </summary>
        /// <returns></returns>
        public IWebProxy GetSystemProxy()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                logger?.LogInformation("ProxyService: Windows detected");
                RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", false);
                bool isProxyEnabled = ((int)registry.GetValue("ProxyEnable")) == 1 ? true : false;
                if (isProxyEnabled)
                {

                    string proxyValue = (string)registry.GetValue("ProxyServer");
                    logger?.LogInformation($"ProxyService: Proxy is enabled in Windows Internet Settings, using proxy \"{proxyValue}\".");

                    return new WebProxy()
                    {
                        Address = new Uri(proxyValue),
                        BypassProxyOnLocal = false,
                        UseDefaultCredentials = false
                    };
                }
                else
                {
                    logger?.LogInformation("ProxyService: no system proxy set");
                    return null;
                }

            }

            throw new PlatformNotSupportedException();
        }
    }
}
