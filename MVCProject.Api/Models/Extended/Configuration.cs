// -----------------------------------------------------------------------
// <copyright file="ChangeManagement.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
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
    }

    public partial class ConfigLevel
    {
    }

    public partial class ConfigRole
    {
    }

    public partial class ConfigModule
    {
    }

    public class Hierarchy
    {
        [DataMember]
        public int HierarchyId { get; set; }
    }
}