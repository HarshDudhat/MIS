// -----------------------------------------------------------------------
// <copyright file="TaskManagementAreaRegistration.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 
namespace MVCProject.Web.Areas.TaskManagement
{
    #region Namespace
    using System.Web.Mvc;
    using System.Web.Optimization;
    #endregion    

    /// <summary>
    /// Task Management Area Registration
    /// </summary>
    public class TaskManagementAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets Area Name
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "TaskManagement";
            }
        }

        /// <summary>
        /// Register TaskManagement Area
        /// </summary>
        /// <param name="context">Area Registration Context</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.RegisterRoutes(context);
            this.RegisterBundles(BundleTable.Bundles);            
        }


        /// <summary>
        /// Register TaskManagement Routes
        /// </summary>
        /// <param name="context">Area Registration Context</param>
        private void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TaskManagement_default",
                "TaskManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }

        /// <summary>
        /// Register TaskManagement Bundles
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        private void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/TaskManagement/ActionItem")
                    .Include("~/Scripts/angular/directives/CommonCapaGrid.js")
                    .Include("~/Areas/TaskManagement/Scripts/ActionItemCtrl.js"));
        }
    }
}
