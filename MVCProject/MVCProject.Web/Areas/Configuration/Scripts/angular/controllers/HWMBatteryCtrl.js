(function () {
    'use strict'
    angular.module("DorfKetalMVCApp").controller("HWMBatteryCtrl", [
        "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "HWMBatteryService", "CommonService", HWMBatteryCtrl
    ]);

    function HWMBatteryCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, HWMBatteryService, CommonService) {

        $scope.Initstatus = {
            opened: false
        };

        $scope.maxChar = 8000;
        $scope.maxlength = 5;

        $scope.Obj = {
            Type: 'Battery'
        };
        $scope.MaxDate = new Date();

        $scope.Obj.WasteGeneration = {
            WasteTypeId: 2,
            IsActive: true,
            EmployeeName: userContext.EmployeeName,
            EntryBy: userContext.UserId,
            FunctionalWasteTypeName: 'Battery Waste Generation',
            FunctionalWasteTypeId: 2
        };

        $scope.WasteCategories = [];

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
                var level = selected.originalObject;
                $scope.Obj.WasteGeneration.FunctionLevelId = level.LevelId;
                $scope.Obj.WasteGeneration.FunctionLevelName = level.Name;
            }
            else {
                $scope.Obj.WasteGeneration.FunctionLevelId = "";
                $scope.Obj.WasteGeneration.FunctionLevelName = "";
            }
        };

        var GetWasteTypeDDL = function () {
            HWMBatteryService.GetWasteTypeDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteCategories = data.Result;
                }
            });
        };

        var GetWasteCategotyDetails = function (category) {
            HWMBatteryService.GetWasteCategotyDetails(category).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.Obj.WasteGenerationDetail.UOM = data.Result.UnitShortCode;
                }
            });
        };

        $scope.fnWasteTypeChange = function (category) {
            $scope.Obj.WasteGenerationDetail.UOM = '';

            if (category) {
                GetWasteCategotyDetails(category);
            }
        };

        $scope.SaveWasteBattery = function (frmWasteBattery, WasteGenerationObj) {

            console.log($rootScope.permission.CanWrite);
            if (!$rootScope.permission.CanWrite) {
                return;
            }

            if (frmWasteBattery.$valid) {

                var param = angular.copy(WasteGenerationObj);
                console.log(param);

                HWMBatteryService.SaveWasteStorage(param).then(function (response) {
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
                toastr.error("Invalid data.");
            }
        };

        $scope.GetWasteGenerationDetails = function (WasteGenerationId) {

            if (WasteGenerationId > 0) {
                HWMBatteryService.GetWasteGenerationDetails(WasteGenerationId).then(function (response) {
                    var data = response.data;

                    if (data.MessageType == messageTypes.Success) {
                        $scope.IsDisableOnEdit = true;
                        $scope.IsEdit = true;

                        $scope.Obj.WasteGeneration = data.Result.wasteGeneration;
                        $scope.Obj.WasteGenerationDetail = data.Result.lstWasteGenerationDetails[0];

                        if (data.Result.wasteGeneration.RequestedDate) {
                            $scope.Obj.WasteGeneration.RequestedDate = new Date(data.Result.wasteGeneration.RequestedDate + "Z");
                        }
                        if (data.Result.wasteGeneration.EPRAuthorizationIssueDate) {
                            $scope.Obj.WasteGeneration.EPRAuthorizationIssueDate = new Date(data.Result.wasteGeneration.EPRAuthorizationIssueDate + "Z");
                        }
                        if (data.Result.wasteGeneration.MonitoringDate) {
                            $scope.Obj.WasteGeneration.MonitoringDate = new Date(data.Result.wasteGeneration.MonitoringDate + "Z");
                        }
                        $scope.Obj.WasteGeneration.EmployeeName = data.Result.wasteGeneration.FirstName + " " + data.Result.wasteGeneration.LastName;
                    }
                    else {
                        toastr.error(data.Message);
                    }
                });
            }
        };

        // Init
        var Init = function () {

            GetWasteTypeDDL();
        };

        Init();

    }

})();