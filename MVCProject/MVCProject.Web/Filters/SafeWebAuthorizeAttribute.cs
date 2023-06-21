// -----------------------------------------------------------------------
// <copyright file="SafeWebAuthorizeAttribute.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Filters
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MVCProject.Utilities;
    using MVCProject.ViewModel;

    /// <summary>
    /// Custom authorization attribute. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class SafeWebAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Holds role(s) which are authorized to access particular resource.
        /// </summary>
        private Module[] modules;

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeWebAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="modules">Passed modules which should be accessible or not.</param>
        public SafeWebAuthorizeAttribute(params Module[] modules)
        {
            this.modules = modules;
        }

        /// <summary>
        /// Gets or sets a value indicating whether user can access or not.
        /// </summary>
        public bool CanAccess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user can access all modules or not.
        /// </summary>
        public bool AccessAllModules { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether only admin user can access or not.
        /// </summary>
        public bool OnlyAdmin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether only site user can access or not.
        /// </summary>
        public bool RestrictSiteUser { get; set; }

        /// <summary>
        /// to check Authorization 
        /// </summary>
        /// <param name="filterContext">Object of<see cref="AuthorizationContext"/>class</param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthorize = false;

            if (filterContext.HttpContext.Session["UserContext"] != null)
            {
                UserContext userContext = (UserContext)filterContext.HttpContext.Session["UserContext"];
                if (this.OnlyAdmin)
                {
                    //isAuthorize = userContext.IsAdmin;
                }
                else
                {
                    if (this.AccessAllModules)
                    {
                        isAuthorize = true;
                    }
                    else
                    {
                        //foreach (var module in this.modules)
                        //{
                        //    if (userContext.Modules.Contains((int)module))
                        //    {
                        //        isAuthorize = true;
                        //        break;
                        //    }
                        //}
                        isAuthorize = true;
                    }
                }

                if (isAuthorize && this.RestrictSiteUser)
                {
                    //isAuthorize = userContext.IsAdmin || userContext.IsCorporate;
                }
            }
            else
            {
                string lastUrl = System.Web.HttpContext.Current.Request.Url.OriginalString;
                if (lastUrl.Contains("InternalAudit"))
                {
                    lastUrl = lastUrl.Split('#')[0].Replace("InternalAudit", "MyAudit");
                }

                filterContext.HttpContext.Session["LastUrl"] = lastUrl;
                isAuthorize = false;
            }

            if (!isAuthorize || !this.CanAccess)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary 
                { 
                    { "controller", "Account" }, 
                    { "action", "RedirectToDefaultUrl" },
                    { "area", string.Empty },
                    { "noAccess", "y" }
                });
            }
        }
    }
}