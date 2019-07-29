namespace Panelak.Utils
{
    using System;

    /// <summary>
    /// Attribute for marking the controller action methods as a member of a site tree structure
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class PageMapAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="PageMapAttribute"/>.
        /// </summary>
        /// <param name="treePath">String dot separated path</param>
        public PageMapAttribute(string treePath) => TreePath = treePath;

        /// <summary>
        /// Creates a new instance of <see cref="PageMapAttribute"/>.
        /// </summary>
        public PageMapAttribute(string treePath, Type modelResolver)
            : this(treePath) => ModelResolver = modelResolver;

        /// <summary>
        /// Creates a new instance of <see cref="PageMapAttribute"/>.
        /// </summary>
        public PageMapAttribute(string treePath, string title)
            : this(treePath)
        {
            TreePath = treePath;
            Title = title;
        }

        /// <summary>
        /// String path of the action method
        /// </summary>
        public string TreePath { get; }

        /// <summary>
        /// Page title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Resolver of the model
        /// </summary>
        public Type ModelResolver { get; }
    }
}
