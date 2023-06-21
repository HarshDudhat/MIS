﻿

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
    public class ProjectMasterController : BaseController
    {
        private MVCProjectEntities entities;

        public ProjectMasterController()
        {
            this.entities = new MVCProjectEntities();
        }

        /// <summary>
        /// Gets all Vertical details. 
        /// </summary>
        /// <param name="projectpagingParams">Pass parameters of designation details</param>
        /// <returns>Returns response of type <see cref="ApiResonse"/> class.</returns>
        [HttpPost]
        public ApiResponse GetAllProjects(PagingParams projectpagingParams)
        {
            if (string.IsNullOrWhiteSpace(projectpagingParams.Search))
            {
                projectpagingParams.Search = string.Empty;
            }

            var projectlist = (from s in this.entities.USP_MIS_GetProjectList().AsEnumerable().Where(x => x.ProjectName.Trim().ToLower().Contains(projectpagingParams.Search.Trim().ToLower()))
                                   let TotalRecords = this.entities.USP_MIS_GetProjectList().AsEnumerable().Where(x => x.ProjectName.Trim().ToLower().Contains(projectpagingParams.Search.Trim().ToLower())).Count()
                                   select new
                                   {
                                       ProjectId = s.ProjectId,
                                       ProjectName = s.ProjectName,
                                       IsActive = s.IsActive,
                                       TotalRecords
                                   }).AsQueryable().OrderByField(projectpagingParams.OrderByColumn, projectpagingParams.IsAscending).Skip((projectpagingParams.CurrentPageNumber - 1) * projectpagingParams.PageSize).Take(projectpagingParams.PageSize);

            return this.Response(Utilities.MessageTypes.Success, string.Empty, projectlist);
        }
        
        /// <summary>
        /// Get Vertical Master List by Id
        /// </summary>
        /// <param name="projectId">Designation id.</param>
        /// <returns>Returns response type of <see cref="ApiResponse"/> class.></returns>
        [HttpGet]
        public ApiResponse GetProjectById(int projectId)
        {
            var projectDetail = this.entities.USP_MIS_GetProjectList().Where(a => a.ProjectId == projectId)
                        .Select(g => new
                        {
                            ProjectId = g.ProjectId,
                            ProjectName = g.ProjectName,
                            IsActive = g.IsActive
                        // EntryBy = g.EntryBy,
                        //EntryDate = g.EntryDate,
                    }).SingleOrDefault();
            if (projectDetail != null)
            {
                return this.Response(Utilities.MessageTypes.Success, string.Empty, projectDetail);
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
        public ApiResponse SaveProjectDetails(MIS_ProjectMaster projectDetail)
        {
            if (this.entities.MIS_ProjectMaster.Any(x => x.ProjectId != projectDetail.ProjectId && x.ProjectName.Trim() == projectDetail.ProjectName.Trim()))
            {
                return this.Response(Utilities.MessageTypes.Warning, string.Format(Resource.AlreadyExists,Resource.Project));
            }
            else
            {
                MIS_ProjectMaster existingProjectDetail = this.entities.MIS_ProjectMaster.Where(a => a.ProjectId == projectDetail.ProjectId).FirstOrDefault();
                if (existingProjectDetail == null)
                {
                    //designationDetail.EntryDate = DateTime.UtcNow;
                    //designationDetail.EntryBy = UserContext.EmployeeId;
                    this.entities.MIS_ProjectMaster.AddObject(projectDetail);
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError,Resource.Project));
                    }

                    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully,Resource.Project));
                }
                else
                {
                    // For update record
                    existingProjectDetail.ProjectName = projectDetail.ProjectName;
                    existingProjectDetail.IsActive = projectDetail.IsActive;
                    //existingDesignationDetail.UpdateBy = UserContext.EmployeeId;
                    //existingDesignationDetail.UpdateDate = DateTime.UtcNow;
                    this.entities.MIS_ProjectMaster.ApplyCurrentValues(existingProjectDetail);
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError), Resource.Project);
                    }

                    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.UpdatedSuccessfully, Resource.Project));
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
