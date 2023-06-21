// -----------------------------------------------------------------------
// <copyright file="GroupMaster.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Partial class of group master.
    /// </summary>
    public partial class GroupMaster
    {
        /// <summary>
        /// Gets or sets Remarks from UI and set in Remarks property
        /// </summary>
        [DataMember]
        public string Remarks { get; set; }
    }
}