namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using System;
    using System.Collections.Generic;

    public class StartupValidatableFilter : IStartupFilter
    {
        private readonly IEnumerable<IStartupValidatable> validatableObjects;
        public StartupValidatableFilter(IEnumerable<IStartupValidatable> validatableObjects) 
            => this.validatableObjects = validatableObjects;

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            foreach (IStartupValidatable validatableObject in validatableObjects)
                validatableObject.Validate();
            
            return next;
        }
    }
}
