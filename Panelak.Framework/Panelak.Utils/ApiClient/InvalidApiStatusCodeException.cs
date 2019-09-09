namespace Panelak.Utils
{
    using System;
    using System.Net;
    public class InvalidApiStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public InvalidApiStatusCodeException(HttpStatusCode statusCode) => StatusCode = statusCode;
    }
}
