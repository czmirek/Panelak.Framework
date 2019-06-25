namespace Panelak.Utils
{
    using System;

    /// <summary>
    /// Default date time provider using DateTime.Now
    /// </summary>
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
