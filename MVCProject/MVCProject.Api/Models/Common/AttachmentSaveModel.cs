// -----------------------------------------------------------------------
// <copyright file="AttachmentSaveModel.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Attachment save model.
    /// </summary>
    public class AttachmentSaveModel
    {
        /// <summary>
        /// Gets or sets module id.
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// Gets or sets reference id.
        /// </summary>
        public int ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets attachments.
        /// </summary>
        public List<Attachments> AttachmentsList { get; set; }
    }
}