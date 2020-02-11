namespace Panelak.Utils
{
    /// <summary>
    /// Provider to obtain the root of the page tree
    /// </summary>
    public interface IPageTreeProvider
    {
        /// <summary>
        /// Gets the root of the page tree
        /// </summary>
        IPageTree Root { get; }
    }
}
