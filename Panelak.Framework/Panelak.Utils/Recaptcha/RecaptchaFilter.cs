namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
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
        private readonly RecaptchaValidationService rvService;

        private readonly string modelParameterName;
        private readonly string modelErrorMessage = null;
        private readonly Action<ActionExecutingContext> onRecaptchaInvalid;

        /// <summary>
        /// Initializes a new instance of <see cref= "RecaptchaFilter" />.
        /// </summary>
        /// <param name="logger">Logger service</param>
        /// <param name="rvService">Recaptcha service</param>
        /// <param name="modelParameterName">Model parameter name</param>
        /// <param name="modelErrorMessage">Model error message</param>
        public RecaptchaFilter(RecaptchaValidationService rvService, 
            string modelParameterName,
            ILogger<RecaptchaFilter> logger = null,
            string modelErrorMessage = null)
        {
            this.logger = logger;
            this.rvService = rvService ?? throw new ArgumentNullException(nameof(rvService));
            this.modelParameterName = modelParameterName ?? throw new ArgumentNullException(nameof(modelParameterName));
            this.modelErrorMessage = modelErrorMessage ?? throw new ArgumentNullException(nameof(modelErrorMessage));
        }

        /// <summary>
        /// Initializes a new instance of <see cref= "RecaptchaFilter" />.
        /// </summary>
        /// <param name="logger">Logger service</param>
        /// <param name="rvService">Recaptcha service</param>
        /// <param name="onRecaptchaInvalid">Method for custom context configuration after recaptcha validation failiure.</param>
        public RecaptchaFilter(RecaptchaValidationService rvService,
            Action<ActionExecutingContext> onRecaptchaInvalid,
            ILogger<RecaptchaFilter> logger = null)
        {
            this.logger = logger;
            this.rvService = rvService ?? throw new ArgumentNullException(nameof(rvService));
            this.onRecaptchaInvalid = onRecaptchaInvalid ?? throw new ArgumentNullException(nameof(onRecaptchaInvalid));
        }

        /// <summary>
        /// Validates the recaptcha token found in a form response.
        /// </summary>
        /// <param name="context">Action executing context</param>
        /// <param name="next">Pipeline delegate</param>
        /// <returns>Task result</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (rvService.Active)
            {
                logger?.LogTrace($"RecaptchaFilter: Recaptcha is active. Reading form request value \"{modelParameterName}\"");

                if (context.HttpContext.Request.Form[modelParameterName].Count > 0)
                {
                    string token = context.HttpContext.Request.Form[modelParameterName][0];
                    logger?.LogTrace($"RecaptchaFilter: Found \"{modelParameterName}\" = {token}");

                    bool recaptchaValid = await rvService.ValidateAsync(token);

                    if (!recaptchaValid)
                    {
                        if (onRecaptchaInvalid != null)
                            onRecaptchaInvalid.Invoke(context);
                        else
                            context.ModelState.AddModelError(modelParameterName, modelErrorMessage ?? Recaptcha.Resources.Recaptcha.RecaptchaModelError);
                    }
                }
                else
                {
                    logger?.LogTrace($"RecaptchaFilter: Token not found");
                }
            }
            await next();
        }
    }

}
