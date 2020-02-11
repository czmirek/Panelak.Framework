namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    public interface IConfigureBuilder
    {
        IApplicationBuilder ApplicationBuilder { get; }
        IWebHostEnvironment Environment { get; }
    }
}