// -----------------------------------------------------------------------
// <copyright file="Attachments.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api.Models
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Extended properties of Attachments
    /// </summary>
    public partial class Attachments
    {
        /// <summary>
        /// Gets or sets ActionFlag
        /// </summary>
        [DataMember]
        public string ActionFlag { get; set; }


        /// <summary>
        /// Gets or sets Module History UID
        /// </summary>
        [DataMember]
        public long ModuleHistoryUID { get; set; }

        /// <summary>
        /// Gets or sets Attachment Path
        /// </summary>
        [DataMember]
        public string AttachmentPath { get; set; }
    }
}