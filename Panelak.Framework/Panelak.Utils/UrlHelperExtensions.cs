namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Collection of extensions methods on MVC IUrlHelper
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Returns the host's URL implemented via <see cref="IHostUrlConfiguration"/>.
        /// </summary>
        /// <param name="helper">Url helper</param>
        /// <returns>Host URL string</returns>
        public static string GetHostUrl(this IUrlHelper helper)
        {
            var hostUrlConfig = helper.ActionContext.HttpContext.RequestServices.GetService(typeof(IHostUrlConfiguration)) as IHostUrlConfiguration;
            return hostUrlConfig.HostUrl.ToString();
        }

        /// <summary>
        /// Helper method to combine an absolute and a relative path.
        /// </summary>
        /// <param name="path1">Absolute path</param>
        /// <param name="path2">Relative path</param>
        /// <returns>Url</returns>
        private static string CombinePaths(string path1, string path2) => path1.TrimEnd('/') + "/" + path2.TrimStart('/'); public static string AbsolutePath(this IUrlHelper helper, string relativePath)
        {
            string hostUrl = helper.GetHostUrl();
            return CombinePaths(hostUrl, relativePath);
        }

        /// <summary>
        /// Extensions of TypedRouting Action helper method, taking an absolute url from <see cref="IHostUrlConfiguration"/>.
        /// </summary>
        /// <typeparam name="TController">Controller type</typeparam>
        /// <param name="helper">Url helper</param>
        /// <param name="action">Action expression</param>
        /// <returns>Absolute Url</returns>
        public static string AbsoluteAction<TController>(this IUrlHelper helper, Expression<Action<TController>> action) where TController : class
        {
            string relativePath = helper.Action(action);
            return helper.AbsolutePath(relativePath);
        }

        /// <summary>
        /// Extensions of TypedRouting Action helper method, taking an absolute url from <see cref="IHostUrlConfiguration"/>.
        /// </summary>
        /// <typeparam name="TController">Controller type</typeparam>
        /// <param name="helper">Url helper</param>
        /// <param name="action">Action expression</param>
        /// <returns>Absolute Url</returns>
        public static string AbsoluteAction<TController>(this IUrlHelper helper, Expression<Func<TController, Task>> action) where TController : class
        {
            string relativePath = helper.Action(action);
            return helper.AbsolutePath(relativePath);
        }
    }
}
