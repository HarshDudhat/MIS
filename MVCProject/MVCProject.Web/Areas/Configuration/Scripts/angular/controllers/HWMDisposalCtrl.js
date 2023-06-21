(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("HWMDisposalCtrl", [
        "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "HWMDisposalService", "CommonService", HWMDisposalCtrl
    ]);

    function HWMDisposalCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, HWMDisposalService, CommonService) {

        $scope.maxChar = 8000;
        $scope.selectedTab = 1;

        $scope.Obj = {};
        $scope.options = {
            showAddHWMDisposal: false
        };
        $scope.MaxDate = new Date();
        $scope.emailPattern = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;

        $scope.currentDate = new Date();
        $scope.startDate = new Date();
        $scope.ActionItems = [];
        $scope.AttachmentsList = [];
        $scope.DocumentsList = [];
        $scope.isAllFileUploaded = false;

        $scope.Obj.WasteDisposal = {

            WasteDisposalId: 0
            , WasteTypeId: 1
            , RequestedDate: new Date()
            , FunctionalWasteTypeId: 4
            , FunctionalWasteTypeName: 'Hazardous Waste Disposal'
            , EmployeeName: userContext.EmployeeName
        };
        $scope.medicineOpeningStock = [];

        $scope.IsActive = true;
        $scope.sites = [];
        $scope.WasteCategories = [];
        $scope.VehicleTypeList = [];
        $scope.WasteStorages = [];
        $scope.WasteItems = [];

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.GetFunctionAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Function, 0, 0);
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        var GetWasteTypeDDL = function () {
            HWMDisposalService.GetWasteTypeDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteCategories = data.Result;
                }
            });
        };

        var GetVehicleTypeDDL = function () {
            HWMDisposalService.GetVehicleTypeDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.VehicleTypeList = data.Result;
                }
            });
        };

        var GetWasteStorageDDL = function () {
            HWMDisposalService.GetWasteStorageDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteStorages = data.Result;
                }
            });
        };

        var GetWasteCategotyDetails = function (category, actionToAdd) {
            HWMDisposalService.GetWasteCategotyDetails(category).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    //actionToAdd.UOM = data.Result.UnitId;
                    actionToAdd.UOM = data.Result.UnitShortCode;
                    actionToAdd.PhysicalState = data.Result.WasteStateId;
                    actionToAdd.PhysicalStateName = data.Result.WasteStateName;
                }
            });
        };

        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                console.log(level);
                $scope.Obj.WasteDisposal.SiteLeveleId = level.LevelId;
                $scope.Obj.WasteDisposal.SiteName = level.Name;
            }
        };

        $scope.fnWasteTypeChange = function (actionToAdd) {
            var selectedIndex = $scope.WasteCategories.map(function (obj) { return obj.Id; }).indexOf(parseInt(actionToAdd.WasteCategoryId));
            actionToAdd.WasteCategoryName = $scope.WasteCategories[selectedIndex].Name;
            var category = actionToAdd.WasteCategoryId;
            actionToAdd.UOM = '';
            if (category) {
                GetWasteCategotyDetails(category, actionToAdd);
            }
        };

        $scope.GetWasteDisposalDetails = function (WasteDisposalId) {
            if (WasteDisposalId > 0) {
                HWMDisposalService.GetWasteDisposalDetails(WasteDisposalId).then(function (response) {
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

            HWMDisposalService.SaveAttachments(param).then(function (response) {
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

            HWMDisposalService.SaveWasteDisposal(param).then(function (response) {
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

        $scope.switchTab = function (selectedTab) {
            $scope.selectedTab = selectedTab;
        };
        //#region Grid Functions    

        // Add New Medicine Details
        $scope.AddHWMDisposal = function (frmHWMDisposalAdd, medicineOpeningStock, actionToAdd, options) {

            $scope.IsSave = true;
            $scope.NoFound = false;
            console.log(actionToAdd);

            if (frmHWMDisposalAdd.$valid) {

                if (medicineOpeningStock == null)
                    medicineOpeningStock = [];

                medicineOpeningStock.push(actionToAdd);
                console.log(medicineOpeningStock);

                options.showAddHWMDisposal = !options.showAddHWMDisposal;
            }
            else {
                $scope.IsSave = false;
                return;
            }
        };

        //Medicine Name Changed in Autocomplete For Edit Mode.
        $scope.ChangedMedicineNameEdit = function (selected, actionToEdit, id) {

            if (angular.isDefined(selected) && selected.originalObject.MedicineId > 0) {

                actionToEdit.WasteCategoryId = selected.originalObject.WasteCategoryId;
                actionToEdit.Item = selected.originalObject.Item;
                actionToEdit.Quantity = selected.originalObject.Quantity;
                actionToEdit.UOM = selected.originalObject.UOM;
            }
            else {
                actionToEdit.WasteCategoryId = 0;
                actionToEdit.Item = '';
                actionToEdit.Quantity = '';
                actionToEdit.UOM = '';
                $scope.$broadcast('angucomplete-alt:clearInput');
            }
        };

        // Edit Medicine Details
        $scope.EditHWMDisposal = function (item, actionToEdit, showHideAction) {

            console.log(item);
            showHideAction.isEdit = !showHideAction.isEdit;
            angular.copy(item, actionToEdit);
        };

        $scope.UpdateHWMDisposal = function (frmMedicineDetailsEdit, medicineOpeningStock, item, actionToEdit, showHideAction) {

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

        // Init
        var Init = function () {

            GetWasteTypeDDL();
            GetWasteStorageDDL();
            GetVehicleTypeDDL();
            //allowNumbersOnly();
        };

        Init();
    }

})();