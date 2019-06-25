namespace Panelak.Utils
{
    using System;

    /// <summary>
    /// Represents a configuration with <see cref="HostUrl"/> property which points to the absolute URL
    /// of a website project. This is used in helper methods for generating absolute URLs in links and 
    /// HTTP redirects.
    /// </summary>
    public interface IHostUrlConfiguration
    {
        /// <summary>
        /// Absolute URL of an website project including path.
        /// </summary>
        Uri HostUrl { get; }
    }
}
