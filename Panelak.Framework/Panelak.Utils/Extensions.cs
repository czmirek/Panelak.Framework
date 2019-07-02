namespace Panelak.Utils
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Service extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds the proxy services
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddProxySupport(this IServiceCollection services)
            => services.AddSingleton<IProxyService, ProxyService>();

        /// <summary>
        /// Adds the proxy service
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="proxyUri">URL of proxy</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddProxySupport(this IServiceCollection services, string proxyUri)
            => services.Configure<ProxyOptions>(options =>
            {
                options.ProxyUrl = proxyUri;
            }).AddSingleton<IProxyService, ProxyService>();

        /// <summary>
        /// Adds the proxy service
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="config">Proxy configuration section</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddProxySupport(this IServiceCollection services, IConfigurationSection config)
            => services.Configure<ProxyOptions>(config)
                       .AddSingleton<IProxyService, ProxyService>();

        /// <summary>
        /// Adds the recaptcha services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="configuration">Recaptcha configuration section</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddRecaptchaService(this IServiceCollection services, IConfigurationSection configuration) 
            => services.Configure<RecaptchaOptions>(configuration)
                       .AddSingleton<IRecaptchaTokenValidationService, RecaptchaValidationService>()
                       .AddSingleton<RecaptchaFilter>();

        /// <summary>
        /// Adds the recaptcha services
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="active">Whether recaptcha is active</param>
        /// <param name="secretKey">Secret key</param>
        /// <param name="siteKey">Site key</param>
        /// <param name="throwExceptions">True if service throw exception on HTTP request fail for google API. False = return false on token validation instead.</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddRecaptchaService(this IServiceCollection services, bool active, string secretKey, string siteKey, bool throwExceptions = true) 
            => services.Configure<RecaptchaOptions>(options =>
                {
                    options.Active = active;
                    options.SecretKey = secretKey;
                    options.SiteKey = siteKey;
                    options.ThrowExceptions = throwExceptions;
                })
                .AddSingleton<IRecaptchaTokenValidationService, RecaptchaValidationService>()
                .AddSingleton<RecaptchaFilter>();

        /// <summary>
        /// Adds datetime provider
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddDateTimeProvider(this IServiceCollection services) 
            => services.AddSingleton<IDateTimeProvider, DefaultDateTimeProvider>();

        /// <summary>
        /// Serializes the object to XML using <see cref="XmlSerializer"/>.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="value">Instance of object</param>
        /// <returns>Serialized XML</returns>
        public static string ToXml<T>(this T value)
        {
            if (value == null)
                return String.Empty;
            
            var xmlserializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlserializer.Serialize(writer, value);
                return stringWriter.ToString();
            }
        }
    }
}
