namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 
    /// </summary>
    public class ViewResultFacade
    {
        /// <summary>
        /// 
        /// </summary>
        public Controller Controller { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public ViewResultFacade(Controller controller) 
            => Controller = controller ?? throw new System.ArgumentNullException(nameof(controller));
    }
}
