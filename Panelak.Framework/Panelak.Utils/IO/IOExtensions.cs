namespace Panelak.Utils
{
    using System;
    using System.IO;

    /// <summary>
    /// Extensions related to IO operations
    /// </summary>
    public static class IOExtensions
    {
        /// <summary>
        /// Checks whether the file exists and throws exception if not. The check is
        /// applied on the path and on the combined path with current domain base directory
        /// and the returned string contains the path in which the file has been found.
        /// </summary>
        /// <param name="path">String containing the path to the file</param>
        /// <returns>Path to the file where the file has been found</returns>
        public static string GetPathWithCheck(this string path)
        {
            string filePath = path;
            string domainLocalPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            
            if (File.Exists(filePath))
                return filePath;
            else if (File.Exists(domainLocalPath))
                return domainLocalPath;
            else
                throw new InvalidOperationException($"File \"{path}\" not found");
        }
    }
}
