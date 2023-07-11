

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
    public class VerticalMasterController : BaseController
    {
        private MVCProjectEntities entities;

        public VerticalMasterController()
        {
            this.entities = new MVCProjectEntities();
        }

        /// <summary>
        /// Gets all Vertical details. 
        /// </summary>
        /// <param name="verticalpagingParams">Pass parameters of designation details</param>
        /// <returns>Returns response of type <see cref="ApiResonse"/> class.</returns>
        [HttpPost]
        public ApiResponse GetAllVerticals(PagingParams verticalpagingParams)
        {
            if (string.IsNullOrWhiteSpace(verticalpagingParams.Search))
            {
                verticalpagingParams.Search = string.Empty;
            }

            var verticallist = (from s in this.entities.USP_MIS_GetVerticalList().AsEnumerable().Where(x => x.VerticalName.Trim().ToLower().Contains(verticalpagingParams.Search.Trim().ToLower()))
                                   let TotalRecords = this.entities.USP_MIS_GetVerticalList().AsEnumerable().Where(x => x.VerticalName.Trim().ToLower().Contains(verticalpagingParams.Search.Trim().ToLower())).Count()
                                   select new
                                   {
                                       VerticalId = s.VerticalId,
                                       VerticalName = s.VerticalName,
                                       IsActive = s.IsActive,
                                       SiteinchargeId=s.SiteinchargeId,
                                       TotalRecords
                                   }).AsQueryable().OrderByField(verticalpagingParams.OrderByColumn, verticalpagingParams.IsAscending).Skip((verticalpagingParams.CurrentPageNumber - 1) * verticalpagingParams.PageSize).Take(verticalpagingParams.PageSize);

            return this.Response(Utilities.MessageTypes.Success, string.Empty, verticallist);
        }
        
        /// <summary>
        /// Get Vertical Master List by Id
        /// </summary>
        /// <param name="verticalId">Designation id.</param>
        /// <returns>Returns response type of <see cref="ApiResponse"/> class.></returns>
        [HttpGet]
        public ApiResponse GetVerticalById(int verticalId)
        {
            var verticalDetail = this.entities.USP_MIS_GetVerticalList().Where(a => a.VerticalId == verticalId)
                        .Select(g => new
                        {
                            VerticalId = g.VerticalId,
                            VerticalName = g.VerticalName,
                            SiteinchargeId=g.SiteinchargeId,
                            IsActive = g.IsActive
                        // EntryBy = g.EntryBy,
                        //EntryDate = g.EntryDate,
                    }).SingleOrDefault();
            if (verticalDetail != null)
            {
                return this.Response(Utilities.MessageTypes.Success, string.Empty, verticalDetail);
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
        public ApiResponse SaveVerticalDetails(MIS_VerticalMaster verticalDetail)
        {
            if (this.entities.MIS_VerticalMaster.Any(x => x.VerticalId != verticalDetail.VerticalId && x.VerticalName.Trim() == verticalDetail.VerticalName.Trim()))
            {
                return this.Response(Utilities.MessageTypes.Warning, string.Format(Resource.AlreadyExists, Resource.Vertical));
            }
            else
            {
                MIS_VerticalMaster existingVerticalDetail = this.entities.MIS_VerticalMaster.Where(a => a.VerticalId == verticalDetail.VerticalId).FirstOrDefault();
                if (existingVerticalDetail == null)
                {
                    //designationDetail.EntryDate = DateTime.UtcNow;
                    //designationDetail.EntryBy = UserContext.EmployeeId;
                    this.entities.MIS_VerticalMaster.AddObject(verticalDetail);
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.Vertical));
                    }

                    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.Vertical));
                }
                else
                {
                    // For update record
                    existingVerticalDetail.VerticalName = verticalDetail.VerticalName;
                    existingVerticalDetail.IsActive = verticalDetail.IsActive;
                    existingVerticalDetail.SiteinchargeId = verticalDetail.SiteinchargeId;
                    //existingDesignationDetail.UpdateBy = UserContext.EmployeeId;
                    //existingDesignationDetail.UpdateDate = DateTime.UtcNow;
                    this.entities.MIS_VerticalMaster.ApplyCurrentValues(existingVerticalDetail);
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError), Resource.Vertical);
                    }

                    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.UpdatedSuccessfully, Resource.Vertical));
                }
            }
        }

        /// <summary>
        /// Get Site in Charge
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse GetSiteinCharge()
        {
            var data = this.entities.MIS_Users.Where(x => x.IsActive.Value && x.RoleId == 1).
                   Select(x => new { Email = x.Email, SiteinchargeId = x.UserId }).OrderBy(x => x.Email).ToList();
            return this.Response(Utilities.MessageTypes.Success, responseToReturn: data);

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
