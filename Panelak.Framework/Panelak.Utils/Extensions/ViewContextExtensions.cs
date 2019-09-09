namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// ViewContext extensions
    /// </summary>
    public static class ViewContextExtensions
    {
        public static T GetService<T>(this ViewContext viewContext) => viewContext.HttpContext.RequestServices.GetService<T>();
    }
}
