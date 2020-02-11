namespace Panelak.Utils
{
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Tree node of the page tree of the site
    /// </summary>
    [DebuggerDisplay("{Page.UrlTemplate}")]
    public class PageTree : IPageTree
    {
        /// <summary>
        /// Gets or sets the tree node data
        /// </summary>
        public PageInfo Page { get; internal set; }

        /// <summary>
        /// Gets or sets the tree parent of this node
        /// </summary>
        public PageTree Parent { get; internal set; }

        /// <summary>
        /// Gets or sets the tree children of this node
        /// </summary>
        public List<PageTree> Children { get; internal set; } = new List<PageTree>();

        /// <summary>
        /// The tree node data
        /// </summary>
        IPageInfo IPageTree.Page => Page;

        /// <summary>
        /// The tree parent of this node
        /// </summary>
        IPageTree IPageTree.Parent => Parent;
        
        /// <summary>
        /// The tree children of this node
        /// </summary>
        IEnumerable<IPageTree> IPageTree.Children => Children;
    }
}
