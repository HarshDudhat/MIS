// -----------------------------------------------------------------------
// <copyright file="DashboardController.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Areas.Dashboard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MVCProject.Controllers;
    using MVCProject.Filters;
    using MVCProject.Utilities;
    using MVCProject.ViewModel;


    [WebAuthorize(Page = (int)PageAccess.Dashboard)]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/Dashboard/

        // [WebAuthorizeAttribute(Page = Pages.General.Dashboard)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
