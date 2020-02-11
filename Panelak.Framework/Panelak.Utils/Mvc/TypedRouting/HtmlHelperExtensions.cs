﻿namespace Panelak.Utils
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified action
        /// by using <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent ActionLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            Expression<Action<TController>> action)
            where TController : class
        {
            return helper.ActionLink(
                linkText,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified action
        /// by using <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent ActionLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            Expression<Func<TController, Task>> action)
            where TController : class
        {
            return helper.ActionLink(
                linkText,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified action
        /// by using <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent ActionLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            Expression<Action<TController>> action,
            object routeValues)
            where TController : class
        {
            return helper.ActionLink(
                linkText,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: routeValues,
                htmlAttributes: null);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified action
        /// by using <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent ActionLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            Expression<Func<TController, Task>> action,
            object routeValues)
            where TController : class
        {
            return helper.ActionLink(
                linkText,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: routeValues,
                htmlAttributes: null);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified action
        /// by using <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent ActionLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            Expression<Action<TController>> action,
            object routeValues,
            object htmlAttributes)
            where TController : class
        {
            return helper.ActionLink(
                linkText,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: routeValues,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified action
        /// by using <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent ActionLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            Expression<Func<TController, Task>> action,
            object routeValues,
            object htmlAttributes)
            where TController : class
        {
            return helper.ActionLink(
                linkText,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: routeValues,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified action
        /// by using <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="protocol">The protocol for the URL, such as &quot;http&quot; or &quot;https&quot;.</param>
        /// <param name="hostНame">The host name for the URL.</param>
        /// <param name="fragment">The URL fragment name (the anchor name).</param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent ActionLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            Expression<Action<TController>> action,
            string protocol,
            string hostНame,
            string fragment,
            object routeValues,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(action, routeValues);
            return helper.ActionLink(
                linkText,
                expressionRouteValues.Action,
                expressionRouteValues.Controller,
                protocol: protocol,
                hostname: hostНame,
                fragment: fragment,
                routeValues: expressionRouteValues.RouteValues,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified action
        /// by using <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="protocol">The protocol for the URL, such as &quot;http&quot; or &quot;https&quot;.</param>
        /// <param name="hostНame">The host name for the URL.</param>
        /// <param name="fragment">The URL fragment name (the anchor name).</param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent ActionLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            Expression<Func<TController, Task>> action,
            string protocol,
            string hostНame,
            string fragment,
            object routeValues,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(action, routeValues);
            return helper.ActionLink(
                linkText,
                expressionRouteValues.Action,
                expressionRouteValues.Controller,
                protocol: protocol,
                hostname: hostНame,
                fragment: fragment,
                routeValues: expressionRouteValues.RouteValues,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified route and
        /// <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent RouteLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            string routeName,
            Expression<Action<TController>> action)
            where TController : class
        {
            return helper.RouteLink(
                linkText,
                routeName,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified route and
        /// <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent RouteLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            string routeName,
            Expression<Func<TController, Task>> action)
            where TController : class
        {
            return helper.RouteLink(
                linkText,
                routeName,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified route and
        /// <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent RouteLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            string routeName,
            Expression<Action<TController>> action,
            object routeValues)
            where TController : class
        {
            return helper.RouteLink(
                linkText,
                routeName,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: routeValues,
                htmlAttributes: null);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified route and
        /// <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent RouteLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            string routeName,
            Expression<Func<TController, Task>> action,
            object routeValues)
            where TController : class
        {
            return helper.RouteLink(
                linkText,
                routeName,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: routeValues,
                htmlAttributes: null);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified route and
        /// <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent RouteLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            string routeName,
            Expression<Action<TController>> action,
            object routeValues,
            object htmlAttributes)
            where TController : class
        {
            return helper.RouteLink(
                linkText,
                routeName,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: routeValues,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified route and
        /// <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent RouteLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            string routeName,
            Expression<Func<TController, Task>> action,
            object routeValues,
            object htmlAttributes)
            where TController : class
        {
            return helper.RouteLink(
                linkText,
                routeName,
                action,
                protocol: null,
                hostНame: null,
                fragment: null,
                routeValues: routeValues,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified route and
        /// <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="protocol">The protocol for the URL, such as &quot;http&quot; or &quot;https&quot;.</param>
        /// <param name="hostНame">The host name for the URL.</param>
        /// <param name="fragment">The URL fragment name (the anchor name).</param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent RouteLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            string routeName,
            Expression<Action<TController>> action,
            string protocol,
            string hostНame,
            string fragment,
            object routeValues,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(action, routeValues, addControllerAndActionToRouteValues: true);
            return helper.RouteLink(
                linkText,
                routeName,
                protocol: protocol,
                hostName: hostНame,
                fragment: fragment,
                routeValues: expressionRouteValues.RouteValues,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Returns an anchor (&lt;a&gt;) element that contains a URL path to the specified route and
        /// <see cref="Expression{TDelegate}"/> for an action method,
        /// from which action name, controller name and route values are resolved.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="linkText">The inner text of the anchor element. Must not be <c>null</c>.</param>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="protocol">The protocol for the URL, such as &quot;http&quot; or &quot;https&quot;.</param>
        /// <param name="hostНame">The host name for the URL.</param>
        /// <param name="fragment">The URL fragment name (the anchor name).</param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>A new <see cref="IHtmlContent"/> containing the anchor element.</returns>
        public static IHtmlContent RouteLink<TController>(
            this IHtmlHelper helper,
            string linkText,
            string routeName,
            Expression<Func<TController, Task>> action,
            string protocol,
            string hostНame,
            string fragment,
            object routeValues,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(action, routeValues, addControllerAndActionToRouteValues: true);
            return helper.RouteLink(
                linkText,
                routeName,
                protocol: protocol,
                hostName: hostНame,
                fragment: fragment,
                routeValues: expressionRouteValues.RouteValues,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: FormMethod.Post,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: FormMethod.Post,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: FormMethod.Post,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: FormMethod.Post,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            object routeValues)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues,
                FormMethod.Post,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            object routeValues)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues,
                FormMethod.Post,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            object routeValues,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues,
                FormMethod.Post,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            object routeValues,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues,
                FormMethod.Post,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            FormMethod method)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: method,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            FormMethod method)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: method,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            FormMethod method,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: method,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            FormMethod method,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: method,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            object routeValues,
            FormMethod method)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues,
                method,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            object routeValues,
            FormMethod method)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues,
                method,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            object routeValues,
            FormMethod method,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues,
                method,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            object routeValues,
            FormMethod method,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues,
                method,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            FormMethod method,
            object htmlAttributes)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: method,
                antiforgery: null,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            FormMethod method,
            object htmlAttributes)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: method,
                antiforgery: null,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: method,
                antiforgery: antiforgery,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes)
            where TController : class
        {
            return helper.BeginForm(
                action,
                routeValues: null,
                method: method,
                antiforgery: antiforgery,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            object routeValues,
            FormMethod method,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(action, routeValues);
            return helper.BeginForm(
                expressionRouteValues.Action,
                expressionRouteValues.Controller,
                routeValues: expressionRouteValues.RouteValues,
                method: method,
                antiforgery: null,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            object routeValues,
            FormMethod method,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(action, routeValues);
            return helper.BeginForm(
                expressionRouteValues.Action,
                expressionRouteValues.Controller,
                routeValues: expressionRouteValues.RouteValues,
                method: method,
                antiforgery: null,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Action<TController>> action,
            object routeValues,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(action, routeValues);
            return helper.BeginForm(
                expressionRouteValues.Action,
                expressionRouteValues.Controller,
                routeValues: expressionRouteValues.RouteValues,
                method: method,
                antiforgery: antiforgery,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. When the user submits the form, the action from the
        /// <see cref="Expression{TDelegate}"/> will process the request.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginForm<TController>(
            this IHtmlHelper helper,
            Expression<Func<TController, Task>> action,
            object routeValues,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(action, routeValues);
            return helper.BeginForm(
                expressionRouteValues.Action,
                expressionRouteValues.Controller,
                routeValues: expressionRouteValues.RouteValues,
                method: method,
                antiforgery: antiforgery,
                htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action)
            where TController : class
        {
            return helper.BeginRouteForm(
                routeName,
                routeValues: null,
                method: FormMethod.Post,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action)
            where TController : class
        {
            return helper.BeginRouteForm(
                routeName,
                routeValues: null,
                method: FormMethod.Post,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginRouteForm(
                routeName,
                routeValues: null,
                method: FormMethod.Post,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginRouteForm(
                routeName,
                routeValues: null,
                method: FormMethod.Post,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            FormMethod method)
            where TController : class
        {
            return helper.BeginRouteForm(
                routeName,
                routeValues: null,
                method: method,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            FormMethod method)
            where TController : class
        {
            return helper.BeginRouteForm(
                routeName,
                routeValues: null,
                method: method,
                antiforgery: null,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            FormMethod method,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginRouteForm(
                routeName,
                routeValues: null,
                method: method,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            FormMethod method,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginRouteForm(
                routeName,
                routeValues: null,
                method: method,
                antiforgery: antiforgery,
                htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            object routeValues)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues, FormMethod.Post, antiforgery: null, htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            object routeValues)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues, FormMethod.Post, antiforgery: null, htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            object routeValues,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues, FormMethod.Post, antiforgery: antiforgery, htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey,TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            object routeValues,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues, FormMethod.Post, antiforgery: antiforgery, htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            object routeValues,
            FormMethod method)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues, method, antiforgery: null, htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            object routeValues,
            FormMethod method)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues, method, antiforgery: null, htmlAttributes: null);
        }
        
        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            object routeValues,
            FormMethod method,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues, method, antiforgery: antiforgery, htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            object routeValues,
            FormMethod method,
            bool? antiforgery)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues, method, antiforgery: antiforgery, htmlAttributes: null);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            FormMethod method,
            object htmlAttributes)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues: null, method: method, antiforgery: null, htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            FormMethod method,
            object htmlAttributes)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues: null, method: method, antiforgery: null, htmlAttributes: htmlAttributes);
        }
        
        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues: null, method: method, antiforgery: antiforgery, htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes)
            where TController : class
        {
            return helper.BeginRouteForm(routeName, routeValues: null, method: method, antiforgery: antiforgery, htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param> 
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            object routeValues,
            FormMethod method,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(
                action,
                routeValues,
                addControllerAndActionToRouteValues: true);

            return helper.BeginRouteForm(routeName, expressionRouteValues.RouteValues, method, antiforgery: null, htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param> 
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            object routeValues,
            FormMethod method,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(
                action,
                routeValues,
                addControllerAndActionToRouteValues: true);

            return helper.BeginRouteForm(routeName, expressionRouteValues.RouteValues, method, antiforgery: null, htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param> 
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Action<TController>> action,
            object routeValues,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(
                action,
                routeValues,
                addControllerAndActionToRouteValues: true);

            return helper.BeginRouteForm(routeName, expressionRouteValues.RouteValues, method, antiforgery: antiforgery, htmlAttributes: htmlAttributes);
        }

        /// <summary>
        /// Renders a &lt;form&gt; start tag to the response. The route with name <paramref name="routeName"/>
        /// generates the &lt;form&gt;'s <c>action</c> attribute value.
        /// </summary>
        /// <typeparam name="TController">Controller, from which the action is specified.</typeparam>
        /// <param name="routeName">The name of the route.</param>
        /// <param name="action">
        /// The <see cref="Expression{TDelegate}"/>, from which action name, 
        /// controller name and route values are resolved.
        /// </param>
        /// <param name="routeValues">
        /// An <see cref="Object"/> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="Object"/>. This <see cref="Object"/> is typically
        /// created using <see cref="Object"/> initializer syntax. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param> 
        /// <param name="htmlAttributes">
        /// An <see cref="Object"/> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="IDictionary{TKey, TValue}"/> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will include an antiforgery token.
        /// </param>
        /// <returns>
        /// An <see cref="MvcForm"/> instance which renders the &lt;/form&gt; end tag when disposed.
        /// </returns>
        /// <remarks>
        /// In this context, "renders" means the method writes its output using <see cref="ViewContext.Writer"/>.
        /// </remarks>
        public static MvcForm BeginRouteForm<TController>(
            this IHtmlHelper helper,
            string routeName,
            Expression<Func<TController, Task>> action,
            object routeValues,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes)
            where TController : class
        {
            ExpressionRouteValues expressionRouteValues = GetExpresionRouteHelper(helper).Resolve(
                action,
                routeValues,
                addControllerAndActionToRouteValues: true);

            return helper.BeginRouteForm(routeName, expressionRouteValues.RouteValues, method, antiforgery: antiforgery, htmlAttributes: htmlAttributes);
        }
        
        private static IExpressionRouteHelper GetExpresionRouteHelper(IHtmlHelper helper)
            => helper.ViewContext.HttpContext.RequestServices.GetExpressionRouteHelper();
    }
}
