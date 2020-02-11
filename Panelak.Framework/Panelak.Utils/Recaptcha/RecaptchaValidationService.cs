namespace Panelak.Utils
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Service used for validating Google Recaptcha tokens.
    /// </summary>
    public class RecaptchaValidationService : IRecaptchaTokenValidationService
    {
        /// <summary>
        /// Gets the recaptcha options
        /// </summary>
        public RecaptchaOptions Options { get; }

        /// <summary>
        /// Logging service
        /// </summary>
        private readonly ILogger<RecaptchaValidationService> logger;

        /// <summary>
        /// Optional proxy service through which token validation service is contacted.
        /// </summary>
        private readonly IProxyService proxyService;

        /// <summary>
        /// Initializes a new instance of <see cref="RecaptchaValidationService" />.
        /// </summary>
        /// <param name="recaptchaOptions">Recaptcha options</param>
        /// <param name="logger">Logger service</param>
        public RecaptchaValidationService(IOptions<RecaptchaOptions> recaptchaOptions, ILogger<RecaptchaValidationService> logger)
        {
            Options = recaptchaOptions.Value;
            this.logger = logger;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RecaptchaValidationService" />.
        /// </summary>
        /// <param name="recaptchaOptions">Recaptcha options</param>
        /// <param name="proxyService">Proxy service</param>
        /// <param name="logger">Logger service</param>
        public RecaptchaValidationService(IOptions<RecaptchaOptions> recaptchaOptions, IProxyService proxyService, ILogger<RecaptchaValidationService> logger)
            : this(recaptchaOptions, logger)
            => this.proxyService = proxyService ?? throw new ArgumentNullException(nameof(proxyService));

        /// <summary>
        /// Validates the response token.
        /// </summary>
        /// <param name="token">Response token to validate</param>
        /// <returns>True if token is valid, false othrewise.</returns>
        public async Task<bool> ValidateAsync(string token)
        {
            if (!Options.Active)
            {
                logger?.LogDebug($"Recaptcha: skipping validation of token \"{token}\" because recaptcha is inactive (Active = false).");
                return true;
            }

            if (String.IsNullOrEmpty(token))
            {
                logger?.LogWarning($"Recaptcha: Token is empty!");
                return false;
            }

            var verifyParamsEncoded = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "secret", Options.SecretKey },
                { "response", token }
            });

            var httpClient = new HttpClient();

            if (proxyService != null)
            {
                IWebProxy webProxy = proxyService.GetProxy();
                if (webProxy != null)
                {
                    var proxyHandler = new HttpClientHandler()
                    {
                        Proxy = webProxy,
                        UseProxy = true
                    };
                    httpClient = new HttpClient(proxyHandler);
                }
            }

            logger?.LogDebug($"Recaptcha: Sending validation request to \"https://www.google.com/recaptcha/api/siteverify\"");

            string resultStr;

            try
            {
                HttpResponseMessage recaptchaVerifyResult = await httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", verifyParamsEncoded);
                resultStr = await recaptchaVerifyResult.Content.ReadAsStringAsync();

                if (String.IsNullOrEmpty(resultStr))
                    throw new InvalidOperationException("Recaptcha returned result is an empty string");

            } 
            catch(Exception e)
            {
                if (Options.ThrowExceptions)
                {
                    throw;
                }
                else
                {
                    logger?.LogError(default, e, "Recaptcha: Exception thrown during sending a validation request to Google Recaptcha API");
                    logger?.LogDebug("Recaptcha: returning false on token validation. (Recaptcha is configured to return false on token validation instead of throwing an exception.)");
                    return false;
                }
            }
            
            var resultJObject = JObject.Parse(resultStr);
            bool success = resultJObject["success"].Value<bool>();

            if (success)
                logger?.LogDebug("Recaptcha: validation successful");
            else
                logger?.LogDebug("Recaptcha: validation failed");

            return success;
        }
    }
}
