// -----------------------------------------------------------------------
// <copyright file="SiteMaster.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api.Models
{

    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Extended properties of SiteMaster
    /// </summary>
    public partial class SiteMaster
    {
        /// <summary>
        /// Gets or sets photo of an employee
        /// </summary>
        [DataMember]
        public Attachments Photo { get; set; }
    }

}