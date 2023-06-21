//-----------------------------------------------------------------------
// <copyright file="RoleController.cs" company="ASK-EHS">
//     Copyright (c) ASK-EHS 2018.
// </copyright>
//-----------------------------------------------------------------------

namespace MVCProject.Api.Controllers.Configuration
{
    using System.Data.Objects;
    using System.Linq;
    using System.Web.Http;
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Common.Resources;
    using System;

    /// <summary>
    /// Role API Controller
    /// </summary>
    public class RoleController : BaseController
    {
        /// <summary>
        /// Holds context object.
        /// </summary>
        private MVCProjectEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        public RoleController()
        {
            this.entities = new MVCProjectEntities();
        }

        /// <summary>
        /// Get role levels
        /// </summary>
        /// <param name="isActiveOnly">To get only active levels</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpGet]
        public ApiResponse GetRoleLevel(bool isActiveOnly = true)
        {
            //this.entities.AreaMaster.MergeOption = MergeOption.NoTracking;
            var result = this.entities.RoleLevel.Where(x => isActiveOnly ? x.IsActive : true)
                             .Select(x => new
                             {
                                 x.RoleLevelId,
                                 x.LevelName,
                                 x.IsActive
                             }).ToList();

            if (result != null)
            {
                return this.Response(MessageTypes.Success, responseToReturn: result, totalRecord: result.Count);
            }
            else
            {
                return this.Response(MessageTypes.NotFound, string.Format(Resource.NotFound, Resource.RoleLevel));
            }
        }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <param name="isActiveOnly">To get only active roles</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpGet]
        public ApiResponse GetRoles(bool isActiveOnly = true)
        {
            //this.entities.AreaMaster.MergeOption = MergeOption.NoTracking;
            var result = this.entities.RoleMaster.Where(x => isActiveOnly ? x.IsActive : true)
                             .Select(x => new
                             {
                                 x.RoleId,
                                 x.RoleName,
                                 x.RoleLevelId,
                                 LevelName = x.RoleLevel.LevelName,
                                 x.IsActive
                             }).ToList();

            if (result != null)
            {
                return this.Response(MessageTypes.Success, responseToReturn: result, totalRecord: result.Count);
            }
            else
            {
                return this.Response(MessageTypes.NotFound, string.Format(Resource.NotFound, Resource.Role));
            }
        }

        /// <summary>
        /// Get role to edit
        /// </summary>
        /// <param name="roleId">To get role</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpGet]
        public ApiResponse EditRole(int roleId)
        {
            //this.entities.AreaMaster.MergeOption = MergeOption.NoTracking;
            var result = this.entities.RoleMaster.Where(x => x.RoleId == roleId).AsEnumerable()
                             .Select(x => new
                             {
                                 x.RoleId,
                                 x.RoleName,
                                 x.RoleLevelId,
                                 LevelName = x.RoleLevel.LevelName,
                                 x.IsActive,
                                 PageAccess = x.PageAccess.Select(p => new
                                 {
                                     p.AccessId,
                                     p.PageId,
                                     p.RoleId,
                                     p.CanRead,
                                     p.CanWrite
                                 }).ToList()
                             }).FirstOrDefault();

            if (result != null)
            {
                return this.Response(MessageTypes.Success, responseToReturn: result, totalRecord: 1);
            }
            else
            {
                return this.Response(MessageTypes.NotFound, string.Format(Resource.NotFound, Resource.Role));
            }
        }

        /// <summary>
        /// Get module pages
        /// </summary>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpGet]
        public ApiResponse GetModulePages()
        {
            //this.entities.AreaMaster.MergeOption = MergeOption.NoTracking;
            var result = this.entities.ModulePage.AsEnumerable().Where(x => x.IsActive && x.ParentId == null && x.IsMainModule)
                             .Select(x => new
                             {
                                 x.PageId,
                                 x.PageName,
                                 Pages = x.ModulePage1.Where(p => p.IsActive).Select(p => new
                                 {
                                     p.PageId,
                                     p.PageName
                                 }).ToList()
                             }).ToList();

            if (result != null)
            {
                return this.Response(MessageTypes.Success, responseToReturn: result, totalRecord: result.Count);
            }
            else
            {
                return this.Response(MessageTypes.NotFound, string.Format(Resource.NotFound, Resource.ModuleOrPages));
            }
        }

        /// <summary>
        /// Save Role
        /// </summary>
        /// <param name="areaDetailParams">Pass parameters of areas details</param>
        /// <returns>Returns response of type <see cref="ApiResonse"/> class.</returns>
        [HttpPost]
        public ApiResponse SaveRole(RoleMaster role)
        {
            if (this.entities.RoleMaster.Any(x => x.RoleId != role.RoleId && x.RoleName.Trim().ToLower() == role.RoleName.Trim().ToLower()))
            {
                return this.Response(MessageTypes.Warning, string.Format(Resource.AlreadyExists, Resource.Role));
            }
            else
            {
                var roleMasterdb = this.entities.RoleMaster.Where(x => x.RoleId == role.RoleId).FirstOrDefault();
                if (roleMasterdb == null)
                {
                    role.EntryBy = UserContext.EmployeeId;
                    role.EntryDate = DateTime.UtcNow;
                    this.entities.RoleMaster.AddObject(role);
                }
                else
                {
                    if (role.PageAccess != null && role.PageAccess.Count > 0)
                    {
                        // Remove Old Page Access
                        int[] newPages = role.PageAccess.Select(x => x.PageId).ToArray();
                        var deletePage = roleMasterdb.PageAccess.Where(x => !newPages.Contains(x.PageId)).ToArray();
                        for (int i = 0; i < deletePage.Length; i++)
                        {
                            this.entities.PageAccess.DeleteObject(deletePage[i]);
                        }

                        PageAccess pageAccess = null;
                        PageAccess pageAccessDb = null;
                        for (int i = 0; i < role.PageAccess.Count; i++)
                        {
                            pageAccess = role.PageAccess.ToArray()[i];
                            pageAccessDb = roleMasterdb.PageAccess.FirstOrDefault(x => x.PageId == pageAccess.PageId);
                            if (pageAccessDb == null)
                            {
                                pageAccess = new PageAccess()
                                {
                                    PageId = pageAccess.PageId,
                                    RoleId = role.RoleId,
                                    CanRead = pageAccess.CanRead,
                                    CanWrite = pageAccess.CanWrite
                                };

                                // Add Page Access
                                roleMasterdb.PageAccess.Add(pageAccess);
                            }
                            else
                            {
                                // Update Page Access                            
                                pageAccessDb.CanRead = pageAccess.CanRead;
                                pageAccessDb.CanWrite = pageAccess.CanWrite;
                            }
                        }

                        var usersDb = this.entities.Users.AsEnumerable().Where(x => x.UserRole.Any(r => r.RoleId == role.RoleId && r.IsActive == true));
                        foreach (var user in usersDb)
                        {
                            user.IsTokenExpired = true;
                        }

                        //var usersDb = (from u in entities.Users
                        //               join ur in entities.UserRole on u.UserId equals ur.UserId
                        //               where ur.RoleId == role.RoleId && ur.IsActive == true
                        //               select new 
                        //               {
                        //                   u.CompanyId,
                        //                   u.EmployeeId,
                        //                   u.EntryBy,
                        //                   u.EntryDate,
                        //                   u.IsActive,
                        //                   IsTokenExpired = true,
                        //                   u.IsUserLoggedIn,
                        //                   u.Password,
                        //                   u.ResetPasswordToken,
                        //                   u.SubscriptionDate,
                        //                   u.TermAccept,
                        //                   u.UpdateBy,
                        //                   u.UpdateDate,
                        //                   u.UserId,
                        //                   u.UserName,
                        //                   u.ValidUptoDate
                        //               }).AsEnumerable().ToList();
                    }
                    else
                    {
                        // Remove Page Access
                        var deletePage = roleMasterdb.PageAccess.ToArray();
                        for (int i = 0; i < deletePage.Length; i++)
                        {
                            this.entities.PageAccess.DeleteObject(deletePage[i]);
                        }
                    }

                    roleMasterdb.RoleName = role.RoleName;
                    roleMasterdb.RoleLevelId = role.RoleLevelId;
                    roleMasterdb.IsActive = role.IsActive;
                    roleMasterdb.UpdateBy = UserContext.EmployeeId;
                    roleMasterdb.UpdateDate = DateTime.UtcNow;
                    this.entities.RoleMaster.ApplyCurrentValues(roleMasterdb);
                }

                if (this.entities.SaveChanges() > 0)
                {
                    return this.Response(Utilities.MessageTypes.Success, string.Format(roleMasterdb == null ? Resource.SaveSuccess : Resource.UpdateSuccess, Resource.Role));
                }
                else
                {
                    return this.Response(Utilities.MessageTypes.Error, string.Format(roleMasterdb == null ? Resource.SaveError : Resource.UpdateError, Resource.Role));
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
