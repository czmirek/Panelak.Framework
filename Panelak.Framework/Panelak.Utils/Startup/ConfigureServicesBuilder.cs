namespace Panelak.Utils
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;

    public class ConfigureServicesBuilder : IConfigureServicesBuilder
    {
        public IServiceCollection Services { get; }
        public IDictionary<string, object> Properties { get; }

        internal ConfigureServicesBuilder(IServiceCollection services, IDictionary<string, object> properties)
        {
            this.Services = services ?? throw new ArgumentNullException(nameof(services));
            this.Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }
    }
}
