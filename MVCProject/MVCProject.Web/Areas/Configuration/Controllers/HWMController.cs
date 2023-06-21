using MVCProject.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Areas.Configuration.Controllers
{
    public class HWMController : Controller
    {
        //
        // GET: /Configuration/HWM/
        [WebAuthorizeAttribute(Page = Pages.General.HWM)]

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WasteActionCenter()
        {
            return View();
        }

        #region Hazardous Waste 

        public ActionResult HWGenerationStorage(int? WasteGenerationId)
        {
            ViewBag.WasteGenerationId = WasteGenerationId ?? 0;
            return View();
        }

        public ActionResult HWDisposal(int? WasteDisposalId)
        {
            ViewBag.WasteDisposalId = WasteDisposalId ?? 0;
            return View();
        }

        #endregion

        #region Battery Waste Generation,Storage,Disposal

        public ActionResult BatteryWasteGenerationStorage(int? WasteGenerationId)
        {
            ViewBag.WasteGenerationId = WasteGenerationId ?? 0;
            return View();
        }

        public ActionResult BatteryWasteDisposal(int? WasteDisposalId)
        {
            ViewBag.WasteDisposalId = WasteDisposalId ?? 0;
            return View();
        }

        #endregion

        #region e-Waste Generation,Storage

        public ActionResult eWasteDisposal(int? WasteDisposalId)
        {
            ViewBag.WasteDisposalId = WasteDisposalId ?? 0;
            return View();
        }

        public ActionResult eWasteGenerationStorage(int? WasteGenerationId)
        {
            ViewBag.WasteGenerationId = WasteGenerationId ?? 0;
            return View();
        }

        #endregion

        #region Daily Biomedical Waste Record

        public ActionResult DailyBiomedicalWasteRecord()
        {
            return View();
        }

        public ActionResult BiomedicalWasteReport()
        {
            return View();
        }

        #endregion

        #region Ash Disposal

        public ActionResult MonthlyAshDisposalRecord()
        {
            return View();
        }

        public ActionResult AshUtilizationReport()
        {
            return View();
        }

        #endregion

        #region Municipal Solid Waste

        public ActionResult DailyMunicipalSolidWasteRecord()
        {
            return View();
        }

        public ActionResult MunicipalSolidWasteReport()
        {
            return View();
        }

        #endregion


        #region Waste Generation & Storage Search

        public ActionResult WasteGenerationStorageSearch()
        {
            return View();
        }

        #endregion

        #region Waste Disposal & Storage Search

        public ActionResult WasteDisposalSearch()
        {
            return View();
        }

        #endregion


        ///// <summary>
        ///// Gets the report no.
        ///// </summary>
        ///// <param name="departmentId">The department identifier.</param>
        ///// <param name="siteId">The site identifier.</param>
        ///// <param name="date">The date.</param>
        ///// <param name="safetyObservationSrNo">The safety observation no.</param>
        ///// <returns>String SSafetyObservation Report Number</returns>
        //private string GetReportNo(WasteGeneration WasteGeneration)
        //{
        //    //SiteLevelCode/ModuleCode/Year/SrNo
        //    string siteLevelCode = AppUtility.GetParentLevelFromHierarchy(safetyObservation.SubLocationLevelId.Value, (int)HierarchyLevel.Site, UserContext).FirstOrDefault().LevelCode;
        //    string safetyObservationShort = Resource.SafetyObservationShort;
        //    return string.Format("{0}/{1}/{2}/{3}", siteLevelCode, safetyObservationShort, WasteGeneration.ObservationDate.Year, (WasteGeneration.SafetyObservationSrNo ?? 1).ToString("0000"));
        //}
    }
}
