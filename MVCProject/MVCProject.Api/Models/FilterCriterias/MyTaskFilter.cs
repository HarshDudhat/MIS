// -----------------------------------------------------------------------
// <copyright file="MyTaskFilter.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// Extended properties of Tasks
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.FilterCriterias
{
    using System;

    /// <summary>
    ///  My task filter
    /// </summary>
    public class MyTaskFilter
    {
        public MyTaskFilter()
        {
            IsFirstLoad = false;
        }

        /// <summary>
        /// Gets or sets StartDate
        /// </summary>      
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets EmployeeId
        /// </summary>      
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets EmployeeId
        /// </summary>      
        public string EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets TaskFilterTypeId
        /// </summary>      
        public int TaskFilterTypeId { get; set; }

        /// <summary>
        /// Gets or sets TaskStatusId
        /// </summary>      
        public int TaskStatusId { get; set; }

        /// <summary>
        /// Gets or sets PageNo
        /// </summary>      
        public int PageNo { get; set; }

        /// <summary>
        /// Gets or sets PageSize
        /// </summary>      
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets SortColumn
        /// </summary>      
        public string SortColumn { get; set; }

        /// <summary>
        /// Gets or sets SortDirection
        /// </summary>      
        public string SortDirection { get; set; }

        /// <summary>
        /// Gets or sets TimeZoneMinutes
        /// </summary>            
        public int TimeZoneMinutes { get; set; }

        /// <summary>
        /// Gets or sets IsDraftOnlyNearmiss
        /// </summary>            
        public bool IsDraftOnlyNearmiss { get; set; }

        /// <summary>
        /// Gets or sets AreaIds
        /// </summary>            
        public string AreaIds { get; set; }

        /// <summary>
        /// Gets or sets SiteId
        /// </summary>            
        public int SiteId { get; set; }
        
        /// <summary>
        /// Gets or sets for default page load
        /// </summary>            
        public bool IsFirstLoad { get; set; }

        /// <summary>
        /// Gets or sets ReportedById
        /// </summary>            
        public int ReportedById { get; set; }

        /// <summary>
        /// Gets or sets ModuleId
        /// </summary>      
        public int ModuleId { get; set; }

        /// <summary>
        /// Gets or sets TaskTypeId
        /// </summary>      
        public int? TaskTypeId { get; set; }

    }
}