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
        public ApiResponse GetGroupList([FromUri]int ProjectId, DateTime ReportDate)
        {
            var list = entities.USP_MIS_GetGroupList(ReportDate,ProjectId).ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpGet]
        public ApiResponse GetGroupById([FromUri]int GroupId)
        {
            var list = entities.USP_MIS_GetGroupById(GroupId).ToList();
            return this.Response(MessageTypes.Success, string.Empty, list);
        }

        [HttpPost]
        public ApiResponse ReportMIS([FromBody]MISReport mis)
        {
            var report = this.entities.MIS_MISReport.Where(x => x.ReportId == mis.ReportId).FirstOrDefault();
            if(report == null)
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
                    return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.Report));
                }
                if(data.ReportId > 0)
                {
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
                            EntryDate = DateTime.Now,
                            IsActive = true
                        });
                    }
                    if (!(this.entities.SaveChanges() > 0))
                    {
                        return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.FieldData));
                    }
                    return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.FieldData));
                }
                else
                {
                    return this.Response(Utilities.MessageTypes.Error, string.Format(Resource.SaveError, Resource.FieldData));
                }
            }
            else
            {
                var reportData = entities.USP_MIS_GetDataByReportId(mis.ReportId).ToList();
                return this.Response(Utilities.MessageTypes.Success, string.Format(Resource.CreatedSuccessfully, Resource.FieldData));
            }

        }

    }
}
