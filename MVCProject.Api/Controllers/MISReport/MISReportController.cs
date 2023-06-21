// -----------------------------------------------------------------------
// <copyright file="MISReportController.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Controllers.MISReport
{
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Api.ViewModel;
    using MVCProject.Common.Resources;

    #region Namespaces
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using System.Web.Http;
    using System.Web.ModelBinding;
    using Newtonsoft.Json;
    using System.Text;
    using System.Web;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.IO;
    using System.Web.UI;
    #endregion
    public class MISReportController : BaseController
    {
        private MVCProjectEntities entities;

        public MISReportController()
        {
            this.entities = new MVCProjectEntities();
        }

        [HttpGet]
        public ApiResponse GetVerticalList()
        {
            var list = entities.USP_MIS_GetVerticalList().ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpGet]
        public ApiResponse GetProjectList()
        {
            var list = entities.USP_MIS_GetProjectList().ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpGet]
        public ApiResponse GetGroupList()
        {
            var list = entities.USP_MIS_GetGroupList().ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpGet]
        public ApiResponse GetGroupById([FromUri]int GroupId)
        {
            var list = entities.USP_MIS_GetGroupById(GroupId).ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

    }
}
