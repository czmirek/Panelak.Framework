namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.IO;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    /// <summary>
    /// asdf
    /// </summary>
    public abstract class ComponentTagHelper : TagHelper
    {
        private readonly IHtmlHelper htmlHelper;
        private readonly HtmlEncoder htmlEncoder;

        /// <summary>
        /// ViewContext
        /// </summary>
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Creates a new instance of ComponentTagHelper.
        /// </summary>
        /// <param name="htmlHelper">Html helper</param>
        /// <param name="htmlEncoder">Html encoder</param>
        public ComponentTagHelper(IHtmlHelper htmlHelper, HtmlEncoder htmlEncoder)
        {
            this.htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
            this.htmlEncoder = htmlEncoder ?? throw new ArgumentNullException(nameof(htmlEncoder));
        }

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
        protected abstract object GetViewModel();

        /// <summary>
        /// Processes the tag helper
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="output">Output</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            SetTag(output);

            (htmlHelper as IViewContextAware).Contextualize(ViewContext);

            Microsoft.AspNetCore.Html.IHtmlContent partial = await htmlHelper.PartialAsync(GetView(), GetViewModel());
            var writer = new StringWriter();
            partial.WriteTo(writer, htmlEncoder);
            output.Content.SetHtmlContent(writer.ToString());
        }
    }
}
