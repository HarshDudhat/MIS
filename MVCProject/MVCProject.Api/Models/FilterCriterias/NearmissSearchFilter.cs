// -----------------------------------------------------------------------
// <copyright file="IncidentSearchFilter.cs" company="ASK-EHS">
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
    /// Incident Search Filter
    /// </summary>
    public class NearmissSearchFilter
    {
        /// <summary>
        /// Gets or sets Start Date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets End Date
        /// </summary>
        public DateTime EndDate { get; set; }


        /// <summary>
        /// Gets or sets comma separated status Ids
        /// </summary>
        public string StatusIds { get; set; }

       

        /// <summary>
        /// Gets or sets page number
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// Gets or sets page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets Sort column name
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Gets or sets sort direction
        /// </summary>
        public string SortDirection { get; set; }

        /// <summary>
        /// Gets or sets time zone minutes
        /// </summary>
        public int TimeZoneMinutes { get; set; }

        /// <summary>
        /// Gets or sets site id
        /// </summary>
        public int SiteId { get; set; }

    }
}