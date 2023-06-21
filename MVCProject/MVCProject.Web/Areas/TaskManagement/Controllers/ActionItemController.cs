// -----------------------------------------------------------------------
// <copyright file="ActionItemController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 
namespace MVCProject.Web.Areas.TaskManagement.Controllers
{
    #region Namespace
    using System.Web.Mvc;    
    #endregion
    public class ActionItemController : Controller
    {
        //
        // GET: /TaskManagement/ActionItem/
        public ActionResult Index()
        {
            return View();
        }

    }
}
