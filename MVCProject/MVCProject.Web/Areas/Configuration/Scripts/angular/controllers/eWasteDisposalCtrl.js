(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("eWasteDisposalCtrl", [
        "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "eWasteDisposalService", "CommonService", eWasteDisposalCtrl
    ]);

    function eWasteDisposalCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, eWasteDisposalService, CommonService) {

        $scope.Obj = {};
        $scope.options = {
            showAddHWMDisposal: false
        };
        $scope.MaxDate = new Date();

        $scope.emailPattern = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
        $scope.selectedTab = 1;

        $scope.currentDate = new Date();
        $scope.startDate = new Date();
        $scope.ActionItems = [];
        $scope.AttachmentsList = [];
        $scope.DocumentsList = [];
        $scope.isAllFileUploaded = false;

        $scope.IsDisableOnEdit = false;
        $scope.IsEdit = false;

        $scope.Obj.WasteDisposal = {
            WasteDisposalId: 0
            , WasteTypeId: 3
            , RequestedDate: new Date()
            , FunctionalWasteTypeId: 6
            , FunctionalWasteTypeName: 'e-Waste Disposal'
            , EmployeeName: userContext.EmployeeName
        };
        $scope.medicineOpeningStock = [];

        $scope.Obj.WasteDisposalDetail = {
            WasteDisposalDetailId: 0
            , WasteDisposalId: 0
            , WasteCategoryId: 0
            , MethodofStorage: ''
            , Quantity: 0
            , RecQuantity: 0
        };

        $scope.IsActive = true;
        $scope.sites = [];
        $scope.WasteCategories = [];
        $scope.WasteItems = [];
        $scope.WasteStorages = [];
        $scope.WasteWeigthUnits = [];

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.GetFunctionAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Function, 0, 0);
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        $scope.switchTab = function (selectedTab) {
            $scope.selectedTab = selectedTab;
        };

        var GetWasteTypeDDL = function () {
            eWasteDisposalService.GetWasteTypeDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteCategories = data.Result;
                }
            });
        };

        var GetWasteItemsDDL = function () {
            eWasteDisposalService.GetWasteItemsDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteItems = data.Result;
                }
            });
        };

        var GetWasteWeigthUnitsDDL = function () {
            eWasteDisposalService.GetWasteWeigthUnitsDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteWeigthUnits = data.Result;
                }
            });
        };

        var GetWasteStorageDDL = function () {
            eWasteDisposalService.GetWasteStorageDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteStorages = data.Result;
                }
            });
        };

        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.Obj.WasteDisposal.SiteLeveleId = level.LevelId;
                $scope.Obj.WasteDisposal.SiteName = level.Name;
            }
        };

        $scope.fnWasteTypeChangeSetName = function (actionToAdd) {
            

            var selectedIndex = $scope.WasteItems.map(function (obj) { return obj.Id; }).indexOf(parseInt(actionToAdd.WasteCategoryId));
            actionToAdd.WasteCategoryName = $scope.WasteCategories[selectedIndex].Name;
            if (actionToAdd.WasteCategoryId) {
                GetWasteCategotyDetails(actionToAdd.WasteCategoryId, actionToAdd);
            }
        };

        var GetWasteCategotyDetails = function (category, actionToAdd) {
            eWasteDisposalService.GetWasteCategotyDetails(category).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    //actionToAdd.UOM = data.Result.UnitId;
                    actionToAdd.UOM = data.Result.UnitShortCode;
                    actionToAdd.PhysicalState = data.Result.WasteStateId;
                    actionToAdd.PhysicalStateName = data.Result.WasteStateName;
                }
            });
        };

        var GetVehicleTypeDDL = function () {
            eWasteDisposalService.GetVehicleTypeDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.VehicleTypeList = data.Result;
                }
            });
        };


        $scope.fnItemChange = function (actionToAdd) {
            //var selectedIndex = $scope.WasteItems.map(function (obj) { return obj.Id; }).indexOf(parseInt(actionToAdd.Item));
            //actionToAdd.ItemName = $scope.WasteItems[selectedIndex].Name;
        };

        $scope.fnUnitWeigthChange = function (actionToAdd) {
            var selectedIndex = $scope.WasteWeigthUnits.map(function (obj) { return obj.Id; }).indexOf(parseInt(actionToAdd.UnitWeight));
            actionToAdd.WeightUnitName = $scope.WasteWeigthUnits[selectedIndex].Name;
        };

        $scope.GetWasteDisposalDetails = function (WasteDisposalId) {
            if (WasteDisposalId > 0) {
                eWasteDisposalService.GetWasteDisposalDetails(WasteDisposalId).then(function (response) {
                    var data = response.data;

                    if (data.MessageType == messageTypes.Success) {
                        $scope.IsDisableOnEdit = true;
                        $scope.IsEdit = true;

                        $scope.Obj.WasteDisposal = data.Result.wasteDisposal;

                        $scope.medicineOpeningStock = [];
                        $scope.medicineOpeningStock = data.Result.wasteDisposalDetailList;
                    }
                    else {
                        toastr.error(data.Message);
                    }
                });
            }
        };

        $scope.imageUpload = function () {

            var param = {};
            param.AttachmentsList = angular.copy($scope.AttachmentsList);   
            param.AttachmentsList = angular.copy($scope.AttachmentsList);   
            param.ReferenceId = angular.copy($scope.Obj.WasteDisposal.WasteDisposalId);   
            param.ModuleId = angular.copy($scope.Obj.WasteDisposal.WasteTypeId);
            

            eWasteDisposalService.SaveAttachments(param).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    toastr.success(data.Message, successTitle);

                    var origin = window.location.origin;   // Returns base URL (https://example.com)
                    setTimeout(function () {
                        location.href = origin + "/Configuration/HWM/WasteActionCenter";
                    }, 3000);
                }
                else {
                    toastr.error(data.Message);
                }
            });

        };


        $scope.SaveWasteDisposal = function (frmWasteDisposal, WasteDisposalObj) {

            //if (!$rootScope.permission.CanWrite) {
            //    return;
            //}

            //if (frmWasteDisposal.$valid) {

            var param = {};
            param.WasteDisposal = angular.copy(WasteDisposalObj.WasteDisposal);            
            param.WasteDisposalDetailList = angular.copy($scope.medicineOpeningStock);
            
            eWasteDisposalService.SaveWasteDisposal(param).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    toastr.success(data.Message, successTitle);

                    var origin = window.location.origin;   // Returns base URL (https://example.com)
                    setTimeout(function () {
                        location.href = origin + "/Configuration/HWM/WasteActionCenter";
                    }, 3000);
                }
                else {
                    toastr.error(data.Message);
                }
            });
            //  }
            //else {
            //        toastr.error("Invalid data.");
            //    }
        };


        //#region Grid Functions  

        $scope.EditMedicineDetails = function (item, actionToEdit, showHideAction) {
            showHideAction.isEdit = !showHideAction.isEdit;
            
            angular.copy(item, actionToEdit);
        };

        // Add New Medicine Details
        $scope.AddMedicineDetails = function (frmMedicineDetailsAdd, medicineOpeningStock, actionToAdd, options) {

            $scope.IsSave = true;
            $scope.NoFound = false;            

            if (frmMedicineDetailsAdd.$valid) {

                if (medicineOpeningStock == null)
                    medicineOpeningStock = [];

                medicineOpeningStock.push(actionToAdd);
                options.showAddHWMDisposal = !options.showAddHWMDisposal;
            }
            else {
                $scope.IsSave = false;
                return;
            }
        };


        $scope.UpdateMedicineDetails = function (frmMedicineDetailsEdit, medicineOpeningStock, item, actionToEdit, showHideAction) {

            var index = medicineOpeningStock.indexOf(item);
            $scope.medicineOpeningStockForDuplicateCheck = angular.copy(medicineOpeningStock);
            $scope.medicineOpeningStockForDuplicateCheck.splice(index, 1);

            if (frmMedicineDetailsEdit.$valid) {
                if (actionToEdit.MedicineReceivedId > 0) {
                    angular.copy(actionToEdit, item);
                    showHideAction.isEdit = !showHideAction.isEdit;
                }
                else {
                    angular.copy(actionToEdit, item);
                    showHideAction.isEdit = !showHideAction.isEdit;
                }
            }

        };

        // Delete Medicine Details
        $scope.DeleteHWMDisposal = function (medicineOpeningStock, item) {
            $ngBootbox.confirm(DeleteConfirmMsg)
                .then(function () {
                    var index = medicineOpeningStock.indexOf(item);
                    medicineOpeningStock.splice(index, 1);
                },
                    function () {
                    });
        };

        //#endregion

        var LoadDropdowns = function () {
            GetWasteTypeDDL();
            GetWasteItemsDDL();
            GetWasteStorageDDL();
            GetWasteWeigthUnitsDDL();
            GetVehicleTypeDDL();
        };

        // Init
        var Init = function () {
            LoadDropdowns();
        };

        Init();
    }
})();