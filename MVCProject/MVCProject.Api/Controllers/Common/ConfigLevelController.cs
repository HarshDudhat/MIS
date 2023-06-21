//-----------------------------------------------------------------------
// <copyright file="CategoryController.cs" company="ASK-EHS">
//     Copyright (c) ASK-EHS 2019.
// </copyright>
//-----------------------------------------------------------------------

namespace MVCProject.Api.Controllers.Common
{
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Common.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// ConfigModule Controller
    /// </summary>
    public class ConfigLevelController : BaseController
    {
        private MVCProjectEntities entities;

        readonly string SP_IU = "USP_ConfigModule_IU";
        readonly string SP_Search = "USP_ConfigModule_Search";

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

        [HttpGet]
        public ApiResponse FilterList(string levelIds, string hierarchyIds, int moduleId, int parentLevelId, string searchText)
        {
            var levelList = AppUtility.GetLevel(levelIds, hierarchyIds, moduleId, searchText, parentLevelId, UserContext);
            if (levelList != null)
            {
                return this.Response(MessageTypes.Success, string.Empty, levelList);
            }
            else
            {
                return this.Response(MessageTypes.Error, string.Format(Resource.NoRecordFound, Resource.ConfigLevel), string.Empty);
            }
        }

    }
}
