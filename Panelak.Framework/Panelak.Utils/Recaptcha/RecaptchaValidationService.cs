namespace Panelak.Utils
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Service used for validating Google Recaptcha tokens.
    /// </summary>
    public class RecaptchaValidationService : ICaptchaTokenValidationService
    {
        /// <summary>
        /// Gets or sets the value indicating whether the token validation method contacts the validation service (true) or returns true immediately without checking anything (false).
        /// </summary>
        public bool Active { get; }

        /// <summary>
        /// Logging service
        /// </summary>
        private readonly ILogger<RecaptchaValidationService> logger;

        /// <summary>
        /// Optional proxy service through which token validation service is contacted.
        /// </summary>
        private readonly IProxyService proxyService;

        /// <summary>
        /// True if the service should throw exceptions on contacting the validation service, false if token 
        /// validation should return false instead in this case.
        /// </summary>
        private readonly bool throwExceptions;

        /// <summary>
        /// Secret key of the token validation service.
        /// </summary>
        private readonly string secretKey;

        /// <summary>
        /// Initializes a new instance of <see cref="RecaptchaValidationService" />.
        /// </summary>
        /// <param name="logger">Logger service</param>
        /// <param name="active">True if service is active, false otherwise.</param>
        /// <param name="secretKey">Sercret key</param>
        /// <param name="throwExceptions">True if exceptions should be thrown, false if validation should just fail instead</param>
        public RecaptchaValidationService(ILogger<RecaptchaValidationService> logger, bool active, string secretKey, bool throwExceptions = true)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.throwExceptions = throwExceptions;

            Active = active;
            if (Active)
                this.secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RecaptchaValidationService" />.
        /// </summary>
        /// <param name="logger">Logger service</param>
        /// <param name="proxyService">Proxy service</param>
        /// <param name="active">True if service is active, false otherwise.</param>
        /// <param name="secretKey">Sercret key</param>
        /// <param name="throwExceptions">True if exceptions should be thrown, false if validation should just fail instead</param>
        public RecaptchaValidationService(ILogger<RecaptchaValidationService> logger, IProxyService proxyService, bool active, string secretKey, bool throwExceptions = true)
            : this(logger, active, secretKey, throwExceptions)
            => this.proxyService = proxyService ?? throw new ArgumentNullException(nameof(proxyService));

        /// <summary>
        /// Validates the response token.
        /// </summary>
        /// <param name="token">Response token to validate</param>
        /// <returns>True if token is valid, false othrewise.</returns>
        public async Task<bool> ValidateAsync(string token)
        {
            if (!Active)
            {
                logger?.LogTrace($"Recaptcha: skipping validation of token \"{token}\" because recaptcha is inactive (Active = false).");
                return true;
            }

            if (String.IsNullOrEmpty(token))
            {
                logger?.LogWarning($"Recaptcha: Token is empty!");
                return false;
            }

            var verifyParamsEncoded = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "secret", secretKey },
                { "response", token }
            });

            var httpClient = new HttpClient();

            if (proxyService != null)
            {
                IWebProxy webProxy = proxyService.GetSystemProxy();
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
                if (throwExceptions)
                {
                    throw;
                }
                else
                {
                    logger?.LogError(default, e, "Recaptcha: Exception thrown during sending a validation request to Google Recaptcha API");
                    logger?.LogTrace("Recaptcha: returning false on token validation. (Recaptcha is configured to return false on token validation instead of throwing an exception.)");
                    return false;
                }
            }
            
            var resultJObject = JObject.Parse(resultStr);
            bool success = resultJObject["success"].Value<bool>();
            return success;
        }
    }
}
