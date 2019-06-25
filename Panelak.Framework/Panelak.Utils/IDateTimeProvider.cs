using System;

namespace Panelak.Utils
{
    /// <summary>
    /// Provider for date and time
    /// </summary>
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
