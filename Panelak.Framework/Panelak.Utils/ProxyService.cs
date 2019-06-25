namespace Panelak.Utils
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Win32;
    using System;
    using System.Net;
    using System.Runtime.InteropServices;

    public class ProxyService : IProxyService
    {
        private readonly ILogger<ProxyService> logger;

        public ProxyService(ILogger<ProxyService> logger) 
            => this.logger = logger;

        public IWebProxy GetSystemProxy()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                logger?.LogTrace("ProxyService: Windows detected");
                RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", false);
                bool isProxyEnabled = ((int)registry.GetValue("ProxyEnable")) == 1 ? true : false;
                if (isProxyEnabled)
                {

                    string proxyValue = (string)registry.GetValue("ProxyServer");
                    logger?.LogTrace($"ProxyService: Proxy is enabled in Windows Internet Settings, loaded proxy value {proxyValue}");

                    return new WebProxy()
                    {
                        Address = new Uri(proxyValue),
                        BypassProxyOnLocal = false,
                        UseDefaultCredentials = false
                    };
                }
                else
                {
                    logger?.LogTrace("ProxyService: no system proxy set");
                    return null;
                }

            }

            throw new PlatformNotSupportedException();
        }
    }
}
