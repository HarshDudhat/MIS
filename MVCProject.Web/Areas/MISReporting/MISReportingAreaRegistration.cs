// -----------------------------------------------------------------------
// <copyright file="MISReportingAreaRegistration.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Areas.MISReporting
{
    using System.Web.Mvc;
    using System.Web.Optimization;

    /// <summary>
    /// Configuration Area Registration
    /// </summary>
    public class MISReportingAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets Area Name
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "MISReporting";
            }
        }

        /// <summary>
        /// Register MISReporting Area
        /// </summary>
        /// <param name="context">Area Registration Context</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.RegisterRoutes(context);
            this.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Register MISReporting Routes
        /// </summary>
        /// <param name="context">Area Registration Context</param>
        private void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MISReporting_default",
                "MISReporting/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }

        /// <summary>
        /// Bundle Registration Collection
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        private void RegisterBundles(BundleCollection bundles)
        {

            // MIS Review Reporting 
            bundles.Add(new ScriptBundle("~/bundles/MISReporting/ReviewReport")
                .Include("~/Areas/MISReporting/Scripts/angular/services/ReviewReportService.js")
                .Include("~/Areas/MISReporting/Scripts/angular/controllers/ReviewReportCtrl.js"));
          
            // MIS Approve Reporting 
            bundles.Add(new ScriptBundle("~/bundles/MISReporting/ApproveReport")
                .Include("~/Areas/MISReporting/Scripts/angular/services/ApproveReportService.js")
                .Include("~/Areas/MISReporting/Scripts/angular/controllers/ApproveReportCtrl.js"));

        }
    }
}
