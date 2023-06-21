(function () {
    'use strict'
    angular.module("DorfKetalMVCApp").controller("eWasteGenerationCtrl", [
        "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "eWasteGenerationService", "CommonService", eWasteGenerationCtrl
    ]);

    function eWasteGenerationCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, eWasteGenerationService, CommonService) {

        $scope.Initstatus = {
            opened: false
        };

        $scope.maxChar = 8000;
        $scope.IsDisableOnEdit = false;
        $scope.IsEdit = false;
        $scope.MaxDate = new Date();

        $scope.Obj = {};

        $scope.options = {
            showAddMedicineDetails: false
        };

        $scope.Obj.WasteGeneration = {
            EmployeeName: userContext.EmployeeName
            , WasteTypeId: 3
            , FunctionalWasteTypeName: 'e-Waste Generation'
            , FunctionalWasteTypeId: 3
        };

        $scope.Obj.WasteGenerationDetail = [];
        $scope.medicineOpeningStock = [];

        $scope.WasteCategories = [];
        $scope.WasteItems = [];

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.GetFunctionAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Function, 0, 0);
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.Obj.WasteGeneration.SiteLeveleId = level.LevelId;
                $scope.Obj.WasteGeneration.SiteName = level.Name;
            }
            else {
                $scope.Obj.WasteGeneration.SiteLeveleId = "";
                $scope.Obj.WasteGeneration.SiteName = "";
            }
        };

        $scope.ChangedFunctionLevelId = function (selected) {
            if (angular.isDefined(selected)) {
                $scope.Obj.WasteGeneration.FunctionLevelId = selected.originalObject.HierarchyId;
                $scope.Obj.WasteGeneration.FunctionLevelName = selected.originalObject.Name;
            }
            else {
                $scope.Obj.WasteGeneration.FunctionLevelId = "";
                $scope.Obj.WasteGeneration.FunctionLevelName = "";
            }
        };

        var GetWasteTypeDDL = function () {
            eWasteGenerationService.GetWasteTypeDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteCategories = data.Result;
                }
            });
        };

        var GetWasteItemsDDL = function () {
            eWasteGenerationService.GetWasteTypeDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteItems = data.Result;
                }
            });
        };

        var GetWasteCategotyDetails = function (category, actionToAdd) {

            eWasteGenerationService.GetWasteCategotyDetails(category).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    actionToAdd.UOM = data.Result.UnitShortCode;
                }
            });
        };

        $scope.fnWasteTypeChangeSetName = function (actionToAdd) {

            var selectedIndex = $scope.WasteCategories.map(function (obj) { return obj.Id; }).indexOf(parseInt(actionToAdd.WasteCategoryId));
            actionToAdd.WasteCategoryName = $scope.WasteCategories[selectedIndex].Name;
            if (actionToAdd.WasteCategoryId) {
                GetWasteCategotyDetails(actionToAdd.WasteCategoryId, actionToAdd);
            }
        };

        $scope.fnItemChangeSetName = function (actionToAdd) {
            //var selectedIndex = $scope.WasteItems.map(function (obj) { return obj.Id; }).indexOf(parseInt(actionToAdd.Item));
            //actionToAdd.ItemName = $scope.WasteItems[selectedIndex].Name;
        };

        $scope.fnItemChange = function (actionToAdd) {
            //actionToAdd.Item = actionToAdd.ItemName.Id;
        };

        $scope.SaveeWasteGeneration = function (frmeWasteGeneration, WasteGenerationObj) {

            if (!$rootScope.permission.CanWrite) {
                return;
            }

            if (frmeWasteGeneration.$valid) {

                if ($scope.IsEdit) {
                    var paramArr = [];
                    paramArr = angular.copy($scope.medicineOpeningStock);

                    eWasteGenerationService.UpdateWasteGenerationDetails(paramArr).then(function (response) {
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


                    console.log(WasteGenerationObj.WasteGeneration);

                    param.WasteGeneration = angular.copy(WasteGenerationObj.WasteGeneration);
                    param.WasteGenerationDetailList = angular.copy($scope.medicineOpeningStock);

                    eWasteGenerationService.SaveWasteStorage(param).then(function (response) {
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

        //#region Grid Functions

        $scope.GetWasteGenerationDetails = function (WasteGenerationId) {

            if (WasteGenerationId > 0) {
                eWasteGenerationService.GetWasteGenerationDetails(WasteGenerationId).then(function (response) {
                    var data = response.data;

                    if (data.MessageType == messageTypes.Success) {
                        $scope.IsDisableOnEdit = true;
                        $scope.IsEdit = true;

                        $scope.Obj.WasteGeneration = data.Result.wasteGeneration;
                        $scope.medicineOpeningStock = data.Result.lstWasteGenerationDetails;

                        if (data.Result.wasteGeneration.RequestedDate) {
                            $scope.Obj.WasteGeneration.RequestedDate = new Date(data.Result.wasteGeneration.RequestedDate + "Z");
                        }
                        if (data.Result.wasteGeneration.EPRAuthorizationIssueDate) {
                            console.log(data.Result.wasteGeneration.EPRAuthorizationIssueDate);
                            $scope.Obj.WasteGeneration.EPRAuthorizationIssueDate = new Date(data.Result.wasteGeneration.EPRAuthorizationIssueDate + "Z");
                        }

                        $scope.Obj.WasteGeneration.EmployeeName = data.Result.wasteGeneration.FirstName + " " + data.Result.wasteGeneration.LastName;

                    }
                    else {
                        toastr.error(data.Message);
                    }
                });
            }
        };

        // Add New Medicine Details
        $scope.AddMedicineDetails = function (frmMedicineDetailsAdd, medicineOpeningStock, actionToAdd, options) {

            $scope.IsSave = true;
            $scope.NoFound = false;

            if (frmMedicineDetailsAdd.$valid) {

                if (medicineOpeningStock == null)
                    medicineOpeningStock = [];

                actionToAdd.IsActive = true;
                medicineOpeningStock.push(actionToAdd);
                options.showAddMedicineDetails = !options.showAddMedicineDetails;
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
        $scope.EditMedicineDetails = function (item, actionToEdit, showHideAction) {
            showHideAction.isEdit = !showHideAction.isEdit;
            angular.copy(item, actionToEdit);
        };

        //Update Medicine Details
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
        $scope.DeleteMedicineDetails = function (medicineOpeningStock, item) {
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
            GetWasteItemsDDL();
        };

        Init();

    }

})();