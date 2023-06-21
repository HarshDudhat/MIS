// -----------------------------------------------------------------------
// <copyright file="EmployeeManagementAreaRegistration.cs" company="ASK-EHS">
// TODO: All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Web.Areas.InventoryManagement.Controllers
{
    using MVCProject.Controllers;
    using System.Web.Mvc;

    public class InventoryController : BaseController
    {
        /// <summary>
        /// Redirect Inventory to Medicine Opening Stock
        /// GET: /Inventory/MedicineOpeningStock/
        /// </summary>
        /// <returns>HTML contents</returns>
        public ActionResult MedicineOpeningStock()
        {
            return this.View();
        }

        /// <summary>
        /// Redirect Inventory to Medicine Received
        /// GET: /Inventory/MedicineReceived/
        /// </summary>
        /// <returns>HTML contents</returns>
        public ActionResult MedicineReceived(int? GroupId)
        {
            ViewBag.GroupId = GroupId.HasValue ? GroupId : 0;       // Group ID
            return this.View();
        }

        public ActionResult MedicineReceivedDemo(int? GroupId)
        {
            ViewBag.GroupId = GroupId.HasValue ? GroupId : 0;       // Group ID
            return this.View();
        }

        /// <summary>
        /// Redirect Inventory to Dead Medicine Stock
        /// GET: /Inventory/DeadMedicineStock/
        /// </summary>
        /// <returns>HTML contents</returns>
        public ActionResult DeadMedicineStock()
        {
            return this.View();
        }

        /// <summary>
        /// Redirect Inventory to Daily Medicine Consumption
        /// GET: /Inventory/DailyMedicineConsumption/
        /// </summary>
        /// <returns>HTML contents</returns>
        public ActionResult DailyMedicineConsumption()
        {
            return this.View();
        }

    }
}
