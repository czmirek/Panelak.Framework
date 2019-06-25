﻿namespace Panelak.Utils
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
        public static IServiceCollection AddProxySupport(this IServiceCollection serviceCollection)
            => serviceCollection.AddSingleton<IProxyService, ProxyService>();

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
    }
}