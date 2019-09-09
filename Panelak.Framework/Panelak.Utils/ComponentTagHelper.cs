namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.IO;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    /// <summary>
    /// Tag helper with view context support with component-like rendering through a view with a view model
    /// </summary>
    public abstract class ComponentTagHelper : TagHelper
    {
        /// <summary>
        /// ViewContext
        /// </summary>
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Configures the TagHelperOutput
        /// </summary>
        /// <param name="output">Output</param>
        protected virtual void SetTag(TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        /// <summary>
        /// Returns the path of the view
        /// </summary>
        /// <returns>Path of the view</returns>
        protected abstract string GetView();

        /// <summary>
        /// Returns the generated ViewModel of the View.
        /// </summary>
        /// <returns></returns>
        protected abstract Task<object> GetViewModelAsync();

        /// <summary>
        /// Processes the tag helper
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            SetTag(output);

            IHtmlHelper htmlHelper = ViewContext.GetService<IHtmlHelper>();
            HtmlEncoder htmlEncoder = ViewContext.GetService<HtmlEncoder>();

            (htmlHelper as IViewContextAware).Contextualize(ViewContext);

            Microsoft.AspNetCore.Html.IHtmlContent partial = await htmlHelper.PartialAsync(GetView(), await GetViewModelAsync());
            var writer = new StringWriter();
            partial.WriteTo(writer, htmlEncoder);
            output.Content.SetHtmlContent(writer.ToString());
        }
    }
}
