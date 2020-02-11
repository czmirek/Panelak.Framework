namespace Panelak.Utils
{
    using System;

    public interface IStartupComponent 
    {
        Action<IConfigureServicesBuilder> ConfigureServices(Action<IConfigureServicesBuilder> next);
        Action<IConfigureBuilder> Configure(Action<IConfigureBuilder> next);
    }
}
