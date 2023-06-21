// -----------------------------------------------------------------------
// <copyright file="AuditReportData.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]

namespace MVCProject.ViewModel
{
    #region namespaces

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Holds audit summary report data object.
    /// </summary>
    public class AuditReportData
    {
        /// <summary>
        /// Gets or sets report logo.
        /// </summary>
        public ReportLogo ReportLogo { get; set; }

        /// <summary>
        /// Gets or sets plant name.
        /// </summary>
        public string PlantName { get; set; }

        /// <summary>
        /// Gets or sets type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets industry type.
        /// </summary>
        public string IndustryType { get; set; }

        /// <summary>
        /// Gets or sets scope of industry.
        /// </summary>
        public string ScopeOfIndustry { get; set; }

        /// <summary>
        /// Gets or sets audit title.
        /// </summary>
        public string AuditTitle { get; set; }

        /// <summary>
        /// Gets or sets start date.
        /// </summary>
        public string AuditStartDate { get; set; }

        /// <summary>
        /// Gets or sets end date.
        /// </summary>
        public string AuditEndDate { get; set; }

        /// <summary>
        /// Gets or sets scope.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets objective.
        /// </summary>
        public string Objective { get; set; }

        /// <summary>
        /// Gets or sets executive summary.
        /// </summary>
        public string ExecutiveSummary { get; set; }

        /// <summary>
        /// Gets or sets approved by.
        /// </summary>
        public string ApprovedBy { get; set; }

        /// <summary>
        /// Gets or sets auditor team.
        /// </summary>
        public List<AuditorTeam> AuditorTeam { get; set; }

        /// <summary>
        /// Gets or sets auditor summary.
        /// </summary>
        public List<AuditSummaryList> AuditSummaryList { get; set; }

        /// <summary>
        /// Gets or sets task list.
        /// </summary>
        public List<TaskList> TaskList { get; set; }

        /// <summary>
        /// Gets or sets comment list.
        /// </summary>
        public List<CommentList> CommentList { get; set; }

        /// <summary>
        /// Gets or sets non compliance list.
        /// </summary>
        public List<NonComplianceList> NonComplianceList { get; set; }

        /// <summary>
        /// Gets or sets attachment list.
        /// </summary>
        public List<Attachments> Attachments { get; set; }

        /// <summary>
        /// Time zone minutes difference.
        /// </summary>
        public int TimeZoneMinutes { get; set; }
    }

    /// <summary>
    /// Holds auditor team object.
    /// </summary>
    public class AuditorTeam
    {
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets position.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets department.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets email id.
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets direct no.
        /// </summary>
        public string DirectNo { get; set; }

        /// <summary>
        /// Gets or sets mobile no.
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the team leader is true or false..
        /// </summary>
        public bool TeamLeader { get; set; }
    }

    /// <summary>
    /// Holds task object.
    /// </summary>
    public class TaskList
    {
        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets responsible.
        /// </summary>
        public string Responsible { get; set; }

        /// <summary>
        /// Gets or sets reviewer.
        /// </summary>
        public string Reviewer { get; set; }

        /// <summary>
        /// Gets or sets priority.
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets due date.
        /// </summary>
        public string DueDate { get; set; }

        /// <summary>
        /// Gets or sets status.
        /// </summary>
        public string Status { get; set; }
    }

    /// <summary>
    /// Holds audit summary object.
    /// </summary>
    public class AuditSummaryList
    {
        /// <summary>
        /// Gets or sets axis.
        /// </summary>
        public string axis { get; set; }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public decimal value { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string description { get; set; }
    }

    /// <summary>
    /// Holds comment object.
    /// </summary>
    public class CommentList
    {
        /// <summary>
        /// Gets or sets comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets commented by and date and time.
        /// </summary>
        public string CommentedByAndtime { get; set; }
    }

    /// <summary>
    /// Holds non compliance object.
    /// </summary>
    public class NonComplianceList
    {
        /// <summary>
        /// Gets or sets compliance.
        /// </summary>
        public string Compliance { get; set; }

        /// <summary>
        /// Gets or sets observation.
        /// </summary>
        public string Observation { get; set; }

        /// <summary>
        /// Gets or sets recommendation.
        /// </summary>
        public string Recommendation { get; set; }

        /// <summary>
        /// Gets or sets item description.
        /// </summary>
        public string ItemDescription { get; set; }
    }

    /// <summary>
    /// Holds attachment object.
    /// </summary>
    public class Attachments
    {
        /// <summary>
        /// Gets or sets file name.
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets or sets attachment path.
        /// </summary>
        public string AttachmentPath { get; set; }
    }

    /// <summary>
    /// Holds report logo object.
    /// </summary>
    public class ReportLogo
    {
        /// <summary>
        /// Gets or sets attachment path.
        /// </summary>
        public string AttachmentPath { get; set; }
    }
}