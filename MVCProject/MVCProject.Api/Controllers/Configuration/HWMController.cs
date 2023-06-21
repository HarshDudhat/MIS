// -----------------------------------------------------------------------
// <copyright file="DesignationsController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Controllers.Configuration
{
    using Elmah;
    #region Namespaces

    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Api.ViewModel;
    using MVCProject.Common.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Web.Http;

    #endregion

    /// <summary>
    /// Holds Designations Master related methods
    /// </summary>
    public class HWMController : BaseController
    {
        /// <summary>
        /// Holds context object. 
        /// </summary>
        private MVCProjectEntities entities;
        /// <summary>
        /// Disposes expensive resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether to dispose or not.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.entities != null)
                {
                    this.entities.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignationsController"/> class.
        /// </summary>
        public HWMController()
        {
            this.entities = new MVCProjectEntities();
        }


        #region Dashboard

        [HttpPost]
        public ApiResponse GetWasteStorage(PagingParams wasteStorageDetailParams, string asda = "")
        {
            if (string.IsNullOrWhiteSpace(wasteStorageDetailParams.Search))
                wasteStorageDetailParams.Search = string.Empty;

            var queryable = (from wg in this.entities.WasteGenerations

                             join site in this.entities.View_GetConfigLevel.Where(m => m.LanguageId == 1) on wg.SiteLeveleId equals site.LevelId into sites
                             from site in sites.DefaultIfEmpty()

                             join fn in this.entities.View_GetConfigLevel.Where(m => m.LanguageId == 1) on wg.FunctionLevelId equals fn.LevelId into fns
                             from fn in fns.DefaultIfEmpty()

                             join usr in this.entities.Users on wg.EntryBy equals usr.UserId into usrs
                             from usr in usrs.DefaultIfEmpty()

                             join wt in this.entities.WasteType on wg.WasteTypeId equals wt.WasteTypeId into wts
                             from wt in wts.DefaultIfEmpty()

                             join msm in this.entities.MethodStorageMaster on wg.MethodofStorage equals msm.StorageId into msms
                             from msm in msms.DefaultIfEmpty()

                             select new
                             {
                                 WasteGenerationId = wg.WasteGenerationId,
                                 SiteLeveleId = wg.SiteLeveleId,
                                 FunctionLevelId = wg.FunctionLevelId,
                                 WasteTypeId = wg.WasteTypeId,
                                 RequestedDate = wg.RequestedDate,
                                 MethodofStorage = wg.MethodofStorage,
                                 MonitoringDate = wg.MonitoringDate,
                                 IsActive = wg.IsActive,
                                 EntryDate = wg.EntryDate,
                                 EntryBy = wg.EntryBy,
                                 UpdateDate = wg.UpdateDate,
                                 UpdateBy = wg.UpdateBy,
                                 StatusId = wg.StatusId,
                                 DocumentNo = wg.DocumentNo,
                                 Characteristics = wg.Characteristics,
                                 IncompatibleWasteSubstances = wg.IncompatibleWasteSubstances,
                                 WasteTypeName = wt.WasteType1,
                                 FunctionalWasteTypeId = wg.FunctionalWasteTypeId,
                                 FunctionalWasteTypeName = wg.FunctionalWasteTypeName,
                                 WastePhysicalState = wg.WastePhysicalState,
                                 Address = wg.Address,
                                 EPRAuthorizationIssueDate = wg.EPRAuthorizationIssueDate,
                                 EPRAuthorizationValidityYear = wg.EPRAuthorizationValidityYear,
                                 FirstName = usr.FirstName ?? "",
                                 LastName = usr.LastName ?? "",
                                 SiteName = site.Name,
                                 FunctionLevelName = fn.Name,
                                 StorageName = msm.StorageName,
                                 Quantity = this.entities.WasteGenerationDetails.Where(m => m.WasteGenerationId == wg.WasteGenerationId).Sum(a => a.Quantity)
                             }).AsQueryable();


            if (!string.IsNullOrEmpty(wasteStorageDetailParams.Search))
            {
                queryable = queryable.Where(m => (
                                m.DocumentNo.Trim().ToLower().Contains(wasteStorageDetailParams.Search.Trim().ToLower())
                                || m.FirstName.Trim().ToLower().Contains(wasteStorageDetailParams.Search.Trim().ToLower())
                                || m.LastName.Trim().ToLower().Contains(wasteStorageDetailParams.Search.Trim().ToLower())
                                || m.FunctionLevelName.Trim().ToLower().Contains(wasteStorageDetailParams.Search.Trim().ToLower())
                                || m.SiteName.Trim().ToLower().Contains(wasteStorageDetailParams.Search.Trim().ToLower())
                                || m.StorageName.Trim().ToLower().Contains(wasteStorageDetailParams.Search.Trim().ToLower())
                                || m.WasteTypeName.Trim().ToLower().Contains(wasteStorageDetailParams.Search.Trim().ToLower())
                                )
                             );
            }

            int totalRecord = queryable.Count();

            queryable = queryable.OrderByField(wasteStorageDetailParams.OrderByColumn, wasteStorageDetailParams.IsAscending).Skip((wasteStorageDetailParams.CurrentPageNumber - 1) * wasteStorageDetailParams.PageSize).Take(wasteStorageDetailParams.PageSize);

            return this.Response(Utilities.MessageTypes.Success, responseToReturn: queryable.ToList(), totalRecord: totalRecord);
        }

        [HttpPost]
        public ApiResponse GetWasteDisposal(PagingParams wasteDisposalParams)
        {
            if (string.IsNullOrWhiteSpace(wasteDisposalParams.Search))
                wasteDisposalParams.Search = string.Empty;

            var queryable = (from wd in this.entities.WasteDisposals

                             join site in this.entities.View_GetConfigLevel.Where(m => m.LanguageId == 1) on wd.SiteLeveleId equals site.LevelId into sites
                             from site in sites.DefaultIfEmpty()

                             join usr in this.entities.Users on wd.EntryBy equals usr.UserId into usrs
                             from usr in usrs.DefaultIfEmpty()

                             join wt in this.entities.WasteType on wd.WasteTypeId equals wt.WasteTypeId into wts
                             from wt in wts.DefaultIfEmpty()

                             join stm in this.entities.StatusMaster on wd.StatusId equals stm.StatusId into stms
                             from stm in stms.DefaultIfEmpty()


                             select new
                             {
                                 wd.WasteDisposalId,
                                 wd.SiteLeveleId,
                                 wd.WasteTypeId,
                                 wd.RequestedDate,
                                 wd.SenderAuthno,
                                 wd.Mainfestdocno,
                                 wd.TranspoterName,
                                 wd.ConatctNo,
                                 wd.Emailaddress,
                                 wd.Transporteraddress,
                                 wd.TranspoterRegNo,
                                 wd.VehicleType,
                                 wd.TranspoterVehicleNo,
                                 wd.RecieverName,
                                 wd.RecieverConatctNo,
                                 wd.RecieverEmailaddress,
                                 wd.Recieveraddress,
                                 wd.RecieverAuthorisation,
                                 wd.IsActive,
                                 wd.EntryDate,
                                 wd.EntryBy,
                                 wd.UpdateDate,
                                 wd.UpdateBy,
                                 wd.StatusId,
                                 wd.TranspoterFax,
                                 wd.TranspoterContactPerson,
                                 wd.TranspoterContactPersonPhone,
                                 wd.RecieverFax,
                                 wd.RecieverContactPerson,
                                 wd.RecieverContactPersonPhone,
                                 wd.EmergencyContactPerson,
                                 wd.EmergencyContactPersonPhone,
                                 wd.DocumentNo,
                                 wd.FunctionalWasteTypeId,
                                 wd.FunctionalWasteTypeName,
                                 SiteName = site.Name,
                                 stm.StatusName,
                                 WasteTypeName = wt.WasteType1,
                                 FirstName = usr.FirstName ?? "",
                                 LastName = usr.LastName ?? "",
                                 AttachmentCount = this.entities.Attachments.Where(c => c.ModuleId == 1 && c.ReferenceId == wd.WasteDisposalId).Count(),
                                 Quantity = this.entities.WasteDisposalDetails.Where(c => c.WasteDisposalId == wd.WasteDisposalId).Sum(m => m.Quantity),
                                 WasteDisposalDetails = this.entities.WasteDisposalDetails.Where(c => c.WasteDisposalId == wd.WasteDisposalId).Select(m => new { m.UOM, m.Quantity, m.WasteCategoryId })
                             }).AsQueryable();

            if (!string.IsNullOrEmpty(wasteDisposalParams.Search))
            {
                queryable = queryable.Where(m => (
                                m.DocumentNo.Trim().ToLower().Contains(wasteDisposalParams.Search.Trim().ToLower())
                                || m.FirstName.Trim().ToLower().Contains(wasteDisposalParams.Search.Trim().ToLower())
                                || m.LastName.Trim().ToLower().Contains(wasteDisposalParams.Search.Trim().ToLower())
                                || m.StatusName.Trim().ToLower().Contains(wasteDisposalParams.Search.Trim().ToLower())
                                || m.SiteName.Trim().ToLower().Contains(wasteDisposalParams.Search.Trim().ToLower())
                                //|| m.StorageName.Trim().ToLower().Contains(wasteDisposalParams.Search.Trim().ToLower())
                                || m.FunctionalWasteTypeName.Trim().ToLower().Contains(wasteDisposalParams.Search.Trim().ToLower())
                                )
                             );
            }

            int totalRecord = queryable.Count();

            queryable = queryable.OrderByField(wasteDisposalParams.OrderByColumn, wasteDisposalParams.IsAscending).Skip((wasteDisposalParams.CurrentPageNumber - 1) * wasteDisposalParams.PageSize).Take(wasteDisposalParams.PageSize);

            return this.Response(Utilities.MessageTypes.Success, responseToReturn: queryable.ToList(), totalRecord: totalRecord);
        }

        #endregion

        #region Common Method

        [HttpGet]
        public ApiResponse GetWasteCategotyForDropdown()
        {
            var data = this.entities.WasteCategory.Select(m => new { Id = m.WasteCategoryId, Name = m.WasteDescription }).ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }

        [HttpGet]
        public ApiResponse GetUOMForDropdown()
        {
            var data = this.entities.UnitofMeasure.Select(m => new { Id = m.UnitId, Name = m.UnitShortCode + "-" + m.UnitName }).ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }


        [HttpGet]
        public ApiResponse GetWasteCategotyDetails(int WasteCategoryId)
        {
            var data = (from wc in this.entities.WasteCategory
                        join uom in this.entities.UnitofMeasure on wc.UnitOfMeasure equals uom.UnitId
                        join ws in this.entities.WasteState on wc.WastestateId equals ws.WasteStateId
                        where wc.WasteCategoryId == WasteCategoryId
                        select new
                        {
                            uom.UnitId,
                            uom.UnitName,
                            uom.UnitShortCode,
                            ws.WasteStateId,
                            ws.WasteStateName
                        }).FirstOrDefault();


            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }

        [HttpGet]
        public ApiResponse GetWasteStorageForDropdown()
        {
            var data = this.entities.MethodStorageMaster.Select(m => new
            {
                Id = m.StorageId,
                Name = m.StorageName
            }).ToList();

            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }

        [HttpGet]
        public ApiResponse GetWasteDisposalDetails(int WasteDisposalId)
        {
            var wasteDisposal = (from wd in this.entities.WasteDisposals.Where(m => m.WasteDisposalId == WasteDisposalId)

                                 join site in this.entities.View_GetConfigLevel.Where(m => m.LanguageId == 1) on wd.SiteLeveleId equals site.LevelId into sites
                                 from site in sites.DefaultIfEmpty()

                                 join usr in this.entities.Users on wd.EntryBy equals usr.UserId into usrs
                                 from usr in usrs.DefaultIfEmpty()

                                 join wt in this.entities.WasteType on wd.WasteTypeId equals wt.WasteTypeId into wts
                                 from wt in wts.DefaultIfEmpty()
                                 select new
                                 {
                                     wd.WasteDisposalId,
                                     wd.SiteLeveleId,
                                     wd.WasteTypeId,
                                     wd.RequestedDate,
                                     wd.SenderAuthno,
                                     wd.Mainfestdocno,
                                     wd.TranspoterName,
                                     wd.ConatctNo,
                                     wd.Emailaddress,
                                     wd.Transporteraddress,
                                     wd.TranspoterRegNo,
                                     wd.VehicleType,
                                     wd.TranspoterVehicleNo,
                                     wd.RecieverName,
                                     wd.RecieverConatctNo,
                                     wd.RecieverEmailaddress,
                                     wd.Recieveraddress,
                                     wd.RecieverAuthorisation,
                                     wd.IsActive,
                                     wd.EntryDate,
                                     wd.EntryBy,
                                     wd.UpdateDate,
                                     wd.UpdateBy,
                                     wd.StatusId,
                                     wd.TranspoterFax,
                                     wd.TranspoterContactPerson,
                                     wd.TranspoterContactPersonPhone,
                                     wd.RecieverFax,
                                     wd.RecieverContactPerson,
                                     wd.RecieverContactPersonPhone,
                                     wd.EmergencyContactPerson,
                                     wd.EmergencyContactPersonPhone,
                                     wd.DocumentNo,
                                     wd.FunctionalWasteTypeId,
                                     wd.FunctionalWasteTypeName,
                                     wd.Address,
                                     wd.MailingAddress,
                                     wd.TranspoterConatctNo,
                                     wd.TranspoterEmail,
                                     SiteName = site.Name
                                 }).FirstOrDefault();

            if (wasteDisposal != null)
            {


                var wasteDisposalDetailList = (from wdds in this.entities.WasteDisposalDetails.Where(m => m.WasteDisposalId == WasteDisposalId)

                                               join catg in this.entities.WasteCategory on wdds.WasteCategoryId equals catg.WasteCategoryId into catgs
                                               from catg in catgs.DefaultIfEmpty()

                                                   //join itm in this.entities.WasteCategory on wdds.Item equals itm.WasteCategoryId into itms
                                                   //from itm in itms.DefaultIfEmpty()

                                               join wu in this.entities.WasteCategory on wdds.UnitWeight equals wu.WasteCategoryId into wus
                                               from wu in wus.DefaultIfEmpty()

                                               join ws in this.entities.WasteState on wdds.PhysicalState equals ws.WasteStateId into wss
                                               from ws in wss.DefaultIfEmpty()

                                               join uom in this.entities.UnitofMeasure on wdds.UOM equals uom.UnitShortCode into uoms
                                               from uom in uoms.DefaultIfEmpty()

                                               select new
                                               {
                                                   wdds.WasteDisposalDetailId,
                                                   wdds.WasteDisposalId,
                                                   wdds.WasteCategoryId,
                                                   wdds.Quantity,
                                                   wdds.NoofContainer,
                                                   wdds.Weight,
                                                   wdds.WasteTypeId,
                                                   wdds.Characteristics,
                                                   wdds.IncompatibleWasteSubstances,
                                                   wdds.PhysicalState,
                                                   wdds.UOM,
                                                   wdds.UnitWeight,
                                                   wdds.Item,
                                                   WasteCategoryName = catg.WasteDescription,
                                                   //ItemName = itm.WasteDescription,
                                                   WeightUnitName = wu.WasteDescription,
                                                   wdds.WasteDescription,
                                                   uom.UnitName,
                                                   UOMName = uom.UnitShortCode,
                                                   ws.WasteStateId,
                                                   PhysicalStateName = ws.WasteStateName

                                               }).ToList();

                return this.Response(Utilities.MessageTypes.Success, string.Empty, new { wasteDisposal, wasteDisposalDetailList });
            }
            else
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.NoRecordFound));
            }
        }

        [HttpGet]
        public ApiResponse GetWasteTypeForDropdown()
        {
            var data = this.entities.WasteType.Select(m => new { Id = m.WasteTypeId, Name = m.WasteType1 }).ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }

        [HttpGet]
        public ApiResponse GetStatusMasterForDropdown()
        {
            var data = this.entities.StatusMaster.Select(m => new { Id = m.StatusId, Name = m.StatusName }).ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }

        [HttpGet]
        public ApiResponse GetWasteStateForDropdown()
        {
            var data = this.entities.WasteState.Select(m => new { Id = m.WasteStateName, Name = m.WasteStateId }).ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }


        /// <summary>
        /// Gets the report no.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="date">The date.</param>
        /// <param name="safetyObservationSrNo">The safety observation no.</param>
        /// <returns>String SSafetyObservation Report Number</returns>
        private string GetReportNo(WasteGeneration WasteGeneration)
        {
            //SiteLevelCode/ModuleCode/Year/SrNo
            string siteLevelCode = Convert.ToString(WasteGeneration.SiteLeveleId);//  AppUtility.GetParentLevelFromHierarchy(safetyObservation.SubLocationLevelId.Value, (int)HierarchyLevel.Site, UserContext).FirstOrDefault().LevelCode;
            string safetyObservationShort = Convert.ToString(WasteGeneration.FunctionLevelId);// Resource.SafetyObservationShort;
            return string.Format("{0}/{1}/{2}/{3}", siteLevelCode, safetyObservationShort, DateTime.UtcNow.Year /*WasteGeneration.ObservationDate.Year*/, WasteGeneration.WasteGenerationId.ToString("0000"));
        }

        private string GetWDReportNo(WasteDisposal WasteDisposal)
        {
            //SiteLevelCode/ModuleCode/Year/SrNo
            string siteLevelCode = Convert.ToString(WasteDisposal.SiteLeveleId);
            string safetyObservationShort = Convert.ToString(WasteDisposal.WasteTypeId);
            return string.Format("{0}/{1}/{2}/{3}", siteLevelCode, safetyObservationShort, DateTime.UtcNow.Year, WasteDisposal.WasteDisposalId.ToString("0000"));
        }

        public class getMonthName
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [HttpGet]
        public ApiResponse GetMonthListForDropdown()
        {
            getMonthName onjMonth = new getMonthName();
            Dictionary<int, string> Months = Enumerable.Range(1, 12).Select(i => new KeyValuePair<int, string>(i, System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(i))).ToDictionary(x => x.Key, x => x.Value);

            var data = Months.Select(m => new { Id = m.Key, Name = m.Value }).ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }

        [HttpGet]
        public ApiResponse GetVehicleTypeForDropdown()
        {
            var data = this.entities.VehicleTypes.Select(m => new { Id = m.VehicleTypeId, Name = m.VehicleType1 }).ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }

        [HttpGet]
        public ApiResponse GetWasteDetails(int WasteGenerationId)
        {
            var wasteGenerations = (from wd in this.entities.WasteGenerations.Where(m => m.WasteGenerationId == WasteGenerationId)

                                    join site in this.entities.View_GetConfigLevel.Where(m => m.LanguageId == 1) on wd.SiteLeveleId equals site.LevelId into sites
                                    from site in sites.DefaultIfEmpty()

                                    join fn in this.entities.View_GetConfigLevel.Where(m => m.LanguageId == 1) on wd.FunctionLevelId equals fn.LevelId into fns
                                    from fn in fns.DefaultIfEmpty()

                                    join usr in this.entities.Users on wd.EntryBy equals usr.UserId into usrs
                                    from usr in usrs.DefaultIfEmpty()

                                    join wt in this.entities.WasteType on wd.WasteTypeId equals wt.WasteTypeId into wts
                                    from wt in wts.DefaultIfEmpty()
                                    select new
                                    {
                                        wd.WasteGenerationId,
                                        wd.SiteLeveleId,
                                        wd.FunctionLevelId,
                                        wd.WasteTypeId,
                                        wd.RequestedDate,
                                        wd.MethodofStorage,
                                        wd.MonitoringDate,
                                        wd.IsActive,
                                        wd.StatusId,
                                        wd.DocumentNo,
                                        wd.Characteristics,
                                        wd.IncompatibleWasteSubstances,
                                        wd.WastePhysicalState,
                                        wd.Address,
                                        wd.EPRAuthorizationIssueDate,
                                        wd.EPRAuthorizationValidityYear,
                                        wd.EntryDate,
                                        wd.EntryBy,
                                        wd.UpdateDate,
                                        wd.UpdateBy,
                                        SiteName = site.Name,
                                        FunctionLevelName = fn.Name,
                                        usr.Name,
                                        usr.LastName,
                                        usr.MiddleName

                                    }).FirstOrDefault();

            if (wasteGenerations != null)
            {
                var wasteGenerationDetailsList = (from wdds in this.entities.WasteGenerationDetails.Where(m => m.WasteGenerationId == WasteGenerationId)

                                                  join catg in this.entities.WasteCategory on wdds.WasteCategoryId equals catg.WasteCategoryId into catgs
                                                  from catg in catgs.DefaultIfEmpty()

                                                      //join itm in this.entities.WasteCategory on wdds.Item equals itm.WasteCategoryId into itms
                                                      //from itm in itms.DefaultIfEmpty()

                                                  join mos in this.entities.MethodStorageMaster on wdds.MethodofStorage equals mos.StorageId into moss
                                                  from mos in moss.DefaultIfEmpty()

                                                  join uom in this.entities.UnitofMeasure on wdds.MethodofStorage equals uom.UnitId into uoms
                                                  from uom in uoms.DefaultIfEmpty()

                                                  select new
                                                  {
                                                      wdds.WasteGenerationDetailId,
                                                      wdds.WasteGenerationId,
                                                      wdds.WasteCategoryId,
                                                      wdds.MethodofStorage,
                                                      wdds.Quantity,
                                                      wdds.RecQuantity,
                                                      wdds.UOM,
                                                      wdds.Item,
                                                      WasteCategoryName = catg.WasteDescription,
                                                      //ItemName = itm.WasteDescription,
                                                      mos.StorageName,
                                                      uom.UnitName,
                                                      UOMName = uom.UnitShortCode
                                                  }).ToList();

                return this.Response(Utilities.MessageTypes.Success, string.Empty, new { wasteGeneration = wasteGenerations, lstWasteGenerationDetails = wasteGenerationDetailsList });
            }
            else
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.NoRecordFound));
            }
        }

        #endregion

        #region Hazardous Waste Generation

        [HttpPost]
        //UpdateWasteStorage
        public ApiResponse AddUpdateWasteStorage(WasteGenerationViewModel Obj)
        {
            var wasteGeneration = this.entities.WasteGenerations.Where(m => m.WasteGenerationId == Obj.WasteGeneration.WasteGenerationId).FirstOrDefault();
            bool IsEdit = true;

            if (wasteGeneration == null)
            {
                IsEdit = false;
                wasteGeneration = new WasteGeneration();
                wasteGeneration.EntryBy = UserContext.EmployeeId;
                wasteGeneration.EntryDate = DateTime.UtcNow;

                wasteGeneration.SiteLeveleId = Obj.WasteGeneration.SiteLeveleId;
                wasteGeneration.FunctionLevelId = Obj.WasteGeneration.FunctionLevelId;
                wasteGeneration.WasteTypeId = Obj.WasteGeneration.WasteTypeId;
                wasteGeneration.RequestedDate = Obj.WasteGeneration.RequestedDate;
                wasteGeneration.MethodofStorage = Obj.WasteGeneration.MethodofStorage;
                wasteGeneration.MonitoringDate = Obj.WasteGeneration.MonitoringDate;
                wasteGeneration.IsActive = true;
                wasteGeneration.IncompatibleWasteSubstances = Obj.WasteGeneration.IncompatibleWasteSubstances;
                wasteGeneration.Characteristics = Obj.WasteGeneration.Characteristics;
                wasteGeneration.FunctionalWasteTypeId = Obj.WasteGeneration.FunctionalWasteTypeId;
                wasteGeneration.FunctionalWasteTypeName = Obj.WasteGeneration.FunctionalWasteTypeName;
                wasteGeneration.WastePhysicalState = Obj.WasteGeneration.WastePhysicalState;
                wasteGeneration.Address = Obj.WasteGeneration.Address;
                wasteGeneration.EPRAuthorizationIssueDate = Obj.WasteGeneration.EPRAuthorizationIssueDate;
                wasteGeneration.EPRAuthorizationValidityYear = Obj.WasteGeneration.EPRAuthorizationValidityYear;

                this.entities.WasteGenerations.AddObject(wasteGeneration);

                if (this.entities.SaveChanges() > 0)
                {
                    string DocNo = GetReportNo(wasteGeneration);
                    wasteGeneration.DocumentNo = DocNo;
                    this.entities.SaveChanges();


                    if (Obj.WasteGenerationDetail != null)
                    {
                        WasteGenerationDetail wasteGenerationDetail = new WasteGenerationDetail();
                        wasteGenerationDetail.WasteGenerationId = wasteGeneration.WasteGenerationId;
                        wasteGenerationDetail.WasteCategoryId = Obj.WasteGenerationDetail.WasteCategoryId;
                        wasteGenerationDetail.MethodofStorage = Obj.WasteGenerationDetail.MethodofStorage;
                        wasteGenerationDetail.Quantity = Obj.WasteGenerationDetail.Quantity;
                        wasteGenerationDetail.RecQuantity = Obj.WasteGenerationDetail.RecQuantity;
                        wasteGenerationDetail.UOM = Obj.WasteGenerationDetail.UOM;
                        wasteGenerationDetail.Item = Obj.WasteGenerationDetail.Item;
                        this.entities.WasteGenerationDetails.AddObject(wasteGenerationDetail);
                        this.entities.SaveChanges();
                    }
                    else if (Obj.WasteGenerationDetailList != null && Obj.WasteGenerationDetailList.Count > 0)
                    {
                        foreach (var item in Obj.WasteGenerationDetailList)
                        {
                            WasteGenerationDetail wasteGenerationDetail = new WasteGenerationDetail();
                            wasteGenerationDetail.WasteGenerationId = wasteGeneration.WasteGenerationId;
                            wasteGenerationDetail.WasteCategoryId = item.WasteCategoryId;
                            wasteGenerationDetail.MethodofStorage = item.MethodofStorage;
                            wasteGenerationDetail.Quantity = item.Quantity;
                            wasteGenerationDetail.RecQuantity = item.RecQuantity;
                            wasteGenerationDetail.UOM = item.UOM;
                            wasteGenerationDetail.Item = item.Item;
                            this.entities.WasteGenerationDetails.AddObject(wasteGenerationDetail);
                            this.entities.SaveChanges();
                        }
                    }
                    return this.Response(Utilities.MessageTypes.Success, string.Format(IsEdit ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.HWMGenerationAndStorage));
                }
                else
                {
                    return this.Response(Utilities.MessageTypes.Error, string.Format(IsEdit ? Resource.UpdateError : Resource.SaveError, Resource.HWMGenerationAndStorage));
                }
            }
            else if (wasteGeneration != null)
            {
                wasteGeneration.UpdateBy = UserContext.EmployeeId;
                wasteGeneration.UpdateDate = DateTime.UtcNow;

                if (Obj.WasteGeneration.WasteTypeId == 1)
                {
                    wasteGeneration.EPRAuthorizationIssueDate = Obj.WasteGeneration.EPRAuthorizationIssueDate;
                }
                else if (Obj.WasteGeneration.WasteTypeId == 2)
                {
                    wasteGeneration.MonitoringDate = Obj.WasteGeneration.MonitoringDate;
                }

                if (this.entities.SaveChanges() > 0)
                {
                    var objwastegeneration = this.entities.WasteGenerationDetails.Where(m => m.WasteGenerationId == Obj.WasteGeneration.WasteGenerationId).FirstOrDefault();

                    if (objwastegeneration != null)
                    {
                        objwastegeneration.Quantity = Obj.WasteGenerationDetail.Quantity;
                        this.entities.SaveChanges();
                    }
                    return this.Response(Utilities.MessageTypes.Success, string.Format(IsEdit ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.HWMGenerationAndStorage));
                }
                else
                {
                    return this.Response(Utilities.MessageTypes.Error, string.Format(IsEdit ? Resource.UpdateError : Resource.SaveError, Resource.HWMGenerationAndStorage));
                }
            }
            else
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(IsEdit ? Resource.UpdateError : Resource.SaveError, Resource.HWMGenerationAndStorage));
            }
        }

        [HttpPost]
        public ApiResponse UpdateWasteGenerationDetails(List<WasteGenerationDetail> lstWasteGenerationDetails)
        {
            string message = "";
            bool flag = false;
            this.entities.Connection.Open();
            using (DbTransaction dbTransaction = this.entities.Connection.BeginTransaction())
            {
                try
                {
                    if (lstWasteGenerationDetails.Any<WasteGenerationDetail>())
                    {
                        foreach (WasteGenerationDetail generationDetail in lstWasteGenerationDetails)
                        {
                            WasteGenerationDetail objWasteGenerationDetail = generationDetail;
                            WasteGenerationDetail currentEntity = this.entities.WasteGenerationDetails.Where<WasteGenerationDetail>(a => a.WasteGenerationDetailId == objWasteGenerationDetail.WasteGenerationDetailId).FirstOrDefault<WasteGenerationDetail>();
                            if (currentEntity != null)
                            {
                                flag = true;
                                currentEntity.Quantity = objWasteGenerationDetail.Quantity;
                                this.entities.WasteGenerationDetails.ApplyCurrentValues(currentEntity);
                            }

                            int? WasteGenerationId = lstWasteGenerationDetails.First().WasteGenerationId;
                            WasteGeneration objwasteGeneration = this.entities.WasteGenerations.Where(c => c.WasteGenerationId == WasteGenerationId).First();
                            objwasteGeneration.UpdateBy = UserContext.EmployeeId;
                            objwasteGeneration.UpdateDate = DateTime.UtcNow;
                        }
                        if (this.entities.SaveChanges() <= 0)
                        {
                            message = Resource.MedicineOpeningStockSaveFailed;
                        }
                        else
                        {
                            dbTransaction.Commit();
                            this.entities.Connection.Close();
                            return this.Response(MessageTypes.Success, string.Format(flag ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.HWMGenerationAndStorage), null, 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = Resource.MedicineOpeningStockSaveFailed;
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
                dbTransaction.Rollback();
                this.entities.Connection.Close();
                return this.Response(MessageTypes.Error, message, null, 0);
            }
        }

        #endregion


        #region Hazardous Waste Disposal

        [HttpPost]
        public ApiResponse AddWasteDisposal(WasteDisposalViewModel Obj)
        {
            var wasteDisposal = this.entities.WasteDisposals.Where(m => m.WasteDisposalId == Obj.WasteDisposal.WasteDisposalId).FirstOrDefault();
            bool IsEdit = true;

            if (wasteDisposal == null)
            {
                IsEdit = false;
                wasteDisposal = new WasteDisposal();

                wasteDisposal.EntryBy = UserContext.EmployeeId;
                wasteDisposal.EntryDate = DateTime.UtcNow;
                wasteDisposal.IsActive = true;

                wasteDisposal.WasteDisposalId = Obj.WasteDisposal.WasteDisposalId;
                wasteDisposal.SiteLeveleId = Obj.WasteDisposal.SiteLeveleId;
                wasteDisposal.WasteTypeId = Obj.WasteDisposal.WasteTypeId;
                wasteDisposal.RequestedDate = Obj.WasteDisposal.RequestedDate;
                wasteDisposal.SenderAuthno = Obj.WasteDisposal.SenderAuthno;
                wasteDisposal.Mainfestdocno = Obj.WasteDisposal.Mainfestdocno;
                wasteDisposal.TranspoterName = Obj.WasteDisposal.TranspoterName;
                wasteDisposal.ConatctNo = Obj.WasteDisposal.ConatctNo;
                wasteDisposal.Emailaddress = Obj.WasteDisposal.Emailaddress;
                wasteDisposal.Transporteraddress = Obj.WasteDisposal.Transporteraddress;
                wasteDisposal.TranspoterRegNo = Obj.WasteDisposal.TranspoterRegNo;
                wasteDisposal.VehicleType = Obj.WasteDisposal.VehicleType;
                wasteDisposal.TranspoterVehicleNo = Obj.WasteDisposal.TranspoterVehicleNo;
                wasteDisposal.RecieverName = Obj.WasteDisposal.RecieverName;
                wasteDisposal.RecieverConatctNo = Obj.WasteDisposal.RecieverConatctNo;
                wasteDisposal.RecieverEmailaddress = Obj.WasteDisposal.RecieverEmailaddress;
                wasteDisposal.Recieveraddress = Obj.WasteDisposal.Recieveraddress;
                wasteDisposal.RecieverAuthorisation = Obj.WasteDisposal.RecieverAuthorisation;
                wasteDisposal.StatusId = Obj.WasteDisposal.StatusId;
                wasteDisposal.TranspoterFax = Obj.WasteDisposal.TranspoterFax;
                wasteDisposal.TranspoterContactPerson = Obj.WasteDisposal.TranspoterContactPerson;
                wasteDisposal.TranspoterContactPersonPhone = Obj.WasteDisposal.TranspoterContactPersonPhone;
                wasteDisposal.RecieverFax = Obj.WasteDisposal.RecieverFax;
                wasteDisposal.RecieverContactPerson = Obj.WasteDisposal.RecieverContactPerson;
                wasteDisposal.RecieverContactPersonPhone = Obj.WasteDisposal.RecieverContactPersonPhone;
                wasteDisposal.EmergencyContactPerson = Obj.WasteDisposal.EmergencyContactPerson;
                wasteDisposal.EmergencyContactPersonPhone = Obj.WasteDisposal.EmergencyContactPersonPhone;
                wasteDisposal.DocumentNo = Obj.WasteDisposal.DocumentNo;
                wasteDisposal.FunctionalWasteTypeId = Obj.WasteDisposal.FunctionalWasteTypeId;
                wasteDisposal.FunctionalWasteTypeName = Obj.WasteDisposal.FunctionalWasteTypeName;
                wasteDisposal.Address = Obj.WasteDisposal.Address;
                wasteDisposal.MailingAddress = Obj.WasteDisposal.MailingAddress;
                wasteDisposal.TranspoterConatctNo = Obj.WasteDisposal.TranspoterConatctNo;
                wasteDisposal.TranspoterEmail = Obj.WasteDisposal.TranspoterEmail;

                this.entities.WasteDisposals.AddObject(wasteDisposal);
            }

            if (this.entities.SaveChanges() > 0)
            {
                string DocNo = GetWDReportNo(wasteDisposal);
                wasteDisposal.DocumentNo = DocNo;
                this.entities.SaveChanges();

                if (Obj.WasteDisposalDetail != null)
                {
                    WasteDisposalDetail wasteDisposalDetail = new WasteDisposalDetail();
                    wasteDisposalDetail.WasteDisposalId = wasteDisposal.WasteDisposalId;
                    wasteDisposalDetail.WasteDisposalDetailId = Obj.WasteDisposalDetail.WasteDisposalDetailId;
                    wasteDisposalDetail.WasteCategoryId = Obj.WasteDisposalDetail.WasteCategoryId;
                    wasteDisposalDetail.Quantity = Obj.WasteDisposalDetail.Quantity;
                    wasteDisposalDetail.NoofContainer = Obj.WasteDisposalDetail.NoofContainer;
                    wasteDisposalDetail.Weight = Obj.WasteDisposalDetail.Weight;
                    wasteDisposalDetail.WasteTypeId = Obj.WasteDisposalDetail.WasteTypeId;
                    wasteDisposalDetail.Characteristics = Obj.WasteDisposalDetail.Characteristics;
                    wasteDisposalDetail.IncompatibleWasteSubstances = Obj.WasteDisposalDetail.IncompatibleWasteSubstances;
                    wasteDisposalDetail.PhysicalState = Obj.WasteDisposalDetail.PhysicalState;
                    wasteDisposalDetail.UOM = Obj.WasteDisposalDetail.UOM;
                    this.entities.WasteDisposalDetails.AddObject(wasteDisposalDetail);
                    this.entities.SaveChanges();
                }
                else if (Obj.WasteDisposalDetailList != null && Obj.WasteDisposalDetailList.Count > 0)
                {
                    foreach (var item in Obj.WasteDisposalDetailList)
                    {
                        WasteDisposalDetail wasteDisposalDetail = new WasteDisposalDetail();
                        wasteDisposalDetail.WasteDisposalId = wasteDisposal.WasteDisposalId;
                        wasteDisposalDetail.WasteDisposalDetailId = item.WasteDisposalDetailId;
                        wasteDisposalDetail.WasteCategoryId = item.WasteCategoryId;
                        wasteDisposalDetail.Quantity = item.Quantity;
                        wasteDisposalDetail.NoofContainer = item.NoofContainer;
                        wasteDisposalDetail.Weight = item.Weight;
                        wasteDisposalDetail.WasteTypeId = item.WasteTypeId;
                        wasteDisposalDetail.Characteristics = item.Characteristics;
                        wasteDisposalDetail.IncompatibleWasteSubstances = item.IncompatibleWasteSubstances;
                        wasteDisposalDetail.PhysicalState = item.PhysicalState;
                        wasteDisposalDetail.UOM = item.UOM;
                        wasteDisposalDetail.WasteDescription = item.WasteDescription;
                        wasteDisposalDetail.Item = item.Item;
                        wasteDisposalDetail.UnitWeight = item.UnitWeight;
                        this.entities.WasteDisposalDetails.AddObject(wasteDisposalDetail);
                        this.entities.SaveChanges();
                    }
                }
                return this.Response(Utilities.MessageTypes.Success, string.Format(IsEdit ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.HWMGenerationAndStorage));
            }
            else
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(IsEdit ? Resource.UpdateError : Resource.SaveError, Resource.HWMGenerationAndStorage));
            }
        }

        [HttpPost]
        public ApiResponse UpdateWasteDisposalDetails(List<WasteDisposalDetail> lstWasteDisposalDetails)
        {
            string message = "";
            bool flag = false;
            this.entities.Connection.Open();
            using (DbTransaction dbTransaction = this.entities.Connection.BeginTransaction())
            {
                try
                {
                    if (lstWasteDisposalDetails.Any<WasteDisposalDetail>())
                    {
                        foreach (WasteDisposalDetail disposalDetail in lstWasteDisposalDetails)
                        {
                            WasteDisposalDetail objWasteDisposalDetail = disposalDetail;
                            WasteDisposalDetail currentEntity = this.entities.WasteDisposalDetails.Where<WasteDisposalDetail>(a => a.WasteDisposalDetailId == objWasteDisposalDetail.WasteDisposalDetailId).FirstOrDefault<WasteDisposalDetail>();
                            if (currentEntity != null)
                            {
                                flag = true;
                                currentEntity.Quantity = objWasteDisposalDetail.Quantity;
                                this.entities.WasteDisposalDetails.ApplyCurrentValues(currentEntity);
                            }
                        }

                        int WasteDisposalId = lstWasteDisposalDetails.First().WasteDisposalId;
                        WasteDisposal objwasteDisposal = this.entities.WasteDisposals.Where(c => c.WasteDisposalId == WasteDisposalId).First();
                        objwasteDisposal.UpdateBy = UserContext.EmployeeId;
                        objwasteDisposal.UpdateDate = DateTime.UtcNow;

                        if (this.entities.SaveChanges() <= 0)
                        {
                            message = Resource.MedicineOpeningStockSaveFailed;
                        }
                        else
                        {
                            dbTransaction.Commit();
                            this.entities.Connection.Close();
                            return this.Response(MessageTypes.Success, string.Format(flag ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.HWMGenerationAndStorage), null, 0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = Resource.MedicineOpeningStockSaveFailed;
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
                dbTransaction.Rollback();
                this.entities.Connection.Close();
                return this.Response(MessageTypes.Error, message, null, 0);
            }
        }

        #endregion


        #region Biomedical Waste Report

        [HttpPost]
        public ApiResponse AddUpdateDailyBiomedicalWasteRecord(BioMedicalWasteGeneration Obj)
        {
            var bioMedicalWasteGeneration = this.entities.BioMedicalWasteGeneration.Where(m => m.BioMedicalWasteGenerationId == Obj.BioMedicalWasteGenerationId).FirstOrDefault();
            bool IsEdit = true;

            if (Obj.BioMedicalWasteGenerationId == 0)
            {
                IsEdit = false;
                bioMedicalWasteGeneration = new BioMedicalWasteGeneration();
                bioMedicalWasteGeneration.EntryBy = UserContext.EmployeeId;
                bioMedicalWasteGeneration.EntryDate = DateTime.UtcNow;
                bioMedicalWasteGeneration.SiteLeveleId = Obj.SiteLeveleId;
                bioMedicalWasteGeneration.RequestedDate = Obj.RequestedDate;
                bioMedicalWasteGeneration.Quantity = Obj.Quantity;
                bioMedicalWasteGeneration.IsActive = true;

                this.entities.BioMedicalWasteGeneration.AddObject(bioMedicalWasteGeneration);
                this.entities.SaveChanges();
                return this.Response(Utilities.MessageTypes.Success, string.Format(IsEdit ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.HWMGenerationAndStorageSuccess));
            }
            else if (Obj.BioMedicalWasteGenerationId > 0)
            {
                if (bioMedicalWasteGeneration != null)
                {
                    bioMedicalWasteGeneration.SiteLeveleId = Obj.SiteLeveleId;
                    bioMedicalWasteGeneration.RequestedDate = Obj.RequestedDate;
                    bioMedicalWasteGeneration.Quantity = Obj.Quantity;
                    bioMedicalWasteGeneration.UpdateBy = UserContext.EmployeeId;
                    bioMedicalWasteGeneration.UpdateDate = DateTime.UtcNow;
                    this.entities.BioMedicalWasteGeneration.AddObject(bioMedicalWasteGeneration);
                    this.entities.SaveChanges();
                }
                return this.Response(Utilities.MessageTypes.Success, string.Format(IsEdit ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.HWMGenerationAndStorageSuccess));
            }
            else
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(IsEdit ? Resource.UpdateError : Resource.SaveError, Resource.HWMGenerationAndStorageError));
            }
        }

        [HttpGet]
        public ApiResponse GetBiomedicalWasteReport(string Year)
        {
            DataTable dt = new DataTable();
            GeneralFuncations GF = new GeneralFuncations();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FinYear", Year);

            DataSet DS = GF.GetDatatableFromSP("GetBiomedicalWasteReport", parameters);
            dt = DS.Tables[0];

            return this.Response(Utilities.MessageTypes.Success, responseToReturn: dt);
        }

        #endregion

        #region Ash Utilization

        [HttpPost]
        public ApiResponse AddUpdateMonthlyDisposal(AshWasteGenerationViewModel Obj)
        {
            bool IsEdit = true;

            if (Obj.AshWasteGeneration != null)
            {
                AshWasteGeneration objAWG = new AshWasteGeneration();
                objAWG.EntryBy = UserContext.EmployeeId;
                objAWG.EntryDate = DateTime.UtcNow;
                objAWG.MuncipalWasteGenerationId = Obj.AshWasteGeneration.MuncipalWasteGenerationId;
                objAWG.SiteLeveleId = Obj.AshWasteGeneration.SiteLeveleId;
                //objAWG.RequestedDate = Obj.AshWasteGeneration.RequestedDate;
                objAWG.CoalConsumed = Obj.AshWasteGeneration.CoalConsumed;
                objAWG.Ashcontentcoalper = Obj.AshWasteGeneration.Ashcontentcoalper;
                objAWG.TotalAshGeneration = Obj.AshWasteGeneration.TotalAshGeneration;
                objAWG.FlyAshGeneration = Obj.AshWasteGeneration.FlyAshGeneration;
                objAWG.BottomAshGeneration = Obj.AshWasteGeneration.BottomAshGeneration;
                objAWG.FlyAshUtilized = Obj.AshWasteGeneration.FlyAshUtilized;
                objAWG.BottomAshUtilized = Obj.AshWasteGeneration.BottomAshUtilized;
                objAWG.FlyAshUtilizationper = Obj.AshWasteGeneration.FlyAshUtilizationper;
                objAWG.TotalAshUtilizationper = Obj.AshWasteGeneration.TotalAshUtilizationper;
                objAWG.CementMfdUtilization = Obj.AshWasteGeneration.CementMfdUtilization;
                objAWG.ClayMfdUtilization = Obj.AshWasteGeneration.ClayMfdUtilization;
                objAWG.RoadDevUtilization = Obj.AshWasteGeneration.RoadDevUtilization;
                objAWG.Quaryyfilling = Obj.AshWasteGeneration.Quaryyfilling;
                objAWG.AshpondDumping = Obj.AshWasteGeneration.AshpondDumping;
                objAWG.PonfAshLifting = Obj.AshWasteGeneration.PonfAshLifting;
                objAWG.InstalledCapacity = Obj.AshWasteGeneration.InstalledCapacity;
                objAWG.IsActive = true;

                DateTime rqDate = Convert.ToDateTime(Obj.AshWasteGeneration.RequestedYear);
                int year = rqDate.Year;
                int month = Obj.AshWasteGeneration.RequestedMonth;
                //int iMonthNo = 3;
                DateTime dtDate = new DateTime(2000, month, 1);
                string sMonthName = dtDate.ToString("MMM");

                objAWG.RequestedDate = Convert.ToDateTime("1 " + sMonthName + " " + Obj.AshWasteGeneration.selectedyear);

                this.entities.AshWasteGenerations.AddObject(objAWG);
                this.entities.SaveChanges();
                return this.Response(Utilities.MessageTypes.Success, string.Format(IsEdit ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.MADRHeader));

            }
            else
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(IsEdit ? Resource.UpdateError : Resource.SaveError, Resource.HWMGenerationAndStorage));
            }
        }


        [HttpGet]
        public ApiResponse GetAshUtilizationReport(string Year, string SiteId, int Type)
        {
            DataTable dt = new DataTable();
            GeneralFuncations GF = new GeneralFuncations();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (SiteId == null)
            {
                SiteId = "";
            }
            parameters.Add("FinYear", Year);
            parameters.Add("siteId", SiteId);

            DataSet DS = GF.GetDatatableFromSP("GetAshUtilizationReport", parameters);

            if (Type == 100)
            {
                return this.Response(Utilities.MessageTypes.Success, responseToReturn: DS);
            }
            else
            {
                dt = DS.Tables[Type];
                return this.Response(Utilities.MessageTypes.Success, responseToReturn: dt);
            }

        }

        #endregion

        #region Municipal Solid Waste


        [HttpGet]
        public ApiResponse GetMuncipalWasteGenerationYearForDropdown()
        {
            var data = this.entities.MuncipalWasteGeneration.Select(p => new { Id = p.RequestedDate.Value.Year, Name = p.RequestedDate.Value.Year }).Distinct().ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }


        [HttpPost]
        public ApiResponse AddUpdateDailyMunicipalSolidWasteRecord(MuncipalWasteGeneration Obj)
        {
            bool IsEdit = true;

            if (Obj != null)
            {
                Obj.EntryBy = UserContext.EmployeeId;
                Obj.EntryDate = DateTime.UtcNow;
                Obj.IsActive = true;

                this.entities.MuncipalWasteGeneration.AddObject(Obj);
                this.entities.SaveChanges();
                return this.Response(Utilities.MessageTypes.Success, string.Format(IsEdit ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.DMSWRHeader));
            }
            else
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(IsEdit ? Resource.UpdateError : Resource.SaveError, Resource.DMSWRHeader));
            }
        }


        [HttpGet]
        public ApiResponse GetMunicipalSolidWasteReport(string Year, string compostable)
        {
            DataTable dt = new DataTable();
            GeneralFuncations GF = new GeneralFuncations();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string Compostable = "0", NonCompostable = "0", All = "0";

            if (compostable == "1")
            {
                Compostable = "1";
            }
            else if (compostable == "2")
            {
                NonCompostable = "1";
            }
            else if (compostable == "0")
            {
                All = "1";
            }

            parameters.Add("FinYear", Year);
            parameters.Add("Compostable", Compostable);
            parameters.Add("NonCompostable", NonCompostable);
            parameters.Add("All", All);

            DataSet DS = GF.GetDatatableFromSP("GetMunicipalSolidWasteReport", parameters);

            dt = DS.Tables[0];

            return this.Response(Utilities.MessageTypes.Success, responseToReturn: dt);
        }

        #endregion


        #region Search

        [HttpPost]
        public ApiResponse GetWasteStorageSearch(PagingParams wasteStorageDetailParams, string asda = "")
        {
            if (string.IsNullOrWhiteSpace(wasteStorageDetailParams.Search))
                wasteStorageDetailParams.Search = string.Empty;

            var queryable = (from wg in this.entities.WasteGenerations

                             join site in this.entities.View_GetConfigLevel.Where(m => m.LanguageId == 1) on wg.SiteLeveleId equals site.LevelId into sites
                             from site in sites.DefaultIfEmpty()

                             join fn in this.entities.View_GetConfigLevel.Where(m => m.LanguageId == 1) on wg.FunctionLevelId equals fn.LevelId into fns
                             from fn in fns.DefaultIfEmpty()

                             join usr in this.entities.Users on wg.EntryBy equals usr.UserId into usrs
                             from usr in usrs.DefaultIfEmpty()

                             join wt in this.entities.WasteType on wg.WasteTypeId equals wt.WasteTypeId into wts
                             from wt in wts.DefaultIfEmpty()

                             join msm in this.entities.MethodStorageMaster on wg.MethodofStorage equals msm.StorageId into msms
                             from msm in msms.DefaultIfEmpty()

                             join stm in this.entities.StatusMaster on wg.StatusId equals stm.StatusId into stms
                             from stm in stms.DefaultIfEmpty()

                             select new
                             {
                                 WasteGenerationId = wg.WasteGenerationId,
                                 SiteLeveleId = wg.SiteLeveleId,
                                 FunctionLevelId = wg.FunctionLevelId,
                                 WasteTypeId = wg.WasteTypeId,
                                 RequestedDate = wg.RequestedDate,
                                 MethodofStorage = wg.MethodofStorage,
                                 MonitoringDate = wg.MonitoringDate,
                                 IsActive = wg.IsActive,
                                 EntryDate = wg.EntryDate,
                                 EntryBy = wg.EntryBy,
                                 UpdateDate = wg.UpdateDate,
                                 UpdateBy = wg.UpdateBy,
                                 StatusId = wg.StatusId,
                                 DocumentNo = wg.DocumentNo,
                                 Characteristics = wg.Characteristics,
                                 IncompatibleWasteSubstances = wg.IncompatibleWasteSubstances,
                                 WasteTypeName = wt.WasteType1,
                                 FunctionalWasteTypeId = wg.FunctionalWasteTypeId,
                                 FunctionalWasteTypeName = wg.FunctionalWasteTypeName,
                                 WastePhysicalState = wg.WastePhysicalState,
                                 Address = wg.Address,
                                 EPRAuthorizationIssueDate = wg.EPRAuthorizationIssueDate,
                                 EPRAuthorizationValidityYear = wg.EPRAuthorizationValidityYear,
                                 FirstName = usr.FirstName ?? "",
                                 LastName = usr.LastName ?? "",
                                 SiteName = site.Name,
                                 FunctionLevelName = fn.Name,
                                 StorageName = msm.StorageName,
                                 Quantity = this.entities.WasteGenerationDetails.Where(m => m.WasteGenerationId == wg.WasteGenerationId).Sum(a => a.Quantity),
                                 stm.StatusName
                             }).AsQueryable();



            if (wasteStorageDetailParams.StartDate != null && wasteStorageDetailParams.StartDate != DateTime.MinValue)
            {
                DateTime StartDate = wasteStorageDetailParams.StartDate.Date.AddDays(1).AddSeconds(-1);
                queryable = queryable.Where(m => m.EntryDate > StartDate);
            }
            if (wasteStorageDetailParams.EndDate != null && wasteStorageDetailParams.EndDate != DateTime.MinValue)
            {
                DateTime EndDate = wasteStorageDetailParams.EndDate.Date.AddDays(1).AddSeconds(-1);
                queryable = queryable.Where(m => m.EntryDate <= EndDate);
            }
            if (wasteStorageDetailParams.SiteLeveleId != null && wasteStorageDetailParams.SiteLeveleId > 0)
            {
                queryable = queryable.Where(m => m.SiteLeveleId == wasteStorageDetailParams.SiteLeveleId);
            }
            if (wasteStorageDetailParams.FunctionLevelId != null && wasteStorageDetailParams.FunctionLevelId > 0)
            {
                queryable = queryable.Where(m => m.FunctionLevelId == wasteStorageDetailParams.FunctionLevelId);
            }
            if (wasteStorageDetailParams.FunctionLevelId != null && wasteStorageDetailParams.FunctionLevelId > 0)
            {
                queryable = queryable.Where(m => m.FunctionLevelId == wasteStorageDetailParams.FunctionLevelId);
            }
            if (wasteStorageDetailParams.WasteTypeId != null && wasteStorageDetailParams.WasteTypeId > 0)
            {
                queryable = queryable.Where(m => m.WasteTypeId == wasteStorageDetailParams.WasteTypeId);
            }
            if (!string.IsNullOrEmpty(wasteStorageDetailParams.StatusIds) && wasteStorageDetailParams.StatusIds != "0")
            {
                int StatusId = Convert.ToInt32(wasteStorageDetailParams.StatusIds);
                queryable = queryable.Where(m => m.StatusId == StatusId);
            }

            int totalRecord = queryable.Count();

            queryable = queryable.OrderByField(string.IsNullOrEmpty(wasteStorageDetailParams.OrderByColumn) ? "WasteGenerationId" : wasteStorageDetailParams.OrderByColumn, 
                wasteStorageDetailParams.IsAscending).Skip((wasteStorageDetailParams.CurrentPageNumber - 1) * wasteStorageDetailParams.PageSize).Take(wasteStorageDetailParams.PageSize);

            return this.Response(Utilities.MessageTypes.Success, responseToReturn: queryable.ToList(), totalRecord: totalRecord);
        }

        [HttpPost]
        public ApiResponse GetWasteDisposalSearch(PagingParams wasteDisposalParams)
        {
            if (string.IsNullOrWhiteSpace(wasteDisposalParams.Search))
                wasteDisposalParams.Search = string.Empty;

            var queryable = (from wd in this.entities.WasteDisposals

                             join site in this.entities.View_GetConfigLevel.Where(m => m.LanguageId == 1) on wd.SiteLeveleId equals site.LevelId into sites
                             from site in sites.DefaultIfEmpty()

                             join usr in this.entities.Users on wd.EntryBy equals usr.UserId into usrs
                             from usr in usrs.DefaultIfEmpty()

                             join wt in this.entities.WasteType on wd.WasteTypeId equals wt.WasteTypeId into wts
                             from wt in wts.DefaultIfEmpty()

                             join stm in this.entities.StatusMaster on wd.StatusId equals stm.StatusId into stms
                             from stm in stms.DefaultIfEmpty()


                             select new
                             {
                                 wd.WasteDisposalId,
                                 wd.SiteLeveleId,
                                 wd.WasteTypeId,
                                 wd.RequestedDate,
                                 wd.SenderAuthno,
                                 wd.Mainfestdocno,
                                 wd.TranspoterName,
                                 wd.ConatctNo,
                                 wd.Emailaddress,
                                 wd.Transporteraddress,
                                 wd.TranspoterRegNo,
                                 wd.VehicleType,
                                 wd.TranspoterVehicleNo,
                                 wd.RecieverName,
                                 wd.RecieverConatctNo,
                                 wd.RecieverEmailaddress,
                                 wd.Recieveraddress,
                                 wd.RecieverAuthorisation,
                                 wd.IsActive,
                                 wd.EntryDate,
                                 wd.EntryBy,
                                 wd.UpdateDate,
                                 wd.UpdateBy,
                                 wd.StatusId,
                                 wd.TranspoterFax,
                                 wd.TranspoterContactPerson,
                                 wd.TranspoterContactPersonPhone,
                                 wd.RecieverFax,
                                 wd.RecieverContactPerson,
                                 wd.RecieverContactPersonPhone,
                                 wd.EmergencyContactPerson,
                                 wd.EmergencyContactPersonPhone,
                                 wd.DocumentNo,
                                 wd.FunctionalWasteTypeId,
                                 wd.FunctionalWasteTypeName,
                                 SiteName = site.Name,
                                 stm.StatusName,
                                 WasteTypeName = wt.WasteType1,
                                 FirstName = usr.FirstName ?? "",
                                 LastName = usr.LastName ?? "",
                                 AttachmentCount = this.entities.Attachments.Where(c => c.ModuleId == 1 && c.ReferenceId == wd.WasteDisposalId).Count(),
                                 Quantity = this.entities.WasteDisposalDetails.Where(c => c.WasteDisposalId == wd.WasteDisposalId).Sum(m => m.Quantity),
                                 WasteDisposalDetails = this.entities.WasteDisposalDetails.Where(c => c.WasteDisposalId == wd.WasteDisposalId).Select(m => new { m.UOM, m.Quantity, m.WasteCategoryId })
                             }).AsQueryable();

            if (wasteDisposalParams.StartDate != null && wasteDisposalParams.StartDate != DateTime.MinValue)
            {
                DateTime StartDate = wasteDisposalParams.StartDate.Date.AddDays(1).AddSeconds(-1);
                queryable = queryable.Where(m => m.EntryDate > StartDate);
            }
            if (wasteDisposalParams.EndDate != null && wasteDisposalParams.EndDate != DateTime.MinValue)
            {
                DateTime EndDate = wasteDisposalParams.EndDate.Date.AddDays(1).AddSeconds(-1);
                queryable = queryable.Where(m => m.EntryDate <= EndDate);
            }
            if (wasteDisposalParams.SiteLeveleId != null && wasteDisposalParams.SiteLeveleId > 0)
            {
                queryable = queryable.Where(m => m.SiteLeveleId == wasteDisposalParams.SiteLeveleId);
            }
            if (wasteDisposalParams.WasteTypeId != null && wasteDisposalParams.WasteTypeId > 0)
            {
                queryable = queryable.Where(m => m.WasteTypeId == wasteDisposalParams.WasteTypeId);
            }
            if (!string.IsNullOrEmpty(wasteDisposalParams.StatusIds) && wasteDisposalParams.StatusIds != "0")
            {
                int StatusId = Convert.ToInt32(wasteDisposalParams.StatusIds);
                queryable = queryable.Where(m => m.StatusId == StatusId);
            }

            int totalRecord = queryable.Count();

            queryable = queryable.OrderByField(string.IsNullOrEmpty(wasteDisposalParams.OrderByColumn) ? "WasteDisposalId" : wasteDisposalParams.OrderByColumn, 
                wasteDisposalParams.IsAscending).Skip((wasteDisposalParams.CurrentPageNumber - 1) * wasteDisposalParams.PageSize).Take(wasteDisposalParams.PageSize);

            return this.Response(Utilities.MessageTypes.Success, responseToReturn: queryable.ToList(), totalRecord: totalRecord);
        }

        #endregion

        #region file Upload


        [HttpPost]
        public ApiResponse Attachments(AttachmentViewModel attachmentsvw)
        {
            bool IsEdit = true;

            if (attachmentsvw.AttachmentsList != null && attachmentsvw.AttachmentsList.Count > 0)
            {
                foreach (var item in attachmentsvw.AttachmentsList)
                {
                    Attachments objattachments = new Attachments();

                    objattachments.ModuleId = attachmentsvw.ModuleId;
                    objattachments.ReferenceId = attachmentsvw.ReferenceId;
                    objattachments.OriginalFileName = item.OriginalFileName;
                    objattachments.FileName = item.FileName;
                    objattachments.DeleteFlag = item.DeleteFlag;
                    objattachments.FilePath = item.FilePath;
                    objattachments.EntryBy = UserContext.EmployeeId;
                    //objattachments.FileSize = item.FileSize;
                    objattachments.EntryDate = DateTime.UtcNow;
                    this.entities.Attachments.AddObject(objattachments);
                    this.entities.SaveChanges();
                }

                return this.Response(Utilities.MessageTypes.Success, string.Format(IsEdit ? Resource.UpdateSuccess : Resource.SaveSuccess, Resource.HWMGenerationAndStorage));
            }
            else
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(IsEdit ? Resource.UpdateError : Resource.SaveError, Resource.HWMGenerationAndStorage));
            }
        }


        #endregion


    }
}
