namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public abstract class SingleViewController : BaseController
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Result is ViewResult vr)
            {
                vr.ViewName ??= context.Controller.GetType().Name.Replace("Controller", "");
                return;
            }

            if (context.Result is PartialViewResult pvr)
            {
                pvr.ViewName ??= ("_" + context.Controller.GetType().Name.Replace("Controller", ""));
                return;
            }
        }
    }
}
