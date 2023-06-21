// -----------------------------------------------------------------------
// <copyright file="TemplateController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 
namespace MVCProject.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Template MVC Controller for common templates
    /// </summary>
    public class TemplateController : BaseController
    {
        /// <summary>
        /// Ask Password popup template
        /// GET: /Template/_AskPassword
        /// </summary>
        /// <returns>Partial View Result</returns>
        public PartialViewResult _AskPassword()
        {
            return this.PartialView();
        }

        /// <summary>
        /// file upload template
        /// </summary>
        /// <returns>Partial View Result</returns>
        public ActionResult _FileUploder()
        {
            return this.PartialView();
        }

        /// <summary>
        /// CAPA grid template
        /// </summary>
        /// <returns>Partial View Result</returns>
        [HttpGet]
        public PartialViewResult _CommonCapaGrid()
        {
            return this.PartialView();
        }

        /// <summary>
        /// Attachment template
        /// GET: /Template/_AttachmentsPopup
        /// </summary>
        /// <returns>Partial View Result</returns>
        [HttpGet]
        public PartialViewResult _AttachmentsPopup()
        {
            return this.PartialView();
        }

        /// <summary>
        /// Comments template
        /// GET: /Template/_CommentsPopup
        /// </summary>
        /// <returns>Partial View Result</returns>
        [HttpGet]
        public PartialViewResult _CommentsPopup()
        {
            return this.PartialView();
        }

        /// <summary>
        /// Notes template
        /// GET: /Template/_NotesPopup
        /// </summary>
        /// <returns>Partial View Result</returns>
        [HttpGet]
        public PartialViewResult _NotesPopup()
        {
            return this.PartialView();
        }

        /// <summary>
        /// Notes template
        /// GET: /Template/_MessagePopup
        /// </summary>
        /// <returns>Partial View Result</returns>
        [HttpGet]
        public PartialViewResult _MessagePopup()
        {
            return this.PartialView();
        }


        /// <summary>
        /// select set reminder template
        /// GET: /Template/_SetReminderPopup
        /// </summary>
        /// <returns>Partial View Result</returns>
        [HttpGet]
        public PartialViewResult _SetReminderPopup()
        {
            return this.PartialView();
        }

        /// <summary>
        /// Character paging template
        /// GET: /Template/_CharacterPaging
        /// </summary>
        /// <returns>Partial View Result</returns>
        [HttpGet]
        public PartialViewResult _CharacterPaging()
        {
            return this.PartialView();
        }

        /// <summary>
        /// select area map template
        /// GET: /Template/_SelectAreaMapPopup
        /// </summary>
        /// <returns>Partial View Result</returns>
        [HttpGet]
        public PartialViewResult _SelectAreaMapPopup()
        {
            return this.PartialView();
        }
    }
}
