namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Relative to absolute redirect rewriter extension
    /// </summary>
    public static class RelativeToAbsoluteRedirectRewriterExtension
    {
        /// <summary>
        /// Adds a middleware which detects HTTP 302 or 301 responses and if the Location header
        /// contains a relative path which starts with '/' then it will get the absolute URL from
        /// <see cref="IHostUrlConfiguration"/> and change to location to absolute path.
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <returns>Application builder</returns>
        public static IApplicationBuilder UseRelativeToAbsoluteRedirectRewritter(this IApplicationBuilder app) 
            => app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 302 || context.Response.StatusCode == 301)
                {
                    string location = context.Response.Headers["Location"][0];
                    if (location.StartsWith("/"))
                    {
                        IHostUrlConfiguration hostUrl = context.RequestServices.GetRequiredService<IHostUrlConfiguration>();
                        string absoluteLocation = hostUrl.HostUrl.ToString().TrimEnd('/') + location;
                        context.Response.Headers["Location"] = absoluteLocation;
                    }
                }
            });
    }
}
