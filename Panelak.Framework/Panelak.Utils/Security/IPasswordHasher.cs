namespace Panelak.Utils
{
    /// <summary>
    /// Provides password hashing abilities
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes the string password
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Hashed password</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifies the string password against hash
        /// </summary>
        /// <param name="passwordHash">Password hash</param>
        /// <param name="password">Password</param>
        /// <returns>True if the hash corresponds to the password, false otherwise.</returns>
        bool VerifyHashedPassword(string passwordHash, string password);
    }
}
