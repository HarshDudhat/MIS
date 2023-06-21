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
    public class WasteDisposalViewModel
    {
        public WasteDisposalDTO WasteDisposal { get; set; }
        public WasteDisposalDetailDTO WasteDisposalDetail { get; set; }
        public List<WasteDisposalDetailDTO> WasteDisposalDetailList { get; set; }

        //public string Type { get; set; }
    }

    public class WasteDisposalDTO
    {
        public int WasteDisposalId { get; set; }
        public int SiteLeveleId { get; set; }
        public int WasteTypeId { get; set; }
        public DateTime RequestedDate { get; set; }
        public string SenderAuthno { get; set; }
        public string Mainfestdocno { get; set; }
        public string TranspoterName { get; set; }
        public string ConatctNo { get; set; }
        public string Emailaddress { get; set; }
        public string Transporteraddress { get; set; }
        public string TranspoterRegNo { get; set; }
        public string VehicleType { get; set; }
        public string TranspoterVehicleNo { get; set; }
        public string RecieverName { get; set; }
        public string RecieverConatctNo { get; set; }
        public string RecieverEmailaddress { get; set; }
        public string Recieveraddress { get; set; }
        public string RecieverAuthorisation { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime EntryDate { get; set; }
        public int EntryBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public int StatusId { get; set; }
        public string TranspoterFax { get; set; }
        public string TranspoterContactPerson { get; set; }
        public string TranspoterContactPersonPhone { get; set; }
        public string RecieverFax { get; set; }
        public string RecieverContactPerson { get; set; }
        public string RecieverContactPersonPhone { get; set; }
        public string EmergencyContactPerson { get; set; }
        public string EmergencyContactPersonPhone { get; set; }
        public string DocumentNo { get; set; }
        public int FunctionalWasteTypeId { get; set; }
        public string FunctionalWasteTypeName { get; set; }
        public string Address { get; set; }
        public string MailingAddress { get; set; }
        public string TranspoterConatctNo { get; set; }
        public string TranspoterEmail { get; set; }
    }

    public class WasteDisposalDetailDTO
    {
        public int WasteDisposalDetailId { get; set; }
        public int WasteDisposalId { get; set; }
        public int WasteCategoryId { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> NoofContainer { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public int WasteTypeId { get; set; }
        public string Characteristics { get; set; }
        public string IncompatibleWasteSubstances { get; set; }
        public int PhysicalState { get; set; }
        public string UOM { get; set; }

        public string WasteDescription { get; set; }
        public string Item { get; set; }
        public int? UnitWeight { get; set; }

    }
}