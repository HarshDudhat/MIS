// -----------------------------------------------------------------------
// <copyright file="SubGroupMaster.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Partial class of sub group master.
    /// </summary>
    public partial class SubGroupMaster
    {
        /// <summary>
        /// Gets or sets remarks property
        /// </summary>
        [DataMember]
        public string Remarks { get; set; }
    }
}