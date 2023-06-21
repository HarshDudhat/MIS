angular.module("DorfKetalMVCApp").service("CommonEnums", [CommonEnums]);

function CommonEnums() {
    var vm = this;

    // Convert Enum to  Array of {Id, Name}
    vm.toArray = function (enumObject) {
        var _EnumName = "";
        var tempArray = [];
        for (var key in enumObject) {
            if (key == "_EnumName") {//Skip this key from enum, "_EnumName" contains object name of Enum
                _EnumName = enumObject[key];
                continue;
            }
            tempArray.push({ Id: enumObject[key], Name: enumResource[_EnumName][key] });
        }
        return tempArray;
    };

    // Frequency
    vm.Frequency = {
        _EnumName: "Frequency",
        Monthly: 1,
        Quarterly: 2,
        Yearly: 3
    };

    // Permission Level
    vm.PermissionLevel = {
        _EnumName: "PermissionLevel",
        Admin: 1,
        Corporate: 2,
        Vertical: 3,
        Site: 4,
        Department: 5,
        Location: 6
    };

    // RoleLevel
    vm.RoleLevel = {
        _EnumName: "RoleLevel",
        All: 1,
        Vertical: 2,
        Site: 3,
        Department: 4,
        Location: 5
    };


    // Role groups
    vm.Rolegroup = {
        _EnumName: "Rolegroup",
        SiteInspectionFirstApproval: 1,
        SiteInspectionSecondApproval: 2,
        SafetyObservationFirstApproval: 3,
        ChangeManagementFirstApproval: 4,
        ChangeManagementSecondApproval: 5,
        ChangeManagementDepartmentHeadApproval: 6,
        KnowledgeInformationCompanyApproval: 7,
        KnowledgeInformationNonCompanyApproval: 8
    };

    // Role Name
    vm.RoleName = {
        _EnumName: "RoleName",
        SystemAdministrator: 1,
        Corporate: 2,
        CorporateEHSHead: 3,
        ITAdministrator: 4,
        SBU: 5,
        SiteAdministrator: 6,
        SiteHead: 7,
        SiteEHSHead: 8,
        SiteEHSManager: 9,
        SiteEHSTeam: 10,
        OHC: 11,
        FunctionHead: 12,
        SectionHead: 13,
        Engineers: 14,
        LocationIncharge: 15,
        AgencyHead: 16,
        AgencyEngineer: 17,
        OMHead: 18,
        PlantHRHead: 19,
        MA: 20,
        MedicalOfficer: 21,
        Auditor: 22
    };

    // Module Name
    vm.ModuleName = {
        _EnumName: "ModuleName",
        Incident: 1,
        Nearmiss: 44,
        EHSObservation: 6,
        Task: 3,
        SiteInspection: 25,
        ThemeInspection: 27,
        EHSMeeting: 15,
        Meeting: 28,
        //For Planner Control Test
        Audit: 8,
        SafetyEquipmentInspection: 45,
        ManagmentWalkthrough: 46,
        EHSTraining: 51,
        InternalAudit_Observation_Attachment: 56,
        InternalAudit_Attachment: 26,
        MedicalCheckup: 49,
        Medical: 48,
        LeaveCertificate: 55
    };

    // Attachment Type
    vm.AttachmentType = {
        _EnumName: "AttachmentType",
        General: 1,
        IncidentStatements: 2,
        IncidentCosts: 3,
        ThemeInspectionChecklist: 4,
        EquipmentInspectionChecklist: 5,
        SiteInspectionChecklist: 6,
        IncidentVictim: 7,
        ToolboxTalkSignature: 8,
        EHSTrainingSchedule: 9,
        BeforeTaskAssign: 10,
        AfterTaskComplete: 11,
        Meeting: 12,
        MeetingAgenda: 13,
        EHSTraining: 14,
        InternalAuditAttachment: 15,
        InternalAuditObservationAttachment: 16
    };

    // Audit Compliance
    vm.CAPATaskType = {
        _EnumName: "CAPATaskType",
        Interim: 1,
        Corrective: 2,
        Preventive: 3
    };

    // Task Type
    vm.TaskType = {
        _EnumName: "TaskType",
        General: 1,
        Nearmiss: 2,
        Nearmiss_CAPA: 3,
        Multi_Task: 4,
        Incident: 5,
        Incident_CAPA: 6,
        SiteInspection: 7,
        SiteInspection_CAPA: 8,
        SafetyEquipmentInspection: 9,
        SafetyEquipmentInspection_CAPA: 10,
        ThemeInspection: 11,
        ThemeInspection_CAPA: 12,
        SiteInspection_Planned: 13,
        ThemeInspection_Planned: 14,
        Meeting_CAPA: 19,
        Safety_Observation_CAPA: 27,
        Safety_Observation: 28,
        Safety_Observation_Walkthrough: 29,
        Knowledge_Information_CAPA: 35,
        Pending_Knowledge_Information: 36,
        Approved_Knowledge_Information: 37,
        Rejected_Knowledge_Information: 38,
        Walkthrough_Planned: 39,
        EHS_Training_Add_Candidate: 40,
        Internal_Audit_CAPA: 15,
        External_Audit_CAPA: 16,
        Internal_Audit_Observation_CAPA: 17
    };

    // Task Priority
    vm.TaskPriority = {
        _EnumName: "TaskPriority",
        Low: 1,
        Medium: 2,
        High: 3,
        Immediate: 4
    };

    // Task Priority
    vm.TaskPriorityDays = {
        _EnumName: "TaskPriorityDays",
        Low: 15,
        Medium: 7,
        High: 3,
        Immediate: 1
    };

    // Task Priority
    vm.SeverityType = {
        _EnumName: "SeverityType",
        Low: 1,
        Medium: 2,
        High: 3
    };

    //ModuleTypeEnum
    vm.ModuleTypeEnum = {
        _EnumName: "ModuleTypeEnum",
        IncidentCAPA: 1,
        EHSObservationCAPA: 6,
        SiteInspecCAPA: 25,
        Audit: 8
    };

    // Task Status
    vm.TaskStatus = {
        _EnumName: "TaskStatus",
        Active: 1,
        InProgress: 2,
        Completed: 3,
        ReOpen: 4,
        Dropped: 5,
        Closed: 6,
        RequestedforDateRevision: 7,
        RevisionAccepted: 8,
        RevisionRejected: 9,
        Rejected: 10,
        SentForReviewL1: 11,
        SentForReviewL2: 12
    };

    vm.TaskResponsibleType = {
        _EnumName: "TaskResponsibleType",
        Employee: 1,
        Function: 2,
        Agency: 3
    }

    // Task Workflow Actions
    vm.TaskWFActions = {
        _EnumName: "TaskWFActions",
        Active: 1,
        ActiveToInProgress: 2,
        ActiveToComplete: 3,
        ActiveToClose: 4,
        ActiveToDrop: 5,
        InProgressToComplete: 6,
        InProgressToClose: 7,
        InProgressToDrop: 8,
        CompleteToReOpen: 9,
        CompleteToClose: 10,
        ReOpenToInProgress: 11,
        ReOpenToComplete: 12,
        ActiveToReviseDate: 13,
        InProgressToReviseDate: 14,
        ReviseDateAccept: 15,
        ReviseDateReject: 16,
        ActiveToCompleteByReviewL1: 17,
        ActiveToCompleteByReviewL2: 18,
        InProgressToCompleteByReviewL1: 19,
        InProgressToCompleteByReviewL2: 20,
        SentForReviewL1: 21,
        SentForReviewL2: 22,
        ApproveByReviewL2: 23,
        ApproveCloseByReviewL1: 24,
        RejectByReviewL1: 25,
        ApproveCloseByReviewL2: 26,
        RejectByReviewL2: 27,
        AssignToPerson: 28
    }

    // TimeLine
    vm.TimeLine = {
        _EnumName: "TimeLine",
        Within7Days: 1,
        From8To15Days: 2,
        Within15Days: 3,
        MoreThan15Days: 4,
        From16To30Days: 5,
        MoreThan30Days: 6
    };

    // InvolvedVictimType
    vm.InvolvedVictimType = {
        _EnumName: "InvolvedVictimType",
        Employee: 1,
        Contractor: 2,
        //Visitor: 3,
        Other: 4
    };

    // InvolvedStatementType
    vm.InvolvedStatementType = {
        _EnumName: "InvolvedStatementType",
        InjuredPerson: 1,
        InformBy: 2,
        WitnessPerson: 3
    };

    //    vm.InvolvedOtherVictimType = {
    //        _EnumName: "InvolvedOtherVictimType", 
    //        Contractor: 2,
    //        Visitor: 3,
    //        Other: 4
    //    }; 

    // Gender
    vm.Gender = {
        _EnumName: "Gender",
        Male: 1,
        Female: 2
    };

    // TypeofTraining
    vm.TypeofTraining = {
        _EnumName: "TypeofTraining",
        EHS: 1,
        Technical: 2,
        Behavioural: 3
        //HR: 2,
        //GMP: 3,
        //Functional: 4
    };

    // TrainingPersonInvolvementType
    vm.TrainingPersonInvolvementType = {
        _EnumName: "TrainingPersonInvolvementType",
        Initiator: 1,
        Facilitator: 2,
        Trainer: 3,
        Participant: 4
    };

    // Training version
    vm.TrainingVersion =
    {
        _EnumName: "TrainingVersion",
        Major: 1,
        Minor: 2
    };

    // TrainingCycleDuration
    vm.TrainingCycleDuration = {
        _EnumName: "TrainingCycleDuration",
        Quarterly: 1,
        HalfYearly: 2,
        Yearly: 3,
        TwoYears: 4,
        ThreeYears: 5,
        OneTime: 6
    };

    vm.NatureofTraining =
    {
        _EnumName: "NatureofTraining",
        Planned: 1,
        Unplanned: 2
    };

    vm.MethodofTraining =
    {
        _EnumName: "MethodofTraining",
        SelfTraining: 1,
        ClassRoomTraining: 2,
        DemostrationTraining: 3
    };

    vm.EvaluationofTraining =
    {
        _EnumName: "EvaluationofTraining",
        SelfEvaluation: 1,
        EvaluationthroughQuestionnaire: 2,
        EvaluationthroughDemonstrationonjob: 3
    };

    vm.TypeofScheduleTraining =
    {
        _EnumName: "TypeofScheduleTraining",
        Internal: 1,
        External: 2
    };

    vm.ParticipantResult =
    {
        _EnumName: "ParticipantResult",
        Qualified: 1,
        Retraining: 2
    };

    vm.TrainingStatus =
    {
        _EnumName: "TrainingStatus",
        Scheduled: 1,
        Inprogress: 2,
        Completed: 3,
        Postponed: 4,
        Cancelled: 5,
        NominationPending: 6,
        NominationCompleted: 7
    };

    vm.TrainingActionToPerform =
    {
        _EnumName: "TrainingActionToPerform",
        SendNominationNotification: 1
    };

    //Injury Potential
    vm.InjuryPotential = {
        _EnumName: "InjuryPotential",
        Minor: 1,
        Serious: 2,
        Fatality: 3
    };
    //Observation Checklist Status
    vm.ObservationChecklistStatus = {
        _EnumName: "ObservationChecklistStatus",
        Open: 1,
        Closed: 2
    };


    //SafetyObservation Type 
    vm.SafetyObservationType = {
        _EnumName: "SafetyObservationType",
        SafeAct: 1,
        UnsafeAct: 2,
        UnsafeCondition: 3,
        Housekeeping: 4
    };

    // Training log type.
    vm.SiteType =
    {
        _EnumName: "SiteType",
        Offshore: 0,
        Onshore: 1
    };

    // Category type
    vm.EmployeeCategory = {
        _EnumName: "EmployeeCategory",
        Employee: 1,
        Contractor: 2
    };

    // audit status
    vm.AuditSearchStatus = {
        _EnumName: "AuditSearchStatus",
        All: 0,
        Draft: 1,
        Completed: 2,
        Approve: 3,
        Reject: 4
    };

    // Agency Type
    vm.AgencyType = {
        _EnumName: "AgencyType",
        Machinery: 1,
        Equipment: 2,
        Material: 3,
        Working_Environment: 4,
        Equipment_Material_Involved: 5
    };

    // Employment Type
    vm.EmploymentType = {
        _EnumName: "EmploymentType",
        Employee: 1,
        Agency: 2,
        Transporter: 3,
        Visitor: 4,
        Consultant: 5
    };

    // InvolvedType
    vm.InvolvedType = {
        _EnumName: "InvolvedType",
        Equipment: 1,
        Material: 2
    };

    // Medical Employee Type
    vm.MedicalEmployeeType = {
        _EnumName: "MedicalEmployeeType",
        Employee: 1,
        Contractor: 2,
        Other: 0
    };

    // CheckupType
    vm.CheckupType = {
        _EnumName: "CheckupType",
        External: 1,
        Internal: 2,
        //Campaign: 3,
        PreEmployment: 4
    };

    // Wages Payable
    vm.WagesPayable = {
        _EnumName: "WagesPayable",
        Full: 1,
        Part: 2,
        NotApplicable: 3
    };

    // Directions
    vm.Direction = {
        _EnumName: "Direction",
        East: 1,
        North: 2,
        South: 3,
        West: 4
    };

    // month.
    vm.Month = {
        _EnumName: "Month",
        January: 1,
        February: 2,
        March: 3,
        April: 4,
        May: 5,
        June: 6,
        July: 7,
        August: 8,
        September: 9,
        October: 10,
        November: 11,
        December: 12
    };

    // Component
    vm.Component = {
        _EnumName: "Component",
        ActionItemsStatistics: 1,
        InjuryStatistics: 2,
        NearmissStatistics: 3,
        IncidentClassificationStatistics: 4,
        ObservationStatistics: 5,
        SafetyEquipmentInspectionStatistics: 6,
        SiteInspectionStatistics: 7,
        ToolboxtalkStatistics: 8

    };

    //ComponentFilter
    vm.ComponentFilter = {
        _EnumName: "ComponentFilter",
        ChartType: 1,
        Segregation: 2,
        TimeRange: 3,
        Site: 4,
        Functions: 5,
        Agencies: 6,
        Shift: 7,
        TypeofInjury: 8,
        EHSActivity: 9,
        Priority: 10,
        Status: 11,
        IncidentClassification: 12,
        ActionItemType: 13,
        BodyPart: 14,
        SafetyObservationType: 15,
        Location: 16,
        EquipmentCategory: 17,
        ChecklistType: 18
    };

    //ComponentSubFilter
    vm.ComponentSubFilter = {
        _EnumName: "ComponentSubFilter",
        Column: 1,
        Table: 2,
        Pie: 3,
        Bar: 4,
        Line: 5,
        Area: 6,
        TimelinewiseMonthly: 7,
        Sitewise: 8,
        Functionwise: 9,
        Locationwise: 10,
        EHSActivitywise: 11,
        NatureofInjurywise: 12,
        IncidentClassificationwise: 13,
        ShiftWise: 14,
        BodyPartwise: 15,
        Agencieswise: 16,
        EquipmentCategorywise: 17,
        CheckListwise: 18,
        Prioritywise: 19,
        Last7Days: 20,
        Last15Days: 21,
        Last30Days: 22,
        Last2Months: 23,
        Last3Months: 24,
        Last6Months: 25,
        Last12Months: 26,
        CurrentYear: 27,
        LastYear: 28,
        Last2Years: 29,
        Last5Years: 30,
        CustomRange: 31
    };

    // Chart Color
    vm.ChartColor = {
        _EnumName: "ChartColor",
        Open: 'red',
        Inprogress: 'yellow',
        Close: 'green',
        FAC: 'green',
        MTC: 'blue',
        FAT: 'red',
        LTI: 'yellow',
        RWC: 'orange',
        digitCount: 'lightgreen',
        IncidentCount: 'blue',
        SafeAct: 'green',
        UnSafeAct: 'yellow',
        UnSafeCondition: 'orange',
        ToolBoxCount: 'blue',
        ToolBoxCountPerson: 'green'
    };

    vm.ChartToolTip = {
        _EnumName: "ChartToolTip",
        Open: 'Open Action Item(s)',
        Inprogress: 'In-Progress Action Item(s)',
        Close: 'Closed Action Item(s)',
        FAC: 'No of First Aid Case',
        MTC: 'No of Medical Tratment Case',
        FAT: 'No of Fatality',
        LTI: 'No of Lost Time Incident',
        RWC: 'No of Restricted Work Case',
        digitCount: 'No of Near Miss',
        IncidentCount: 'No of Incident',
        SafeAct: 'Safe Act',
        UnSafeAct: 'Un-Safe Act',
        UnSafeCondition: 'Un-Safe Condition',
        ToolBoxCount: 'Tool Box Talk Conducted',
        ToolBoxCountPerson: 'No of Person(s)'
    };

    // TaskFilterType
    vm.TaskFilterType = {
        _EnumName: "TaskFilterType",
        OnGoing: 1,
        ReviewNeeded: 2,
        AssignByMe: 3,
        All: 4,
        CAPAActionItem: 5
    };

    // Site Inspection Status
    vm.SiteInspectionStatus =
    {
        _EnumName: "SiteInspectionStatus",
        InProgress: 1,
        Completed: 2
    };

    // Site Inspection Status
    vm.SiteInspectionSearchStatus =
    {
        _EnumName: "SiteInspectionSearchStatus",
        Draft: 1,
        Submitted: 2,
        //FirstApproved: 3,
        //FirstRejected: 4,
        Approved: 3,
        ChangesSuggestionForApproval: 4
    };

    // Safety Observation Status
    vm.SafetyObservationStatus =
    {
        _EnumName: "SafetyObservationStatus",
        Request: 1,
        Reject: 2,
        ApproveAndClose: 3,
        Discard: 4
    };

    // Safety Management Walkthrough Status
    vm.SafetyManagementWalkthroughStatus =
    {
        _EnumName: "SafetyManagementWalkthroughStatus",
        Request: 1,
        Reject: 2,
        ApproveAndClose: 3,
        Discard: 4
    };

    // Equipement Inspection Status
    vm.EquipmentInspectionStatus =
    {
        _EnumName: "EquipmentInspectionStatus",
        Request: 1,
        Reject: 2,
        ApproveAndClose: 3,
        Discard: 4
    };

    // ChangeManagement Status
    vm.ChangeManagementStatus =
    {
        _EnumName: "ChangeManagementStatus",
        Draft: 1,
        Approve: 2,
        Reject: 3,
        Cancel: 4
    };

    // Change Management Inner Status
    vm.ChangeManagementInnerStatus =
    {
        _EnumName: "ChangeManagementInnerStatus",
        Draft: 1,
        Submitted: 2,
        FirstApproved: 3,
        FirstRejected: 4,
        FirstCancelled: 5,
        SecondApproved: 6,
        SecondRejected: 7,
        SecondCancelled: 8
    };

    // Inspection Input type
    vm.InspectionSubCategoryInputType =
    {
        _EnumName: "InspectionSubCategoryInputType",
        FreeText: 1,
        Selection: 2,
        DateTime: 3,
        Date: 4,
        Score: 5
    };

    // Incident Status Filter
    vm.IncidentStatusFilter =
    {
        _EnumName: "IncidentStatusFilter",
        Completed: 1,
        Approved: 2,
        Closed: 3,
        Rejected: 4
    };

    // OPD Type
    vm.OPDType =
    {
        _EnumName: "OPDType",
        Sickness: 1,
        Injury: 2,
        Other: 3
    };

    // Fitness Status
    vm.FitnessStatus =
    {
        _EnumName: "FitnessStatus",
        Pending: 1,
        Fit: 2,
        Unfit: 3,
        Fitwithrestriction: 4
    };


    // Meeting Status
    vm.MeetingStatus =
    {
        _EnumName: "MeetingStatus",
        Scheduled: 1,
        Postponed: 2,
        InProgress: 3,
        Completed: 4,
        Discard: 5,
        Closed: 6
    };

    // Meeting WF Action
    vm.MeetingWFAction =
    {
        _EnumName: "MeetingWFAction",
        Schedule: 1,
        Discard: 2,
        InProgress: 3,
        Postpone: 4,
        Complete: 5,
        Closed: 6
    };

    // audit status
    vm.KnowledgeInformationStatus = {
        _EnumName: "KnowledgeInformationStatus",
        SendForApproval: 1,
        Approve: 2,
        Reject: 3
    };

    //Domain
    vm.Domain = {
        _EnumName: "Domain",
        Environment: 1,
        Health: 2,
        Safety: 3
    };

    //Hierarchy
    vm.HierarchyLevel = {
        _EnumName: "HierarchyLevel",
        Administrator: 1,
        EnergyGroup: 2,
        Services: 3,
        Site: 4,
        Function: 5,
        SubFunction: 6,
        Location: 7,
        SubLocation: 8,
        Agency: 9,
        SubAgency: 10
    };

    //Catagory
    vm.Category = {
        _EnumName: "Category",
        Environment: 1,
        Health: 2,
        Safety: 3
    };

    vm.SafetyObservationAction =
    {
        _EnumName: "SafetyObservationAction",
        Request: 1,
        Reject: 2,
        ApproveAndClose: 3,
        Discard: 4
    };

    vm.NearmissAction =
    {
        _EnumName: "NearmissAction",
        Request: 1,
        Reject: 2,
        SendForInvestigation: 3,
        CloseFIR: 4,
        Discard: 5,
        InvestigationCompletedSentForEHSHeadReview: 6,
        InvestigationCompletedSentForOMHeadReview: 7,
        SendForOMReview: 8,
        RejectedByEHSHead: 9,
        SendForSiteHeadReview: 10,
        RejectedByOMHead: 11,
        Close: 12,
        RejectedBySiteHead: 13,
        CloseAndSendForReview: 14,
        EHSSendForInvestigation: 15,
        EHSClose: 16,
        EHSReject: 17,
        EHSDiscard: 18
    };

    vm.NearmissStatus =
    {
        _EnumName: "NearmissStatus",
        Draft: 1,
        Rejected: 2,
        Requested: 3,
        Sent_For_Review: 4,
        Rejected_By_EHS_Head: 5,
        Rejected_By_OM_Head: 6,
        Rejected_By_Site_Head: 7,
        Investigation_Sent_For_EHS_Head_Review: 8,
        Sent_For_OM_Review: 9,
        Sent_For_Site_Head_Review: 10,
        Closed: 11,
        Discared: 12,
        Investigation_Closed: 13,
        Close_Send_For_Review: 14
    };

    vm.IncidentAction =
    {
        _EnumName: "IncidentAction",
        RequestFunctionalHead: 1,
        Request: 2,
        SendForFurtherReview: 3,
        Discard: 4,
        SendForInvestigation: 5,
        CloseFIR: 6,
        InvestigationCompletedSentForEHSHeadReview: 7,
        InvestigationCompletedSentForOMHeadReview: 8,
        SendForOMReview: 9,
        RejectedByEHSHead: 10,
        SendForSiteHeadReview: 11,
        RejectedByOMHead: 12,
        Close: 13,
        RejectedBySiteHead: 14,
        RejectFIR: 15,
        CloseAndSendForReview: 16,
        EHSSendForInvestigation: 17,
        EHSClose: 18
    };

    vm.IncidentStatus = {
        _EnumName: "IncidentStatus",
        Draft: 1,
        FIR_Sent_For_Review: 2,
        FIR_Sent_For_Further_Review: 3,
        FIR_Sent_For_Investigation: 4,
        Rejected_By_EHS_Head: 5,
        Rejected_By_OM_Head: 6,
        Rejected_By_Site_Head: 7,
        Investigation_Sent_For_EHS_Head_Review: 8,
        Sent_For_OM_Review: 9,
        Sent_For_Site_Head_Review: 10,
        Closed: 11,
        Discared: 12,
        Investigation_Closed: 13,
        Rejected: 14,
        Close_Send_For_Review: 15
    };

    //Export Type
    vm.ExportType = {
        _EnumName: "ExportType",
        Excel: 1,
        Pdf: 2
    };

    //Incident Search Report Type
    vm.ReportType = {
        _EnumName: "ReportType",
        Notification: 1,
        Summary: 2,
        Detail: 3,
        DMAIC: 4
    };


    //Incident Search Report Type
    vm.CostType = {
        _EnumName: "CostType",
        IndirectCost: 1,
        DirectCost: 2,
    };

    //Incident CausesTypes
    vm.CausesTypes = {
        _EnumName: "CausesTypes",
        DirectCause: 1,
        RootCause: 2,
    };
    //Incident InvolvedType
    vm.InvolvedType = {
        _EnumName: "InvolvedType",
        Equipment: 1,
        Material: 2,
    };

    // Type
    vm.Type = {
        _EnumName: "Type",
        OPD: 1,
        Internal: 2,
        External: 3,
        Leave: 4
    };

    vm.MedicalIndex = {
        _EnumName: "MedicalIndex",
        Normal: 1,
        Moderate: 2,
        High: 3
    };

    // ****************************************** Audit Start ******************************************
    // Audit Compliance
    vm.AuditCompliance = {
        _EnumName: "AuditCompliance",
        Yes: 1,
        No: 2,
        NotApplicable: 3
        //Partially: 4
    };

    // Audit Risk Potential
    vm.AuditRiskPotential = {
        _EnumName: "AuditRiskPotential",
        Low: 1,
        Medium: 2,
        High: 3
    };

    // AuditStep
    vm.AuditStep = {
        _EnumName: "AuditStep",
        PlantDetail: 1,
        AuditorDetail: 2,
        ScopeObjective: 3,
        OpeningMeeting: 4,
        CheckList: 5,
        Observation: 6,
        NonCompliance: 7,
        ActionTobeTaken: 8,
        ClosingMeeting: 9,
        AuditSummary: 10,
        ExecutiveSummary: 11,
        SendForApproval: 12
    };

    // AuditEmployeeType 
    vm.AuditEmployeeType = {
        _EnumName: "AuditEmployeeType",
        Employee: 1,
        Other: 2
    };

    // Audit Meeting Employee Type
    vm.AuditMeetingEmployeeType = {
        _EnumName: "AuditMeetingEmployeeType",
        Employee: 1,
        Other: 2,
        Contractor: 3
    };

    // audit type 
    vm.AuditType = {
        _EnumName: "AuditType",
        Internal: 0,
        External: 1
    };

    //Audit Conduct Type
    vm.AuditConductType = {
        _EnumName: "AuditConductType",
        Planned: 1,
        UnPlanned: 2
    };

    // audit status
    vm.AuditStatus = {
        _EnumName: "AuditStatus",
        Planned: 1,
        InProgress: 2,
        Complete: 3,
        Accepted: 4,
        AcceptedWithComments: 5,
        AuditClosed: 6,
        Cancelled: 7,
        Postpone: 8
    };

    //Audit Finding Type
    vm.AuditFindingType = {
        _EnumName: "AuditFindingType",
        Observation: 1,
        MajorNonComplaince: 2,
        MinorNonComplaince: 3,
        OpportunityForImprovement: 4
    };

    vm.WFAuditAction = {
        _EnumName: "WFAuditAction",
        Schedule: 1,
        Discard: 2,
        PlanToInprogress: 3,
        Postpone: 4,
        PostponeToComplete: 5,
        PostponeToCancelAudit: 6,
        Inprogress: 7,
        InprogressToComplete: 8,
        CompleteToAccepted: 9,
        CompleteToAcceptedWithComments: 10,
        AuditClosed: 11
    };


    //Button Action Type
    vm.ButtonActionType = {
        _EnumName: "ButtonActionType",
        SaveAsDraft: 1,
        Submit: 2,
        Next: 3,
        Back: 4
    };

    // Audit Meeting Employee Type
    vm.AuditMeetingEmployeeType = {
        _EnumName: "AuditMeetingEmployeeType",
        Employee: 1,
        Other: 2,
        Contractor: 3
    };
    // Audit Risk Potential
    vm.AuditRiskPotential = {
        _EnumName: "AuditRiskPotential",
        Low: 1,
        Medium: 2,
        High: 3
    };
    // ****************************************** Audit End ******************************************

    return vm;
}