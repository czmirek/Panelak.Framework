namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.Razor;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Expander to find component views
    /// </summary>
    public class ComponentViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.ViewName.StartsWith("Components"))
                return new string[] { "/{0}" + RazorViewEngine.ViewExtension };

            return viewLocations.Union(new string[] { "/Components/{0}/{0}" + RazorViewEngine.ViewExtension });
        }

        public void PopulateValues(ViewLocationExpanderContext context) { }
    }
}
