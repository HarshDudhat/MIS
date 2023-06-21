using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Api.Models.Common
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    /// <summary>
    /// Register Model
    /// </summary>
    [DataContract]
    public class RegisterModel
    {
        /// <summary>
        /// Gets or sets First Name
        /// </summary>
        [Required]
        [DataMember(IsRequired = true)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets Last Name
        /// </summary>
        [Required]
        [DataMember(IsRequired = true)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Mobile Number
        /// </summary>
        [Required]
        [DataMember(IsRequired = true)]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets Employee Code
        /// </summary>
        [Required]
        [DataMember(IsRequired = true)]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Gets or sets Company Email Id
        /// </summary>
        [DataMember]
        public string CompanyEmailId { get; set; }

        /// <summary>
        /// Gets or sets Request Url
        /// </summary>
        [Required]
        [DataMember(IsRequired = true)]
        public string CompanyName { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        public string Password { get; set; }

        [DataMember]
        public bool TermsAccepted { get; set; }

        [DataMember]
        public int SiteId { get; set; }

        [DataMember]
        public int DepartmentId { get; set; }

        [DataMember]
        public int DesignationId { get; set; }
    }

    public enum RegisterResponseType
    {
        AddedSuccessfully = 1,
        EmployeeCodeAlreadyExists = 2,
        ContactNoAlreadyInUse = 3,
        EmailIdAlreadyInUse = 4,
        AnErrorHasOccurred = 5,
        CompanyExpired = 6,
        SomethingWrong = 7,
        InvalidData = 8
    }
}