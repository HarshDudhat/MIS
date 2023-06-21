using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Filters;
using MVCProject.ViewModel;

namespace MVCProject.Areas.Configuration.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Configuration/Users/
        [WebAuthorizeAttribute(Page = Pages.General.User)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
