namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("partial-view-ajax", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PartialViewAjaxTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool OnResponseRefresh { get; set; }
        public string Class { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string onResponse = "";
            if (OnResponseRefresh)
                onResponse = "data-on-response-refresh=\"true\"";

            output.TagName = "";
            output.PreContent.SetHtmlContent($"<div id=\"{Id}\" class=\"partial-view-ajax-content {Class}\" data-url=\"{Url}\" {onResponse}>");
            output.PostContent.SetHtmlContent($"</div>");
        }
    }
}
