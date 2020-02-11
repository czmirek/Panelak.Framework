namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;

    public abstract class MultiViewController : BaseController
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            string ctrlName = context.Controller.GetType().Name.Replace("Controller", "");
            string actionName = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;

            if (context.Result is ViewResult vr && String.IsNullOrEmpty(vr.ViewName))
            {
                vr.ViewName ??= $"{ctrlName}{actionName}";
                return;
            }

            if (context.Result is PartialViewResult pvr && String.IsNullOrEmpty(pvr.ViewName))
            {
                pvr.ViewName ??= $"_{ctrlName}{actionName}";
                return;
            }
        }
    }
}
