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
    /// the operating system with the OS being preferred.
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
        /// If that proxy is not set, returns the proxy configuration from the
        /// configuration throught the <see cref="IOptionsMonitor"/>.
        /// </summary>
        /// <returns>Proxy configuration, null if no configuration was found.</returns>
        public IWebProxy GetProxy()
        {
            string proxyUrl = options?.CurrentValue?.ProxyUrl;
            if (!String.IsNullOrEmpty(proxyUrl))
            {
                if (Uri.TryCreate(proxyUrl, UriKind.Absolute, out Uri proxyUri))
                {
                    logger?.LogDebug($"ProxyService: creating proxy from configuration \"{proxyUrl}\"");

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
                logger?.LogDebug("ProxyService: Windows detected");

                const string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(subKey, false);

                if (registryKey == null)
                {
                    logger?.LogDebug($"ProxyService: registry \"{subKey}\" not found");
                    return null;
                }

                object proxyEnableRegistryKey = registryKey.GetValue("ProxyEnable");

                if (proxyEnableRegistryKey == null)
                {
                    logger?.LogDebug($"ProxyService: registry value \"ProxyEnable\" in \"{subKey}\" not found");
                    return null;
                }

                bool isProxyEnabled = (int)proxyEnableRegistryKey == 1 ? true : false;
                if (!isProxyEnabled)
                {
                    logger?.LogDebug("ProxyService: registry value \"ProxyEnable\" in \"{subKey}\" is set to false.");
                    return null;
                }

                string proxyValue = (string)registryKey.GetValue("ProxyServer");
                logger?.LogDebug($"ProxyService: Proxy is enabled in Windows Internet Settings, using proxy \"{proxyValue}\".");

                return new WebProxy()
                {
                    Address = new Uri(proxyValue),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false
                };
            }


            logger?.LogInformation("ProxyService: no proxy found.");
            return null;
        }
    }
}
