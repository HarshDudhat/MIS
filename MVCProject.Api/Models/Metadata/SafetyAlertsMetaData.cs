// -----------------------------------------------------------------------
// <copyright file="OrderMasterMetaData.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]

namespace MVCProject.Api.Models
{    
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Safety Alerts Meta Data
    /// </summary>
    [DataContract]
    public class SafetyAlertsMetaData
    {
        /// <summary>
        /// Gets or sets Heading
        /// </summary>
        [Required(ErrorMessage = "Heading is required.")]
        [DataMember(IsRequired = true)]
        [StringLength(500, ErrorMessage = "Heading cannot be longer than 500 characters.")]
        public string Heading { get; set; }

        /// <summary>
        /// Gets or sets WhatHappenedDesc
        /// </summary>
        [Required(ErrorMessage = "What Happened is required.")]
        [DataMember(IsRequired = true)]
        public string WhatHappenedDesc { get; set; }

        /// <summary>
        /// Gets or sets KeyMessage
        /// </summary>
        [Required(ErrorMessage = "Key Messages is required.")]
        [DataMember(IsRequired = true)]
        public string KeyMessage { get; set; }

        /// <summary>
        /// Gets or sets SinglePointLesson
        /// </summary>
        [Required(ErrorMessage = "Single Point Lesson is required.")]
        [DataMember(IsRequired = true)]
        public string SinglePointLesson { get; set; }
    }

    /// <summary>
    /// Safety Alerts partial class
    /// </summary>
    [MetadataType(typeof(SafetyAlertsMetaData))]
    public partial class SafetyAlerts
    {
    }
}