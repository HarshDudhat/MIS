// -----------------------------------------------------------------------
// <copyright file="SafetyObservationFilter.cs" company="ASK-EHS">
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
    /// Safety Observation filter
    /// </summary>
    public class SafetyObservationFilter : PagingParams
    {
        /// <summary>
        /// Gets or sets Employee Id
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets site id
        /// </summary>
        public int? SiteId { get; set; }

        /// <summary>
        /// Gets or sets sites
        /// </summary>
        public string Sites { get; set; }

        /// <summary>
        /// Gets or sets department id
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets departments
        /// </summary>
        public string Departments { get; set; }

        /// <summary>
        /// Gets or sets Observers
        /// </summary>
        public string Observers { get; set; }

        /// <summary>
        /// Gets or sets next app date
        /// </summary>
        public DateTime NextAppDate { get; set; }

        /// <summary>
        /// Gets or sets ObservationType id
        /// </summary>
        public int? ObservationTypeId { get; set; }

        /// <summary>
        /// Gets or sets ObservationType
        /// </summary>
        public string ObservationTypes { get; set; }

        /// <summary>
        /// Gets or sets Category id
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets Category
        /// </summary>
        public string Categories { get; set; }

        /// <summary>
        /// Gets or sets Designation id
        /// </summary>
        public int? DesignationId { get; set; }

        /// <summary>
        /// Gets or sets Designation
        /// </summary>
        public string Designations { get; set; }

        /// <summary>
        /// Gets or sets Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets ShortCode
        /// </summary>
        public string ShortCode { get; set; }
    }
}