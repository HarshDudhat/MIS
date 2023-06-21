// -----------------------------------------------------------------------
// <copyright file="AuditDetailReportData.cs" company="ASK-EHS">
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
    /// Holds audit detail report data object.
    /// </summary>
    public class AuditDetailReportData
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
        /// Gets or sets company detail.
        /// </summary>
        public string CompanyDetail { get; set; }

        /// <summary>
        /// Gets or sets location detail.
        /// </summary>
        public string LocationDetail { get; set; }

        /// <summary>
        /// Gets or sets scope.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets objective.
        /// </summary>
        public string Objective { get; set; }

        /// <summary>
        /// Gets or sets liability.
        /// </summary>
        public string Liability { get; set; }

        /// <summary>
        /// Gets or sets methodology.
        /// </summary>
        public string Methodology { get; set; }

        /// <summary>
        /// Gets or sets opening meeting comment.
        /// </summary>
        public string OpeningMeetingComment { get; set; }

        /// <summary>
        /// Gets or sets closing meeting comment.
        /// </summary>
        public string ClosingMeetingComment { get; set; }

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
        /// Gets or sets person present.
        /// </summary>
        public List<PersonPresent> PersonPresent { get; set; }

        /// <summary>
        /// Gets or sets audit checklist.
        /// </summary>
        public List<CheckList> AuditCheckList { get; set; }

        /// <summary>
        /// Gets or sets attachment list.
        /// </summary>
        public List<Attachments> Attachments { get; set; }

        /// <summary>
        /// Time zone minutes difference.
        /// </summary>
        public int TimeZoneMinutes { get; set; }

        /// <summary>
        /// Audit status
        /// </summary>
        public byte StatusId { get; set; } 
    }

    /// <summary>
    /// Holds meeting person object.
    /// </summary>
    public class PersonPresent
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
        /// Gets or sets meeting type.
        /// </summary>
        public int MeetingType { get; set; }
    }

    /// <summary>
    /// Holds checklist object.
    /// </summary>
    public class CheckList
    {
        /// <summary>
        /// Gets or sets checklist id.
        /// </summary>
        public int CheckListId { get; set; }

        /// <summary>
        /// Gets or sets checklist name.
        /// </summary>
        public string CheckListName { get; set; }

        /// <summary>
        /// Gets or sets category list.
        /// </summary>
        public List<CategoryList> CategoryList { get; set; }
    }

    /// <summary>
    /// Holds checklist category object.
    /// </summary>
    public class CategoryList
    {
        /// <summary>
        /// Gets or sets checklist category id.
        /// </summary>
        public int ChecklistCateId { get; set; }

        /// <summary>
        /// Gets or sets checklist category name.
        /// </summary>
        public string CheckListCategoryName { get; set; }

        /// <summary>
        /// Gets or sets checklist items.
        /// </summary>
        public List<CheckListItems> CheckListItems { get; set; }
    }

    /// <summary>
    /// Holds checklist items object.
    /// </summary>
    public class CheckListItems
    {
        /// <summary>
        /// Gets or sets checklist item name.
        /// </summary>
        public string CheckListItemName { get; set; }

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
    }
}