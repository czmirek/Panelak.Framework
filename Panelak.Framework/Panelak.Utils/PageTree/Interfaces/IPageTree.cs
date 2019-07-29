namespace Panelak.Utils
{
    using System.Collections.Generic;

    /// <summary>
    /// Tree node of the page tree of the site
    /// </summary>
    public interface IPageTree
    {
        /// <summary>
        /// Gets the tree node data
        /// </summary>
        IPageInfo Page { get; }

        /// <summary>
        /// Gets the tree parent of this node
        /// </summary>
        IPageTree Parent { get; }

        /// <summary>
        /// Gets the tree children of this node
        /// </summary>
        IEnumerable<IPageTree> Children { get; }
    }
}
