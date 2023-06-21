// -----------------------------------------------------------------------
// <copyright file="EmployeeManagementAreaRegistration.cs" company="ASK-EHS">
// TODO: All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Controllers.InventoryManagement
{
    using MVCProject.Api.Models;
    using MVCProject.Api.Utilities;
    using MVCProject.Api.ViewModel;
    using MVCProject.Common;
    using MVCProject.Common.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class InventoryController : BaseController
    {
        /// <summary>
        /// Holds context object. 
        /// </summary>
        private MVCProjectEntities entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalOPDController"/> class.
        /// </summary>
        public InventoryController()
        {
            this.entities = this.GetEntity();
        }

        #region Opening Stock

        /// <summary>
        /// Get Medicine Opening Stock Details
        /// </summary>
        /// <param name="objInventory_Filters"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse GetMedicineOpeningStock(Inventory_Filters objInventory_Filters)
        {
            if (!objInventory_Filters.OpeningStockEntryDate.HasValue)
            {
                objInventory_Filters.OpeningStockEntryDate = DateTime.Now;
            }
            //.Where(x => x.ReceivedDate.ToString("dd/MM/yyyy") == objInventory_Filters.OpeningStockEntryDate.Value.ToString("dd/MM/yyyy"))
            if (string.IsNullOrEmpty(objInventory_Filters.searchData))
            {
                var result = this.entities.MedicineReceived.AsEnumerable()
                        .Where(x => x.MedReceivedSourceId == 1 && x.SiteLevelId == objInventory_Filters.SiteLevelId)
                        .Select(a => new MedicineOpeningStockViewModel
                        {
                            MedicineReceivedId = a.MedicineReceivedId,
                            ReceiptNo = a.ReceiptNo,
                            ReceivedDate = a.ReceivedDate,
                            ReceivedFrom = a.ReceivedFrom,
                            MedReceivedSourceId = 1,
                            MedicineId = a.MedicineId,
                            MedicineCode = a.MedicineMaster.MedCode,
                            MedicineName = a.MedicineMaster.MedName,
                            MedicineType = a.MedicineMaster.MedicineType.MedType,
                            ExpiryDate = a.ExpiryDate,
                            ExpiryMonth = a.ExpiryDate.HasValue ? a.ExpiryDate.Value.Month.ToString() : "",
                            ExpiryYear = a.ExpiryDate.HasValue ? a.ExpiryDate.Value.Year.ToString() : "",
                            Price = a.Price.HasValue ? a.Price.Value : 0,
                            Quantity = a.Quantity,
                            IsActive = a.IsActive,
                            IsExpiryRequired = a.MedicineMaster.IsExpiryRequired,
                            IsDefaultExpiry = a.ExpiryDate.HasValue && a.ExpiryDate.Value.ToString("MMMM-yyyy") == Convert.ToDateTime("01-01-1900").ToString("MMMM-yyyy") ? true : false,
                            SiteLevelId = a.SiteLevelId.Value
                        }).ToList();
                if (result != null)
                {
                    return this.Response(MessageTypes.Success, string.Empty, result);
                }
                else
                {
                    return this.Response(MessageTypes.NotFound, string.Format(Resource.NotFound, Resource.Employee), result);
                }
            }
            else
            {
                var result = this.entities.MedicineReceived
                        .Where(x => x.MedReceivedSourceId == 1 && x.SiteLevelId == objInventory_Filters.SiteLevelId).AsEnumerable()
                        .Select(a => new MedicineOpeningStockViewModel
                        {
                            MedicineReceivedId = a.MedicineReceivedId,
                            ReceiptNo = a.ReceiptNo,
                            ReceivedDate = a.ReceivedDate,
                            ReceivedFrom = a.ReceivedFrom,
                            MedReceivedSourceId = 1,
                            MedicineId = a.MedicineId,
                            MedicineCode = a.MedicineMaster.MedCode,
                            MedicineName = a.MedicineMaster.MedName,
                            MedicineType = a.MedicineMaster.MedicineType.MedType,
                            ExpiryDate = a.ExpiryDate,
                            ExpiryMonth = a.ExpiryDate.HasValue ? Convert.ToString(a.ExpiryDate.Value.Month) : "",
                            ExpiryYear = a.ExpiryDate.HasValue ? Convert.ToString(a.ExpiryDate.Value.Year) : "",
                            Price = a.Price.HasValue ? a.Price.Value : 0,
                            Quantity = a.Quantity,
                            IsActive = a.IsActive,
                            IsExpiryRequired = a.MedicineMaster.IsExpiryRequired,
                            IsDefaultExpiry = a.ExpiryDate.HasValue && a.ExpiryDate.Value.ToString("MMMM-yyyy") == Convert.ToDateTime("01-01-1900").ToString("MMMM-yyyy") ? true : false,
                            SiteLevelId = a.SiteLevelId.Value
                        });

                var results = result.Where(x => x.MedicineName.Trim().ToLower().Contains(objInventory_Filters.searchData.Trim().ToLower()) || Convert.ToString(x.MedicineCode).Trim().Contains(objInventory_Filters.searchData.Trim())).ToList();
                if (results != null)
                {
                    return this.Response(MessageTypes.Success, string.Empty, results);
                }
                else
                {
                    return this.Response(MessageTypes.NotFound, string.Format(Resource.NotFound, Resource.Employee), results);
                }
            }

        }

        /// <summary>
        /// Save/Update Medicine Details 
        /// </summary>
        /// <param name="lstMedicineOpeningStockViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse SaveMedicineOpeningStock(List<MedicineOpeningStockViewModel> lstMedicineOpeningStockViewModel)
        {
            bool isUpdated = false;
            string ErrorMsg = string.Empty;
            entities.Connection.Open();
            using (DbTransaction dbTransaction = entities.Connection.BeginTransaction())
            {
                try
                {
                    if (lstMedicineOpeningStockViewModel.Any())
                    {
                        foreach (MedicineOpeningStockViewModel objMedicineOpeningStockViewModel in lstMedicineOpeningStockViewModel)
                        {
                            MedicineReceived tblMedicineReceived = entities.MedicineReceived.Where(a => a.MedicineReceivedId == objMedicineOpeningStockViewModel.MedicineReceivedId).Any() ? entities.MedicineReceived.Where(a => a.MedicineReceivedId == objMedicineOpeningStockViewModel.MedicineReceivedId).FirstOrDefault() : new MedicineReceived();

                            if (tblMedicineReceived.MedicineReceivedId > 0)
                            {
                                isUpdated = true;
                                tblMedicineReceived.ReceiptNo = null;
                                tblMedicineReceived.ReceivedDate = objMedicineOpeningStockViewModel.ReceivedDate;
                                tblMedicineReceived.ReceivedFrom = objMedicineOpeningStockViewModel.ReceivedFrom;
                                tblMedicineReceived.MedReceivedSourceId = 1;
                                tblMedicineReceived.MedicineId = objMedicineOpeningStockViewModel.MedicineId;
                                tblMedicineReceived.ExpiryDate = objMedicineOpeningStockViewModel.IsDefaultExpiry ? Convert.ToDateTime("01-01-1900") : objMedicineOpeningStockViewModel.ExpiryDate;
                                tblMedicineReceived.Price = objMedicineOpeningStockViewModel.Price;
                                tblMedicineReceived.Quantity = objMedicineOpeningStockViewModel.Quantity;
                                tblMedicineReceived.IsActive = objMedicineOpeningStockViewModel.IsActive;
                                tblMedicineReceived.SiteLevelId = objMedicineOpeningStockViewModel.SiteLevelId;
                                tblMedicineReceived.UpdateBy = UserContext.EmployeeId;
                                tblMedicineReceived.UpdateDate = DateTime.UtcNow;
                                entities.MedicineReceived.ApplyCurrentValues(tblMedicineReceived);
                            }
                            else
                            {
                                if (objMedicineOpeningStockViewModel.IsActive)
                                {
                                    tblMedicineReceived.ReceiptNo = null;
                                    tblMedicineReceived.ReceivedDate = objMedicineOpeningStockViewModel.ReceivedDate;
                                    tblMedicineReceived.ReceivedFrom = objMedicineOpeningStockViewModel.ReceivedFrom;
                                    tblMedicineReceived.MedReceivedSourceId = 1;
                                    tblMedicineReceived.MedicineId = objMedicineOpeningStockViewModel.MedicineId;
                                    tblMedicineReceived.ExpiryDate = objMedicineOpeningStockViewModel.IsDefaultExpiry ? Convert.ToDateTime("01-01-1900") : objMedicineOpeningStockViewModel.ExpiryDate;
                                    tblMedicineReceived.Price = objMedicineOpeningStockViewModel.Price;
                                    tblMedicineReceived.Quantity = objMedicineOpeningStockViewModel.Quantity;
                                    tblMedicineReceived.IsActive = objMedicineOpeningStockViewModel.IsActive;
                                    tblMedicineReceived.SiteLevelId = objMedicineOpeningStockViewModel.SiteLevelId;
                                    tblMedicineReceived.EntryBy = UserContext.EmployeeId;
                                    tblMedicineReceived.EntryDate = DateTime.UtcNow;
                                    entities.MedicineReceived.AddObject(tblMedicineReceived);
                                }
                            }
                        }

                        if (!(entities.SaveChanges() > 0))
                        {
                            ErrorMsg = Resource.MedicineOpeningStockSaveFailed;
                            goto Error;
                        }

                        dbTransaction.Commit();
                        entities.Connection.Close();
                        return this.Response(MessageTypes.Success, isUpdated ? Resource.MedicineOpeningStockUpdatedSuccessfully : Resource.MedicineOpeningStockSaveSuccessfully);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg = Resource.MedicineOpeningStockSaveFailed;
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    goto Error;
                }
            Error:
                dbTransaction.Rollback();
                entities.Connection.Close();
                return this.Response(MessageTypes.Error, ErrorMsg);
            }
        }

        #endregion

        #region Medicine Received

        /// <summary>
        /// Get Medicine Received Details
        /// </summary>
        /// <param name="objInventory_Filters"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse GetMedicineReceivedDetails(Inventory_Filters objInventory_Filters)
        {
            if (objInventory_Filters.GroupId.HasValue && objInventory_Filters.GroupId > 0 && this.entities.MedicineReceived.Where(a => a.MedReceivedSourceId == 2 && a.SiteLevelId == objInventory_Filters.SiteLevelId).Any())
            {
                var result = this.entities.MedicineReceived.AsEnumerable()
                    .Where(x => x.GroupId == objInventory_Filters.GroupId && x.MedReceivedSourceId == 2 && x.SiteLevelId == objInventory_Filters.SiteLevelId)
                            .Select(a => new MedicineOpeningStockViewModel
                            {
                                MedicineReceivedId = a.MedicineReceivedId,
                                ReceiptNo = a.ReceiptNo,
                                ReceivedDate = a.ReceivedDate,
                                ReceivedFrom = a.ReceivedFrom,
                                MedReceivedSourceId = 2,
                                MedicineId = a.MedicineId,
                                MedicineCode = a.MedicineMaster.MedCode,
                                MedicineName = a.MedicineMaster.MedName,
                                MedicineType = a.MedicineMaster.MedicineType.MedType,
                                ExpiryDate = a.ExpiryDate,
                                ExpiryMonth = a.ExpiryDate.HasValue ? a.ExpiryDate.Value.Month.ToString() : "",
                                Price = a.Price.HasValue ? a.Price.Value : 0,
                                Quantity = a.Quantity,
                                IsActive = a.IsActive,
                                GroupId = a.GroupId,
                                IsExpiryRequired = a.MedicineMaster.IsExpiryRequired,
                                IsDefaultExpiry = a.ExpiryDate.HasValue && a.ExpiryDate.Value.ToString("MMMM-yyyy") == Convert.ToDateTime("01-01-1900").ToString("MMMM-yyyy") ? true : false,
                                SiteLevelId = a.SiteLevelId.Value
                            }).ToList();
                if (result != null)
                {
                    return this.Response(MessageTypes.Success, string.Empty, result);
                }
                else
                {
                    return this.Response(MessageTypes.NotFound, string.Empty, result);
                }
            }
            else if (objInventory_Filters.ReceiptNo != string.Empty && objInventory_Filters.OpeningStockEntryDate.HasValue && objInventory_Filters.ReceivedFrom != string.Empty && this.entities.MedicineReceived.Where(a => a.MedReceivedSourceId == 2 && a.SiteLevelId == objInventory_Filters.SiteLevelId).Any())
            {
                var result = this.entities.MedicineReceived.Where(a => a.GroupId.HasValue).AsEnumerable()
               .Where(x => !string.IsNullOrEmpty(x.ReceiptNo) && x.ReceiptNo.Trim().ToLower() == objInventory_Filters.ReceiptNo.Trim().ToLower() && x.ReceivedDate.Date == objInventory_Filters.OpeningStockEntryDate.Value.Date && x.ReceivedFrom.Trim().ToLower() == objInventory_Filters.ReceivedFrom.Trim().ToLower() && x.MedReceivedSourceId == 2 && x.SiteLevelId == objInventory_Filters.SiteLevelId)
                       .Select(a => new MedicineOpeningStockViewModel
                       {
                           MedicineReceivedId = a.MedicineReceivedId,
                           ReceiptNo = a.ReceiptNo,
                           ReceivedDate = a.ReceivedDate,
                           ReceivedFrom = a.ReceivedFrom,
                           MedReceivedSourceId = 2,
                           MedicineId = a.MedicineId,
                           MedicineCode = a.MedicineMaster.MedCode,
                           MedicineName = a.MedicineMaster.MedName,
                           MedicineType = a.MedicineMaster.MedicineType.MedType,
                           ExpiryDate = a.ExpiryDate,
                           ExpiryMonth = a.ExpiryDate.HasValue ? a.ExpiryDate.Value.Month.ToString() : "",
                           Price = a.Price.HasValue ? a.Price.Value : 0,
                           Quantity = a.Quantity,
                           IsActive = a.IsActive,
                           GroupId = a.GroupId,
                           SiteLevelId = a.SiteLevelId.Value,
                           IsExpiryRequired = a.MedicineMaster.IsExpiryRequired,
                           IsDefaultExpiry = a.ExpiryDate.HasValue && a.ExpiryDate.Value.ToString("MMMM-yyyy") == Convert.ToDateTime("01-01-1900").ToString("MMMM-yyyy") ? true : false
                       }).ToList();
                if (result != null)
                {
                    return this.Response(MessageTypes.Success, string.Empty, result);
                }
                else
                {
                    return this.Response(MessageTypes.NotFound, string.Empty, result);
                }
            }
            else
            {
                return this.Response(MessageTypes.NotFound, string.Empty);
            }
        }

        /// <summary>
        /// Save/Update Medicine Details 
        /// </summary>
        /// <param name="lstMedicineOpeningStockViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse SaveMedicineReceivedDetails(List<MedicineOpeningStockViewModel> lstMedicineOpeningStockViewModel)
        {
            bool isUpdated = false;
            string ErrorMsg = string.Empty;
            entities.Connection.Open();
            using (DbTransaction dbTransaction = entities.Connection.BeginTransaction())
            {
                try
                {
                    if (lstMedicineOpeningStockViewModel.Any())
                    {
                        int? GroupdId = null;
                        if (lstMedicineOpeningStockViewModel.Where(a => a.GroupId.HasValue && a.GroupId > 0).Any())
                        {
                            GroupdId = lstMedicineOpeningStockViewModel.Where(a => a.GroupId.HasValue && a.GroupId > 0).Select(a => a.GroupId).FirstOrDefault();
                        }
                        else
                        {
                            GroupdId = this.entities.MedicineReceived.Select(a => a.GroupId).Max().HasValue ? this.entities.MedicineReceived.Select(a => a.GroupId).Max() + 1 : 1;
                        }

                        foreach (MedicineOpeningStockViewModel objMedicineOpeningStockViewModel in lstMedicineOpeningStockViewModel)
                        {
                            MedicineReceived tblMedicineReceived = entities.MedicineReceived.Where(a => a.MedicineReceivedId == objMedicineOpeningStockViewModel.MedicineReceivedId).Any() ? entities.MedicineReceived.Where(a => a.MedicineReceivedId == objMedicineOpeningStockViewModel.MedicineReceivedId).FirstOrDefault() : new MedicineReceived();

                            if (tblMedicineReceived.MedicineReceivedId > 0)
                            {
                                isUpdated = true;
                                tblMedicineReceived.ReceiptNo = objMedicineOpeningStockViewModel.ReceiptNo;
                                tblMedicineReceived.ReceivedDate = objMedicineOpeningStockViewModel.ReceivedDate;
                                tblMedicineReceived.ReceivedFrom = objMedicineOpeningStockViewModel.ReceivedFrom;
                                tblMedicineReceived.MedReceivedSourceId = 2;
                                tblMedicineReceived.MedicineId = objMedicineOpeningStockViewModel.MedicineId;
                                tblMedicineReceived.ExpiryDate = objMedicineOpeningStockViewModel.IsDefaultExpiry ? Convert.ToDateTime("01-01-1900") : objMedicineOpeningStockViewModel.ExpiryDate;
                                tblMedicineReceived.Price = objMedicineOpeningStockViewModel.Price;
                                tblMedicineReceived.Quantity = objMedicineOpeningStockViewModel.Quantity;
                                tblMedicineReceived.GroupId = objMedicineOpeningStockViewModel.GroupId;
                                tblMedicineReceived.IsActive = objMedicineOpeningStockViewModel.IsActive;
                                tblMedicineReceived.SiteLevelId = objMedicineOpeningStockViewModel.SiteLevelId;
                                tblMedicineReceived.UpdateBy = UserContext.EmployeeId;
                                tblMedicineReceived.UpdateDate = DateTime.UtcNow;
                                entities.MedicineReceived.ApplyCurrentValues(tblMedicineReceived);
                            }
                            else
                            {
                                if (objMedicineOpeningStockViewModel.IsActive)
                                {
                                    tblMedicineReceived.ReceiptNo = objMedicineOpeningStockViewModel.ReceiptNo;
                                    tblMedicineReceived.ReceivedDate = objMedicineOpeningStockViewModel.ReceivedDate;
                                    tblMedicineReceived.ReceivedFrom = objMedicineOpeningStockViewModel.ReceivedFrom;
                                    tblMedicineReceived.MedReceivedSourceId = 2;
                                    tblMedicineReceived.MedicineId = objMedicineOpeningStockViewModel.MedicineId;
                                    tblMedicineReceived.ExpiryDate = objMedicineOpeningStockViewModel.IsDefaultExpiry ? Convert.ToDateTime("01-01-1900") : objMedicineOpeningStockViewModel.ExpiryDate;
                                    tblMedicineReceived.Price = objMedicineOpeningStockViewModel.Price;
                                    tblMedicineReceived.Quantity = objMedicineOpeningStockViewModel.Quantity;
                                    tblMedicineReceived.GroupId = GroupdId;
                                    tblMedicineReceived.IsActive = objMedicineOpeningStockViewModel.IsActive;
                                    tblMedicineReceived.SiteLevelId = objMedicineOpeningStockViewModel.SiteLevelId;
                                    tblMedicineReceived.EntryBy = UserContext.EmployeeId;
                                    tblMedicineReceived.EntryDate = DateTime.UtcNow;
                                    entities.MedicineReceived.AddObject(tblMedicineReceived);
                                }
                            }
                        }

                        if (!(entities.SaveChanges() > 0))
                        {
                            ErrorMsg = Resource.MedicineOpeningStockSaveFailed;
                            goto Error;
                        }

                        dbTransaction.Commit();
                        entities.Connection.Close();
                        return this.Response(MessageTypes.Success, isUpdated ? Resource.MedicineReceivedUpdatedSuccessfully : Resource.MedicineReceivedSaveSuccessfully);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg = Resource.MedicineOpeningStockSaveFailed;
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    goto Error;
                }
            Error:
                dbTransaction.Rollback();
                entities.Connection.Close();
                return this.Response(MessageTypes.Error, ErrorMsg);
            }
        }

        #endregion

        #region Dead Medicine Stock

        /// <summary>
        /// Get Dead Medicine Stock Details
        /// </summary>
        /// <param name="objInventory_Filters"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse GetDeadMedicineStock(Inventory_Filters objInventory_Filters)
        {
            if (!objInventory_Filters.OpeningStockEntryDate.HasValue)
            {
                objInventory_Filters.OpeningStockEntryDate = DateTime.Now;
            }
            var result = this.entities.USP_GetMedicineDeadStockEditList(objInventory_Filters.OpeningStockEntryDate.Value.Date, objInventory_Filters.SiteLevelId)
                        .Select(a => new DeadMedicineStockViewModel
                        {
                            DeadStockId = a.DeadStockId,
                            DeadStockEntryDate = a.DeadStockEntryDate,
                            Description = a.Description,
                            MedicineId = a.MedicineId,
                            MedicineCode = a.MedCode,
                            MedicineName = a.MedName,
                            MedicineType = a.MedType,
                            ExpiryDate = a.ExpiryDate,
                            ExpiryMonth = a.ExpiryDate.Month.ToString(),
                            Quantity = a.Quantity,
                            MaxQuantity = a.ActualQuantity,
                            IsActive = true,
                            IsDefaultExpiry = a.ExpiryDate.ToString("MMMM-yyyy") == Convert.ToDateTime("01-01-1900").ToString("MMMM-yyyy") ? true : false,
                        }).ToList();
            if (result != null)
            {
                return this.Response(MessageTypes.Success, string.Empty, result);
            }
            else
            {
                return this.Response(MessageTypes.NotFound, string.Format(Resource.NotFound, Resource.Employee), result);
            }
        }

        /// <summary>
        /// Save/Update Medicine Details 
        /// </summary>
        /// <param name="lstMedicineDeadStockStockViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse SaveDeadMedicineStock(List<DeadMedicineStockViewModel> lstMedicineDeadStockStockViewModel)
        {
            bool isUpdated = false;
            string ErrorMsg = string.Empty;
            entities.Connection.Open();
            using (DbTransaction dbTransaction = entities.Connection.BeginTransaction())
            {
                try
                {
                    if (lstMedicineDeadStockStockViewModel.Any())
                    {
                        foreach (DeadMedicineStockViewModel objMedicineDeadStockStockViewModel in lstMedicineDeadStockStockViewModel)
                        {
                            MedicineDeadStock tblMedicineDeadStock = entities.MedicineDeadStock.Where(a => a.DeadStockId == objMedicineDeadStockStockViewModel.DeadStockId).Any() ? entities.MedicineDeadStock.Where(a => a.DeadStockId == objMedicineDeadStockStockViewModel.DeadStockId).FirstOrDefault() : new MedicineDeadStock();

                            if (tblMedicineDeadStock.DeadStockId > 0)
                            {
                                isUpdated = true;
                                tblMedicineDeadStock.DeadStockEntryDate = objMedicineDeadStockStockViewModel.DeadStockEntryDate;
                                tblMedicineDeadStock.Description = objMedicineDeadStockStockViewModel.Description;
                                tblMedicineDeadStock.MedicineId = objMedicineDeadStockStockViewModel.MedicineId;
                                tblMedicineDeadStock.ExpiryDate = objMedicineDeadStockStockViewModel.IsDefaultExpiry ? Convert.ToDateTime("01-01-1900") : objMedicineDeadStockStockViewModel.ExpiryDate;
                                tblMedicineDeadStock.Quantity = objMedicineDeadStockStockViewModel.Quantity;
                                tblMedicineDeadStock.IsActive = objMedicineDeadStockStockViewModel.IsActive;
                                tblMedicineDeadStock.SiteLevelId = objMedicineDeadStockStockViewModel.SiteLevelId;
                                tblMedicineDeadStock.UpdateBy = UserContext.EmployeeId;
                                tblMedicineDeadStock.UpdateDate = DateTime.UtcNow;
                                entities.MedicineDeadStock.ApplyCurrentValues(tblMedicineDeadStock);
                            }
                            else
                            {
                                if (objMedicineDeadStockStockViewModel.IsActive)
                                {
                                    tblMedicineDeadStock.DeadStockEntryDate = objMedicineDeadStockStockViewModel.DeadStockEntryDate;
                                    tblMedicineDeadStock.Description = objMedicineDeadStockStockViewModel.Description;
                                    tblMedicineDeadStock.MedicineId = objMedicineDeadStockStockViewModel.MedicineId;
                                    tblMedicineDeadStock.ExpiryDate = objMedicineDeadStockStockViewModel.IsDefaultExpiry ? Convert.ToDateTime("01-01-1900") : objMedicineDeadStockStockViewModel.ExpiryDate;
                                    tblMedicineDeadStock.Quantity = objMedicineDeadStockStockViewModel.Quantity;
                                    tblMedicineDeadStock.IsActive = objMedicineDeadStockStockViewModel.IsActive;
                                    tblMedicineDeadStock.SiteLevelId = objMedicineDeadStockStockViewModel.SiteLevelId;
                                    tblMedicineDeadStock.EntryBy = UserContext.EmployeeId;
                                    tblMedicineDeadStock.EntryDate = DateTime.UtcNow;
                                    entities.MedicineDeadStock.AddObject(tblMedicineDeadStock);
                                }
                            }
                        }

                        if (!(entities.SaveChanges() > 0))
                        {
                            ErrorMsg = Resource.MedicineOpeningStockSaveFailed;
                            goto Error;
                        }

                        dbTransaction.Commit();
                        entities.Connection.Close();
                        return this.Response(MessageTypes.Success, isUpdated ? Resource.MedicineDeadStockUpdatedSuccessfully : Resource.MedicineDeadStockSaveSuccessfully);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg = Resource.MedicineOpeningStockSaveFailed;
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    goto Error;
                }
            Error:
                dbTransaction.Rollback();
                entities.Connection.Close();
                return this.Response(MessageTypes.Error, ErrorMsg);
            }
        }

        #endregion

        #region Daily Consumption Stock

        /// <summary>
        /// Get Daily Consumption Stock Details
        /// </summary>
        /// <param name="objInventory_Filters"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse GetDailyConsumptionStock(Inventory_Filters objInventory_Filters)
        {
            if (!objInventory_Filters.OpeningStockEntryDate.HasValue)
            {
                objInventory_Filters.OpeningStockEntryDate = DateTime.Now;
            }
            var result = this.entities.USP_GetMedicineConsumptionEditList(objInventory_Filters.OpeningStockEntryDate.Value.Date, objInventory_Filters.SiteLevelId)
                        .Select(a => new MedicineConsuptionStockViewModel
                        {
                            MedConsumptionId = a.MedConsumptionId,
                            ConsumptionEntryDate = a.ConsumptionEntryDate,
                            MedicineId = a.MedicineId,
                            Description = a.Remarks,
                            MedicineCode = a.MedCode,
                            MedicineName = a.MedName,
                            MedicineType = a.MedType,
                            ExpiryDate = a.ExpiryDate,
                            ExpiryMonth = a.ExpiryDate.Month.ToString(),
                            Quantity = a.Quantity,
                            MaxQuantity = a.ActualQuantity,
                            IsActive = true,
                            IsDefaultExpiry = a.ExpiryDate.ToString("MMMM-yyyy") == Convert.ToDateTime("01-01-1900").ToString("MMMM-yyyy") ? true : false
                        }).ToList();
            if (result != null)
            {
                return this.Response(MessageTypes.Success, string.Empty, result);
            }
            else
            {
                return this.Response(MessageTypes.NotFound, string.Format(Resource.NotFound, Resource.Employee), result);
            }
        }


        /// <summary>
        /// Save/Update Medicine Details 
        /// </summary>
        /// <param name="lstMedicineDeadStockStockViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponse SaveDailyConsumptionStock(List<MedicineConsuptionStockViewModel> lstMedicineConsuptionStockViewModel)
        {
            bool isUpdated = false;
            string ErrorMsg = string.Empty;
            entities.Connection.Open();
            using (DbTransaction dbTransaction = entities.Connection.BeginTransaction())
            {
                try
                {
                    if (lstMedicineConsuptionStockViewModel.Any())
                    {
                        foreach (MedicineConsuptionStockViewModel objMedicineConsuptionStockViewModel in lstMedicineConsuptionStockViewModel)
                        {
                            MedicineConsumption tblMedicineConsumption = entities.MedicineConsumption.Where(a => a.MedConsumptionId == objMedicineConsuptionStockViewModel.MedConsumptionId).Any() ? entities.MedicineConsumption.Where(a => a.MedConsumptionId == objMedicineConsuptionStockViewModel.MedConsumptionId).FirstOrDefault() : new MedicineConsumption();

                            if (tblMedicineConsumption.MedConsumptionId > 0)
                            {
                                isUpdated = true;
                                tblMedicineConsumption.ConsumptionEntryDate = objMedicineConsuptionStockViewModel.ConsumptionEntryDate;
                                tblMedicineConsumption.ConsumptionById = (int)ConsumptionType.Inventory;
                                tblMedicineConsumption.Remarks = objMedicineConsuptionStockViewModel.Description;
                                tblMedicineConsumption.MedicineId = objMedicineConsuptionStockViewModel.MedicineId;
                                tblMedicineConsumption.ExpiryDate = objMedicineConsuptionStockViewModel.IsDefaultExpiry ? Convert.ToDateTime("01-01-1900") : objMedicineConsuptionStockViewModel.ExpiryDate;
                                tblMedicineConsumption.Quantity = objMedicineConsuptionStockViewModel.Quantity;
                                tblMedicineConsumption.IsActive = objMedicineConsuptionStockViewModel.IsActive;
                                tblMedicineConsumption.SiteLevelId = objMedicineConsuptionStockViewModel.SiteLevelId;
                                tblMedicineConsumption.UpdateBy = UserContext.EmployeeId;
                                tblMedicineConsumption.UpdateDate = DateTime.UtcNow;
                                entities.MedicineConsumption.ApplyCurrentValues(tblMedicineConsumption);
                            }
                            else
                            {
                                if (objMedicineConsuptionStockViewModel.IsActive)
                                {
                                    tblMedicineConsumption.ConsumptionEntryDate = objMedicineConsuptionStockViewModel.ConsumptionEntryDate;
                                    tblMedicineConsumption.ConsumptionById = (int)ConsumptionType.Inventory;
                                    tblMedicineConsumption.Remarks = objMedicineConsuptionStockViewModel.Description;
                                    tblMedicineConsumption.MedicineId = objMedicineConsuptionStockViewModel.MedicineId;
                                    tblMedicineConsumption.ExpiryDate = objMedicineConsuptionStockViewModel.IsDefaultExpiry ? Convert.ToDateTime("01-01-1900") : objMedicineConsuptionStockViewModel.ExpiryDate;
                                    tblMedicineConsumption.Quantity = objMedicineConsuptionStockViewModel.Quantity;
                                    tblMedicineConsumption.IsActive = objMedicineConsuptionStockViewModel.IsActive;
                                    tblMedicineConsumption.SiteLevelId = objMedicineConsuptionStockViewModel.SiteLevelId;
                                    tblMedicineConsumption.EntryBy = UserContext.EmployeeId;
                                    tblMedicineConsumption.EntryDate = DateTime.UtcNow;
                                    entities.MedicineConsumption.AddObject(tblMedicineConsumption);
                                }
                            }
                        }

                        if (!(entities.SaveChanges() > 0))
                        {
                            ErrorMsg = Resource.MedicineOpeningStockSaveFailed;
                            goto Error;
                        }

                        dbTransaction.Commit();
                        entities.Connection.Close();
                        return this.Response(MessageTypes.Success, isUpdated ? Resource.MedicineConsumptionUpdatedSuccessfully : Resource.MedicineConsumptionSaveSuccessfully);
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg = Resource.MedicineOpeningStockSaveFailed;
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    goto Error;
                }
            Error:
                dbTransaction.Rollback();
                entities.Connection.Close();
                return this.Response(MessageTypes.Error, ErrorMsg);
            }
        }

        #endregion

        #region Common Functions

        /// <summary>
        /// AutoComplete for Medicine
        /// </summary>
        /// <param name="medicineName"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse SearchForMedicineNameAutoComplete(string medicineName)
        {
            if (!string.IsNullOrEmpty(medicineName))
            {
                medicineName = medicineName.ToLower();
                var result = this.entities.MedicineMaster.AsEnumerable().Where(x => x.IsActive
                                && (x.MedName.ToLower().Contains(medicineName) || x.MedCode.ToString().ToLower().Contains(medicineName)))
                                .Select(x => new
                                {
                                    x.MedicineId,
                                    x.MedCode,
                                    x.MedName,
                                    x.MedicineType.MedType,
                                    Description = "Code: " + x.MedCode + " Type:" + x.MedicineType.MedType,
                                    x.IsExpiryRequired

                                }).ToList();

                return this.Response(MessageTypes.Success, string.Empty, result);
            }
            else
            {
                return this.Response(MessageTypes.Success, string.Empty, new List<object>());
            }
        }

        /// <summary>
        /// AutoComplete for Medicine
        /// </summary>
        /// <param name="medicineName"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse SearchForDeadMedicineNameAutoComplete(DateTime? consumptionDate, bool ChkExpired, int siteLevelId, string medicineName)
        {
            if (!string.IsNullOrEmpty(medicineName))
            {
                medicineName = medicineName.ToLower();
                if (consumptionDate.HasValue)
                {
                    consumptionDate = consumptionDate.Value.Date;
                }
                else
                {
                    consumptionDate = DateTime.Now.Date;
                }
                var result = this.entities.USP_GetCurrentMedStockDetails(siteLevelId, medicineName, consumptionDate, ChkExpired)
                                .Select(x => new
                                {
                                    x.MedicineId,
                                    x.MedCode,
                                    x.MedName,
                                    MedType = x.MedType,
                                    Description = "Code:" + x.MedCode + " Type:" + x.MedType + " Exp.:" + (x.ExpiryDate.HasValue ? x.ExpiryDate.Value.ToString("MMMM-yyyy") : ""),
                                    ExpiryDate = x.ExpiryDate,
                                    Quantity = x.Quantity,
                                    IsDefaultExpiry = x.ExpiryDate.HasValue && x.ExpiryDate.Value.ToString("MMMM-yyyy") == Convert.ToDateTime("01-01-1900").ToString("MMMM-yyyy") ? true : false
                                }).ToList();

                return this.Response(MessageTypes.Success, string.Empty, result);
            }
            else
            {
                return this.Response(MessageTypes.Success, string.Empty, new List<object>());
            }
        }

        /// <summary>
        /// AutoComplete for Medicine
        /// </summary>
        /// <param name="medicineName"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse SearchForMedicineReceivedFromAutoComplete(string receivedFrom)
        {
            if (!string.IsNullOrEmpty(receivedFrom))
            {
                receivedFrom = receivedFrom.ToLower();
                var result = this.entities.MedicineReceived.Where(a => a.ReceivedFrom.Contains(receivedFrom) && a.IsActive)
                                .Select(x => new
                                {
                                    x.ReceivedFrom,
                                }).Distinct().ToList();

                return this.Response(MessageTypes.Success, string.Empty, result);
            }
            else
            {
                return this.Response(MessageTypes.Success, string.Empty, new List<object>());
            }
        }

        /// <summary>
        /// Check Received Edit Availibility of Quantity.
        /// </summary>
        /// <param name="medicineName"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse GetMinimumQtyForReceivedStock(int GroupId, int MedicineId, string ExpiryDate)
        {
            int OpeningStockQty = this.entities.MedicineReceived.AsEnumerable().Where(a => a.MedicineId == MedicineId && a.MedReceivedSourceId == 1 && a.IsActive == true && a.ExpiryDate.Value.ToString("MMMM-yyyy") == ExpiryDate).Sum(a => a.Quantity);
            int ReceivedStockQty = this.entities.MedicineReceived.AsEnumerable().Where(a => a.MedicineId == MedicineId && a.MedReceivedSourceId == 2 && a.ExpiryDate.Value.ToString("MMMM-yyyy") == ExpiryDate && a.GroupId != GroupId && a.IsActive == true).Sum(a => a.Quantity);
            int DeadStockQty = this.entities.MedicineDeadStock.AsEnumerable().Where(a => a.MedicineId == MedicineId && a.ExpiryDate.ToString("MMMM-yyyy") == ExpiryDate && a.IsActive == true).Sum(a => a.Quantity);
            int ConsumptionStockQty = this.entities.MedicineConsumption.AsEnumerable().Where(a => a.MedicineId == MedicineId && a.ExpiryDate.ToString("MMMM-yyyy") == ExpiryDate && a.IsActive == true).Sum(a => a.Quantity);

            int MinQty = ((ConsumptionStockQty + DeadStockQty) - OpeningStockQty) >= 0 ? ((ConsumptionStockQty + DeadStockQty) - OpeningStockQty) : 0;

            return this.Response(MessageTypes.Success, string.Empty, (ConsumptionStockQty > 0 || DeadStockQty > 0) ? MinQty : -1);
        }

        /// <summary>
        /// Check Opening Stock Edit Availibility of Quantity.
        /// </summary>
        /// <param name="MedicineId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponse GetMinimumQtyForOpeningStock(int MedicineId, string ExpiryDate)
        {
            int OpeningStockQty = this.entities.MedicineReceived.AsEnumerable().Where(a => a.MedicineId == MedicineId && a.MedReceivedSourceId == 1 && a.ExpiryDate.Value.ToString("MMMM-yyyy") == ExpiryDate && a.IsActive == true).Sum(a => a.Quantity);
            int ReceivedStockQty = this.entities.MedicineReceived.AsEnumerable().Where(a => a.MedicineId == MedicineId && a.MedReceivedSourceId == 2 && a.ExpiryDate.Value.ToString("MMMM-yyyy") == ExpiryDate && a.IsActive == true).Sum(a => a.Quantity);
            int DeadStockQty = this.entities.MedicineDeadStock.AsEnumerable().Where(a => a.MedicineId == MedicineId && a.ExpiryDate.ToString("MMMM-yyyy") == ExpiryDate && a.IsActive == true).Sum(a => a.Quantity);
            int ConsumptionStockQty = this.entities.MedicineConsumption.AsEnumerable().Where(a => a.MedicineId == MedicineId && a.ExpiryDate.ToString("MMMM-yyyy") == ExpiryDate && a.IsActive == true).Sum(a => a.Quantity);

            int MinQty = ((ConsumptionStockQty + DeadStockQty) - ReceivedStockQty) >= 0 ? ((ConsumptionStockQty + DeadStockQty) - ReceivedStockQty) : 0;

            return this.Response(MessageTypes.Success, string.Empty, OpeningStockQty > 0 ? MinQty : -1);
        }

        #endregion
    }
}
