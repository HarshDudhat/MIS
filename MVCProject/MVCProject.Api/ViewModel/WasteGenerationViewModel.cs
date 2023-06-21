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
    public class WasteGenerationViewModel
    {
        public WasteGenerationDTO WasteGeneration { get; set; }
        public WasteGenerationDetailDTO WasteGenerationDetail { get; set; }
        public List<WasteGenerationDetailDTO> WasteGenerationDetailList { get; set; }

    }

    public class WasteGenerationDTO
    {
        public int WasteGenerationId { get; set; }
        public Nullable<int> SiteLeveleId { get; set; }
        public Nullable<int> FunctionLevelId { get; set; }
        public int WasteTypeId { get; set; }
        public Nullable<System.DateTime> RequestedDate { get; set; }
        public Nullable<int> MethodofStorage { get; set; }
        public Nullable<System.DateTime> MonitoringDate { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime EntryDate { get; set; }
        public int EntryBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string Characteristics { get; set; }
        public string IncompatibleWasteSubstances { get; set; }
        public int FunctionalWasteTypeId { get; set; }
        public string FunctionalWasteTypeName { get; set; }
        public string WastePhysicalState { get; set; }
        public string Address { get; set; }
        public Nullable<DateTime> EPRAuthorizationIssueDate { get; set; }
        public int? EPRAuthorizationValidityYear { get; set; }
    }
    public class WasteGenerationDetailDTO
    {
        public int WasteGenerationDetailId { get; set; }
        public Nullable<int> WasteGenerationId { get; set; }
        public Nullable<int> WasteCategoryId { get; set; }
        public Nullable<int> MethodofStorage { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> RecQuantity { get; set; }
        public string UOM { get; set; }
        public string Item { get; set; }
    }
}