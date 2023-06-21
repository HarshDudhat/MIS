// -----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Controllers
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using MVCProject.Api.Models;
    using MVCProject.Api.Models.Common;
    using MVCProject.Api.Utilities;
    using MVCProject.Api.Utilities.Email;
    using MVCProject.Common.Resources;
    using Elmah;
    using MVCProject.Common;
    using Microsoft.Security.Application;
    using MVCProject.Api.Utilities;
    using MVCProject.Api.Models;


    #endregion

    /// <summary>
    /// Account API controller which holds authentication and user related operations.
    /// </summary>
    public class AccountController : BaseController
    {
        /// <summary>
        /// Get list of languages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse GetLanguages()
        {
            using (MVCProjectEntities entities = this.GetEntity())
            {
                var languages = (from l in entities.ConfigLanguage.Where(x => x.IsActive).AsEnumerable()
                                 select new
                                 {
                                     l.LanguageCulture,
                                     l.Language,
                                     l.IsDefault
                                 }).ToList();
                return this.Response(MessageTypes.Success, string.Empty, languages);
            }
        }

        /// <summary>
        /// Authenticates user request by validating credentials.
        /// </summary>
        /// <param name="model">Model object of type <see cref="LogOn"/>.</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpPost]
        public ApiResponse LogIn(LogOn model)
        {
            AADUserGUID adUser = null;
            model.UserName = Sanitizer.GetSafeHtmlFragment(model.UserName);
            model.UserPassword = Sanitizer.GetSafeHtmlFragment(model.UserPassword);

            if (ModelState.IsValid)
            {
                try
                {
                    string[] androidAppVersions = new string[] { "" }, iOSAppVersions = new string[] { "" };

                    using (MVCProjectEntities ent = this.GetEntity())
                    {
                        List<ApplicationConfiguration> appConfiguration = ent.ApplicationConfiguration.Where(a => a.ConfigShotCode == "Mobile_App_Config").ToList();
                        if (appConfiguration != null && appConfiguration.Count > 0)
                        {
                            androidAppVersions = appConfiguration.Where(a => a.ConfigCode == "Android_App_Version").FirstOrDefault().ConfigValue.ToUpper().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
                            iOSAppVersions = appConfiguration.Where(a => a.ConfigCode == "iOS_App_Version").FirstOrDefault().ConfigValue.ToUpper().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
                        }
                    }

                    using (MVCProjectEntities entities = this.GetEntity())
                    {
                        var databaseUser = entities.Users.AsEnumerable().Where(c => c.UserName.ToLower().Trim().Equals(model.UserName.ToLower().Trim()))
                                            .Select(a => new
                                            {
                                                a.UserId,
                                                CompanyId = 1,
                                                a.UserName,
                                                a.Password,
                                                UserRole = entities.USP_GetUserRole(a.UserId, "en-US").ToList(),
                                                SubscriptionDate = DateTime.Today.AddDays(-10),
                                                ValidUptoDate = DateTime.Today.AddDays(10),
                                                TermAccept = true,
                                                a.IsUserLoggedIn,
                                                a.IsActive,
                                                EmployeeId = 1
                                            }).FirstOrDefault();

                        if (databaseUser == null)
                        {
                            return this.Response(MessageTypes.Warning, Resource.UserInvalid);
                        }
                        else
                        {
                            DateTime subscriptionDate;
                            DateTime validUptoDate;
                            subscriptionDate = databaseUser.SubscriptionDate;
                            validUptoDate = databaseUser.ValidUptoDate;


                            string companyDBname = this.GetDatabaseName();
                            UserContext finalUserContext = new UserContext();
                            string attachmentPathUrl = AppUtility.GetDirectoryPath(DirectoryPath.Attachment_ApplicationLogo, companyDBname, true);
                            finalUserContext.UserName = databaseUser.UserName;
                            finalUserContext.CompanyDB = companyDBname;
                            finalUserContext.CompanyId = 1;
                            finalUserContext.CompanyName = "XYZ Company";

                            finalUserContext.UserId = databaseUser.UserId;
                            finalUserContext.RoleId = databaseUser.UserRole.FirstOrDefault().RoleId;
                            finalUserContext.LevelIds = databaseUser.UserRole.FirstOrDefault().LevelIds;
                            finalUserContext.EmployeeId = databaseUser.EmployeeId;
                            finalUserContext.EmpId = SecurityUtility.Encrypt(databaseUser.EmployeeId.ToString());
                            finalUserContext.EmployeeName = "XYZ Safety";
                            finalUserContext.Designation = "EHS Head";
                            finalUserContext.UserRole = databaseUser.UserRole;
                            finalUserContext.ProfilePicturePath = string.Empty;
                            finalUserContext.ApplicationLogo = entities.ApplicationConfiguration.AsEnumerable().Where(x => x.ConfigDesc == "Application Logo").Select(x => string.IsNullOrWhiteSpace(x.ConfigValue) ? string.Empty : string.Format("{0}{1}", attachmentPathUrl, x.ConfigValue)).FirstOrDefault();
                            finalUserContext.IsTermAccept = true;
                            finalUserContext.LandingPage = 1;
                            finalUserContext.IsADUser = false;

                            finalUserContext.SiteLevelId = 9;
                            var siteLevel = AppUtility.GetLevel("9", null, 0, null, 0, UserContext).FirstOrDefault();
                            if (siteLevel != null)
                            {
                                finalUserContext.SiteName = siteLevel.Name;
                                finalUserContext.SiteDescription = siteLevel.Description;
                            }

                            finalUserContext.FunctionLevelId = 14;
                            finalUserContext.EmpContactNo = "9999999999";
                            var level = AppUtility.GetLevel("14", null, 0, null, 0, UserContext).FirstOrDefault();
                            if (level != null)
                            {
                                finalUserContext.FunctionName = level.Name;
                                finalUserContext.FunctionDescription = level.Description;
                            }
                            finalUserContext.PageAccess = new List<UserContext.PagePermission>();
                            var pages = entities.PageAccess.Where(p => p.RoleId == finalUserContext.RoleId && entities.ModulePage.Any(m => m.PageId == p.PageId && m.IsActive && m.ModulePage2.IsActive)).Select(p => new
                            {
                                p.PageId,
                                p.CanRead,
                                p.CanWrite
                            }).ToList();

                            foreach (var p in pages)
                            {
                                finalUserContext.PageAccess.Add(new UserContext.PagePermission()
                                {
                                    PageId = p.PageId,
                                    CanRead = p.CanRead,
                                    CanWrite = p.CanWrite,
                                });
                            }

                            finalUserContext.IsSiteUser = true;
                            finalUserContext.Ticks = DateTime.UtcNow.Ticks;
                            finalUserContext.UserAgent = Sanitizer.GetSafeHtmlFragment(Request.Headers.UserAgent.ToString());
                            finalUserContext.TimeZoneMinutes = model.TimeZoneMinutes;
                            finalUserContext.LanguageCulture = "en-US";
                            finalUserContext.Token = SecurityUtility.GetToken(finalUserContext);

                            // Set IsUserLoggedIn
                            entities.Connection.Open();
                            using (DbTransaction transaction = entities.Connection.BeginTransaction(IsolationLevel.ReadUncommitted))
                            {
                                Users loggedUser = entities.Users.Where(c => c.UserId == finalUserContext.UserId && c.IsActive == true).FirstOrDefault();
                                loggedUser.IsUserLoggedIn = true;
                                loggedUser.IsTokenExpired = false;

                                entities.SaveChanges();
                                transaction.Commit();
                            }

                            return this.Response(MessageTypes.Success, string.Empty, responseToReturn: finalUserContext);
                        }

                    }
                }
                catch (Exception ex)
                {
                    AppUtility.ElmahErrorLog(ex);
                    return this.Response(MessageTypes.Error, Resource.SomethingWrong);
                }
            }
            else
            {
                var modelErrors = this.GetModelErrors(ModelState);
                return this.Response(MessageTypes.Error, modelErrors);
            }

            return this.Response(MessageTypes.Error, Resource.SomethingWrong);
        }

        /// <summary>
        /// Logout current user
        /// </summary>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpPost]
        public ApiResponse LogOut()
        {
            using (MVCProjectEntities entities = this.GetEntity())
            {
                entities.Connection.Open();
                using (DbTransaction transaction = entities.Connection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    Users user = entities.Users.Where(c => c.UserId == this.UserContext.UserId && c.IsActive).FirstOrDefault();
                    if (user != null)
                    {
                        user.IsUserLoggedIn = false;
                        entities.SaveChanges();
                        transaction.Commit();
                    }
                }
            }

            return this.Response(MessageTypes.Success);
        }

        /// <summary>
        /// Change current role of user
        /// </summary>
        /// <param name="roleId">new role id</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpPut]
        public ApiResponse ChangeRole(int roleId, string languageCulture)
        {
            using (MVCProjectEntities entities = this.GetEntity())
            {
                var databaseUser = entities.Users.AsEnumerable().Where(c => c.UserId == this.UserContext.UserId)
                                           .Select(a => new
                                           {
                                               a.UserId,
                                               CompanyId = 1,
                                               a.UserName,
                                               a.Password,
                                               UserRole = entities.USP_GetUserRole(a.UserId, "en-US").ToList(),
                                               SubscriptionDate = DateTime.Today.AddDays(-10),
                                               ValidUptoDate = DateTime.Today.AddDays(10),
                                               TermAccept = true,
                                               a.IsUserLoggedIn,
                                               a.IsActive,
                                               EmployeeId = 1
                                           }).FirstOrDefault();

                UserContext finalUserContext = new UserContext();
                string attachmentPathUrl = AppUtility.GetDirectoryPath(DirectoryPath.Attachment_ApplicationLogo, this.UserContext.CompanyDB, true);
                finalUserContext.UserName = databaseUser.UserName;
                finalUserContext.CompanyDB = this.UserContext.CompanyDB;
                finalUserContext.CompanyId = 1;
                finalUserContext.CompanyName = "XYZ Company";

                finalUserContext.UserId = databaseUser.UserId;
                finalUserContext.RoleId = roleId;
                finalUserContext.LevelIds = databaseUser.UserRole.Where(a => a.RoleId == roleId).FirstOrDefault().LevelIds;
                finalUserContext.EmpId = SecurityUtility.Encrypt(databaseUser.EmployeeId.ToString());
                finalUserContext.EmployeeId = databaseUser.EmployeeId;
                finalUserContext.EmployeeName = "XYZ Safety";
                finalUserContext.Designation = "EHS Head";
                finalUserContext.UserRole = databaseUser.UserRole;
                finalUserContext.ProfilePicturePath = string.Empty;
                finalUserContext.ApplicationLogo = entities.ApplicationConfiguration.AsEnumerable().Where(x => x.ConfigDesc == "Application Logo").Select(x => string.IsNullOrWhiteSpace(x.ConfigValue) ? string.Empty : string.Format("{0}{1}", attachmentPathUrl, x.ConfigValue)).FirstOrDefault();
                finalUserContext.IsTermAccept = databaseUser.TermAccept;
                finalUserContext.LandingPage = 1;

                finalUserContext.SiteLevelId = 9;
                var siteLevel = AppUtility.GetLevel("9", null, 0, null, 0, UserContext).FirstOrDefault();
                if (siteLevel != null)
                {
                    finalUserContext.SiteName = siteLevel.Name;
                    finalUserContext.SiteDescription = siteLevel.Description;
                }

                finalUserContext.FunctionLevelId = 14;
                finalUserContext.EmpContactNo = "9999999999";
                var level = AppUtility.GetLevel("14", null, 0, null, 0, UserContext).FirstOrDefault();
                if (level != null)
                {
                    finalUserContext.FunctionName = level.Name;
                    finalUserContext.FunctionDescription = level.Description;
                }

                finalUserContext.PageAccess = new List<UserContext.PagePermission>();
                var pages = entities.ModulePage.Select(p => new
                {
                    p.PageId,
                    CanRead = true,
                    CanWrite = true
                }).ToList();

                foreach (var p in pages)
                {
                    finalUserContext.PageAccess.Add(new UserContext.PagePermission()
                    {
                        PageId = p.PageId,
                        CanRead = p.CanRead,
                        CanWrite = p.CanWrite
                    });
                }

                finalUserContext.IsSiteUser = true;
                finalUserContext.Ticks = DateTime.UtcNow.Ticks;
                finalUserContext.UserAgent = Request.Headers.UserAgent.ToString();
                finalUserContext.TimeZoneMinutes = this.UserContext.TimeZoneMinutes;
                finalUserContext.LanguageCulture = languageCulture;
                finalUserContext.Token = SecurityUtility.GetToken(finalUserContext);

                return this.Response(MessageTypes.Success, string.Empty, responseToReturn: finalUserContext);
            }
        }

        /// <summary>
        /// Get EMLAH Connection
        /// </summary>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpGet]
        public string GetElmahConnection()
        {
            string connectionString = string.Empty;
            try
            {
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["elmah-express"].ConnectionString;
            }
            catch (Exception ex)
            {
                AppUtility.ElmahErrorLog(ex);
            }

            return connectionString;
        }

        /// <summary>
        /// Get Company Subscription Details
        /// </summary>
        /// <returns>Returns details of Company Subscription<see cref="ApiResponse"/> class.></returns>
        [HttpGet]
        public ApiResponse GetCompanySubscriptionDetails()
        {
            var companyDetail = new
            {
                SubscriptionStartDate = DateTime.Today.AddDays(-10),
                SubscriptionEndDate = DateTime.Today.AddDays(-10),
                NoOfUsers = 100,
                NoOfSites = 10
            };

            return this.Response(MessageTypes.Success, responseToReturn: companyDetail);
        }

        /// <summary>
        /// CheckIs Valid User
        /// </summary>
        /// <param name="password">pass password parameter</param>
        /// <returns>Returns of List Check Is Valid User</returns>
        [HttpGet]
        public ApiResponse CheckIsValidUser(string password)
        {
            return this.Response(MessageTypes.Success, "Success", responseToReturn: null);
        }


    }
}
