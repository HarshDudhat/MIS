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
    using iTextSharp.text;
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
        public ApiResponse GetProjectList(int VerticalId)
        {
            var list = entities.USP_MIS_GetProjectList(VerticalId).ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpGet]
        public ApiResponse GetGroupList([FromUri]int ProjectId, DateTime ReportDate)
        {
            var list = entities.USP_MIS_GetGroupList(ReportDate,ProjectId).ToList();
            if(list.Count == 0)
            {
                return this.Response(MessageTypes.Error);
            }
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpGet]
        public ApiResponse GetGroupById([FromUri]int GroupId,int ProjectId, DateTime ReportDate)
        {
            var list = entities.USP_MIS_GetGroupDataById(ReportDate,ProjectId,GroupId).ToList();
            if(list.Count == 0)
            {
                var list2 = entities.USP_MIS_GetGroupById(GroupId).ToList();
                return this.Response(MessageTypes.Success, string.Empty, list2);
            }
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpGet]
        public ApiResponse GetPendingList()
        {
            var list = entities.USP_MIS_PendingReportList().ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }


        [HttpGet]
        public ApiResponse GetAllList()
        {
            var list = entities.USP_MIS_AllReportList().ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpPost]
        public ApiResponse ReportMIS([FromBody]MISReport mis)
        {
            var report = this.entities.MIS_MISReport.Where(x => x.ReportId == mis.ReportId).FirstOrDefault();
            if (report == null)
            {
                MIS_MISReport data = new MIS_MISReport();
                //data.ReportId = mis.ReportId;
                data.VerticalId = mis.VerticalId;
                data.ProjectId = mis.ProjectId;
                data.ReportDate = mis.ReportDate;
                data.EntryBy = 1;
                data.EntryDate = DateTime.Now;
                data.StatusId = 1;
                data.IsActive = true;
                this.entities.MIS_MISReport.AddObject(data);
                if(!(this.entities.SaveChanges() > 0))
                {
                    return this.Response(MessageTypes.Error, string.Format(Resource.SaveError, Resource.Report));
                }
                if(data.ReportId > 0)
                {
                    DateTime now = DateTime.Now;
                    var ReportId = data.ReportId;
                    List<MISReport.Fields> myField = mis.FieldData;
                    foreach(MISReport.Fields fieldData in myField)
                    {
                        this.entities.MIS_FieldData.AddObject(new MIS_FieldData()
                        {
                            ReportId = ReportId,
                            FieldId = fieldData.FieldId,
                            FieldValue = fieldData.FieldValue,
                            Remarks = fieldData.Remarks,
                            EntryBy = 1,
                            EntryDate = now,
                            IsActive = true
                        });
                    }
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(MessageTypes.Error, string.Format(Resource.SaveError, Resource.FieldData));
                    }
                    return this.Response(MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.FieldData));
                }
                else
                {
                    return this.Response(MessageTypes.Error, string.Format(Resource.SaveError, Resource.FieldData));
                }
            }
            else
            {
                report.StatusId = 1;
                this.entities.SaveChanges();
                List<MISReport.Fields> myField = mis.FieldData;
                foreach (MISReport.Fields fieldData in myField)
                {
                    var list = this.entities.MIS_FieldData.Where(x => x.FieldId == fieldData.FieldId && x.ReportId == report.ReportId).FirstOrDefault();
                    list.FieldValue = fieldData.FieldValue;
                    list.Remarks = fieldData.Remarks;
                }
                if (!(this.entities.SaveChanges() > 0))
                {
                    return this.Response(MessageTypes.Error, string.Format(Resource.SaveError, Resource.FieldData));
                }
                return this.Response(MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.FieldData));
            }

        }

    }
}
