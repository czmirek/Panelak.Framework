namespace Panelak.Utils
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.Win32;
    using System;
    using System.Net;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Service for obtaining proxy configuration either from configured options or from
    /// window registry.
    /// </summary>
    public class ProxyService : IProxyService
    {
        /// <summary>
        /// Proxy options
        /// </summary>
        private readonly IOptionsMonitor<ProxyOptions> options;

        /// <summary>
        /// Logger service
        /// </summary>
        private readonly ILogger<ProxyService> logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ProxyService"/>.
        /// </summary>
        /// <param name="logger">Logger service</param>
        public ProxyService(IOptionsMonitor<ProxyOptions> options, ILogger<ProxyService> logger)
        {
            this.options = options;
            this.logger = logger;
        }

        /// <summary>
        /// Returns the proxy configured in the operating system.
        /// </summary>
        /// <returns></returns>
        public IWebProxy GetProxy()
        {
            string proxyUrl = options.CurrentValue.ProxyUrl;
            if (!String.IsNullOrEmpty(proxyUrl))
            {
                if (Uri.TryCreate(proxyUrl, UriKind.Absolute, out Uri proxyUri))
                {
                    logger?.LogInformation($"ProxyService: creating proxy from configuration \"{proxyUrl}\"");

                    return new WebProxy()
                    {
                        Address = proxyUri,
                        BypassProxyOnLocal = false,
                        UseDefaultCredentials = false
                    };
                }
                else
                {
                    logger?.LogError($"ProxyService: ERROR - invalid URL in proxy config \"{proxyUrl}\". " +
                                     $"Set the value to empty or remove it from configuration to automatically use " +
                                     $"current user's proxy settings without generating this error message.");
                }
            }

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
