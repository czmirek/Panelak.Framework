namespace Panelak.Utils
{
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Default page metadata
    /// </summary>
    [DebuggerDisplay("{UrlTemplate}")]
    public class PageInfo : IPageInfo
    {
        /// <summary>
        /// Gets or sets the URL template from the Route attribute
        /// </summary>
        public string UrlTemplate { get; internal set; }

        /// <summary>
        /// Gets or sets the reflected action from the action context
        /// </summary>
        public MethodInfo Action { get; internal set; }

        /// <summary>
        /// Gets or sets the localized caption
        /// </summary>
        public string Caption { get; internal set; }

        /// <summary>
        /// Gets or sets the localized header
        /// </summary>
        public string Header { get; internal set; }

        /// <summary>
        /// Gets or sets the value indicating whether the header should be used.
        /// </summary>
        public bool RenderHeader { get; internal set; }

        /// <summary>
        /// Gets or sets the value indicating whether the caption should be used.
        /// </summary>
        public bool RenderCaption { get; internal set; }

        /// <summary>
        /// Gets or sets the current url.
        /// </summary>
        public string CurrentUrl { get; internal set; }
    }
}
