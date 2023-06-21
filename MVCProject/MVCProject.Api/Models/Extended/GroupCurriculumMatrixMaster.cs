// -----------------------------------------------------------------------
// <copyright file="GroupCurriculumMatrixMaster.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

    /// <summary>
    /// Partial class of group curriculum matrix.
    /// </summary>
    public partial class GroupCurriculumMatrixMaster
    {
        /// <summary>
        /// Gets or sets remarks property.
        /// </summary>
        [DataMember]
        public string Remarks { get; set; }
    }
}