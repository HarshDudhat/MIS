

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
    public class UserMasterController : BaseController
    {
        private MVCProjectEntities entities;

        public UserMasterController()
        {
            this.entities = new MVCProjectEntities();
        }

        /// <summary>
        /// Gets all UserMaster details. 
        /// </summary>
        /// <param name="userMasterDetailParams">Pass parameters of UserMaster details</param>
        /// <returns>Returns response of type <see cref="ApiResonse"/> class.</returns>
        [HttpPost]  
        public ApiResponse GetAllUserMasters(PagingParams userMasterDetailParams)
        {
            if (string.IsNullOrWhiteSpace(userMasterDetailParams.Search))
            {
                userMasterDetailParams.Search = string.Empty;
            }

            var results = this.entities.USP_MIS_GetUserList().Where(x => x.Email.Trim().ToLower().Contains(userMasterDetailParams.Search.Trim().ToLower())).ToList().AsQueryable().OrderByField(userMasterDetailParams.OrderByColumn, userMasterDetailParams.IsAscending).Skip((userMasterDetailParams.CurrentPageNumber - 1) * userMasterDetailParams.PageSize).Take(userMasterDetailParams.PageSize);
            var TotalRecords = this.entities.USP_MIS_GetUserList().Where(x => x.Email.Trim().ToLower().Contains(userMasterDetailParams.Search.Trim().ToLower())).Count();


            return this.Response(Utilities.MessageTypes.Success, string.Empty, new { list = results, Total = TotalRecords });

        }


        /// <summary>
        /// Get UserMaster 
        /// </summary>
        /// <param name="isGetAll">To get active records</param>
        /// <returns>Returns response of type</returns>class.
        [HttpGet]
        public ApiResponse GetUserMasterList(bool isGetAll = false)
        {
            var result = this.entities.USP_MIS_GetUserList().Where(x => (isGetAll || x.IsActive.Value)).Select(x => new { Id = x.UserId, Name = x.Email }).OrderBy(y => y.Name).ToList();
            return this.Response(MessageTypes.Success, string.Empty, result);
        }

        /// <summary>
        /// Get UserMaster Master List by Id
        /// </summary>
        /// <param name="UserId">UserMaster id.</param>
        /// <returns>Returns response type of <see cref="ApiResponse"/> class.></returns>
        [HttpGet]
        public ApiResponse GetUserMasterById(int UserId)
        {
            var userMasterDetail = this.entities.USP_MIS_GetUserList()

                 .Where(a => a.UserId == UserId).ToList()
                        .Select(g => new
                        {
                            UserId = g.UserId,
                            Email=g.Email,
                            Password = g.Password,
                            IsActive = g.IsActive,
                            RoleId = g.RoleId,
                            RoleName=g.RoleName
                        }).FirstOrDefault();
            if (userMasterDetail != null)
            {
                return this.Response(Utilities.MessageTypes.Success, string.Empty, userMasterDetail);
            }
            else
            {
                return this.Response(Utilities.MessageTypes.NotFound, string.Empty);
            }
        }

        /// <summary>
        /// Add/update UserMaster details
        /// </summary>
        /// <param name="userMasterDetail">UserMaster Details</param>
        /// <returns>Returns response type of <see cref="ApiResponse"/> class.></returns>
        [HttpPost]
        public ApiResponse SaveuserMasterDetails(MIS_Users userMasterDetail)
        {
            if (this.entities.MIS_Users.Any(x => x.UserId != userMasterDetail.UserId && x.Email.Trim() == userMasterDetail.Email.Trim()))
            {
                return this.Response(Utilities.MessageTypes.Warning, string.Format(Resource.AlreadyExists, Resource.UserMaster));
            }
            else
            {
                MIS_Users existinguserMasterDetail = this.entities.MIS_Users.Where(a => a.UserId == userMasterDetail.UserId).FirstOrDefault();
                if (existinguserMasterDetail == null)
                {
                    MIS_Users data = new MIS_Users();
                    data.Email = userMasterDetail.Email;
                    data.Password = userMasterDetail.Password;
                    data.RoleId = userMasterDetail.RoleId;
                    data.IsActive = userMasterDetail.IsActive;
                    data.EntryBy = 1;
                    data.EntryDate = DateTime.Now;
                    this.entities.MIS_Users.AddObject(data);
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.User));
                    }

                    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.UserMaster));
                }
                else
                {
                    // For update record
                    existinguserMasterDetail.Email = userMasterDetail.Email;
                    existinguserMasterDetail.Password = userMasterDetail.Password;
                    existinguserMasterDetail.UserId = userMasterDetail.UserId;
                    existinguserMasterDetail.RoleId = userMasterDetail.RoleId;
                    existinguserMasterDetail.IsActive = userMasterDetail.IsActive;
                    existinguserMasterDetail.UpdateBy = 1;
                    existinguserMasterDetail.UpdateDate = DateTime.UtcNow;
                    this.entities.MIS_Users.ApplyCurrentValues(existinguserMasterDetail);
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.User));
                    }

                }
                return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.UpdatedSuccessfully, Resource.UserMaster));
            }
        }

        /// <summary>
        /// Get Drop Down List Of Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse GetRoleDropDown()
        {
            var data = this.entities.MIS_RoleMaster.Where(x => x.IsActive.Value).Select(x=>new { RoleName=x.RoleName ,RoleId=x.RoleId}).OrderBy(x=>x.RoleName).ToList();
            return this.Response(Utilities.MessageTypes.Success,responseToReturn:data);
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
