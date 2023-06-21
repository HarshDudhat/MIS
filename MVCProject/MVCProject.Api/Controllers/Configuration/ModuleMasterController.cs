// -----------------------------------------------------------------------
// <copyright file="ModuleMasterController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Controllers.Configuration
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;

    #endregion

    /// <summary>
    /// Module Master Controller
    /// </summary>
    public class ModuleMasterController : BaseController
    {
        /// <summary>
        /// Holds context object. 
        /// </summary>
        private MVCProjectEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleMasterController"/> class.
        /// </summary>
        public ModuleMasterController()
        {
            this.entities = new MVCProjectEntities();
        }

        /// <summary>
        /// Get main modules
        /// </summary>
        /// <returns>list of modules</returns>
        public ApiResponse GetMainModules()
        {
            var modules = this.entities.ModuleMaster.Where(x => x.IsActive
                && x.isMainModule.HasValue
                && x.isMainModule.Value)
                .Select(x => new
                {
                    x.ModuleId,
                    x.ModuleName
                });

            return this.Response(MessageTypes.Success, responseToReturn: modules);
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
