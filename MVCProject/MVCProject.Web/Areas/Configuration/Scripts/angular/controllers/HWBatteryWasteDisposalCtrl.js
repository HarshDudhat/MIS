(function () {
    'use strict';

    angular.module("DorfKetalMVCApp").controller('HWBatteryWasteDisposalCtrl', [
        '$scope', '$q', '$rootScope', '$ngBootbox', '$filter', 'CommonFunctions', 'CommonEnums', 'HWBatteryWasteDisposalService', 'CommonService', HWBatteryWasteDisposalCtrl
    ]);

    function HWBatteryWasteDisposalCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, HWBatteryWasteDisposalService, CommonService) {

        //#region Varriables

        $scope.Obj = {};
        $scope.options = {
            showAddHWMDisposal: false
        };
        $scope.emailPattern = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;

        $scope.Obj.WasteDisposal = {
            WasteDisposalId: 0
            , WasteTypeId: 2
            , RequestedDate: new Date()
            , FunctionalWasteTypeId: 5
            , FunctionalWasteTypeName: 'Battery Waste Disposal'
            , EmployeeName: userContext.EmployeeName
        };

        $scope.medicineOpeningStock = [];

        $scope.options = {
            showAddBatteryWasteDisposal: false
        };

        $scope.medicineOpeningStock = [];

        var GetWasteTypeDDL = function () {
            HWBatteryWasteDisposalService.GetWasteTypeDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteCategories = data.Result;
                }
            });
        };

        //True if records for particuler date is already exist in Database
        $scope.IsEdit = false;
        $scope.MaxDate = new Date();

        //#endregion

        //Get sites
        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.Obj.WasteDisposal.SiteLeveleId = level.LevelId;
                $scope.Obj.WasteDisposal.SiteName = level.Name;
            }
            else {
                $scope.Obj.WasteDisposal.SiteLeveleId = "";
                $scope.Obj.WasteDisposal.SiteName = "";
            }
        };

        var GetWasteCategotyDetails = function (category, actionToAdd) {
            HWBatteryWasteDisposalService.GetWasteCategotyDetails(category).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    //actionToAdd.UOMName = data.Result.UnitId;
                    actionToAdd.UOM = data.Result.UnitShortCode;
                    actionToAdd.PhysicalState = data.Result.WasteStateId;
                    actionToAdd.PhysicalStateName = data.Result.WasteStateName;
                }
            });
        };

        $scope.fnWasteTypeChange = function (actionToAdd) {

            var selectedIndex = $scope.WasteCategories.map(function (obj) { return obj.Id; }).indexOf(parseInt(actionToAdd.WasteCategoryId));
            actionToAdd.WasteCategoryName = $scope.WasteCategories[selectedIndex].Name;


            actionToAdd.WasteCategoryId = actionToAdd.WasteCategoryId;

            var category = actionToAdd.WasteCategoryId;
            actionToAdd.UOM = '';
            if (category) {
                GetWasteCategotyDetails(category, actionToAdd);
            }
        };

        //#region Grid Functions

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

        $scope.GetWasteDisposalDetails = function (WasteDisposalId) {
            if (WasteDisposalId > 0) {
                HWBatteryWasteDisposalService.GetWasteDisposalDetails(WasteDisposalId).then(function (response) {
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

        $scope.EditMedicineDetails = function (item, actionToEdit, showHideAction) {
            showHideAction.isEdit = !showHideAction.isEdit;
            angular.copy(item, actionToEdit);
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

        $scope.SaveBatteryDisposal = function (frmWasteBattery, WasteDisposalObj) {

            console.log($rootScope.permission.CanWrite);
            if (!$rootScope.permission.CanWrite) {
                return;
            }

            console.log(frmWasteBattery.$valid);
            if (frmWasteBattery.$valid) {

                console.log($scope.IsEdit);

                if ($scope.IsEdit) {
                    var paramArr = [];
                    paramArr = angular.copy($scope.medicineOpeningStock);

                    console.log("Edit");
                    console.log(paramArr);

                    HWBatteryWasteDisposalService.UpdateWasteDisposalDetails(paramArr).then(function (response) {
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
                }
                else {
                    var param = {};
                    param.WasteDisposal = angular.copy(WasteDisposalObj.WasteDisposal);
                    param.WasteDisposalDetailList = angular.copy($scope.medicineOpeningStock);

                    console.log("Add");
                    console.log(param);

                    HWBatteryWasteDisposalService.SaveBatteryDisposal(param).then(function (response) {
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
                }
            }
            else {
                toastr.error("Invalid data.");
            }
        };

        var Init = function () {
            GetWasteTypeDDL();
            //GetWasteStorageDDL();
        };
        Init();
    }
})();