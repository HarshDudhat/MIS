// -----------------------------------------------------------------------
// <copyright file="PagingParams.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api.ViewModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Paging parameters 
    /// </summary>
    public class AttachmentViewModel
    {
        public List<AttachmentsDTO> AttachmentsList { get; set; }
        public int ModuleId { get; set; }
        public int ReferenceId { get; set; }
    }

    public class AttachmentsDTO
    {
        public int AttachmentId { get; set; }
        public int ModuleId { get; set; }
        public int ReferenceId { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }
        public bool DeleteFlag { get; set; }
        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int DeleteBy { get; set; }
        public DateTime DeleteDate { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
    }

}