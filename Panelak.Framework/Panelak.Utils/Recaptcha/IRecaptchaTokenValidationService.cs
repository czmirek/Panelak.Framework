namespace Panelak.Utils
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a token validation service for captcha purposes.
    /// </summary>
    public interface IRecaptchaTokenValidationService
    {
        /// <summary>
        /// Validates the captcha token
        /// </summary>
        /// <param name="token">Captcha token</param>
        /// <returns>True if token is valid, false otherwise</returns>
        Task<bool> ValidateAsync(string token);
    }
}
