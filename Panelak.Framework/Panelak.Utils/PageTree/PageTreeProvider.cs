namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Page tree provider for obtaining the page tree conventionally by reflection
    /// from <see cref="RouteAttribute"/> on the action methods.
    /// </summary>
    public class PageTreeProvider : IPageTreeProvider
    {
        /// <summary>
        /// List of parsed route attributes
        /// </summary>
        private static List<PageInfo> templates = null;

        /// <summary>
        /// Default assembly to read controller types from
        /// </summary>
        private readonly Assembly assembly;

        /// <summary>
        /// Base controller type to read actions from.
        /// </summary>
        private readonly Type baseControllerType;

        /// <summary>
        /// The page tree root.
        /// </summary>
        public PageTree Root { get; private set; }

        /// <summary>
        /// The page tree root.
        /// </summary>
        IPageTree IPageTreeProvider.Root => Root;

        /// <summary>
        /// Initializes a new instance of <see cref="PageTreeProvider"/>.
        /// </summary>
        /// <param name="assembly">Assembly with controllers</param>
        public PageTreeProvider(Assembly assembly)
        {
            this.assembly = assembly;
            baseControllerType = typeof(Controller);
            Build();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PageTreeProvider"/>.
        /// </summary>
        /// <param name="assembly">Assembly with controllers</param>
        /// <param name="baseControllerType">Custom base controller type</param>
        public PageTreeProvider(Assembly assembly, Type baseControllerType) : this(assembly)
             => this.baseControllerType = baseControllerType;

        /// <summary>
        /// Builds the page tree
        /// </summary>
        private void Build()
        {
            if (templates == null)
                BuildTemplates();

            if (!templates.Any())
                throw new InvalidOperationException("No Routes found. To build the page tree, define [Routes] on your actions first");

            PageInfo rootRoute = templates.FirstOrDefault(t => t.UrlTemplate == "/");

            if (rootRoute == null)
                throw new InvalidOperationException("No root route found. Mark your home route with Route[\"\"].");

            var root = new PageTree() { Page = rootRoute };

            foreach (PageInfo route in templates)
            {
                if (ReferenceEquals(route, rootRoute))
                    continue;

                PageTree parent = FindParent(root, route);
                parent.Children.Add(new PageTree()
                {
                    Page = route,
                    Parent = parent
                });
            }

            Root = root;
        }

        /// <summary>
        /// Recursive search of the parent for current node.
        /// </summary>
        /// <param name="current">Current node</param>
        /// <param name="route">Route to attach</param>
        /// <returns>Found suitable parent</returns>
        private PageTree FindParent(PageTree current, PageInfo route)
        {
            foreach (PageTree item in current.Children)
            {
                if (route.UrlTemplate.StartsWith(item.Page.UrlTemplate))
                    return FindParent(item, route);
            }
            return current;
        }

        /// <summary>
        /// Builds the <see cref="templates"/> static variable containing data
        /// about existing <see cref="RouteAttribute"/>s.
        /// </summary>
        private void BuildTemplates()
        {
            templates = new List<PageInfo>();

            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsSubclassOf(baseControllerType))
                    continue;

                IEnumerable<RouteAttribute> ctrlRoutes = type.GetCustomAttributes<RouteAttribute>();
                if (ctrlRoutes.Any())
                {
                    foreach (RouteAttribute ctrlRoute in ctrlRoutes)
                    {
                        string ctrlTemplate = ctrlRoute.Template;
                        foreach (MethodInfo method in type.GetMethods())
                        {
                            foreach (RouteAttribute methodAttribute in method.GetCustomAttributes<RouteAttribute>())
                            {
                                string actionTemplate = methodAttribute.Template;
                                string fullTemplate = "/" + ctrlTemplate.Trim('/') + "/" + actionTemplate.Trim('/');

                                if (fullTemplate != "/")
                                    fullTemplate = fullTemplate.TrimEnd('/');

                                templates.Add(new PageInfo() { UrlTemplate = fullTemplate, Action = method });
                            }
                        }
                    }
                }
                else
                {
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        foreach (RouteAttribute methodAttribute in method.GetCustomAttributes<RouteAttribute>())
                        {
                            string actionTemplate = "/" + methodAttribute.Template.Trim('/');
                            templates.Add(new PageInfo() { UrlTemplate = actionTemplate, Action = method });
                        }
                    }
                }
            }

            //sort for tree building
            templates = templates.OrderBy(t => t.UrlTemplate).ToList();
        }
    }
}
