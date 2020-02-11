namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using System;

    public class ConfigureBuilder : IConfigureBuilder
    {
        internal ConfigureBuilder(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment)
        {
            this.ApplicationBuilder = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));
            this.Environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public IApplicationBuilder ApplicationBuilder { get; }

        public IWebHostEnvironment Environment { get; }
    }
}
