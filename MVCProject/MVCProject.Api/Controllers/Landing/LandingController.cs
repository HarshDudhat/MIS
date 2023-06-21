// -----------------------------------------------------------------------
// <copyright file="LandingController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>

namespace MVCProject.Api.Controllers.Landing
{
    #region namespace
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    #endregion

    /// <summary>
    /// Landing controller performs operations related to landing page.
    /// </summary>
    public class LandingController : BaseController
    {
        /// <summary>
        /// Holds context object.
        /// </summary>
        private MVCProjectEntities entities = new MVCProjectEntities();

        /// <summary>
        /// Holds company database name.
        /// </summary>
        private string companyDBname = string.Empty;

        /// <summary>
        /// Holds company expired or not.
        /// </summary>
        private string isCmpExpire = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="LandingController"/> class.
        /// </summary>
        public LandingController()
        {
            this.GetDBName();
            this.entities = new MVCProjectEntities();
        }

        /// <summary>
        /// Get Company Logo
        /// </summary>
        /// <returns>Company Logo. <see cref="ApiResonse"/> class.</returns>
        [HttpGet]
        public ApiResponse GetCompanyLogo()
        {
            try
            {
                string attachmentPathUrl = AppUtility.GetDirectoryPath(DirectoryPath.Attachment_ApplicationLogo, this.companyDBname, true);
                //var logo = this.entities.ApplicationConfiguration.AsEnumerable().Where(x => x.ConfigDesc == "Application Logo").Select(x => string.IsNullOrWhiteSpace(x.ConfigValue) ? string.Empty : string.Format("{0}{1}", attachmentPathUrl, x.ConfigValue)).FirstOrDefault();
                var logo = string.Empty;
                return this.Response(Utilities.MessageTypes.Success, string.Empty, logo);
            }
            catch { }

            return this.Response(Utilities.MessageTypes.Success, string.Empty, string.Empty);
        }

        /// <summary>
        /// Get data base name according client.
        /// </summary>
        private void GetDBName()
        {
            try
            {
                // Get company db name.
                ConnectionStringSettings settings = System.Configuration.ConfigurationManager.ConnectionStrings["SAFEASKMVCEntities"];
                string connectionstring = settings.ConnectionString;
                using (SqlConnection adminDatabaseConnection = new SqlConnection(connectionstring))
                {
                    adminDatabaseConnection.Open();
                    SqlCommand command = new SqlCommand("USP_GetCompanyDBInfofromClient", adminDatabaseConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserUrl", this.RequestUrl);

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        this.companyDBname = (string)dataReader["CompanyDatabaseName"];
                        this.isCmpExpire = (string)dataReader["IsCompanyExpire"];
                    }

                    dataReader.Close();
                    adminDatabaseConnection.Close();
                }
            }
            catch { }
        }
    }
}
