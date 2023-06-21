// -----------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api
{
    using System.Web.Http.Filters;
    using System.Web.Mvc;
    using MVCProject.Api.Filters;

    /// <summary>
    /// Filter Collection Configuration
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register Global Filter Collection
        /// </summary>
        /// <param name="filters">object of <see cref="GlobalFilterCollection"/></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Register Http Filters Collection
        /// </summary>
        /// <param name="filters">object of <see cref="HttpFilterCollection"/></param>
        public static void RegisterHttpFilters(HttpFilterCollection filters)
        {
            filters.Add(new HandleExceptionAttribute());
        }
    }
}