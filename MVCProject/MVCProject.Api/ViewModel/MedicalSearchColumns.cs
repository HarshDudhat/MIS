// -----------------------------------------------------------------------
// <copyright file="MedicalSearchColumns.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// Extended properties of Tasks
// </copyright>

namespace MVCProject.Api.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// General class to get MedicalSearchColumns data
    /// </summary>
    public class MedicalSearchColumns
    {
        /// <summary>
        /// Gets or sets ColumnName
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets TestType
        /// </summary>
        public string TestType { get; set; }

        /// <summary>
        /// Gets or sets MedicalCriteria
        /// </summary>
        public string MedicalCriteria { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Visible
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets or sets Status
        /// </summary>
        public string Status { get; set; }
    }
}