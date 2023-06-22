// -----------------------------------------------------------------------
// <copyright file="MISReport.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// User Context class for Safe subscription module 
    /// </summary>
    public class MISReport
    {
        /// <summary>
        /// Gets or sets Report Id
        /// </summary>
        public int ReportId { get; set; }

        /// <summary>
        /// Gets or sets Report Date
        /// </summary>
        public DateTime ReportDate { get; set; }

        /// <summary>
        /// Gets or sets Vertical Id
        /// </summary>
        public int VerticalId { get; set; }

        /// <summary>
        /// Gets or sets Project Id
        /// </summary>
        public int ProjectId { get; set; }


        /// <summary>
        /// Gets or sets List of Fields
        /// </summary>
        public List<Fields> FieldData { get; set; }


        /// <summary>
        /// Fields
        /// </summary>
        public class Fields
        {
            /// <summary>
            /// Gets or sets Field Id
            /// </summary>
            public int FieldId { get; set; }

            /// <summary>
            /// Gets or sets Field Data.
            /// </summary>
            public string FieldValue { get; set; }

            /// <summary>
            /// Gets or sets Remarks.
            /// </summary>
            public string Remarks { get; set; }
        }

    }
}