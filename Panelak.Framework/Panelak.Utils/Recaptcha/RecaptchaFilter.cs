namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Action filter for validating the recaptcha response token in a form request.
    /// </summary>
    public class RecaptchaFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Logger service
        /// </summary>
        private readonly ILogger<RecaptchaFilter> logger;
        
        /// <summary>
        /// Recaptcha validation service
        /// </summary>
        public IRecaptchaTokenValidationService Service { get; }

        /// <summary>
        /// Recaptcha options
        /// </summary>
        public RecaptchaOptions Options { get; }

        /// <summary>
        /// Custom action invoked when recaptcha token is invalid
        /// </summary>
        private readonly Action<ActionExecutingContext> onRecaptchaInvalid;

        /// <summary>
        /// Initializes a new instance of <see cref="RecaptchaFilter"/>.
        /// </summary>
        /// <param name="service">Recaptcha validation service</param>
        /// <param name="options">Recaptcha options</param>
        /// <param name="logger">Logger service</param>
        public RecaptchaFilter(IRecaptchaTokenValidationService service, 
            IOptions<RecaptchaOptions> options,
            ILogger<RecaptchaFilter> logger)
        {
            this.logger = logger;
            Service = service ?? throw new ArgumentNullException(nameof(service));
            Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RecaptchaFilter"/>.
        /// </summary>
        /// <param name="service">Recaptcha validation service</param>
        /// <param name="options">Recaptcha options</param>
        /// <param name="onRecaptchaInvalid">Custom action invoked when recaptcha token is invalid</param>
        /// <param name="logger">Logger</param>
        public RecaptchaFilter(IRecaptchaTokenValidationService service,
            IOptions<RecaptchaOptions> options,
            Action<ActionExecutingContext> onRecaptchaInvalid,
            ILogger<RecaptchaFilter> logger) : this(service, options, logger) 
            => this.onRecaptchaInvalid = onRecaptchaInvalid ?? throw new ArgumentNullException(nameof(onRecaptchaInvalid));

        /// <summary>
        /// Validates the recaptcha token found in a form response.
        /// </summary>
        /// <param name="context">Action executing context</param>
        /// <param name="next">Pipeline delegate</param>
        /// <returns>Task result</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (Options.Active)
            {
                logger?.LogDebug($"RecaptchaFilter: Recaptcha is active. Reading form request value \"{RecaptchaOptions.FormParameterName}\"");

                if (context.HttpContext.Request.Form[RecaptchaOptions.FormParameterName].Count > 0)
                {
                    string token = context.HttpContext.Request.Form[RecaptchaOptions.FormParameterName][0];
                    logger?.LogDebug($"RecaptchaFilter: Found \"{RecaptchaOptions.FormParameterName}\" = {token}");

                    bool recaptchaValid = await Service.ValidateAsync(token);

                    if (!recaptchaValid)
                    {
                        if (onRecaptchaInvalid != null)
                            onRecaptchaInvalid.Invoke(context);
                        else
                            context.ModelState.AddModelError(RecaptchaOptions.FormParameterName, Recaptcha.Resources.Recaptcha.RecaptchaModelError);
                    }
                }
                else
                {
                    logger?.LogDebug($"RecaptchaFilter: Token not found");
                }
            }
            await next();
        }
    }

}
