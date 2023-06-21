// -----------------------------------------------------------------------
// <copyright file="ChangeManagement.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

    public partial class ConfigHierarchy
    {
        [DataMember]
        public List<USP_ConfigHierarchy_Search_Result> HierarchyList { get; set; }
    }

    public partial class ConfigLevel
    {
        [DataMember]
        public List<USP_ConfigLevel_Search_Result> LevelList { get; set; }
    }

    public partial class ConfigRole
    {
        [DataMember]
        public List<USP_ConfigRole_Search_Result> RoleList { get; set; }
    }

    public partial class ConfigModule
    {
        [DataMember]
        public List<USP_ConfigModule_Search_Result> ModuleList { get; set; }

        [DataMember]
        public List<Hierarchy> HierarchyIds { get; set; }
    }

    public class Hierarchy
    {
        [DataMember]
        public int HierarchyId { get; set; }
    }
}