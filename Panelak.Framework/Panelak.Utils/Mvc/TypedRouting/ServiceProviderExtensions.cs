namespace Panelak.Utils
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ServiceProviderExtensions
    {
        public static IExpressionRouteHelper GetExpressionRouteHelper(this IServiceProvider serviceProvider)
        {
            IExpressionRouteHelper expressionRouteHelper = serviceProvider?.GetService<IExpressionRouteHelper>();
            if (expressionRouteHelper == null)
            {
                throw new InvalidOperationException("'AddTypedRouting' must be called after 'AddMvc' in order to use typed routing and link generation.");
            }

            return expressionRouteHelper;
        }
    }
}
