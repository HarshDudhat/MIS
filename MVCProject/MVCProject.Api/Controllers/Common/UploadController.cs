// -----------------------------------------------------------------------
// <copyright file="UploadController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Controllers.Common
{
    #region Namespaces

    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Common.Resources;
    using System.Drawing;
    using System.Drawing.Imaging;

    #endregion

    /// <summary>
    /// File Upload Controller.
    /// </summary>
    public class UploadController : BaseController
    {        
        /// <summary>
        /// Delete file from temporary folder.
        /// </summary>
        /// <param name="fileToRemove">Name of file which needs to be removed.</param>
        /// <param name="databaseName">Name of database.</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpDelete]
        public ApiResponse DeleteDocumentFromTempLocation(string databaseName,string fileToRemove)
        {
            try
            {
                string tempFilePath = AppUtility.GetDirectoryPath(DirectoryPath.Attachment_Temp, databaseName);
                var filePath = Path.Combine(tempFilePath, fileToRemove);

                // Delete file if exist.
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }

                return this.Response(MessageTypes.Success, string.Empty);
            }
            catch (Exception ex)
            {
                AppUtility.ElmahErrorLog(ex);
                return this.Response(MessageTypes.Error, Resource.SomethingWrong);
            }
        }

        /// <summary>
        /// Upload file in temp folder.
        /// </summary>
        /// <param name="databaseName">Name of database.</param>
        /// <param name="directoryPathEnumName">Directory Path Enum Name for get Attachment Folder</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpPost]
        public ApiResponse UploadFile(string databaseName, string directoryPathEnumName = "Attachment_Temp")
        {
            string FileURL = string.Empty;
            //using (SAFESUBMVCEntities entities = this.GetEntity(UserContext.CompanyDB, UserContext.CompanyServer, UserContext.CompanyUser, UserContext.CompanyPassword))
            //    FileURL = entities.Company.FirstOrDefault().FileURL;

            string directoryPath = string.Empty;
            DirectoryPath enumDirectoryPath = new DirectoryPath();
            if (Enum.IsDefined(typeof(DirectoryPath), directoryPathEnumName))
            {
                Enum.TryParse(directoryPathEnumName, out enumDirectoryPath);
                directoryPath = AppUtility.GetDirectoryPath(enumDirectoryPath, databaseName, false, FileURL);
            }

            string originalFileName = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Get Files from request
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                originalFileName = httpPostedFile.FileName;
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(originalFileName);
                filePath = Path.Combine(directoryPath, fileName);
                try
                {
                    // Delete file if exists
                    FileInfo fileInfo = new FileInfo(filePath);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }

                    // Save File
                    httpPostedFile.SaveAs(filePath);
                }
                catch (Exception ex)
                {
                    AppUtility.ElmahErrorLog(ex);
                    return this.Response(MessageTypes.Error, Resource.SomethingWrong);
                }
            }

            // Set object for response 
            var attachments = new
            {
                OriginalFileName = originalFileName,
                FileName = fileName,
                IsDeleted = false,
                FilePath = filePath,
                Size = File.Exists(new FileInfo(filePath).ToString()) ? new FileInfo(filePath).Length : 0,
                //Thumbnail = "" + CreateThumbnail(string.Format("{0}{1}", AppUtility.GetDirectoryPath(enumDirectoryPath, databaseName, true, FileURL), fileName), 300, 200),
                //Thumbnail = "data:image/png;base64, " + CreateThumbnail(string.Format("{0}{1}", AppUtility.GetDirectoryPath(enumDirectoryPath, databaseName, true, FileURL), fileName), 300, 200),
                FileRelativePath = string.Format("{0}{1}", AppUtility.GetDirectoryPath(enumDirectoryPath, databaseName, true, FileURL), fileName),
            };

            return this.Response(MessageTypes.Success, responseToReturn: attachments);
        }
        
        /// <summary>
        /// Upload file in temp folder and return thumbnail image base64.
        /// </summary>
        /// <param name="databaseName">Name of database.</param>
        /// <param name="directoryPathEnumName">Directory Path Enum Name for get Attachment Folder</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        [HttpPost]
        public ApiResponse UploadImage(string directoryPathEnumName = "Attachment_Temp")
        {
            try
            {
                string FileURL = string.Empty;
                //using (SAFESUBMVCEntities entities = this.GetEntity(UserContext.CompanyDB, UserContext.CompanyServer, UserContext.CompanyUser, UserContext.CompanyPassword))
                //    FileURL = entities.Company.FirstOrDefault().FileURL;

                string directoryPath = string.Empty;
                DirectoryPath enumDirectoryPath = new DirectoryPath();
                if (Enum.IsDefined(typeof(DirectoryPath), directoryPathEnumName))
                {
                    Enum.TryParse(directoryPathEnumName, out enumDirectoryPath);
                    directoryPath = AppUtility.GetDirectoryPath(enumDirectoryPath, UserContext.CompanyDB, false, FileURL);
                }

                string originalFileName = string.Empty;
                string fileName = string.Empty;
                string filePath = string.Empty;

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Get Files from request
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = HttpContext.Current.Request.Files["file"];
                    originalFileName = httpPostedFile.FileName;
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(originalFileName);
                    filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        // Delete file if exists
                        FileInfo fileInfo = new FileInfo(filePath);
                        if (fileInfo.Exists)
                        {
                            fileInfo.Delete();
                        }

                        // Save File
                        httpPostedFile.SaveAs(filePath);
                    }
                    catch (Exception ex)
                    {
                        AppUtility.ElmahErrorLog(ex);
                        return this.Response(MessageTypes.Error, Resource.SomethingWrong);
                    }
                }
                else
                    throw new Exception("error");

                // Set object for response 
                var attachments = new
                {
                    OriginalFileName = originalFileName,
                    FileName = fileName,
                    IsDeleted = false,
                    FilePath = filePath,
                    Size = File.Exists(new FileInfo(filePath).ToString()) ? new FileInfo(filePath).Length : 0,
                    FileRelativePath = string.Format("{0}{1}", AppUtility.GetDirectoryPath(enumDirectoryPath, UserContext.CompanyDB, true), fileName),
                    Thumbnail = AppUtility.CreateThumbnail(filePath, 116, 116)
                };

                return this.Response(MessageTypes.Success, responseToReturn: attachments);
            }
            catch (Exception ex)
            {
                AppUtility.ElmahErrorLog(ex);
            }


            return this.Response(MessageTypes.Error, responseToReturn: null);
        }

    }
}