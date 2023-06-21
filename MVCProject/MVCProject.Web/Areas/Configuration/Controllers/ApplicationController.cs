// -----------------------------------------------------------------------
// <copyright file="ApplicationController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Areas.Configuration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MVCProject.Controllers;
    using MVCProject.Filters;
    using MVCProject.ViewModel;

    /// <summary>
    /// Application Controller
    /// </summary>    
    public class ApplicationController : BaseController
    {
        /// <summary>
        /// Redirect to Application Configuration UI
        /// GET: /Configuration/Application/
        /// </summary>
        /// <returns>Action Result UI</returns>
        [WebAuthorizeAttribute(Page = Pages.General.CommonConfiguartion)]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Update Application Logo
        /// </summary>
        /// <param name="logo">application logo</param>
        [HttpPut]
        public void UpdateApplicationLogo(string logo)
        {
            if (!string.IsNullOrWhiteSpace(logo))
            {
                UserContext context = (UserContext)this.Session["UserContext"];
                context.ApplicationLogo = logo;
                this.Session["UserContext"] = context;
            }
        }
    }
}
