namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement(Attributes = "partial-view-form-button")]
    public class PartialViewFormButtonTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("data-partial-view-form-button", "true");
        }
    }
}
