namespace Panelak.Utils
{
    using System;

    public interface IDynamicPageMapModel<T> : IPageMapModel where T : ITuple
    {
        Func<T, string> GetUrl();
    }
}
