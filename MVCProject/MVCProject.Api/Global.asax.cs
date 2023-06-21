// -----------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MVCProject.Api.Models;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// Web API Application class
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// called on application start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            HandlerConfig.RegisterHandlers(GlobalConfiguration.Configuration.MessageHandlers);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterHttpFilters(GlobalConfiguration.Configuration.Filters);
            //// Below will prevent exception details to be rendered on client.
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;
        }

        /// <summary>
        /// called on session end
        /// </summary>
        /// <param name="sender">object of sender</param>
        /// <param name="e">Event Arguments</param>
        protected void Session_End(object sender, EventArgs e)
        {
            string apiBaseUrl = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"].ToString();
            UserContext userContext = (UserContext)Session["UserContext"];
            if (userContext != null)
            {
                try
                {
                    string logoutApi = string.Format("{0}{1}", apiBaseUrl, "Account/LogOut");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(logoutApi);
                    request.Method = WebRequestMethods.Http.Post;
                    request.ContentType = "application/json";
                    request.ContentLength = 0;
                    request.Headers.Add("__RequestAuthToken", userContext.Token);
                    request.GetResponse();
                }
                catch
                {
                }
            }
        }
    }
}