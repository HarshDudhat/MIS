// -----------------------------------------------------------------------
// <copyright file="BBSObservationPlannerView.cs" company="ASK-EHS">
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
    public class BBSObservationPlannerView
    {
        /// <summary>
        /// Gets or sets employee id
        /// </summary>
        public int? PlanYear { get; set; }

        /// <summary>
        /// Gets or sets site id
        /// </summary>
        public bool isEmployeewise { get; set; }

        /// <summary>
        /// Gets or sets sites
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets planner year
        /// </summary>
        public int SelectedMonth { get; set; }

        /// <summary>
        /// Gets or sets Month
        /// </summary>
        public string Month { get; set; }
    }
}