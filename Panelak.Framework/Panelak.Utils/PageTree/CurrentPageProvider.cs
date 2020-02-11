namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Routing;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Service to obtain and configure the current running page from the ASP.NET Core scoped action context
    /// using the page tree from the <see cref="PageTreeProvider"/>.
    /// </summary>
    public class CurrentPageProvider : ICurrentPageProvider
    {
        /// <summary>
        /// Action context
        /// </summary>
        private readonly IActionContextAccessor actionContextAccessor;

        /// <summary>
        /// Page tree provider
        /// </summary>
        private readonly PageTreeProvider pageTreeProvider;

        /// <summary>
        /// Creates a new instance of <see cref="CurrentPageProvider" />.
        /// </summary>
        /// <param name="actionContextAccessor">Action context accessor</param>
        /// <param name="pageTreeProvider">Page tree provider</param>
        public CurrentPageProvider(IActionContextAccessor actionContextAccessor, PageTreeProvider pageTreeProvider)
        {
            this.actionContextAccessor = actionContextAccessor ?? throw new ArgumentNullException(nameof(actionContextAccessor));
            this.pageTreeProvider = pageTreeProvider ?? throw new ArgumentNullException(nameof(pageTreeProvider));

            Build();
        }

        /// <summary>
        /// Builds and configures the current page reference
        /// </summary>
        private void Build()
        {
            var descriptor = actionContextAccessor.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            MethodInfo currentAction = descriptor.MethodInfo;

            //find the current page in the page tree provider
            CurrentPageTree = FindPageTree(pageTreeProvider.Root, currentAction);

            
            var path = new List<PageInfo>();
            PageTree pgt = CurrentPageTree;
            while (pgt != null)
            {
                //configure the current page with strings from attributes
                ApplyAttributes(pgt);

                //set the path collection for building menus
                path.Add(pgt.Page);
                pgt = pgt.Parent;
            }

            path.Reverse();
            CurrentPagePath = path.AsReadOnly();
        }

        /// <summary>
        /// Configures the localizable strings to the <see cref="PageInfo"/> in the tree path 
        /// created from the current request. Uses <see cref="DisplayAttribute"/> and/or the 
        /// <see cref="PageMetadataAttribute"/>.
        /// </summary>
        /// <param name="currentPageTree">Current page tree to apply</param>
        private void ApplyAttributes(PageTree currentPageTree)
        {
            PageInfo currentPage = currentPageTree.Page;
            MethodInfo pageAction = currentPage.Action;

            //detect and apply the display attribute
            DisplayAttribute displayAttribute = pageAction.GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute != null)
            {
                if (displayAttribute.ResourceType != null)
                {
                    var rs = new ResourceManager(displayAttribute.ResourceType);
                    currentPage.Caption = rs.GetString(displayAttribute.Name);
                }
                else
                    currentPage.Caption = displayAttribute.Name;
            }

            //detect and apply the page metadata attribute
            PageMetadataAttribute pageMetadataAttribute = pageAction.GetCustomAttribute<PageMetadataAttribute>();
            if (pageMetadataAttribute != null)
            {
                if (pageMetadataAttribute.ResourceType != null)
                {
                    var rs = new ResourceManager(pageMetadataAttribute.ResourceType);

                    if (!String.IsNullOrEmpty(pageMetadataAttribute.Caption))
                        currentPage.Caption = rs.GetString(pageMetadataAttribute.Caption);

                    if (!String.IsNullOrEmpty(pageMetadataAttribute.Header))
                        currentPage.Header = rs.GetString(pageMetadataAttribute.Header);
                }
                else
                {
                    currentPage.Caption = pageMetadataAttribute.Caption;
                    currentPage.Header = pageMetadataAttribute.Header;
                }

                currentPage.RenderHeader = pageMetadataAttribute.RenderHeader;
                currentPage.RenderCaption = pageMetadataAttribute.RenderCaption;
            }

            //translate template variables from route data
            if (!String.IsNullOrEmpty(currentPage.Caption))
                currentPage.Caption = TranslateParameters(currentPage.Caption);

            if (!String.IsNullOrEmpty(currentPage.Header))
                currentPage.Header = TranslateParameters(currentPage.Header);

            var urlHelper = new UrlHelper(actionContextAccessor.ActionContext);

            //build url for the items in the page tree using the existing route values
            var routeValues = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> routeValue in actionContextAccessor.ActionContext.RouteData.Values)
            {
                //detecting a variable template with "{" should be enough
                if (currentPage.UrlTemplate.Contains("{" + routeValue.Key))
                    routeValues.Add(routeValue.Key, routeValue.Value);
            }

            currentPage.CurrentUrl = urlHelper.Action(new UrlActionContext()
            {
                Action = currentPage.Action.Name,
                Controller = currentPage.Action.DeclaringType.Name.Replace("controller", "", StringComparison.InvariantCultureIgnoreCase),
                Values = routeValues
            });
        }

        /// <summary>
        /// Translates the Route attribute template parameters to actual values
        /// from the route data. Works only for straightforward templates such as {id},
        /// does not work yet work default values and such.
        /// </summary>
        /// <param name="text">Url template</param>
        /// <returns>Translated url path</returns>
        private string TranslateParameters(string text)
        {
            RouteValueDictionary dict = actionContextAccessor.ActionContext.RouteData.Values;
            foreach (KeyValuePair<string, object> item in dict)
            {
                if (text.Contains("{" + item.Key + "}"))
                    text = text.Replace("{" + item.Key + "}", item.Value.ToString());
            }

            return text;
        }

        /// <summary>
        /// DFS tree search
        /// </summary>
        /// <param name="tree">Page tree</param>
        /// <param name="action">Action to search</param>
        /// <returns>Node element</returns>
        private PageTree FindPageTree(PageTree tree, MethodInfo action)
        {
            if (tree.Page.Action.Equals(action))
                return tree;

            foreach (PageTree child in tree.Children)
            {
                PageTree foundChild = FindPageTree(child, action);

                if (foundChild != null)
                    return foundChild;
            }
            
            return null;
        }

        /// <summary>
        /// Gets the current page
        /// </summary>
        public PageInfo CurrentPage => CurrentPageTree?.Page;

        /// <summary>
        /// Gets the node of the current page in the page tree
        /// </summary>
        public PageTree CurrentPageTree { get; internal set; }

        /// <summary>
        /// Gets the current page
        /// </summary>
        IPageInfo ICurrentPageProvider.CurrentPage => CurrentPage;

        /// <summary>
        /// Gets the node of the current page in the page tree
        /// </summary>
        IPageTree ICurrentPageProvider.CurrentPageTree => CurrentPageTree;

        /// <summary>
        /// Gets the path to current page represented by a collection in which the first page is the first item
        /// in the collection and the current page is the last item in the collection
        /// </summary>
        public IReadOnlyList<IPageInfo> CurrentPagePath { get; private set; }
    }
}
