// -----------------------------------------------------------------------
// <copyright file="BBSObservationFilter.cs" company="ASK-EHS">
// TODO: All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Models.FilterCriterias
{
    using System;
    using MVCProject.Api.ViewModel;

    /// <summary>
    /// BBS observation filter
    /// </summary>
    public class BBSObservationFilter : PagingParams
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
        /// Gets or sets departments
        /// </summary>
        public string Departments { get; set; }

        /// <summary>
        /// Gets or sets observers
        /// </summary>
        public string Observers { get; set; }

        /// <summary>
        /// Gets or sets observation type
        /// </summary>
        public string ObservationType { get; set; }

        /// <summary>
        /// Gets or sets status
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets next app date
        /// </summary>
        public DateTime NextAppDate { get; set; }

        /// <summary>
        /// Gets or sets Category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets SegregationId
        /// </summary>
        public int? SegregationId { get; set; }

        /// <summary>
        /// Gets or sets SegregationId
        /// </summary>
        public int? ChecklistCategoryId { get; set; }

        /// <summary>
        /// Gets or sets CommonFlag
        /// </summary>
        public string CommonFlag { get; set; }
    }
}