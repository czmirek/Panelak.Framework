namespace Panelak.Utils
{
    using System;

    /// <summary>
    /// Information about the page for ASP.NET Core controller actions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PageMetadataAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the resource type for localizable properties.
        /// </summary>
        public Type ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the page caption used for menu, lists etc.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the page header used as the main title in the page.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether the header should be rendered.
        /// </summary>
        public bool RenderHeader { get; set; } = true;

        /// <summary>
        /// Gets or sets the value indicating whether the caption should be rendered.
        /// </summary>
        public bool RenderCaption { get; set; } = true;
    }
}
