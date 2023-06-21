// -----------------------------------------------------------------------
// <copyright file="EmployeeManagementAreaRegistration.cs" company="ASK-EHS">
// TODO: All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Web.Areas.InventoryManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;

    public class InventoryManagementAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets Area Name
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "InventoryManagement";
            }
        }

        /// <summary>
        /// Register Area
        /// </summary>
        /// <param name="context">Area Registration Context</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.RegisterRoutes(context);
            this.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Register Routes
        /// </summary>
        /// <param name="context">Area Registration Context</param>
        private void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapRoute(
               "InventoryManagement_default",
               "InventoryManagement/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional });
        }

        /// <summary>
        /// Register Bundles
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        private void RegisterBundles(BundleCollection bundles)
        {
            // MedicineOpeningStock
            bundles.Add(new ScriptBundle("~/bundles/InventoryManagement/MedicineOpeningStock")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/services/MedicineOpeningStockService.js")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/controllers/MedicineOpeningStockCtrl.js"));

            // MedicineOpeningStock
            bundles.Add(new ScriptBundle("~/bundles/InventoryManagement/MedicineOpeningStockDemo")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/services/MedicineOpeningStockService.js")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/controllers/MedicineOpeningStockDemoCtrl.js"));

            // MedicineReceived
            bundles.Add(new ScriptBundle("~/bundles/InventoryManagement/MedicineReceived")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/services/MedicineReceivedService.js")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/controllers/MedicineReceivedCtrl.js"));

            // DeadMedicineStock
            bundles.Add(new ScriptBundle("~/bundles/InventoryManagement/DeadMedicineStock")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/services/DeadMedicineStockService.js")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/controllers/DeadMedicineStockCtrl.js"));

            // DailyMedicineConsumption
            bundles.Add(new ScriptBundle("~/bundles/InventoryManagement/DailyMedicineConsumption")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/services/DailyMedicineConsumptionService.js")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/controllers/DailyMedicineConsumptionCtrl.js"));


            // MedicineReceived
            bundles.Add(new ScriptBundle("~/bundles/InventoryManagement/MedicineReceivedDemo")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/services/MedicineReceivedDemoService.js")
                 .Include("~/Areas/InventoryManagement/Scripts/angular/controllers/MedicineReceivedDemoCtrl.js"));

        }
    }
}