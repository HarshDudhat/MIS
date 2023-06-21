// -----------------------------------------------------------------------
// <copyright file="MyAssignedTaskSearchFilter.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// Extended properties of Tasks
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.FilterCriterias
{
    /// <summary>
    /// My Assigned Task Search Filter
    /// </summary>
    public class MyAssignedTaskSearchFilter
    {
        /// <summary>
        /// Gets or sets EmployeeId
        /// </summary>      
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether true or false
        /// </summary>
        public bool IsInactiveUserTask { get; set; }

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