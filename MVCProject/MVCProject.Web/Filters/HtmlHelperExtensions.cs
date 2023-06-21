// -----------------------------------------------------------------------
// <copyright file="HtmlHelperExtensions.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace System.Web.Mvc
{
    using MVCProject;
    using MVCProject.Common.Resources;
    using MVCProject.ViewModel;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Routing;

    /// <summary>
    /// Html Helper Extensions
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Get HTML Menu
        /// </summary>
        /// <param name="helper">Html Helper</param>
        /// <param name="permission">Page Permission</param>
        /// <returns>MVC Html String</returns>
        public static MvcHtmlString Menu(this HtmlHelper helper, List<UserContext.PagePermission> permission)
        {
            if (permission != null)
            {
                StringBuilder menuString = new StringBuilder();
                menuString.Append("<div id='sidebar-menu' class='main_menu_side hidden-print main_menu'>");
                menuString.Append("<div class='menu_section'>");
                menuString.Append("<ul class='nav side-menu'>");


                // General
                if (permission.Any(x => x.CanWrite && (
                    x.PageId == Pages.General.User
                    || x.PageId == Pages.General.Designation
                    || x.PageId == Pages.General.CommonConfiguartion

                    || x.PageId == Pages.General.RoleLevel
                    || x.PageId == Pages.General.RoleMaster)))
                {

                    #region Application Roles

                    menuString.Append("<li>");
                    menuString.AppendFormat(GetParentMenu(Resource.Configuration, "fa fa-gears"));
                    menuString.Append("<ul class='nav child_menu'>");

                    if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.CommonConfiguartion))
                        menuString.Append(GetSubMenu(Resource.ApplicationConfiguration, "fa fa-language", "/Configuration/Application"));

                    if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.Designation))
                        menuString.Append(GetSubMenu(Resource.Designation, "ti-id-badge", "/Configuration/Designation"));

                    if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.User))
                        menuString.Append(GetSubMenu(Resource.User, "zmdi zmdi-accounts-alt", "/Configuration/Users"));

                    if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.RoleMaster))
                        menuString.Append(GetSubMenu(Resource.Roles, "zmdi zmdi-accounts-alt", "/Configuration/Role/Master"));

                    if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.RoleLevel))
                        menuString.Append(GetSubMenu(Resource.RoleLevel, "zmdi zmdi-accounts-alt", "/Configuration/Role/Level"));

                    menuString.Append("</ul></li>");
                }

                #endregion

                #region Medical CheckUp

                string medicalInventoryMenuTitle = string.Format("{0} {1}", Resource.MedicalCheckup, Resource.Inventory);

                menuString.Append("<li>");
                menuString.AppendFormat(GetParentMenu(medicalInventoryMenuTitle, "icofont-first-aid"));
                menuString.Append("<ul class='nav child_menu'>");

                //Inventory
                menuString.Append(GetSubMenu(Resource.MedicineOpeningStock, "icofont-drug", "/InventoryManagement/Inventory/MedicineOpeningStock/"));
                menuString.Append(GetSubMenu(Resource.MedicineReceived, "icofont-capsule", "/InventoryManagement/Inventory/MedicineReceived/"));
                menuString.Append(GetSubMenu(Resource.MedicineDeadStock, "icofont-drug-pack", "/InventoryManagement/Inventory/DeadMedicineStock/"));
                menuString.Append(GetSubMenu(Resource.MedicineConsumptionByAgency, "icofont-pills", "/InventoryManagement/Inventory/DailyMedicineConsumption/"));

                menuString.Append("</ul></li>");

                #endregion




                #region HW Dashboard

                menuString.Append("<li>");
                menuString.Append(GetSubMenu(Resource.Common , "zmdi zmdi-accounts-alt", "/Configuration/HWM/WasteActionCenter"));
                menuString.Append("</li>");

                #endregion


                #region Waste Disposal Search

                menuString.Append("<li>");
                menuString.Append(GetSubMenu(Resource.WasteDisposalSearch, "zmdi zmdi-accounts-alt", "/Configuration/HWM/WasteDisposalSearch"));
                menuString.Append("</li>");

                #endregion


                #region Waste Generation Search

                menuString.Append("<li>");
                menuString.Append(GetSubMenu(Resource.WasteGenerationStorageSearch, "zmdi zmdi-accounts-alt", "/Configuration/HWM/WasteGenerationStorageSearch"));
                menuString.Append("</li>");

                #endregion


                #region Hazardous Waste

                menuString.Append("<li>");
                menuString.AppendFormat(GetParentMenu(Resource.HazardousWaste, "fa fa-gears"));
                menuString.Append("<ul class='nav child_menu'>");

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.CommonConfiguartion))
                    menuString.Append(GetSubMenu(Resource.GeneratefunctionwiseHW, "fa fa-language", "/Configuration/HWM/HWGenerationStorage"));

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.Designation))
                    menuString.Append(GetSubMenu(Resource.HWDisposal, "ti-id-badge", "/Configuration/HWM/HWDisposal"));
                menuString.Append("</ul></li>");

                #endregion

                #region Battery Waste

                menuString.Append("<li>");
                menuString.AppendFormat(GetParentMenu(Resource.BatteryWaste, "fa fa-gears"));
                menuString.Append("<ul class='nav child_menu'>");

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.CommonConfiguartion))
                    menuString.Append(GetSubMenu(Resource.GeneratefunctionwiseBW, "fa fa-language", "/Configuration/HWM/BatteryWasteGenerationStorage"));

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.Designation))
                    menuString.Append(GetSubMenu(Resource.BWDisposal, "ti-id-badge", "/Configuration/HWM/BatteryWasteDisposal"));
                menuString.Append("</ul></li>");

                #endregion

                #region e-Waste

                menuString.Append("<li>");
                menuString.AppendFormat(GetParentMenu("e-Waste", "fa fa-gears"));
                menuString.Append("<ul class='nav child_menu'>");

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.CommonConfiguartion))
                    menuString.Append(GetSubMenu(Resource.GeneratefunctionwiseEW, "fa fa-language", "/Configuration/HWM/eWasteGenerationStorage"));

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.Designation))
                    menuString.Append(GetSubMenu(Resource.EWDisposal, "ti-id-badge", "/Configuration/HWM/eWasteDisposal"));
                menuString.Append("</ul></li>");

                #endregion

                #region Biomedical Waste

                menuString.Append("<li>");
                menuString.AppendFormat(GetParentMenu(Resource.BiomedicalWaste, "fa fa-gears"));
                menuString.Append("<ul class='nav child_menu'>");

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.CommonConfiguartion))
                    menuString.Append(GetSubMenu(Resource.DaywiseBiomedicalWaste, "fa fa-language", "/Configuration/HWM/DailyBiomedicalWasteRecord"));

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.Designation))
                    menuString.Append(GetSubMenu(Resource.BiomedicalWasteReport, "ti-id-badge", "/Configuration/HWM/BiomedicalWasteReport"));
                menuString.Append("</ul></li>");

                #endregion

                #region Ash Disposal Record

                menuString.Append("<li>");
                menuString.AppendFormat(GetParentMenu(Resource.AshDisposal, "fa fa-gears"));
                menuString.Append("<ul class='nav child_menu'>");

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.CommonConfiguartion))
                    menuString.Append(GetSubMenu(Resource.MonthwiseAshdisposal, "fa fa-language", "/Configuration/HWM/MonthlyAshDisposalRecord"));

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.Designation))
                    menuString.Append(GetSubMenu(Resource.Ashdisposalreport, "ti-id-badge", "/Configuration/HWM/AshUtilizationReport"));
                menuString.Append("</ul></li>");

                #endregion

                #region Municipal Solid Waste

                menuString.Append("<li>");
                menuString.AppendFormat(GetParentMenu(Resource.MunicipalSolidWaste, "fa fa-gears"));
                menuString.Append("<ul class='nav child_menu'>");

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.CommonConfiguartion))
                    menuString.Append(GetSubMenu(Resource.Daywisemunicipalsolidwaste, "fa fa-language", "/Configuration/HWM/DailyMunicipalSolidWasteRecord"));

                if (permission.Any(x => x.CanWrite && x.PageId == Pages.General.Designation))
                    menuString.Append(GetSubMenu(Resource.Municipalsolidwastereport, "ti-id-badge", "/Configuration/HWM/MunicipalSolidWasteReport"));
                menuString.Append("</ul></li>");

                #endregion

                menuString.Append("<ul class='nav child_menu'>");

                //Inventory
                menuString.Append(GetSubMenu(Resource.MedicineOpeningStock, "icofont-drug", "/InventoryManagement/Inventory/MedicineOpeningStock/"));
                menuString.Append(GetSubMenu(Resource.MedicineReceived, "icofont-capsule", "/InventoryManagement/Inventory/MedicineReceived/"));
                menuString.Append(GetSubMenu(Resource.MedicineDeadStock, "icofont-drug-pack", "/InventoryManagement/Inventory/DeadMedicineStock/"));
                menuString.Append(GetSubMenu(Resource.MedicineConsumptionByAgency, "icofont-pills", "/InventoryManagement/Inventory/DailyMedicineConsumption/"));

                menuString.Append("</ul></li>");

                //menuString.Append("<li>");
                //menuString.AppendFormat(GetParentMenu(Resource.HWM, "icofont-first-aid"));
                //menuString.Append("<ul class='nav child_menu'>");

                ////Inventory
                //menuString.Append(GetSubMenu(Resource.HWMGenerationAndStorage, "icofont-drug", "/Configuration/HWM/HWGenerationStorage/"));
                //menuString.Append(GetSubMenu(Resource.BWGSHeader, "icofont-capsule", "/Configuration/HWM/BatteryWasteGenerationStorage/"));
                //menuString.Append(GetSubMenu(Resource.eWGSHeader, "icofont-drug-pack", "/Configuration/HWM/eWasteGenerationStorage/"));
                //menuString.Append(GetSubMenu(Resource.DBWRHeader, "icofont-pills", "/Configuration/HWM/DailyBiomedicalWasteRecord/"));

                //menuString.Append("</ul></li>");


                menuString.AppendFormat(GetMainMenu(string.Format("{0} {1}", Resource.Action, Resource.Item), "fa fa-tasks", "/TaskManagement/ActionItem"));



                menuString.Append("</ul></div></div>");

                string menu = menuString.ToString();
                return new MvcHtmlString(menu);
            }
            else
            {
                var context = new RequestContext(new HttpContextWrapper(System.Web.HttpContext.Current), new RouteData());
                var urlHelper = new UrlHelper(context);
                var url = urlHelper.Action("Login", "Account");
                System.Web.HttpContext.Current.Response.Redirect(url);
                return new MvcHtmlString(string.Empty);
            }
        }

        /// <summary>
        /// Get Widget Menu
        /// </summary>
        /// <param name="helper">Html Helper</param>
        /// <param name="permission">Page Permission</param>
        /// <returns>Widget Menu String</returns>
        public static MvcHtmlString WidgetMenu(this HtmlHelper helper, List<UserContext.PagePermission> permission)
        {
            StringBuilder menuString = new StringBuilder();

            //if (permission.Any(x => x.CanWrite && (
            //    x.PageId == Pages.CustomDashboard.TaskManagementWidget
            //    || x.PageId == Pages.CustomDashboard.IncidentManagementWidget
            //    || x.PageId == Pages.CustomDashboard.CAPAManagementWidget
            //    || x.PageId == Pages.CustomDashboard.AuditManagementWidget
            //    || x.PageId == Pages.CustomDashboard.BBSObservationWidget
            //    || x.PageId == Pages.CustomDashboard.SiteInspectionStatisticsWidget
            //    || x.PageId == Pages.CustomDashboard.SafetyObservationWidget)))
            //{
            //    menuString.Append("<div class='pull-right'>");
            //    menuString.Append("<button class='btn btn-default waves-effect dropdown-toggle' type='button' aria-haspopup='false' aria-expanded='false' style='margin-top: -5px;' title='" + @Resource.Save + "' ng-click='save()'>");
            //    menuString.Append("<i class='fa fa-save'></i>");
            //    menuString.Append("</button>");
            //    menuString.Append("</div>");


            //    menuString.Append("<div class='pull-right'>");
            //    menuString.Append("<div class='dropdown'>");
            //    menuString.Append("<button class='btn btn-default waves-effect dropdown-toggle' type='button' id='dropdownMenu1' data-toggle='dropdown' aria-haspopup='true' aria-expanded='true' style='margin-top: -5px;'>");
            //    menuString.Append("<i class='fa fa-cog' title='" + Resource.AddWidget + "'></i>&nbsp; &nbsp;<span class='caret'></span>");
            //    menuString.Append("</button>&nbsp;");

            //    menuString.Append("<ul class='dropdown-menu dropdown-menu-right' aria-labelledby='dropdownMenu4'>");
            //    if (permission.Any(x => x.CanWrite && x.PageId == Pages.CustomDashboard.TaskManagementWidget))
            //    {
            //        menuString.Append(GetWidgetMenu(Resource.TaskManagement, 1));
            //    }

            //    if (permission.Any(x => x.CanWrite && x.PageId == Pages.CustomDashboard.IncidentManagementWidget))
            //    {
            //        menuString.Append(GetWidgetMenu(Resource.IncidentManagement, 2));
            //    }

            //    if (permission.Any(x => x.CanWrite && x.PageId == Pages.CustomDashboard.CAPAManagementWidget))
            //    {
            //        menuString.Append(GetWidgetMenu(Resource.CAPAManagement, 3));
            //    }

            //    if (permission.Any(x => x.CanWrite && x.PageId == Pages.CustomDashboard.AuditManagementWidget))
            //    {
            //        menuString.Append(GetWidgetMenu(Resource.AuditManagement, 4));
            //    }

            //    if (permission.Any(x => x.CanWrite && x.PageId == Pages.CustomDashboard.BBSObservationWidget))
            //    {
            //        menuString.Append(GetWidgetMenu(Resource.BBSObservation, 5));
            //    }

            //    if (permission.Any(x => x.CanWrite && x.PageId == Pages.CustomDashboard.SiteInspectionStatisticsWidget))
            //    {
            //        menuString.Append(GetWidgetMenu(Resource.SiteInspectionStatistics, 6));
            //    }

            //    if (permission.Any(x => x.CanWrite && x.PageId == Pages.CustomDashboard.SafetyObservationWidget))
            //    {
            //        menuString.Append(GetWidgetMenu(Resource.SafetyObservationStatistics, 12));
            //    }
            //    menuString.Append("</ul>");
            //    menuString.Append("</div>");
            //    menuString.Append("</div>");
            //}

            string menu = menuString.ToString();
            return new MvcHtmlString(menu);
        }

        /// <summary>
        /// Get Role Array
        /// </summary>
        /// <param name="helper">Html Helper</param>
        /// <param name="roles">Role List</param>
        /// <returns>MVC Html String</returns>
        public static MvcHtmlString RoleJson(this HtmlHelper helper, List<UserContext.Role> roles)
        {
            string roleString = JsonConvert.SerializeObject(roles);
            return new MvcHtmlString(roleString);
        }

        /// <summary>
        /// Get Main Menu
        /// </summary>
        /// <param name="title">Menu title</param>
        /// <param name="icon">Menu icon</param>
        /// <param name="url">Menu URL</param>
        /// <returns>Menu string</returns>
        private static string GetMainMenu(string title, string icon, string url)
        {
            return string.Format("<li><a href='{2}' title='{0}'><i class='{1}'></i><span class='nav-label'>{0}</span></a></li>", title, icon, url);
        }

        /// <summary>
        /// Get Parent Menu
        /// </summary>
        /// <param name="title">Menu title</param>
        /// <param name="icon">Menu icon</param>
        /// <returns>Menu string</returns>
        private static string GetParentMenu(string title, string icon)
        {
            return string.Format("<a title='{0}'><i class='{1}'></i><span class='nav-label'>{0}</span><span class='fa fa-chevron-right'></span></a>", title, icon);
        }

        /// <summary>
        /// Get Sub Menu
        /// </summary>
        /// <param name="title">Menu title</param>
        /// <param name="icon">Menu icon</param>
        /// <param name="url">Menu URL</param>
        /// <returns>Menu string</returns>
        private static string GetSubMenu(string title, string icon, string url)
        {
            return string.Format("<li><a href='{2}'><i class='{1}'></i><span>{0}</span><div class='c'></div></a><div class='c'></div></li>", title, icon, url);
        }

        /// <summary>
        /// Get Widget Menu
        /// </summary>
        /// <param name="title">Widget Menu title</param>
        /// <param name="id">Widget id</param>
        /// <returns>Widget Menu string</returns>
        private static string GetWidgetMenu(string title, int id)
        {
            return string.Format("<li class='dropdown-header'><a href='#' ng-click='addWidget({1})'>{0}</a></li>", title, id);
        }
    }
}