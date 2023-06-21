// -----------------------------------------------------------------------
// <copyright file="ConfigurationAreaRegistration.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Areas.Configuration
{
    using System.Web.Mvc;
    using System.Web.Optimization;

    /// <summary>
    /// Configuration Area Registration
    /// </summary>
    public class ConfigurationAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets Area Name
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "Configuration";
            }
        }

        /// <summary>
        /// Register Configuration Area
        /// </summary>
        /// <param name="context">Area Registration Context</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.RegisterRoutes(context);
            this.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Register Configuration Routes
        /// </summary>
        /// <param name="context">Area Registration Context</param>
        private void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Configuration_default",
                "Configuration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }

        /// <summary>
        /// Bundle Registration Collection
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        private void RegisterBundles(BundleCollection bundles)
        {

            // Application
            bundles.Add(new ScriptBundle("~/bundles/Configuration/Application")
                .Include("~/Scripts/angular/services/SiteMasterService.js")
                .Include("~/Areas/Configuration/Scripts/angular/services/ApplicationService.js")
                .Include("~/Areas/Configuration/Scripts/angular/controllers/ApplicationCtrl.js"));

            // Reset To Login
            bundles.Add(new ScriptBundle("~/bundles/Configuration/ResetToLogin")
                .Include("~/Areas/Configuration/Scripts/angular/services/ResetToLoginService.js")
                .Include("~/Areas/Configuration/Scripts/angular/controllers/ResetToLoginCtrl.js"));

            // Users 
            bundles.Add(new ScriptBundle("~/bundles/Configuration/Users")
                .Include("~/Scripts/angular/services/SiteMasterService.js")
                .Include("~/Scripts/angular/services/DepartmentMasterService.js")
               .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
               .Include("~/Areas/Configuration/Scripts/angular/controllers/UsersCtrl.js"));

            // Role Level
            bundles.Add(new ScriptBundle("~/bundles/Configuration/RoleLevel")
                .Include("~/Areas/Configuration/Scripts/angular/services/RoleService.js")
                .Include("~/Areas/Configuration/Scripts/angular/controllers/RoleLevelCtrl.js"));

            // Role Master
            bundles.Add(new ScriptBundle("~/bundles/Configuration/RoleMaster")
                .Include("~/Areas/Configuration/Scripts/angular/services/RoleService.js")
                .Include("~/Areas/Configuration/Scripts/angular/controllers/RoleMasterCtrl.js"));

            // Designation 
            bundles.Add(new ScriptBundle("~/bundles/Configuration/Designation")
                .Include("~/Areas/Configuration/Scripts/angular/services/DesignationService.js")
                .Include("~/Areas/Configuration/Scripts/angular/controllers/DesignationCtrl.js"));


            // Vertical Master 
            bundles.Add(new ScriptBundle("~/bundles/Configuration/VerticalMaster")
                .Include("~/Areas/Configuration/Scripts/angular/services/VerticalMasterService.js")
                .Include("~/Areas/Configuration/Scripts/angular/controllers/VerticalMasterCtrl.js"));


            // Project Master 
            bundles.Add(new ScriptBundle("~/bundles/Configuration/ProjectMaster")
                .Include("~/Areas/Configuration/Scripts/angular/services/ProjectMasterService.js")
                .Include("~/Areas/Configuration/Scripts/angular/controllers/ProjectMasterCtrl.js"));
        }
    }
}
