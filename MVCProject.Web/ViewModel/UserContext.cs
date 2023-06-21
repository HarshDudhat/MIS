// -----------------------------------------------------------------------
// <copyright file="UserContext.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 
namespace MVCProject.ViewModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// User Context class for Safe subscription module 
    /// </summary>
    public class UserContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class.
        /// </summary>
        public UserContext()
        {
            this.Token = string.Empty;
        }

        /// <summary>
        /// Gets or sets Authentication Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets Company Database Name
        /// </summary>
        public string CompanyDB { get; set; }

        /// <summary>
        /// Gets or sets Company Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets Company Server Name
        /// </summary>
        public string CompanyServer { get; set; }

        /// <summary>
        /// Gets or sets Company User Name
        /// </summary>
        public string CompanyUser { get; set; }

        /// <summary>
        /// Gets or sets Company Password
        /// </summary>
        public string CompanyPassword { get; set; }

        /// <summary>
        /// Gets or sets User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets Role Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets Role Group Id
        /// </summary>
        public int[] RoleGroupId { get; set; }

        /// <summary>
        /// Gets or sets Users role list
        /// </summary>
        public List<Role> UserRole { get; set; }

        /// <summary>
        /// Gets or sets Role Level
        /// </summary>
        public int RoleLevel { get; set; }

        /// <summary>
        /// Gets or sets Site Id
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets department Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets Employee Id
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets Employee name
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Gets or sets User profile picture path
        /// </summary>
        public string ProfilePicturePath { get; set; }

        /// <summary>
        /// Gets or sets Application Logo
        /// </summary>
        public string ApplicationLogo { get; set; }

        /// <summary>
        /// Gets or sets Department label configurable.
        /// </summary>
        public string DepartmentLabel { get; set; }

        /// <summary>
        /// Gets or sets Designation
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets Department
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets Site
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether true or false.
        /// </summary>
        public bool IsTermAccept { get; set; }

        /// <summary>
        /// Gets or sets List of Page Access
        /// </summary>
        public List<PagePermission> PageAccess { get; set; }

        /// <summary>
        /// Gets or sets Authentication Time in Ticks
        /// </summary>
        public long Ticks { get; set; }

        /// <summary>
        /// Gets or sets User Agent of client
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets Time Zone Minutes.
        /// </summary>
        public int TimeZoneMinutes { get; set; }

        /// <summary>
        /// Gets or sets IsCorporateSite flag.
        /// </summary>
        public bool IsCorporateSite { get; set; }

        /// <summary>
        /// Page Permission
        /// </summary>
        public struct PagePermission
        {
            /// <summary>
            /// Gets or sets Page Id
            /// </summary>
            public int PageId { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether true or false.
            /// </summary>
            public bool CanRead { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether true or false.
            /// </summary>
            public bool CanWrite { get; set; }
        }

        /// <summary>
        /// User Role
        /// </summary>
        public struct Role
        {
            /// <summary>
            /// Gets or sets Role Id
            /// </summary>
            public int RoleId { get; set; }

            /// <summary>
            /// Gets or sets Role Name
            /// </summary>
            public string RoleName { get; set; }

            /// <summary>
            /// Gets or sets Role Level
            /// </summary>
            public int RoleLevel { get; set; }
        }


        /// <summary>
        /// Gets or sets AllowSubArea flag.
        /// </summary>
        public string AllowSubArea { get; set; }


        /// <summary>
        /// Gets or sets AllowNearmiss flag.
        /// </summary>
        public string AllowNearmiss { get; set; }

        /// <summary>
        /// Gets or sets AllowRiskBehaviour flag.
        /// </summary>
        public string AllowRiskBehaviour { get; set; }

        /// <summary>
        /// Gets or sets AllowHazard flag.
        /// </summary>
        public string AllowHazard { get; set; }

        /// <summary>
        /// Gets or sets AllowInvestigation flag.
        /// </summary>
        public string AllowInvestigation { get; set; }

        /// <summary>
        /// Gets or sets AllowCAPA flag.
        /// </summary>
        public string AllowCAPA { get; set; }

        /// <summary>
        /// Gets or sets AllowCAPAProgress flag.
        /// </summary>
        public string AllowCAPAProgress { get; set; }

        /// <summary>
        /// Gets or sets AllowReport flag.
        /// </summary>
        public string AllowReport { get; set; }

        /// <summary>
        /// Gets or sets AllowSearch flag.
        /// </summary>
        public string AllowSearch { get; set; }

        /// <summary>
        /// Gets or sets AllowAnalytics flag.
        /// </summary>
        public string AllowAnalytics { get; set; }
    }
}