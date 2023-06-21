// -----------------------------------------------------------------------
// <copyright file="WebAuthorizeAttribute.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Filters
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MVCProject.ViewModel;

    /// <summary>
    /// Custom authorization attribute. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class WebAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Gets or sets Page Id
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets multiple Page Id
        /// </summary>
        private int[] PageList;

        /// <summary>
        /// Gets or sets a value indicating whether user can access or not.
        /// </summary>
        public bool AllAccess { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="pageList">Passed pages which should be accessible or not.</param>
        public WebAuthorizeAttribute(params int[] pageList)
        {
            this.PageList = pageList;
        }

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
                UserContext.PagePermission permission = userContext.PageAccess.Where(p => p.PageId == this.Page || this.PageList.Contains(p.PageId)).FirstOrDefault();
                isAuthorize = permission.CanRead || permission.CanWrite;
                filterContext.HttpContext.Session["PagePermission"] = permission;
                if (permission.PageId == 0)
                {
                    isAuthorize = false;
                }

                if (this.AllAccess && !isAuthorize)
                {
                    isAuthorize = true;
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

            if (!isAuthorize)
            {
                //filterContext.Result = new RedirectToRouteResult(
                //    new RouteValueDictionary 
                //{ 
                //    { "controller", "Account" }, 
                //    { "action", "RedirectToDefaultUrl" },
                //    { "area", string.Empty },
                //    { "noAccess", "y" }
                //});
            }
        }
    }
}