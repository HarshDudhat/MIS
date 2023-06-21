// -----------------------------------------------------------------------
// <copyright file="DesignationsController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Controllers.Configuration
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using Newtonsoft.Json;
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Api.ViewModel;
    using MVCProject.Common.Resources;

    #endregion

    /// <summary>
    /// Holds Designations Master related methods
    /// </summary>
    public class DesignationsController : BaseController
    {
        /// <summary>
        /// Holds context object. 
        /// </summary>
        private MVCProjectEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="DesignationsController"/> class.
        /// </summary>
        public DesignationsController()
        {
            this.entities = new MVCProjectEntities();
        }

        /// <summary>
        /// Get Designations for dropdown
        /// </summary>
        /// <returns>Returns response of type <see cref="ApiResonse"/> class.</returns>
        [HttpGet]
        public ApiResponse GetForDropdown()
        {
            var data = this.entities.Designations.Where(x => x.IsActive).Select(x => new { Name = x.DesignationName, Id = x.DesignationId }).OrderBy(x => x.Name).ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);
        }

        /// <summary>
        /// Gets all Designation details. 
        /// </summary>
        /// <param name="designationDetailParams">Pass parameters of designation details</param>
        /// <returns>Returns response of type <see cref="ApiResonse"/> class.</returns>
        [HttpPost]
        public ApiResponse GetAllDesignations(PagingParams designationDetailParams)
        {
            if (string.IsNullOrWhiteSpace(designationDetailParams.Search))
            {
                designationDetailParams.Search = string.Empty;
            }

            var designationList = (from s in this.entities.Designations.AsEnumerable().Where(x => x.DesignationName.Trim().ToLower().Contains(designationDetailParams.Search.Trim().ToLower()))
                                   let TotalRecords = this.entities.Designations.AsEnumerable().Where(x => x.DesignationName.Trim().ToLower().Contains(designationDetailParams.Search.Trim().ToLower())).Count()
                                   select new
                                   {
                                       DesignationId = s.DesignationId,
                                       DesignationName = s.DesignationName,
                                       IsActive = s.IsActive,
                                       TotalRecords
                                   }).AsQueryable().OrderByField(designationDetailParams.OrderByColumn, designationDetailParams.IsAscending).Skip((designationDetailParams.CurrentPageNumber - 1) * designationDetailParams.PageSize).Take(designationDetailParams.PageSize);

            return this.Response(Utilities.MessageTypes.Success, string.Empty, designationList);
        }

        /// <summary>
        /// Get Designation 
        /// </summary>
        /// <param name="isGetAll">To get active records</param>
        /// <returns>Returns response of type</returns>class.
        [HttpGet]
        public ApiResponse GetDesignationList(bool isGetAll = false)
        {
            var result = this.entities.Designations.Where(x => (isGetAll || x.IsActive)).Select(x => new { Id = x.DesignationId, Name = x.DesignationName }).OrderBy(y => y.Name).ToList();
            return this.Response(MessageTypes.Success, string.Empty, result);
        }

        /// <summary>
        /// Get Designation Master List by Id
        /// </summary>
        /// <param name="designationId">Designation id.</param>
        /// <returns>Returns response type of <see cref="ApiResponse"/> class.></returns>
        [HttpGet]
        public ApiResponse GetDesignationById(int designationId)
        {
            var designationDetail = this.entities.Designations.Where(a => a.DesignationId == designationId)
                        .Select(g => new
                        {
                            DesignationId = g.DesignationId,
                            DesignationName = g.DesignationName,
                            IsActive = g.IsActive,
                            EntryBy = g.EntryBy,
                            EntryDate = g.EntryDate,
                        }).SingleOrDefault();
            if (designationDetail != null)
            {
                return this.Response(Utilities.MessageTypes.Success, string.Empty, designationDetail);
            }
            else
            {
                return this.Response(Utilities.MessageTypes.NotFound, string.Empty);
            }
        }

        /// <summary>
        /// Add/update Designation details
        /// </summary>
        /// <param name="designationDetail">Designation Details</param>
        /// <returns>Returns response type of <see cref="ApiResponse"/> class.></returns>
        [HttpPost]
        public ApiResponse SaveDesignationDetails(Designations designationDetail)
        {
            if (this.entities.Designations.Any(x => x.DesignationId != designationDetail.DesignationId && x.DesignationName.Trim() == designationDetail.DesignationName.Trim()))
            {
                return this.Response(Utilities.MessageTypes.Warning, string.Format(Resource.AlreadyExists, Resource.Designation));
            }
            else
            {
                Designations existingDesignationDetail = this.entities.Designations.Where(a => a.DesignationId == designationDetail.DesignationId).FirstOrDefault();
                if (existingDesignationDetail == null)
                {
                    designationDetail.EntryDate = DateTime.UtcNow;
                    designationDetail.EntryBy = UserContext.EmployeeId;
                    this.entities.Designations.AddObject(designationDetail);
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.Designation));
                    }

                    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.Designation));
                }
                else
                {
                    // For update record
                    existingDesignationDetail.DesignationName = designationDetail.DesignationName;
                    existingDesignationDetail.IsActive = designationDetail.IsActive;
                    existingDesignationDetail.UpdateBy = UserContext.EmployeeId;
                    existingDesignationDetail.UpdateDate = DateTime.UtcNow;
                    this.entities.Designations.ApplyCurrentValues(existingDesignationDetail);
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError), Resource.Designation);
                    }

                    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.UpdatedSuccessfully, Resource.Designation));
                }
            }
        }

        /// <summary>
        /// Create Designation List Excel Report
        /// </summary>
        /// <returns>Returns response of type <see cref="HttpResponseMessage"/> class.</returns>
        [HttpGet]
        public HttpResponseMessage CreateDesignationListReport()
        {
            // Get Designation details.
            object designationResult = this.entities.Designations.Select(s => new
                                        {
                                            s.DesignationId,
                                            s.DesignationName,
                                            s.IsActive
                                        }).ToList();
            List<Dictionary<string, object>> designations = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(designationResult));

            if (designations.Count == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                foreach (var designation in designations)
                {
                    designation["IsActiveText"] = designation["IsActive"] != null && Convert.ToBoolean(designation["IsActive"]) ? Resource.Active : Resource.Inactive;
                }
            }

            // Excel workbook
            string sheetTitle = Resource.Designation;
            Dictionary<string, string> filters = new Dictionary<string, string>();

            // Grid Header
            List<GridColumn> gridColumns = new List<GridColumn>();
            gridColumns.Add(new GridColumn() { Title = Resource.SrNo, IsSrNo = true, Width = 256 * 20 });
            gridColumns.Add(new GridColumn() { Title = Resource.Designation, FieldName = "DesignationName" });
            gridColumns.Add(new GridColumn() { Title = Resource.IsActive, FieldName = "IsActiveText", Width = 256 * 20 });

            // export to excel 
            ExcelHelper excelHelper = new ExcelHelper("Ask-Ehs", sheetTitle);
            excelHelper.ExportGridData(sheetTitle, filters, gridColumns, designations, this.UserContext);

            // Write response
            using (var exportData = new MemoryStream())
            {
                excelHelper.WorkBook.Write(exportData);

                string saveAsFileName = string.Format(sheetTitle + "-{0:d}.xls", DateTime.Now).Replace("/", "-") + ".xls";

                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(exportData.GetBuffer())
                };
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = saveAsFileName
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return result;
            }
        }

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
    }
}
