namespace Panelak.MvcUtils
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// Shows the success div when the TempData["success"] is set.
    /// </summary>
    [HtmlTargetElement("success", TagStructure = TagStructure.WithoutEndTag)]
    public class SuccessMessageTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext.TempData["success"] != null)
            {
                output.TagName = "div";
                output.TagMode = TagMode.StartTagAndEndTag;
                output.Attributes.SetAttribute("class", "alert alert-success");
                output.Content.SetHtmlContent($"<i class=\"fas fa-check\"></i> {Message}");
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}
