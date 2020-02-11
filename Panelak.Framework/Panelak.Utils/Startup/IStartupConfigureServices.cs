namespace Panelak.Utils
{
    using System;

    public interface IStartupConfigureServices
    {
        Action<IConfigureServicesBuilder> ConfigureServices(Action<IConfigureServicesBuilder> next);
    }
}
