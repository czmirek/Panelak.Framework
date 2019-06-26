namespace Panelak.Utils
{
    using System.Net;

    public interface IProxyService
    {
        IWebProxy GetProxy();
    }
}
