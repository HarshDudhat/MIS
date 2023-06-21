// -----------------------------------------------------------------------
// <copyright file="BBSObservationPlannerFilter.cs" company="ASK-EHS">
// TODO: All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Models.FilterCriterias
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using MVCProject.Api.ViewModel;

    /// <summary>
    /// BBS observation planner filter
    /// </summary>
    public class BBSObservationPlannerFilter
    {
        /// <summary>
        /// Gets or sets employee id
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets site id
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets sites
        /// </summary>
        public string Sites { get; set; }

        /// <summary>
        /// Gets or sets planner year
        /// </summary>
        public int PlanerYear { get; set; }

        /// <summary>
        /// Gets or sets paging
        /// </summary>
        public PagingParams Paging { get; set; }
    }
}