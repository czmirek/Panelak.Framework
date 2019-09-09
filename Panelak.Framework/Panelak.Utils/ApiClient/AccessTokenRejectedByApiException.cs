namespace Panelak.Utils
{
    using System;

    /// <summary>
    /// Exception thrown by the application in case of HTTP 401 Unauthorized response to
    /// the API request
    /// </summary>
    public class AccessTokenRejectedByApiException : Exception
    { }
}
