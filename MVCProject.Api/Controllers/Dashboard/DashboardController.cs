// -----------------------------------------------------------------------
// <copyright file="DashboardController.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Controllers.Dashboard
{
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Api.ViewModel;
    using MVCProject.Common.Resources;
    using System.Net.Mail;

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
    public class DashboardController : BaseController
    {
        private MVCProjectEntities entities;

        public DashboardController()
        {
            this.entities = new MVCProjectEntities();
        }

        [HttpPost]
        public ApiResponse GetSubmittedList(PagingParams reportParams)
        {
            var list = entities.USP_MIS_SubmittedReportList().ToList().AsQueryable().OrderByField(reportParams.OrderByColumn,
                reportParams.IsAscending).Skip((reportParams.CurrentPageNumber - 1) * reportParams.PageSize).
                Take(reportParams.PageSize);
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpPost]
        public ApiResponse GetReviewedList(PagingParams reportParams)
        {
            var list = entities.USP_MIS_ReviewedReportList().ToList().AsQueryable().OrderByField(reportParams.OrderByColumn,
                reportParams.IsAscending).Skip((reportParams.CurrentPageNumber - 1) * reportParams.PageSize).
                Take(reportParams.PageSize);
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpPost]
        public ApiResponse GetAllList(PagingParams reportParams)
        {
            var list = entities.USP_MIS_AllReportList().ToList().AsQueryable().OrderByField(reportParams.OrderByColumn,
                reportParams.IsAscending).Skip((reportParams.CurrentPageNumber - 1) * reportParams.PageSize).
                Take(reportParams.PageSize);
            ;
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpPost]
        public ApiResponse GetPendingList(PagingParams reportParams)
        {
            var list = entities.USP_MIS_PendingReportList().ToList().AsQueryable().OrderByField(reportParams.OrderByColumn,
                reportParams.IsAscending).Skip((reportParams.CurrentPageNumber - 1) * reportParams.PageSize).
                Take(reportParams.PageSize);
            ;
            return this.Response(MessageTypes.Success, string.Empty, list);
        }


        [HttpGet]
        public ApiResponse GetCount()
        {
            var list = entities.USP_MIS_GetCount().FirstOrDefault();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }
    }
}
