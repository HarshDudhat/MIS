// -----------------------------------------------------------------------
// <copyright file="SAFESUBEnum.cs"  company="ASK E-Sqaure">
// TODO: All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileHeaderFileNameDocumentationMustMatchTypeName", Justification = "Reviewed.")]

namespace MVCProject.Api.Utilities
{
    using System.ComponentModel;

    /// <summary>
    /// Permission Role level
    /// </summary>
    public enum PermissionLevel : int
    {
        /// <summary>
        /// Admin level
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Corporate level
        /// </summary>
        Corporate = 2,

        /// <summary>
        /// Site level
        /// </summary>
        Site = 3,

        /// <summary>
        /// Department level
        /// </summary>
        Department = 4
    }

    /// <summary>
    /// Role Group
    /// </summary>
    public enum RoleGroup : int
    {
        /// <summary>
        /// EHS/OHS Department approval
        /// </summary>
        EHSOHSDepartment = 1
    }

    /// <summary>
    /// Message types enumerations.
    /// </summary>
    public enum MessageTypes
    {
        /// <summary>
        /// Version not compatible
        /// </summary>
        IsAppVersionAvailable = -1,
        
        /// <summary>
        /// Error message type
        /// </summary>
        Error = 0,

        /// <summary>
        /// Success message type
        /// </summary>
        Success = 1,

        /// <summary>
        /// Warning message type
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Information message type
        /// </summary>
        Information = 3,

        /// <summary>
        /// NotFound  message type
        /// </summary>
        NotFound = 4

    }

    /// <summary>
    /// DirectoryPath path for upload.
    /// </summary>
    public enum DirectoryPath
    {
        /// <summary>
        /// Attachment directoryPath
        /// </summary>
        [Description(@"Attachments\")]
        Attachment = 1,

        /// <summary>
        /// Attachment temp directoryPath
        /// </summary>
        [Description(@"Attachments\Temp\")]
        Attachment_Temp = 2,

       

        /// <summary>
        /// Application Logo directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\ApplicationLogo\")]
        Attachment_ApplicationLogo = 13,

        /// <summary>
        /// Application Logo directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\ReportLogo\")]
        Attachment_ReportLogo = 14,

        
    }


}