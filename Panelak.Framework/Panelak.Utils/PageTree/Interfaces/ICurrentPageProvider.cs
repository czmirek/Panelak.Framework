using System.Collections.Generic;

namespace Panelak.Utils
{
    /// <summary>
    /// Service to obtain the current page of the request.
    /// </summary>
    public interface ICurrentPageProvider
    {
        /// <summary>
        /// Gets the current page information
        /// </summary>
        IPageInfo CurrentPage { get; }

        /// <summary>
        /// Gets the node of the page in the page tree.
        /// </summary>
        IPageTree CurrentPageTree { get; }

        /// <summary>
        /// Gets the path to current page represented by a collection in which the first page is the first item
        /// in the collection and the current page is the last item in the collection
        /// </summary>
        IReadOnlyList<IPageInfo> CurrentPagePath { get; }
    }
}
