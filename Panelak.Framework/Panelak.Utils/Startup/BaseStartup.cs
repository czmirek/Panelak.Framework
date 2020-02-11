namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Startup class which automatically:
    /// - finds "ApplicationConfiguration", registers it, binds the IConfiguration to it and calls its "Validate" method if found
    /// - throws exception if a single ApplicationConfiguration does not exist
    /// - registers services by all classes implementing <see cref="IStartupConfigureServices"/>
    /// - validates all services implementing <see cref="IStartupValidatable"/>
    /// - uses <see cref="GetOrderedStartupFilters"/> to register and run all services implementing <see cref="IStartupFilter"/>
    /// 
    /// Since it's not abstract, it can be used directly or it can be completely overriden.
    /// </summary>
    public class BaseStartup
    {
        private readonly IEnumerable<Type> startupCfgServices;
        private readonly IEnumerable<Type> startupValidatableServices;
        private readonly object appConfigurationObject;
        private readonly IConfiguration configuration;
        private List<IStartupComponent> startupComponents;

        public BaseStartup(IConfiguration configuration)
        {
            Type[] types = GetAssembly().GetTypes();
            Type iStartupConfigureServicesType = typeof(IStartupConfigureServices);
            Type iStartupValidatableType = typeof(IStartupValidatable);
            Type iStartupFilterType = typeof(IStartupFilter);
            Type iStartupComponent = typeof(IStartupComponent);

            startupCfgServices = types.Where(t => !t.Equals(iStartupConfigureServicesType) && iStartupConfigureServicesType.IsAssignableFrom(t));
            startupValidatableServices = types.Where(t => !t.Equals(iStartupValidatableType) && iStartupValidatableType.IsAssignableFrom(t));
            
            appConfigurationObject = SetupConfiguration(configuration);
            this.configuration = configuration;
        }

        public virtual Assembly GetAssembly() => Assembly.GetEntryAssembly();
        public virtual void ConfigureServices(IServiceCollection services)
        {
            if(appConfigurationObject != null)
                services.AddSingleton(appConfigurationObject.GetType(), appConfigurationObject);

            foreach (Type startupFilterType in GetOrderedStartupFiltersPrivate())
                services.AddSingleton(typeof(IStartupFilter), startupFilterType);

            foreach (Type startupFilterType in GetOrderedStartupFilters())
                services.AddSingleton(typeof(IStartupFilter), startupFilterType);

            foreach (Type validatableType in startupValidatableServices)
                services.AddSingleton(typeof(IStartupValidatable), validatableType);

            var serviceBuilder = new ConfigureServicesBuilder(services, new Dictionary<string, object>()
            {
                { "IConfiguration", configuration },
                { "Configuration", appConfigurationObject }
            });

            var nextCfgServices = new Action<IConfigureServicesBuilder>(_ => { });
            
            startupComponents = new List<IStartupComponent>();
            foreach (Type startupComponent in GetOrderedStartupComponents().Reverse())
            {
                var component = Activator.CreateInstance(startupComponent) as IStartupComponent;
                nextCfgServices = component.ConfigureServices(nextCfgServices);

                startupComponents.Add(component);
            }

            foreach (Type cfgServiceType in startupCfgServices.Reverse())
            {
                var cfg = Activator.CreateInstance(cfgServiceType) as IStartupConfigureServices;
                nextCfgServices = cfg.ConfigureServices(nextCfgServices);
            }

            nextCfgServices(serviceBuilder);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Properties["IConfiguration"] = configuration; 
            app.Properties["Configuration"] = appConfigurationObject;
            
            var appBuilderNext = new Action<IConfigureBuilder>(_ => { });
            foreach (IStartupComponent component in startupComponents)
                appBuilderNext = component.Configure(appBuilderNext);

            appBuilderNext(new ConfigureBuilder(app, env));
        }

        public virtual IEnumerable<Type> GetOrderedStartupFilters()
        {
            return Enumerable.Empty<Type>();
        }

        public virtual IEnumerable<Type> GetOrderedStartupComponents()
        {
            return Enumerable.Empty<Type>();
        }

        protected virtual object SetupConfiguration(IConfiguration configuration)
        {
            IEnumerable<Type> appConfigTypes = GetAssembly().GetTypes().Where(t => t.Name == "ApplicationConfiguration");
            if (appConfigTypes.Count() != 1)
                throw new InvalidProgramException("\"ApplicationConfiguration\" class has not been found. Please specify this class exaclty once for configuration binding.");

            Type appConfigType = appConfigTypes.First();

            object appConfigObj = Activator.CreateInstance(appConfigType);
            configuration.Bind(appConfigObj, o => o.BindNonPublicProperties = true);

            MethodInfo validateMethod = appConfigType.GetMethod("Validate");
            if (validateMethod != null)
                validateMethod.Invoke(appConfigObj, null);

            return appConfigObj;
        }
        private IEnumerable<Type> GetOrderedStartupFiltersPrivate()
        {
            yield return typeof(StartupValidatableFilter);
        }
    }
}
