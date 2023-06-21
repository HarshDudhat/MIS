// -----------------------------------------------------------------------
// <copyright file="ErrorController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 
namespace MVCProject.Controllers
{
    using System.Web.Mvc;
    using MVCProject.Common.Resources;

    /// <summary>
    /// Error MVC Controller
    /// </summary>
    public class ErrorController : BaseController
    {
        /// <summary>
        /// Redirect to Error Page
        /// GET: /Error/ServerError/{id}
        /// </summary>
        /// <param name="id">Error Id</param>
        /// <returns>Error Page UI</returns>
        public ActionResult ServerError(int? id)
        {
            ViewBag.HasSession = this.Session["UserContext"] != null;
            ViewBag.Title = Resource.Error;
            ViewBag.ErrorMessage = Resource.SomethingWentWrong;
            switch (id)
            {
                case 404:
                    ViewBag.Title = Resource.PageNotFound;
                    ViewBag.ErrorMessage = Resource.ErrorMsgFor404;
                    break;
                case 500:
                    ViewBag.Title = Resource.SomethingWentWrong;
                    ViewBag.ErrorMessage = Resource.ErrorMsgFor500;
                    break;
                default:
                    ViewBag.Title = Resource.Error;
                    ViewBag.ErrorMessage = Resource.SomethingWentWrong;
                    break;
            }

            return this.View(id);
        }
    }
}
