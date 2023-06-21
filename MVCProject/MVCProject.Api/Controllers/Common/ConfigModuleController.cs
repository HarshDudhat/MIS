// -----------------------------------------------------------------------
// <copyright file="AccountController.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Controllers.Common
{
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Common.Resources;
    using MVCProject.Api.Utilities.ExtensionMethods;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class ConfigModuleController : BaseController
    {
        private MVCProjectEntities entities;

        readonly string SP_IU = "USP_ConfigModule_IU";
        readonly string SP_Search = "USP_ConfigModule_Search";

        [HttpPost]
        public ApiResponse Save(ConfigModule configModule)
        {
            if (configModule != null && configModule.ModuleList != null && configModule.ModuleList.Count > 0)
            {
                for (int i = 0; i < configModule.ModuleList.Count; i++)
                {
                    if (configModule.ModuleList[i].LanguageId == null)
                        configModule.ModuleList[i].LanguageId = configModule.ModuleList[i].ULanguageId;
                    configModule.ModuleList[i].IsActive = configModule.IsActive;
                    configModule.ModuleList[i].ModuleHierarchy = string.Join(",", configModule.HierarchyIds.Select(a => a.HierarchyId).ToArray()) + ",";
                    if (configModule.ModuleList[i].UModuleId == null || configModule.ModuleList[i].UModuleId == 0)
                    {
                        configModule.ModuleList[i].EntryBy = UserContext.EmployeeId;
                        configModule.ModuleList[i].EntryDate = DateTime.UtcNow;
                    }
                    else
                    {
                        configModule.ModuleList[i].UpdateBy = UserContext.EmployeeId;
                        configModule.ModuleList[i].UpdateDate = DateTime.UtcNow;
                    }
                }
                DataTable dtConfigModule = configModule.ModuleList.ToDataTable();

                List<SqlParameter> procParameters = new List<SqlParameter>();
                procParameters.Add(new SqlParameter() { ParameterName = "@TVP_ConfigModule", Value = dtConfigModule, TypeName = "dbo.TVP_ConfigModule", SqlDbType = SqlDbType.Structured });
                SqlDataReader sqlDataReader = SqlDatabaseUtility.ExecuteQuery(SP_IU, procParameters);
                if (sqlDataReader.Read())
                {
                    int result = Convert.ToInt32(sqlDataReader["Result"]);
                    if (result == 0)
                    {
                        return this.Response(MessageTypes.Error, string.Format(Resource.ErrorOccurred, Resource.ConfigModule), string.Empty);
                    }
                    else if (result == 1)
                    {
                        return this.Response(MessageTypes.Success, string.Format(Resource.SaveSuccess, Resource.ConfigModule), string.Empty);
                    }
                    else if (result == 2)
                    {
                        return this.Response(MessageTypes.Error, string.Format(Resource.AlreadyExists, Resource.ConfigModuleName), string.Empty);
                    }
                }
            }
            return this.Response(MessageTypes.Error, string.Format(Resource.NoRecordFound, Resource.ConfigModule), string.Empty);
        }

        [HttpGet]
        public ApiResponse GetDetail(int id)
        {
            var result = DBOperation(string.Empty, id: id);
            if (result != null)
            {
                return this.Response(MessageTypes.Success, string.Empty, result);
            }
            else
            {
                return this.Response(MessageTypes.Error, string.Format(Resource.NoRecordFound, Resource.ConfigModule), string.Empty);
            }
        }

        [HttpGet]
        public ApiResponse List()
        {
            var result = DBOperation(languageCulture: UserContext.LanguageCulture, forList: true);
            if (result != null)
            {
                return this.Response(MessageTypes.Success, string.Empty, result.ModuleList);
            }
            else
            {
                return this.Response(MessageTypes.Error, string.Format(Resource.NoRecordFound, Resource.ConfigModule), string.Empty);
            }
        }

        public ConfigModule DBOperation(string languageCulture, int id = 0, bool forList = false)
        {
            ConfigModule configModule = new ConfigModule();
            List<SqlParameter> procParameters = new List<SqlParameter>();
            procParameters.Add(new SqlParameter() { ParameterName = "@ModuleId", Value = id });
            procParameters.Add(new SqlParameter() { ParameterName = "@LanguageCulture", Value = languageCulture });
            procParameters.Add(new SqlParameter() { ParameterName = "@ForList", Value = forList });
            SqlDataReader sqlDataReader = SqlDatabaseUtility.ExecuteQuery(SP_Search, procParameters);
            configModule.ModuleList = new List<USP_ConfigModule_Search_Result>((this.entities.Translate<USP_ConfigModule_Search_Result>(sqlDataReader)).ToList());
            if (configModule != null)
            {
                configModule.HierarchyIds = new List<Hierarchy>();
                if (id > 0)
                {
                    var firstRecord = configModule.ModuleList.First();
                    configModule.ModuleId = firstRecord.ModuleId ?? 0;
                    var hierarchy = firstRecord.ModuleHierarchy.Split(',').ToArray();
                    foreach (var item in hierarchy)
                    {
                        if (!string.IsNullOrEmpty(item))
                            configModule.HierarchyIds.Add(new Hierarchy() { HierarchyId = Convert.ToInt32(item) });
                    }
                    configModule.IsActive = firstRecord.IsActive ?? true;
                }
                else
                {
                    configModule.IsActive = true;
                }
            }
            return configModule;
        }
    }
}
