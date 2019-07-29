namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;

    /// <summary>
    /// Shows a validation error from the model state for given model expression (<see cref="For"/>)
    /// or for key in the model state (<see cref="ForKey"/>).
    /// </summary>
    [HtmlTargetElement("validation-errors", TagStructure = TagStructure.WithoutEndTag)]
    public class ValidationErrorsTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("validation-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("validation-for-key")]
        public string ForKey { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            ModelValidationState validationState;

            if (For != null)
                validationState = ViewContext.ViewData.ModelState.GetFieldValidationState(For.Name);
            else if (ForKey != null)
                validationState = ViewContext.ViewData.ModelState.GetValidationState(ForKey);
            else
                throw new InvalidOperationException("ForKey or For must be specified");

            if (validationState == ModelValidationState.Invalid)
            {
                ViewContext.ViewData.ModelState.TryGetValue(For?.Name ?? ForKey, out ModelStateEntry errors);

                string errorsHtml = "";

                foreach (ModelError error in errors.Errors)
                {
                    errorsHtml += $"<div class=\"alert alert-danger\">" +
                                        $"<i class=\"fas fa-times\"></i> {error.ErrorMessage}" +
                                  $"</div>";
                }

                output.TagName = "div";
                output.TagMode = TagMode.StartTagAndEndTag;

                string classes = $"validation-errors {For?.Name ?? ForKey}-validation-error";

                if (output.Attributes["class"] != null)
                    classes += " " + output.Attributes["class"].Value.ToString();

                output.Attributes.SetAttribute("class", classes);
                output.Content.SetHtmlContent(errorsHtml);
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}
