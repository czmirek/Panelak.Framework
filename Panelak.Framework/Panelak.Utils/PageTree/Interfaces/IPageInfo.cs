namespace Panelak.Utils
{
    /// <summary>
    /// Defines the URL template and other properties of the site page.
    /// </summary>
    public interface IPageInfo
    {
        /// <summary>
        /// Url template used for this site.
        /// </summary>
        string UrlTemplate { get; }

        /// <summary>
        /// Gets the dynamically generated URL for pages in the active page tree.
        /// </summary>
        string CurrentUrl { get; }

        /// <summary>
        /// Gets the caption of the page intended to be used for breadcrumbs.
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Gets the header text of the page.
        /// </summary>
        string Header { get; }

        /// <summary>
        /// Gets the value indicating whether the header text should be rendered for this page.
        /// </summary>
        bool RenderHeader { get; }

        /// <summary>
        /// Gets the value indicating whether the caption text should be rendered for this page.
        /// </summary>
        bool RenderCaption { get; }
    }
}
