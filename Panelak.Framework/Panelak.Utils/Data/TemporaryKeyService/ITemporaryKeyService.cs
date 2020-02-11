namespace Panelak.Utils
{
    using System;

    /// <summary>
    /// Service to store and retreive temporary keys used in email confirmation and password reset links.
    /// </summary>
    public interface ITemporaryKeyService
    {
        /// <summary>
        /// Creates a new random key
        /// </summary>
        /// <returns>New random string key</returns>
        string CreateRandomKey();

        /// <summary>
        /// Stores any value of given type in the temporary repository for given key.
        /// The value will expire after given time.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="expireAfter">Time after which the value is removed from the repository</param>
        void Store<T>(string key, T value, TimeSpan expireAfter);

        /// <summary>
        /// Attempts to retreive the value of given type from the temporary repository.
        /// If retreival is successful, the value is removed from the repository.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>True if retreival was successful, false otherwise</returns>
        bool TryRetreive<T>(string key, out T value);

        /// <summary>
        /// Attempts to retreive the value of given type from the temporary repository
        /// without removing the value from the repository.
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>True if retreival was successful.</returns>
        bool TryPeek<T>(string key, out T value);
    }
}
