namespace Panelak.Utils
{
    /// <summary>
    /// Options for Recaptcha
    /// </summary>
    public class RecaptchaOptions
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RecaptchaOptions"/>.
        /// </summary>
        public RecaptchaOptions()
        {

        }

        /// <summary>
        /// See Panelak.Utils.Recaptcha.js
        /// </summary>
        public const string FormParameterName = "RecaptchaToken";

        /// <summary>
        /// True if recaptcha is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Site key
        /// </summary>
        public string SiteKey { get; set; }

        /// <summary>
        /// Secret key
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether the token validation service should throw exceptions 
        /// (e.g. in case of Recaptcha API is not available) or just silently fail and return false that token
        /// could not validated.
        /// </summary>
        public bool ThrowExceptions { get; set; }
    }
}
