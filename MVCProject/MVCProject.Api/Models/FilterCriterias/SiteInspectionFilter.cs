// -----------------------------------------------------------------------
// <copyright file="SafetyObservationFilter.cs" company="ASK-EHS">
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
    /// Site Inspection filter
    /// </summary>
    public class SiteInspectionFilter : PagingParams
    {
        /// <summary>
        /// Gets or sets Employee Id
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets site id
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Gets or sets sites
        /// </summary>
        public string Sites { get; set; }

        /// <summary>
        /// Gets or sets inspectors
        /// </summary>
        public string Inspectors { get; set; }

        /// <summary>
        /// Gets or sets status
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets next app date
        /// </summary>
        public DateTime NextAppDate { get; set; }

        /// <summary>
        /// Gets or sets departments
        /// </summary>
        public string Departments { get; set; }

        /// <summary>
        /// Gets or sets Category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets CustomFilter
        /// </summary>
        public string CustomFilter { get; set; }
    }
}