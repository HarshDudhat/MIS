// <copyright file="CapaSearchFilter.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.FilterCriterias
{
    using System;

    /// <summary>
    /// CAPA Search Filter
    /// </summary>
    public class CapaSearchFilter
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
        /// Gets or sets comma separated Site Ids
        /// </summary>
        public string SitesIds { get; set; }

        /// <summary>
        /// Gets or sets comma separated module Ids
        /// </summary>
        public string ModuleIds { get; set; }

        /// <summary>
        /// Gets or sets comma separated Module Names
        /// </summary>
        public string ModuleNames { get; set; }

        /// <summary>
        /// Gets or sets comma separated status Ids
        /// </summary>
        public string StatusIds { get; set; }

        /// <summary>
        /// Gets or sets priority Id
        /// </summary>
        public int PriorityId { get; set; }

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
        /// Gets or sets Task CAPA Type Ids
        /// </summary>
        public string TaskCAPATypeIds { get; set; }
    }
}