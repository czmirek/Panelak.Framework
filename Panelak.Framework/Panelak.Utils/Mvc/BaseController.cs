namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc;
    public class BaseController : Controller
    {
        public BaseController() => ViewResult = new ViewResultFacade(this);

        protected void SetSuccess()
        {
            TempData["success"] = true;
        }

        public ViewResultFacade ViewResult { get; }
    }
}
