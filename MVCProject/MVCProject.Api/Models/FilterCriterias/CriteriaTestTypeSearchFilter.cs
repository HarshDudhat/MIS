// -----------------------------------------------------------------------
// <copyright file="CriteriaTestTypeSearchFilter.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.FilterCriterias
{
    /// <summary>
    /// Criteria TestType Search Filter class for SAFESUB module 
    /// </summary>
    public class CriteriaTestTypeSearchFilter
    {
        /// <summary>
        /// Gets or sets PageNo value
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// Gets or sets PageSize value
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets SortColumn value
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Gets or sets SortDirection value
        /// </summary>
        public string SortDirection { get; set; }

        /// <summary>
        /// Gets or sets Search string
        /// </summary>
        public string Search { get; set; }
    }
}