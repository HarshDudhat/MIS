// -----------------------------------------------------------------------
// <copyright file="EmployeesMetaData.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]

namespace MVCProject.Api.Models
{
    #region namespaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;
    using MVCProject.Api.Filters;

    #endregion

    /// <summary>
    /// Employees meta data
    /// </summary>
    [DataContract]
    public class EmployeesMetaData
    {
        /// <summary>
        /// Gets or sets contact number
        /// </summary>
        [DataMember]
        [ContactNumberAnnotation]
        public string EmpContactNo { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        [DataMember]
        [EmailAnnotation]
        [StringLength(500)]
        public string EmpEmail { get; set; }
    }

    /// <summary>
    /// Employees partial class
    /// </summary>
    [MetadataType(typeof(EmployeesMetaData))]
    public partial class Employees
    {
    }
}