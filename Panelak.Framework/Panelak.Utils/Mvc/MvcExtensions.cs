namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.DependencyInjection;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ViewContext extensions
    /// </summary>
    public static class MvcExtensions
    {
        public static T GetService<T>(this ViewContext viewContext) => viewContext.HttpContext.RequestServices.GetService<T>();
        public static T GetService<T>(this ValidationContext validationContext) => (T)validationContext.GetService(typeof(T));
    }
}
