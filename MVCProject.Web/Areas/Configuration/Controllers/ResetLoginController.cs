// -----------------------------------------------------------------------
// <copyright file="ResetLoginController.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Areas.Configuration.Controllers
{
    #region Namespaces

    using System.Web.Mvc;
    using MVCProject.Controllers;
    using MVCProject.Filters;

    #endregion

    public class ResetLoginController : BaseController
    {
        //
        // GET: /Configuration/ResetLogin/
        [WebAuthorizeAttribute]
        public ActionResult Index()
        {
            return View();
        }

    }
}
