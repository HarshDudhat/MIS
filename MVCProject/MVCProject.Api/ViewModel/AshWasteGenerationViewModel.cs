// -----------------------------------------------------------------------
// <copyright file="PagingParams.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Api.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Paging parameters 
    /// </summary>
    public class AshWasteGenerationViewModel
    {
        public AshWasteGenerationDTO AshWasteGeneration { get; set; }
        public AshDisposalReport AshDisposalReport { get; set; }
        public List<AshDisposalReport> ListAshDisposalReport { get; set; }
        public string Type { get; set; }
    }

    public class AshWasteGenerationDTO
    {
        public int MuncipalWasteGenerationId { get; set; }
        public int SiteLeveleId { get; set; }
        public Nullable<System.DateTime> RequestedDate { get; set; }
        public int RequestedMonth { get; set; }
        public Nullable<System.DateTime> RequestedYear { get; set; }
        public  int CoalConsumed { get; set; }
        public  int Ashcontentcoalper { get; set; }
        public  int TotalAshGeneration { get; set; }
        public  int FlyAshGeneration { get; set; }
        public  int BottomAshGeneration { get; set; }
        public  int FlyAshUtilized { get; set; }
        public  int BottomAshUtilized { get; set; }
        public  int FlyAshUtilizationper { get; set; }
        public  int TotalAshUtilizationper { get; set; }
        public  int CementMfdUtilization { get; set; }
        public  int ClayMfdUtilization { get; set; }
        public  int RoadDevUtilization { get; set; }
        public  int Quaryyfilling { get; set; }
        public  int AshpondDumping { get; set; }
        public  int PonfAshLifting { get; set; }
        public  int InstalledCapacity { get; set; }
        public  int selectedyear { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime EntryDate { get; set; }
        public int EntryBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
    }

    public class AshDisposalReport
    {
        public int ID { get; set; }
        public Nullable<decimal> CoalConsumed { get; set; }
        public Nullable<decimal> Ashcontentcoalper { get; set; }
        public Nullable<decimal> TotalAshGeneration { get; set; }
        public Nullable<decimal> FlyAshGeneration { get; set; }
        public Nullable<decimal> BottomAshGeneration { get; set; }
        public Nullable<decimal> FlyAshUtilized { get; set; }
        public Nullable<decimal> BottomAshUtilized { get; set; }
        public Nullable<decimal> FlyAshUtilizationper { get; set; }
        public Nullable<decimal> TotalAshUtilizationper { get; set; }
        public Nullable<decimal> CementMfdUtilization { get; set; }
        public Nullable<decimal> ClayMfdUtilization { get; set; }
        public Nullable<decimal> RoadDevUtilization { get; set; }
        public Nullable<decimal> Quaryyfilling { get; set; }
        public Nullable<decimal> AshpondDumping { get; set; }
        public Nullable<decimal> PonfAshLifting { get; set; }
        public string Month_Name { get; set; }
    }
}