// -----------------------------------------------------------------------
// <copyright file="UserContext.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models
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
        /// Gets or sets User Id
        /// </summary>
        public int UserId { get; set; }



        /// <summary>
        /// Gets or sets Company Database Name
        /// </summary>
        public string CompanyDB { get; set; }

        /// <summary>
        /// Gets or sets Company Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets Company Name
        /// </summary>
        public string CompanyName { get; set; }


        /// <summary>
        /// Gets or sets Role Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets Level Ids
        /// </summary>
        public string LevelIds { get; set; }

        ///// <summary>
        ///// Gets or sets Users role list
        ///// </summary>
        //public List<USP_GetUserRole_Result> UserRole { get; set; }


        /// <summary>
        /// Gets or sets Designation
        /// </summary>
        public string Designation { get; set; }

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

            /// <summary>
            ///  Gets or sets Menu Id
            /// </summary>
            public int? MenuId { get; set; }
        }

        public string LanguageCulture { get; set; }

        public int SiteLevelId { get; set; }
        public string SiteName { get; set; }
        public string SiteDescription { get; set; }


        public int FunctionLevelId { get; set; }
        public string FunctionName { get; set; }
        public string FunctionDescription { get; set; }
        public string EmpContactNo { get; set; }

        public bool IsSiteUser { get; set; }

        public int LandingPage { get; set; }

        //Encrypted EmployeeId to be Used in My Profile link.
        public string EmpId { get; set; }

        public bool IsADUser { get; set; }

    }
}