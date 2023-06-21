// -----------------------------------------------------------------------
// <copyright file="BaseController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Controllers
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Api;

    /// <summary>
    /// Base Controller for all API Controllers
    /// </summary>
    [CompressContent]
    public class BaseController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class
        /// </summary>
        public BaseController()
        {
            // Set Default Connection string for elmah
            HttpContext.Current.Items["elmah-express-connection"] = System.Configuration.ConfigurationManager.ConnectionStrings["elmah-express"].ConnectionString;
        }

        /// <summary>
        /// Gets user's context.
        /// </summary>
        internal UserContext UserContext
        {
            get
            {
                return SecurityUtility.ExtractUserContext(HttpContext.Current.Request);
            }
        }

        /// <summary>
        /// Gets current company access URL.
        /// </summary>
        internal string RequestUrl
        {
            get
            {
                return AppUtility.GetRequestUrl();
            }
        }

        /// <summary>
        /// Get Entity dynamically.
        /// </summary>
        /// <param name="databaseName">database name</param>
        /// <returns>Returns object of type <see cref="SAFEEntities"/> class.</returns>
        internal MVCProjectEntities GetEntity(string databaseName = "")
        {
            if (HttpContext.Current.Items["elmah-express-connection"] == null || string.IsNullOrEmpty(HttpContext.Current.Items["elmah-express-connection"].ToString()))
            {
                HttpContext.Current.Items["elmah-express-connection"] = System.Configuration.ConfigurationManager.ConnectionStrings["elmah-express"].ConnectionString;
            }

            return new MVCProjectEntities();
        }

        /// <summary>
        /// Get Connection String
        /// </summary>
        /// <param name="databaseName">Database Name</param>
        /// <returns>Database Connection String</returns>
        internal string GetConnectionString()
        {
            var entities = new MVCProjectEntities();
            string providerString = ((System.Data.EntityClient.EntityConnection)entities.Connection).StoreConnection.ConnectionString;
            return providerString;
        }

        /// <summary>
        /// Creates response for request.
        /// </summary>
        /// <param name="messageType">Type of message i.e. Info or warning.</param>
        /// <param name="message">Customized message.</param>
        /// <param name="responseToReturn">Response's result.</param>
        /// <param name="totalRecord">Response's number of total Record.</param>
        /// <returns>Returns response of type <see cref="ApiResponse"/> class.</returns>
        internal ApiResponse Response(MessageTypes messageType, string message = "", object responseToReturn = null, int totalRecord = 0)
        {
            return ApiHttpUtility.CreateResponse(isAuthenticated: true, messageType: messageType, message: message, responseToReturn: responseToReturn, total: totalRecord);
        }

        /// <summary>
        /// Gets model state errors.
        /// </summary>
        /// <param name="modelState">Model state object.</param>
        /// <returns>Returns white space separated model state's errors.</returns>
        internal string GetModelErrors(ModelStateDictionary modelState)
        {
            return string.Join(" ", modelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
        }

        /// <summary>
        /// Get Database name
        /// </summary>
        /// <param name="databaseName">Database Name</param>
        /// <returns>Database Connection String</returns>
        internal string GetDatabaseName()
        {
            string database = string.Empty;
            MVCProjectEntities entities = new MVCProjectEntities();
            string providerString = ((System.Data.EntityClient.EntityConnection)entities.Connection).StoreConnection.ConnectionString;
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(providerString);
            if (builder != null)
            {
                database = builder.InitialCatalog;
            }

            return database;
        }
    }
}
