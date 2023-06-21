// -----------------------------------------------------------------------
// <copyright file="EmployeeRecordsFilter.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Models.FilterCriterias
{
    #region namespaces

    using System;
    using MVCProject.Api.ViewModel;

    #endregion

    /// <summary>
    /// Employee records filter criteria
    /// </summary>
    public class EmployeeRecordsFilter
    {
        /// <summary>
        /// Gets or sets SiteId
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Gets or sets DepartmentId
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets DesignationId
        /// </summary>
        public int? DesignationId { get; set; }

        /// <summary>
        /// Gets or sets EmployeeCode
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Gets or sets NameStart
        /// </summary>
        public string NameStart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ApplyPaging
        /// </summary>
        public bool ApplyPaging { get; set; }

        /// <summary>
        /// Gets or sets Paging
        /// </summary>
        public PagingParams Paging { get; set; }
    }
}