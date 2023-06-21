// -----------------------------------------------------------------------
// <copyright file="ConfigurationAreaRegistration.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
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

            // Users 
            bundles.Add(new ScriptBundle("~/bundles/Configuration/HWM")
               .Include("~/Areas/Configuration/Scripts/angular/services/HWMService.js")
               .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
               .Include("~/Areas/Configuration/Scripts/angular/controllers/HWMCtrl.js")
               );

            bundles.Add(new ScriptBundle("~/bundles/Configuration/HWMWAC")
               .Include("~/Areas/Configuration/Scripts/angular/services/HWMWACService.js")
               .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
               .Include("~/Areas/Configuration/Scripts/angular/controllers/HWMWACCtrl.js")
              );

            bundles.Add(new ScriptBundle("~/bundles/Configuration/HWDisposal")
              .Include("~/Areas/Configuration/Scripts/angular/services/HWMDisposalService.js")
              .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
              .Include("~/Areas/Configuration/Scripts/angular/controllers/HWMDisposalCtrl.js")
              );

            //HWBattery
            bundles.Add(new ScriptBundle("~/bundles/Configuration/HWBattery")
             .Include("~/Areas/Configuration/Scripts/angular/services/HWMBatteryService.js")
             .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
             .Include("~/Areas/Configuration/Scripts/angular/controllers/HWMBatteryCtrl.js")
             );

            //HWBattery Waste Disposal
            bundles.Add(new ScriptBundle("~/bundles/Configuration/HWBatteryWasteDisposal")
             .Include("~/Areas/Configuration/Scripts/angular/services/HWBatteryWasteDisposalService.js")
             .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
             .Include("~/Areas/Configuration/Scripts/angular/controllers/HWBatteryWasteDisposalCtrl.js")
             );

            //Daily Biomedical Waste Record
            bundles.Add(new ScriptBundle("~/bundles/Configuration/DailyBiomedicalWasteRecord")
             .Include("~/Areas/Configuration/Scripts/angular/services/DailyBiomedicalWasteRecordService.js")
             .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
             .Include("~/Areas/Configuration/Scripts/angular/controllers/DailyBiomedicalWasteRecordCtrl.js")
             );

            //Monthly Ash Disposal Record
            bundles.Add(new ScriptBundle("~/bundles/Configuration/MonthlyAshDisposalRecord")
              .Include("~/Areas/Configuration/Scripts/angular/services/MonthlyAshDisposalRecordService.js")
              .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
              .Include("~/Areas/Configuration/Scripts/angular/controllers/MonthlyAshDisposalRecordCtrl.js")
              );

            //Daily Municipal Solid Waste Record
            bundles.Add(new ScriptBundle("~/bundles/Configuration/DailyMunicipalSolidWasteRecord")
             .Include("~/Areas/Configuration/Scripts/angular/services/DailyMunicipalSolidWasteRecordService.js")
             .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
             .Include("~/Areas/Configuration/Scripts/angular/controllers/DailyMunicipalSolidWasteRecordCtrl.js")
             );

            //Municipal Solid Waste Report
            bundles.Add(new ScriptBundle("~/bundles/Configuration/MunicipalSolidWasteReport")
             .Include("~/Areas/Configuration/Scripts/angular/services/MunicipalSolidWasteReportService.js")
             .Include("~/Areas/Configuration/Scripts/angular/controllers/MunicipalSolidWasteReportCtrl.js")
             );

            //Ash Utilization Report
            bundles.Add(new ScriptBundle("~/bundles/Configuration/AshUtilizationReport")
             .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
            .Include("~/Areas/Configuration/Scripts/angular/services/AshUtilizationReportService.js")
            .Include("~/Areas/Configuration/Scripts/angular/controllers/AshUtilizationReportCtrl.js")
            );

            //Biomedical Waste Report
            bundles.Add(new ScriptBundle("~/bundles/Configuration/BiomedicalWasteReport")
            .Include("~/Areas/Configuration/Scripts/angular/services/BiomedicalWasteReportService.js")
            .Include("~/Areas/Configuration/Scripts/angular/controllers/BiomedicalWasteReportCtrl.js")
            );

            //e-Waste Disposal Report
            bundles.Add(new ScriptBundle("~/bundles/Configuration/eWasteDisposal")
            .Include("~/Areas/Configuration/Scripts/angular/services/eWasteDisposalService.js")
            .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
            .Include("~/Areas/Configuration/Scripts/angular/controllers/eWasteDisposalCtrl.js")
            //.Include("~/Areas/TaskManagement/Scripts/ActionItemCtrl.js")
            //.Include("~/bundles/TaskManagement/ActionItem")
            );

            //e-Waste Generation Report
            bundles.Add(new ScriptBundle("~/bundles/Configuration/eWasteGeneration")
            .Include("~/Areas/Configuration/Scripts/angular/services/eWasteGenerationService.js")
            .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
            .Include("~/Areas/Configuration/Scripts/angular/controllers/eWasteGenerationCtrl.js")
            );

            //Waste Generation Storage Search
            bundles.Add(new ScriptBundle("~/bundles/Configuration/WasteGenerationStorageSearch")
            .Include("~/Areas/Configuration/Scripts/angular/services/WasteGenerationStorageSearchService.js")
            .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
            .Include("~/Areas/Configuration/Scripts/angular/controllers/WasteGenerationStorageSearchCtrl.js")
            );

            //Waste Disposal Storage Search
            bundles.Add(new ScriptBundle("~/bundles/Configuration/WasteDisposalSearch")
            .Include("~/Areas/Configuration/Scripts/angular/services/WasteDisposalSearchService.js")
            .Include("~/Areas/Configuration/Scripts/angular/services/UsersService.js")
            .Include("~/Areas/Configuration/Scripts/angular/controllers/WasteDisposalSearchCtrl.js")
            );

        }
    }
}
