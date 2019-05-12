namespace Panelak.MvcUtils
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// Used to implement async ajax reload.
    /// </summary>
    [HtmlTargetElement(Attributes = "vc-action,vc-form,vc-target-id")]
    public class ViewComponentAttributeTagHelper : TagHelper
    {
        /// <summary>
        /// Action on which the ajax reload is invoked. Usually "submit".
        /// </summary>
        [HtmlAttributeName("vc-action")]
        public string VcAction { get; set; }

        /// <summary>
        /// ID of the HTML form which is to be submitted
        /// </summary>
        [HtmlAttributeName("vc-form")]
        public string VcForm { get; set; }

        /// <summary>
        /// ID of the element which is to be replaced from the response
        /// </summary>
        [HtmlAttributeName("vc-target-id")]
        public string VcTargetId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("data-vc-action", VcAction);
            output.Attributes.Add("data-vc-form", VcForm);
            output.Attributes.Add("data-vc-target-id", VcTargetId);
        }
    }
}
