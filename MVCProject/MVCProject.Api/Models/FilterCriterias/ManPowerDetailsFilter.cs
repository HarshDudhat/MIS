// -----------------------------------------------------------------------
// <copyright file="ManPowerDetailsFilter.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.FilterCriterias
{
    #region namespace
    using System;
    #endregion

    /// <summary>
    /// Man power details filter criteria.
    /// </summary>
    public class ManPowerDetailsFilter
    {
        /// <summary>
        /// Gets or sets site id.
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets report date.
        /// </summary>
        public DateTime ReportDate { get; set; }
    }
}