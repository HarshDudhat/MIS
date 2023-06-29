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
            var result = entities.USP_MIS_SubmittedReportList().ToList();
            var TotalRecords = result.Count;
            var list = result.Select(x => new
            {
                x.GroupId,
                x.GroupName,
                x.ReportId,
                x.ReportDate,
                x.VerticalId,
                x.VerticalName,
                x.ProjectId,
                x.ProjectName,
                x.StatusName,
                x.EntryDate,
                TotalRecords
            }).AsQueryable().OrderByField(reportParams.OrderByColumn,
               reportParams.IsAscending).Skip((reportParams.CurrentPageNumber - 1) * reportParams.PageSize).
               Take(reportParams.PageSize);
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpPost]
        public ApiResponse GetReviewedList(PagingParams reportParams)
        {
            var result = entities.USP_MIS_ReviewedReportList().ToList();
            var TotalRecords = result.Count;
            var list = result.Select(x => new
            {
                x.GroupId,
                x.GroupName,
                x.ReportId,
                x.ReportDate,
                x.VerticalId,
                x.VerticalName,
                x.ProjectId,
                x.ProjectName,
                x.StatusName,
                x.EntryDate,
                TotalRecords
            }).AsQueryable().OrderByField(reportParams.OrderByColumn,
               reportParams.IsAscending).Skip((reportParams.CurrentPageNumber - 1) * reportParams.PageSize).
               Take(reportParams.PageSize);
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpPost]
        public ApiResponse GetAllList(PagingParams reportParams)
        {
            var result = entities.USP_MIS_AllReportList().ToList();
            var TotalRecords = result.Count;
            var list = result.Select(x => new
            {
                x.GroupId,
                x.GroupName,
                x.ReportId,
                x.ReportDate,
                x.VerticalId,
                x.VerticalName,
                x.ProjectId,
                x.ProjectName,
                x.StatusName,
                x.EntryDate,
                TotalRecords
            }).AsQueryable().OrderByField(reportParams.OrderByColumn,
                reportParams.IsAscending).Skip((reportParams.CurrentPageNumber - 1) * reportParams.PageSize).
                Take(reportParams.PageSize);
            return this.Response(MessageTypes.Success, string.Empty, list);
        
        }

        [HttpPost]
        public ApiResponse GetPendingList(PagingParams reportParams)
        {
            var result = entities.USP_MIS_PendingReportList().ToList();
            var TotalRecords = result.Count;
            var list = result.Select(x => new
            {
                x.GroupId,
                x.GroupName,
                x.ReportId,
                x.ReportDate,
                x.VerticalId,
                x.VerticalName,
                x.ProjectId,
                x.ProjectName,
                x.StatusName,
                x.EntryDate,
                TotalRecords
            }).AsQueryable().OrderByField(reportParams.OrderByColumn,
               reportParams.IsAscending).Skip((reportParams.CurrentPageNumber - 1) * reportParams.PageSize).
               Take(reportParams.PageSize);
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
