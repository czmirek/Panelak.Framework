namespace Panelak.Utils
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    public interface IConfigureServicesBuilder
    {
        IServiceCollection Services { get; }
        IDictionary<string, object> Properties { get; }
    }
}