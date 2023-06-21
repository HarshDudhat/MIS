// -----------------------------------------------------------------------
// <copyright file="CommonController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Controllers
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Linq;
    using System.Web.Http;
    using MVCProject.Api.Models;
    using MVCProject.Api.Models.Common;
    using MVCProject.Api.Utilities;
    using MVCProject.Common.Resources;

    #endregion

    /// <summary>
    /// Common Controller which holds common operations across all modules.
    /// </summary>
    public class CommonController : BaseController
    {
        /// <summary>
        /// Holds context object.
        /// </summary>
        private MVCProjectEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonController"/> class.
        /// </summary>
        public CommonController()
        {
            this.entities = new MVCProjectEntities();
        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Gets attachments.
        /// </summary>
        /// <param name="moduleId">Module id.</param>
        /// <param name="referenceId">Reference id.</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpGet]
        public ApiResponse GetAttachments(int moduleId, int referenceId)
        {
            DirectoryPath directoryPath = this.GetAttachmentPathByModuleId(moduleId);
            string attachmentPath = AppUtility.GetDirectoryPath(directoryPath, UserContext.CompanyDB);
            string attachmentPathUrl = AppUtility.GetDirectoryPath(directoryPath, UserContext.CompanyDB, true);
            var auditComplianceAttachments = this.entities.Attachments.Where(x => x.ModuleId == moduleId && x.ReferenceId == referenceId && !x.DeleteFlag).AsEnumerable()
                                             .Select(x => new
                                             {
                                                 AttachmentId = x.AttachmentId,
                                                 ModuleId = x.ModuleId,
                                                 ReferenceId = x.ReferenceId,
                                                 OriginalFileName = x.OriginalFileName,
                                                 FileName = x.FileName,
                                                 IsDeleted = x.DeleteFlag,
                                                 EntryBy = x.EntryBy,
                                                 EntryDate = x.EntryDate,
                                                 UpdateBy = x.UpdateBy,
                                                 UpdateDate = x.UpdateDate,
                                                 AttachmentPath = string.Format("{0}{1}", attachmentPathUrl, x.FileName),
                                                 Size = File.Exists(new FileInfo(attachmentPath + x.FileName).ToString()) ? new FileInfo(attachmentPath + x.FileName).Length : 0
                                             }).ToList();

            return this.Response(MessageTypes.Success, responseToReturn: auditComplianceAttachments);
        }

        /// <summary>
        /// Saves attachments.
        /// </summary>
        /// <param name="model">Model object.</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpPost]
        public ApiResponse SaveAttachments(AttachmentSaveModel model)
        {
            this.entities.Connection.Open();
            using (DbTransaction transaction = this.entities.Connection.BeginTransaction())
            {
                try
                {
                    DirectoryPath directoryPath = DirectoryPath.Attachment_Task;
                    if (model.AttachmentsList.Count > 0)
                    {
                        ModuleName moduleName = (ModuleName)model.AttachmentsList[0].ModuleId;
                        switch (moduleName)
                        {
                            case ModuleName.Task:
                                directoryPath = DirectoryPath.Attachment_Task;
                                break;                            
                            default:
                                directoryPath = DirectoryPath.Attachment_Task;
                                break;
                        }
                    }

                    string attachmentPathUrl = AppUtility.GetDirectoryPath(directoryPath, UserContext.CompanyDB, true);
                    for (int i = 0; i < model.AttachmentsList.Count; i++)
                    {
                        if (model.AttachmentsList[i].EntryBy == 0)
                        {
                            model.AttachmentsList[i].FilePath = string.Format("{0}{1}", attachmentPathUrl, model.AttachmentsList[i].FileName);
                            model.AttachmentsList[i].EntryBy = UserContext.EmployeeId;
                            model.AttachmentsList[i].EntryDate = DateTime.UtcNow;
                        }
                    }

                    bool newFileAdded = false;
                    bool isSuccess = true;
                    bool isSaved = AppUtility.SaveAttachments(this.entities, UserContext, model.AttachmentsList, out newFileAdded);
                    if (newFileAdded)
                    {
                        isSuccess = AppUtility.MoveFileTempToDestination(directoryPath, UserContext.CompanyDB, model.AttachmentsList);
                    }

                    if (!isSuccess || !isSaved)
                    {
                        return this.Response(MessageTypes.Error, string.Format(Resource.SaveError, Resource.Attachments));
                    }

                    int total = AppUtility.CountAttachments(this.entities, model.AttachmentsList);
                    transaction.Commit();
                    return this.Response(MessageTypes.Success, string.Format(Resource.SaveSuccess, Resource.Attachments), totalRecord: total);
                }
                catch (Exception ex)
                {
                    AppUtility.ElmahErrorLog(ex);
                    transaction.Rollback();
                    return this.Response(MessageTypes.Error, Resource.AnErrorHasOccurred);
                }
                finally
                {
                    this.entities.Connection.Close();
                }
            }
        }

        /// <summary>
        /// Get comments.
        /// </summary>
        /// <param name="moduleId">Module id.</param>
        /// <param name="referenceId">Reference id.</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        //[HttpGet]
        //public ApiResponse GetComments(int moduleId, int referenceId)
        //{
        //    var comments = (from t1 in this.entities.Comments.Where(x => x.ModuleId == moduleId && x.ReferenceId == referenceId && x.IsActive)
        //                    join t2 in this.entities.Employees on t1.EntryBy equals t2.EmployeeId
        //                    select new
        //                    {
        //                        Comment = t1.Comment,
        //                        EntryDate = t1.EntryDate,
        //                        t2.EmpName
        //                    }).OrderByDescending(x => x.EntryDate)
        //                    .Select(x => new
        //                    {
        //                        Comment = x.Comment,
        //                        EntryDate = x.EntryDate,
        //                        Commentby = x.EmpName
        //                    });
        //    return this.Response(MessageTypes.Success, responseToReturn: comments);
        //}


        /// <summary>
        /// Get server date and time
        /// </summary>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpGet]
        public ApiResponse GetServerDateTime()
        {
            return this.Response(MessageTypes.Success, responseToReturn: DateTime.UtcNow);
        }

        /// <summary>
        /// Synchronize Reporting Data
        /// </summary>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpGet]
        public ApiResponse SynchronizeReportingData()
        {
            try
            {
                return this.Response(MessageTypes.Success, message: "Data Synchronized successfully");
            }
            catch (Exception ex)
            {
                AppUtility.ElmahErrorLog(ex);
                return this.Response(MessageTypes.Error, message: "Data Synchronized failure");
            }
        }

        /// <summary>
        /// Get attachment path by module id.
        /// </summary>
        /// <param name="moduleId">Module id.</param>
        /// <returns>Returns string.</returns>
        private DirectoryPath GetAttachmentPathByModuleId(int moduleId)
        {
            DirectoryPath directoryPath;
            switch (moduleId)
            {
                case (int)ModuleName.Nearmiss:
                    directoryPath = DirectoryPath.Attachment_ReportNearmiss;
                    break;
                case (int)ModuleName.InvestigationNearmiss:
                    directoryPath = DirectoryPath.Attachment_InvestigationNearmiss;
                    break;
                case (int)ModuleName.Task:
                    directoryPath = DirectoryPath.Attachment_Task;
                    break;
                case (int)ModuleName.MultiTask:
                    directoryPath = DirectoryPath.Attachment_MultiTask;
                    break;
                case (int)ModuleName.Employee:
                    directoryPath = DirectoryPath.Attachment_Employee;
                    break;
                default:
                    directoryPath = DirectoryPath.Attachment;
                    break;
            }

            return directoryPath;
        }

    }
}
