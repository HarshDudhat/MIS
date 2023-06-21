// -----------------------------------------------------------------------
// <copyright file="SAFESUBEnum.cs"  company="ASK-EHS">
// TODO: All copy rights reserved @ASK-EHS.
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
        /// Attachment employee directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\Employee\")]
        Attachment_Employee = 3,

        /// <summary>
        /// Attachment task directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\Task\")]
        Attachment_Task = 4,

        /// <summary>
        /// Attachment multiTask directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\MultiTask\")]
        Attachment_MultiTask = 5,

        /// <summary>
        /// Attachment user directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\User\")]
        Attachment_User = 6,


        /// <summary>
        /// Application Logo directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\ApplicationLogo\")]
        Attachment_ApplicationLogo = 7,

        /// <summary>
        /// Application Logo directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\ReportLogo\")]
        Attachment_ReportLogo = 8,

        /// <summary>
        /// 
        /// </summary>
        [Description(@"Attachments\{0}\Nearmiss\")]
        Attachment_ReportNearmiss = 9,

        /// <summary>
        /// 
        /// </summary>
        [Description(@"Attachments\{0}\Nearmiss\")]
        Attachment_InvestigationNearmiss = 10,

        /// <summary>
        /// 
        /// </summary>
        [Description(@"Attachments\{0}\Site\")]
        Attachment_Site = 11,

        /// <summary>
        /// 
        /// </summary>
        [Description(@"Attachments\{0}\Company\")]
        Attachment_Company = 12,

        /// <summary>
        /// Attachment Safety Observation directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\SafetyObservation\")]
        Attachment_SafetyObservation = 13,

        /// <summary>
        /// Attachment Site inspection directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\SiteInspection\")]
        Attachment_SiteInspection = 14,

        /// <summary>
        /// Attachment Site inspection Checklist directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\SiteInspectionChecklist\")]
        Attachment_SiteInspectionChecklist = 15,

        /// <summary>
        /// Attachment BBS observation directoryPath
        /// </summary>
        [Description(@"Attachments\{0}\BBSObservation\")]
        Attachment_BBSObservation = 16,

    }


    /// <summary>
    /// Crud Actions
    /// </summary>
    public enum Action : int
    {
        /// <summary>
        /// Insert Action
        /// </summary>
        I = 1,

        /// <summary>
        /// Update Action
        /// </summary>
        U = 2,

        /// <summary>
        /// Delete Action
        /// </summary>
        D = 3
    }

    /// <summary>
    /// Application settings
    /// </summary>
    public enum ApplicationConfigurations
    {
        /// <summary>
        /// No of days before alert for company subscription
        /// </summary>
        No_of_Days_before_alert_for_Company_subscription = 1,

        /// <summary>
        /// No of days before alert for user subscription 
        /// </summary>
        No_of_Days_before_alert_for_User_subscription = 2,

        /// <summary>
        /// Contractor or department 
        /// </summary>
        Contractor_or_Department = 3,

        /// <summary>
        /// Incident local standards 
        /// </summary>
        Incident_local_standards = 4,

        /// <summary>
        /// Incident british standards 
        /// </summary>
        Incident_british_standards = 5,

        /// <summary>
        /// Incident OSHA standards
        /// </summary>
        Incident_OSHA_standards = 6,

        /// <summary>
        /// Employee no of man day Lost 
        /// </summary>
        Employee_No_of_Manday_Lost = 7,

        /// <summary>
        /// Employee no of man hour lost 
        /// </summary>
        Employee_No_of_Manhour_Lost = 8
    }

    /// <summary>
    /// Get module.
    /// </summary>
    public enum ModuleName : int
    {
        /// <summary>
        /// Task module
        /// </summary>
        Task = 1,

        /// <summary>
        /// Near miss module
        /// </summary>
        Nearmiss = 2,

        /// <summary>
        /// For attachment of Investigation Near-miss
        /// </summary>
        InvestigationNearmiss = 3,

        /// <summary>
        /// 
        /// </summary>
        Employee = 4,

        /// <summary>
        /// 
        /// </summary>
        MultiTask = 5,

        /// <summary>
        /// 
        /// </summary>
        Site = 6,
        
        /// <summary>
        /// 
        /// </summary>
        Company = 7,

        /// <summary>
        /// Safety Observation module
        /// </summary>
        SafetyObservation = 8,

        /// <summary>
        /// Site Inspection Module
        /// </summary>
        SiteInspection = 9,

        /// <summary>
        /// Site Inspection Checklist
        /// </summary>
        SiteInspectionChecklist = 10,

        /// <summary>
        /// Behavior Based SafetyObservation 
        /// </summary>
        BehaviorBasedSafetyObservation = 11,
    }

    /// <summary>
    /// Get user type.
    /// </summary>
    public enum UserType : int
    {
        /// <summary>
        /// Corporate user type
        /// </summary>
        Corporate = 1,

        /// <summary>
        /// Site user type
        /// </summary>
        Site = 2
    }

    /// <summary>
    /// Site type
    /// </summary>
    public enum SiteType : byte
    {
        /// <summary>
        /// Offshore site type
        /// </summary>
        Offshore = 0,

        /// <summary>
        /// Onshore site type
        /// </summary>
        Onshore = 1
    }

    /// <summary>
    /// Column width for excel report. (256 * width)
    /// </summary>
    public enum ExcelReportColumnWidth
    {
        /// <summary>
        /// Excel report row number
        /// </summary>
        RowNumber = 256 * 5,

        /// <summary>
        /// Excel report name
        /// </summary>
        Name = 256 * 15
    }

    /// <summary>
    /// Notification Type
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Near miss capa
        /// </summary>
        Nearmiss_CAPA = 1,

        /// <summary>
        /// Near miss
        /// </summary>
        Nearmiss = 2
    }

    /// <summary>
    /// Task priority type 
    /// </summary>
    public enum TaskPriorityType : int
    {
        /// <summary>
        /// Low priority
        /// </summary>
        [Description("Low")]
        Low = 1,

        /// <summary>
        /// Medium priority
        /// </summary>
        [Description("Medium")]
        Medium = 2,

        /// <summary>
        /// High priority
        /// </summary>
        [Description("High")]
        High = 3
    }

    /// <summary>
    /// CAPA task type 
    /// </summary>
    public enum CAPATaskType : int
    {
        ///// <summary>
        ///// Interim
        ///// </summary>
        //[Description("Interim")]
        //Interim = 1,

        ///// <summary>
        ///// Corrective
        ///// </summary>
        //[Description("Corrective")]
        //Corrective = 2,

        ///// <summary>
        ///// Preventive
        ///// </summary>
        //[Description("Preventive")]
        //Preventive = 3

        [Description("Interim")]
        Management = 1,
        [Description("Engineering")]
        Engineering = 2,
        [Description("Behaviour")]
        Behaviour = 3
    }

    /// <summary>
    /// BBS Observation Type
    /// </summary>
    public enum BBSObservationType
    {
        /// <summary>
        /// Safe BBS observation
        /// </summary>
        [Description("Safe")]
        Safe = 1,

        /// <summary>
        /// Unsafe BBS observation
        /// </summary>
        [Description("Unsafe")]
        Unsafe = 2,

        /// <summary>
        /// Not applicable BBS observation
        /// </summary>
        [Description("Not Applicable")]
        NA = 3,
    }


    /// <summary>
    /// InjuryPotential
    /// </summary>
    public enum InjuryPotential
    {
        /// <summary>
        /// Minor
        /// </summary>
        [Description("Minor")]
        Minor = 1,

        /// <summary>
        /// Serious
        /// </summary>
        [Description("Serious")]
        Serious = 2,

        /// <summary>
        /// Fatality
        /// </summary>
        [Description("Fatality")]
        Fatality = 3
    }


    /// <summary>
    /// ObservationChecklistStatus
    /// </summary>
    public enum ObservationChecklistStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        [Description("Open")]
        Open = 1,

        /// <summary>
        /// Closed
        /// </summary>
        [Description("Closed")]
        Closed = 2

    }


    /// <summary>
    /// Task status type 
    /// </summary>
    public enum TaskStatusType : int
    {
        /// <summary>
        /// Draft status
        /// </summary>
        Draft = 0,

        /// <summary>
        /// Active status
        /// </summary>
        Active = 1,

        /// <summary>
        /// In progress status
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Completed status
        /// </summary>
        Completed = 3,

        /// <summary>
        /// Reopen status
        /// </summary>
        ReOpen = 4,

        /// <summary>
        /// Drop status
        /// </summary>
        Drop = 5,

        /// <summary>
        /// Close status
        /// </summary>
        Close = 6,

        /// <summary>
        /// Review required status
        /// </summary>
        Review_Required = 7
    }

    /// <summary>
    /// Timeline audit
    /// </summary>
    public enum TimeLine : int
    {
        /// <summary>
        /// Not applicable
        /// </summary>
        [Description("Not Applicable")]
        NotApplicable = 0,

        /// <summary>
        /// Within 7 days
        /// </summary>
        [Description("Within 7 days")]
        Within7Days = 1,

        /// <summary>
        /// From 8 to 15 days 
        /// </summary>
        [Description("From 8 to 15 days")]
        From8To15Days = 2,

        /// <summary>
        /// Within 15 days 
        /// </summary>
        [Description("Within 15 days")]
        Within15Days = 3,

        /// <summary>
        /// More than 15 days 
        /// </summary>
        [Description("More than 15 days")]
        MoreThan15Days = 4,

        /// <summary>
        /// From 16 to 30 days 
        /// </summary>
        [Description("From 16 to 30 days")]
        From16To30Days = 5,

        /// <summary>
        /// More than 30 days 
        /// </summary>
        [Description("More than 30 days")]
        MoreThan30Days = 6
    }


    /// <summary>
    /// Task Workflow Actions
    /// </summary>
    public enum TaskWFActions
    {
        /// <summary>
        /// Active task work flow
        /// </summary>
        [Description("Active")]
        Active = 1,

        /// <summary>
        /// Active to inProgress 
        /// </summary>
        [Description("In-Progress")]
        ActiveToInProgress = 2,

        /// <summary>
        /// Active to complete 
        /// </summary>
        [Description("Complete")]
        ActiveToComplete = 3,

        /// <summary>
        /// Active to close 
        /// </summary>
        [Description("Close")]
        ActiveToClose = 4,

        /// <summary>
        /// Active to drop 
        /// </summary>
        [Description("Drop")]
        ActiveToDrop = 5,

        /// <summary>
        /// InProgress to complete 
        /// </summary>
        [Description("Complete")]
        InProgressToComplete = 6,

        /// <summary>
        /// InProgress to close 
        /// </summary>
        [Description("Close")]
        InProgressToClose = 7,

        /// <summary>
        /// InProgress to drop 
        /// </summary>
        [Description("Drop")]
        InProgressToDrop = 8,

        /// <summary>
        /// Complete to reopen 
        /// </summary>
        [Description("Re-Open")]
        CompleteToReOpen = 9,

        /// <summary>
        /// Complete to Close 
        /// </summary>
        [Description("Close")]
        CompleteToClose = 10,

        /// <summary>
        /// ReOpen to inProgress 
        /// </summary>
        [Description("In-Progress")]
        ReOpenToInProgress = 11,

        /// <summary>
        /// ReOpen to complete 
        /// </summary>
        [Description("Complete")]
        ReOpenToComplete = 12
    }

    /// <summary>
    /// Enum of Grid Column Data Type
    /// </summary>
    public enum GridColumnDataType
    {
        /// <summary>
        /// String Type
        /// </summary>
        String,

        /// <summary>
        /// Date Type
        /// </summary>
        Date,

        /// <summary>
        /// DateTime type
        /// </summary>
        Datetime,

        /// <summary>
        /// Integer Type
        /// </summary>
        Int,

        /// <summary>
        /// Double Type
        /// </summary>
        Double,

        /// <summary>
        /// Time Type
        /// </summary>
        Time
    }

    /// <summary>
    /// Orientation enum
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// Portrait orientation
        /// </summary>
        Portrait,

        /// <summary>
        /// Landscape orientation
        /// </summary>
        Landscape
    }

    /// <summary>
    /// Horizontal Align
    /// </summary>
    public enum HorizontalAlign
    {
        /// <summary>
        /// Left Align
        /// </summary>
        [Description("Left")]
        Left,

        /// <summary>
        /// Center Align
        /// </summary>
        [Description("Center")]
        Center,

        /// <summary>
        /// Right Align
        /// </summary>
        [Description("Right")]
        Right
    }

    /// <summary>
    /// Employment Type
    /// </summary>
    public enum EmploymentType
    {
        /// <summary>
        /// Contractor Type
        /// </summary>
        Contractor = 1,

        /// <summary>
        /// Permanent Type
        /// </summary>
        Permanent = 2,

        /// <summary>
        /// Temporary Type
        /// </summary>
        Temporary = 3
    }

    /// <summary>
    /// Wages Payable
    /// </summary>
    public enum WagesPayable
    {
        /// <summary>
        /// Full Payable
        /// </summary>
        Full = 1,

        /// <summary>
        /// Part Payable
        /// </summary>
        Part = 2,

        /// <summary>
        /// Not Applicable
        /// </summary>
        NotApplicable = 3
    }

    /// <summary>
    /// Email Stages
    /// </summary>
    public enum EmailStagesEnum : int
    {
        /// <summary>
        /// Reset Password
        /// </summary>
        ResetPassword = 1
    }

    /// <summary>
    /// Email Configuration
    /// </summary>
    public enum EmailConfiguration : int
    {
        /// <summary>
        /// SMTP Server Host
        /// </summary>
        SMTP_Server_Host = 11,

        /// <summary>
        /// SMTP Server Port
        /// </summary>
        SMTP_Server_Port = 12,

        /// <summary>
        /// SMTP Enable SSL
        /// </summary>
        SMTP_Enable_SSL = 13,

        /// <summary>
        /// SMTP Server Network Credential User Name
        /// </summary>
        SMTP_Server_Network_Credential_User_Name = 14,

        /// <summary>
        /// SMTP Server Network Credential Password
        /// </summary>
        SMTP_Server_Network_Credential_Password = 15,

        /// <summary>
        /// From Email Name
        /// </summary>
        From_Email_Name = 16
    }

    /// <summary>
    /// Near-miss actions
    /// </summary>
    public enum NearmissActions : int
    {
        [Description("Report")]
        Report = 1,

        [Description("Approved")]
        Approve = 2,

        [Description("Discard")]
        Discard = 3,

        [Description("Rejected")]
        Reject = 4
    }

    /// <summary>
    /// Near-miss actions
    /// </summary>
    public enum NearmissStatus : int
    {
        [Description("Draft")]
        Draft = 1,

        [Description("Submitted")]
        Submitted = 2,

        [Description("In-progress")] //in case of CAPA Added
        Inprogress = 3,

        [Description("Closed")]
        Closed = 4,

        [Description("Rejected")]
        Rejected = 5,

        [Description("Discard")]
        Discard = 6
    }

    /// <summary>
    /// Near-miss actions
    /// </summary>
    public enum NearmissType : int
    {
        [Description("NearMiss")]
        NearMiss = 1,

        [Description("Hazard")]
        Hazard = 2,

        [Description("RiskBehaviour")]
        RiskBehaviour = 3,
    }

    /// <summary>
    /// Site Inspection Status
    /// </summary>
    public enum SiteInspectionStatus
    {

        /// <summary>
        /// Draft Site Inspection
        /// </summary>
        [Description("Draft")]
        Draft = 1,

        /// <summary>
        /// Completed Site Inspection
        /// </summary>
        [Description("Completed")]
        Completed = 2,

        /// <summary>
        /// Approve Site Inspection
        /// </summary>
        [Description("Approve")]
        Approve = 3,

        /// <summary>
        /// Reject Site Inspection
        /// </summary>
        [Description("Reject")]
        Reject = 4,

        /// <summary>
        /// Planned Site Inspection
        /// </summary>
        [Description("Plan")]
        Planned = 5
    }

    /// <summary>
    /// Safety Observation Status
    /// </summary>
    public enum SafetyObservationStatus
    {
        /// <summary>
        /// Review Pending Safety Observation
        /// </summary>
        [Description("Review Pending")]
        ReviewPending = 1,

        /// <summary>
        /// Completed Safety Observation
        /// </summary>
        [Description("Completed")]
        Completed = 2,

        /// <summary>
        /// Approve Safety Observation
        /// </summary>
        [Description("Approved")]
        Approve = 3,

        /// <summary>
        /// Reject Safety Observation
        /// </summary>
        [Description("Reject")]
        Reject = 4
    }

    /// <summary>
    /// Safety Observation Type
    /// </summary>
    public enum SafetyObservationType
    {
        /// <summary>
        /// Unsafe Act observation
        /// </summary>
        [Description("Unsafe Act")]
        UnsafeAct = 1,

        /// <summary>
        /// Unsafe Condition observation
        /// </summary>
        [Description("Unsafe Condition")]
        UnsafeCondition = 2,

        /// <summary>
        /// Others observation
        /// </summary>
        [Description("Other")]
        Others = 3,
    }

    /// <summary>
    /// BBS
    /// </summary>
    public enum BBSObservationStatus
    {
        /// <summary>
        /// Draft Site Inspection
        /// </summary>
        [Description("Draft")]
        Draft = 1,

        /// <summary>
        /// Completed Site Inspection
        /// </summary>
        [Description("Completed")]
        Completed = 2,

        /// <summary>
        /// Approve Site Inspection
        /// </summary>
        [Description("Approve")]
        Approve = 3,

        /// <summary>
        /// Reject Site Inspection
        /// </summary>
        [Description("Reject")]
        Reject = 4
    }



    /// <summary>
    /// Inspection Sub Category Input Type
    /// </summary>
    public enum InspectionSubCategoryInputType : int
    {
        /// <summary>
        /// Free Text
        /// </summary>
        FreeText = 1,

        /// <summary>
        /// Selection type
        /// </summary>
        Selection = 2,

        /// <summary>
        /// Date Time
        /// </summary>
        DateTime = 3,

        /// <summary>
        /// Only Date
        /// </summary>
        Date = 4,

        /// <summary>
        /// Score value
        /// </summary>
        Score = 5
    }

    /// <summary>
    /// System Config Code
    /// </summary>
    public enum SystemConfigCode : int
    {
        /// <summary>
        /// BBS observation pending priority
        /// </summary>
        BBS_Observation_Pending_Priority = 1,

        /// <summary>
        /// BBS observation pending due date 
        /// </summary>
        BBS_Observation_Pending_Due_Date = 2,

        /// <summary>
        /// BBS observation approve priority 
        /// </summary>
        BBS_Observation_Approve_Priority = 3,

        /// <summary>
        /// BBS observation approve due date 
        /// </summary>
        BBS_Observation_Approve_Due_Date = 4,

        /// <summary>
        /// Site inspection pending priority
        /// </summary>
        Site_Inspection_Pending_Priority = 5,

        /// <summary>
        /// Site inspection pending due date 
        /// </summary>
        Site_Inspection_Pending_Due_Date = 6,

        /// <summary>
        /// Site inspection approve priority 
        /// </summary>
        Site_Inspection_Approve_Priority = 7,

        /// <summary>
        /// Site inspection approve due date 
        /// </summary>
        Site_Inspection_Approve_Due_Date = 8,

        /// <summary>
        /// Equipment inspection pending priority
        /// </summary>
        Equipment_Inspection_Pending_Priority = 1005,

        /// <summary>
        /// Equipment inspection pending due date 
        /// </summary>
        Equipment_Inspection_Pending_Due_Date = 1006,

        /// <summary>
        /// Equipment inspection approve priority 
        /// </summary>
        Equipment_Inspection_Approve_Priority = 1007,

        /// <summary>
        /// Equipment inspection approve due date 
        /// </summary>
        Equipment_Inspection_Approve_Due_Date = 1008
    }

}