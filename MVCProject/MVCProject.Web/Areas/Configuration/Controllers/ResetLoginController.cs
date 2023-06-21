// -----------------------------------------------------------------------
// <copyright file="ResetLoginController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
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
