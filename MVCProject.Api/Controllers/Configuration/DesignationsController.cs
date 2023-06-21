// -----------------------------------------------------------------------
// <copyright file="DesignationsController.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
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
            var data = this.entities.Designation.Where(x => x.IsActive.Value).Select(x => new { Name = x.DesignationName, Id = x.DesignationId }).OrderBy(x => x.Name).ToList();
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

            var designationList = (from s in this.entities.Designation.AsEnumerable().Where(x => x.DesignationName.Trim().ToLower().Contains(designationDetailParams.Search.Trim().ToLower()))
                                   let TotalRecords = this.entities.Designation.AsEnumerable().Where(x => x.DesignationName.Trim().ToLower().Contains(designationDetailParams.Search.Trim().ToLower())).Count()
                                   select new
                                   {
                                       DesignationId = s.DesignationId,
                                       DesignationName = s.DesignationName,
                                       IsActive = s.IsActive,
                                       Remarks=s.Remarks,
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
            var result = this.entities.Designation.Where(x => (isGetAll || x.IsActive.Value)).Select(x => new { Id = x.DesignationId, Name = x.DesignationName }).OrderBy(y => y.Name).ToList();
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
            var designationDetail = this.entities.Designation.Where(a => a.DesignationId == designationId)
                        .Select(g => new
                        {
                            DesignationId = g.DesignationId,
                            DesignationName = g.DesignationName,
                            IsActive = g.IsActive,
                            Remarks = g.Remarks,
                           // EntryBy = g.EntryBy,
                            //EntryDate = g.EntryDate,
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
        public ApiResponse SaveDesignationDetails(Designation designationDetail)
        {
            if (this.entities.Designation.Any(x => x.DesignationId != designationDetail.DesignationId && x.DesignationName.Trim() == designationDetail.DesignationName.Trim()))
            {
                return this.Response(Utilities.MessageTypes.Warning, string.Format(Resource.AlreadyExists, Resource.Designation));
            }
            else
            {
                Designation existingDesignationDetail = this.entities.Designation.Where(a => a.DesignationId == designationDetail.DesignationId).FirstOrDefault();
                if (existingDesignationDetail == null)
                {
                    //designationDetail.EntryDate = DateTime.UtcNow;
                    //designationDetail.EntryBy = UserContext.EmployeeId;
                    this.entities.Designation.AddObject(designationDetail);
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
                    existingDesignationDetail.Remarks = designationDetail.Remarks;
                    //existingDesignationDetail.UpdateBy = UserContext.EmployeeId;
                    //existingDesignationDetail.UpdateDate = DateTime.UtcNow;
                    this.entities.Designation.ApplyCurrentValues(existingDesignationDetail);
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError), Resource.Designation);
                    }

                    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.UpdatedSuccessfully, Resource.Designation));
                }
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
