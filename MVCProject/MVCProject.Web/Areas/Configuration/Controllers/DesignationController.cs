// -----------------------------------------------------------------------
// <copyright file="DesignationController.cs" company="ASK-EHS">
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

    public class DesignationController : Controller
    {
        //
        // GET: /Configuration/Designation/

        [WebAuthorizeAttribute(Page = Pages.General.Designation)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
