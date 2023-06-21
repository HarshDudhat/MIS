// -----------------------------------------------------------------------
// <copyright file="CommonController.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
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

        

    }
}
