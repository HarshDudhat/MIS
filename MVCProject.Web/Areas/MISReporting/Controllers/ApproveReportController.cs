// -----------------------------------------------------------------------
// <copyright file="ApproveReportController.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Areas.MISReporting.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using MVCProject.Controllers;
    using MVCProject.Filters;
    using MVCProject.ViewModel;

    public class ApproveReportController : Controller
    {
        //
        // GET: /MISReporting/ApproveReport/

        // [WebAuthorizeAttribute(Page = Pages.General.ApproveReport)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
