using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Api.Models.FilterCriterias
{
    /// <summary>
    /// Analytics reports filter
    /// </summary>
    public class ChartFilter
    {
        /// <summary>
        /// Gets or sets StartDate
        /// </summary>      
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets EndDate
        /// </summary>      
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets NearmissTypeId
        /// </summary>      
        public string NearmissTypeId { get; set; }

        /// <summary>
        /// Gets or sets SiteId
        /// </summary>      
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets GroupedCols
        /// </summary>      
        public string GroupedCols { get; set; }
    }
}