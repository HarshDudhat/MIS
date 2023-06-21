
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
    public class VerticalMasterController : Controller
    {
        //
        // GET: /Configuration/VerticalMaster/

        public ActionResult Index()
        {
            return View();
        }

    }
}
