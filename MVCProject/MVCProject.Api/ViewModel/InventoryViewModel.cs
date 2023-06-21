// -----------------------------------------------------------------------
// <copyright file="EmployeeManagementAreaRegistration.cs" company="ASK-EHS">
// TODO: All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// 
    /// </summary>
    public class Inventory_Filters
    {
        public int? GroupId { get; set; }
        public string ReceiptNo { get; set; }
        public string ReceivedFrom { get; set; }
        public DateTime? OpeningStockEntryDate { get; set; }
        public string Description { get; set; }
        public string searchData { get; set; }
        public int SiteLevelId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MedicineOpeningStockViewModel
    {
        public DateTime? OpeningStockEntryDate { get; set; }

        public int MedicineReceivedId { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string ReceivedFrom { get; set; }
        public int MedReceivedSourceId { get; set; }
        public int MedicineId { get; set; }
        public int MedicineCode { get; set; }
        public string MedicineName { get; set; }
        public string MedicineType { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public int? GroupId { get; set; }
        public bool IsExpiryRequired { get; set; }
        public bool IsDefaultExpiry { get; set; }
        public int SiteLevelId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeadMedicineStockViewModel
    {
        public DateTime? OpeningStockEntryDate { get; set; }

        public string Description { get; set; }

        public int DeadStockId { get; set; }
        public DateTime DeadStockEntryDate { get; set; }
        public int MedicineId { get; set; }
        public int MedicineCode { get; set; }
        public string MedicineName { get; set; }
        public string MedicineType { get; set; }
        public string ExpiryMonth { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefaultExpiry { get; set; }
        public int SiteLevelId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MedicineConsuptionStockViewModel
    {
        public DateTime? OpeningStockEntryDate { get; set; }

        public string Description { get; set; }

        public int MedConsumptionId { get; set; }
        public DateTime ConsumptionEntryDate { get; set; }
        public int MedicineId { get; set; }
        public int MedicineCode { get; set; }
        public string MedicineName { get; set; }
        public string MedicineType { get; set; }
        public string ExpiryMonth { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefaultExpiry { get; set; }
        public int SiteLevelId { get; set; }
    }
}