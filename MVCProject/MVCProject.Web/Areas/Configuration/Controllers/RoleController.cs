// -----------------------------------------------------------------------
// <copyright file="RoleController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
namespace MVCProject.Areas.Configuration.Controllers
{
    using System.Web.Mvc;
    using MVCProject.Controllers;
    using MVCProject.Filters;

    /// <summary>
    /// Holds Sites Master related methods
    /// </summary>    
    public class RoleController : BaseController
    {
        /// <summary>
        /// Redirect user to Role level master page
        /// GET: /Role/
        /// </summary>
        /// <returns>HTML Content</returns>
        [WebAuthorizeAttribute(Page = Pages.General.RoleLevel)]
        public ActionResult Level()
        {
            return this.View();
        }

        /// <summary>
        /// Redirect user to Role master master page
        /// GET: /Role/
        /// </summary>
        /// <returns>HTML Content</returns>
        [WebAuthorizeAttribute(Page = Pages.General.RoleMaster)]
        public ActionResult Master()
        {
            return this.View();
        }
    }
}
