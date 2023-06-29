// -----------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api
{
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using MVCProject.Api.Handlers;

    /// <summary>
    /// Web API Route configurations
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register WebAPI routes
        /// </summary>
        /// <param name="config">object of <see cref="HttpConfiguration"/>class.</param>
        public static void Register(HttpConfiguration config)
        {
            // Add Authentication Handler
            AuthenticationHandler defaultHandler = new AuthenticationHandler() { InnerHandler = new HttpControllerDispatcher(config) };
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            config.Routes.MapHttpRoute(
              name: "Authentication",
              routeTemplate: "api/account/{action}/{id}",
              defaults: new { controller = "account", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
              name: "FileUploader",
              routeTemplate: "api/Upload/{action}/{id}",
              defaults: new { controller = "Upload", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
              name: "Landing",
              routeTemplate: "api/Landing/{action}/{id}",
              defaults: new { controller = "Landing", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: defaultHandler);
        }
    }
}
