// -----------------------------------------------------------------------
// <copyright file="ManPowerSearchFilter.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.FilterCriterias
{
    #region namespace
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    #endregion    

    /// <summary>
    /// Man power search filter criteria.
    /// </summary>
    public class ManPowerSearchFilter
    {
        /// <summary>
        /// Gets or sets employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets comma separated sites.
        /// </summary>
        public string SelectedSites { get; set; }

        /// <summary>
        /// Gets or sets comma separated departments.
        /// </summary>
        public string SelectedDepartments { get; set; }

        /// <summary>
        /// Gets or sets comma separated categories.
        /// </summary>
        public string SelectedCategories { get; set; }
    }
}