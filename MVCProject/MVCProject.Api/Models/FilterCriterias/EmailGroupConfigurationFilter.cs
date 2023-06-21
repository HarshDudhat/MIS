using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCProject.Api.ViewModel;

namespace MVCProject.Api.Models.FilterCriterias
{
    public class EmailGroupConfigurationFilter : PagingParams
    {
        /// <summary>
        /// Gets or sets Group Name
        /// </summary>
        public DateTime GroupName { get; set; }

        /// <summary>
        /// Gets or sets Search String
        /// </summary>
        public string Search { get; set; }

    }
}