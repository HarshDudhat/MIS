// -----------------------------------------------------------------------
// <copyright file="MultiTaskListingSearchFilter.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// Extended properties of Tasks
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api.Models.FilterCriterias
{
    /// <summary>
    /// Multi Task Listing Search Filter
    /// </summary>
    public class MultiTaskListingSearchFilter
    {
        /// <summary>
        /// Gets or sets SiteId
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets PageNo
        /// </summary>      
        public int PageNo { get; set; }

        /// <summary>
        /// Gets or sets PageSize
        /// </summary>    
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets SortColumn
        /// </summary>    
        public string SortColumn { get; set; }

        /// <summary>
        /// Gets or sets SortDirection
        /// </summary>    
        public string SortDirection { get; set; }
    }
}