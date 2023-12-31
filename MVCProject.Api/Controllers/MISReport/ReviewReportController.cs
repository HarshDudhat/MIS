﻿// -----------------------------------------------------------------------
// <copyright file="ReviewReportController.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Controllers.ReviewReport
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
    public class ReviewReportController : BaseController
    {
        private MVCProjectEntities entities;

        public ReviewReportController()
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
        public ApiResponse GetProjectList(int VerticalId)
        {
            var list = entities.USP_MIS_GetProjectList(VerticalId).ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpGet]
        public ApiResponse GetGroupData([FromUri]int ProjectId, DateTime ReportDate)
        {
            var groupList = entities.USP_MIS_GetGroupListByReport(ProjectId, ReportDate,0).ToList();
            var list = new List<object>();

            foreach (var group in groupList)
            {
                var FieldData = entities.USP_MIS_GetGroupDataById(ReportDate, ProjectId, group.GroupId).ToList();
                list.Add(new
                {
                    group.GroupId,
                    group.GroupName,
                    FieldData
                });
            }

            return this.Response(MessageTypes.Success, string.Empty, list);

        }


        //[HttpPost]
        //public ApiResponse ReviewMIS([FromBody] List<MISReport> mis)
        //{
        //    var list = new List<object>();
        //    int reportId = mis[0].ReportId; 
        //    var report = this.entities.MIS_MISReport.FirstOrDefault(x => x.ReportId == reportId);
        //    if (report != null)
        //    {
        //        report.StatusId = 3;
        //    }

        //    foreach (var misItem in mis)
        //    {
        //        foreach(var field in misItem.FieldData)
        //        {
        //            var fieldData = entities.MIS_FieldData.FirstOrDefault(f => f.FieldId == field.FieldId);
        //            if (fieldData != null)
        //            {
        //                fieldData.Remarks = field.Remarks;
        //            }
        //        }
        //    }
        //    if (!(this.entities.SaveChanges() > 0))
        //    {
        //        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.FieldData));
        //    }
        //    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.FieldData));

        //}
        [HttpPost]
        public ApiResponse ReviewMIS([FromBody] List<MIS_FieldData> mis)
        {
            int reportid = 0;
            DateTime newDate = DateTime.Now;
            foreach (var field in mis)
            {
                var list = this.entities.MIS_FieldData.Where(x => x.DataId == field.DataId).FirstOrDefault();
                if (list != null)
                {
                    list.Remarks = field.Remarks;
                    list.EntryDate = newDate;
                }
                reportid = (int)field.ReportId;
            }
            var report = this.entities.MIS_MISReport.Where(x => x.ReportId == reportid).FirstOrDefault();
            if (report != null)
            {
                report.StatusId = 3;
                report.EntryDate = newDate;
            }
            if (!(this.entities.SaveChanges() > 0))
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.FieldData));
            }
            return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.FieldData));

        }

        //[HttpPost]
        //public ApiResponse RejectMIS([FromBody] List<MISReport> mis)
        //{
        //    var list = new List<object>();
        //    int reportId = mis[0].ReportId;
        //    var report = this.entities.MIS_MISReport.FirstOrDefault(x => x.ReportId == reportId);
        //    if (report != null)
        //    {
        //        report.StatusId = 2;
        //    }

        //    foreach (var misItem in mis)
        //    {
        //        foreach (var field in misItem.FieldData)
        //        {
        //            var fieldData = entities.MIS_FieldData.FirstOrDefault(f => f.FieldId == field.FieldId);
        //            if (fieldData != null)
        //            {
        //                fieldData.Remarks = field.Remarks;
        //            }
        //        }
        //    }
        //    if (!(this.entities.SaveChanges() > 0))
        //    {
        //        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.FieldData));
        //    }
        //    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.FieldData));

        //}


        [HttpPost]
        public ApiResponse RejectMIS([FromBody] List<MIS_FieldData> mis)
        {
            int reportid = 0;
            DateTime newDate = DateTime.Now;
            foreach (var field in mis)
            {
                var list = this.entities.MIS_FieldData.Where(x => x.DataId == field.DataId).FirstOrDefault();
                if (list != null)
                {
                    list.Remarks = field.Remarks;
                    list.EntryDate = newDate;
                }
                reportid = (int)field.ReportId;
            }
            var report = this.entities.MIS_MISReport.Where(x => x.ReportId == reportid).FirstOrDefault();
            if (report != null)
            {
                report.StatusId = 2;
                report.EntryDate = newDate;
            }
            if (!(this.entities.SaveChanges() > 0))
            {
                return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.FieldData));
            }
            return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.FieldData));

        }
    }
}
