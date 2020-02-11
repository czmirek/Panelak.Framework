namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Options;
    using System;

    /// <summary>
    /// Used to mark buttons in anonymously accessible forms to protect the site from bots by google recaptcha.
    /// </summary>
    [HtmlTargetElement(Attributes = "recaptcha")]
    public class RecaptchaTagHelper : TagHelper
    {
        private readonly RecaptchaOptions recaptchaOptions;

        public RecaptchaTagHelper(IOptions<RecaptchaOptions> recaptchaOptions) 
            => this.recaptchaOptions = recaptchaOptions?.Value ?? throw new ArgumentNullException(nameof(recaptchaOptions));

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (recaptchaOptions.Active)
            {
                output.Attributes.SetAttribute("data-recaptcha", true);
                output.Attributes.SetAttribute("data-sitekey", recaptchaOptions.SiteKey);
            }
        }
    }

}
