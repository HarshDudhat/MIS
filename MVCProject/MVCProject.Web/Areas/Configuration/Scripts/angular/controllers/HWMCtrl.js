(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("HWMCtrl", [
        "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "HWMService", "CommonService", HWMCtrl
    ]);

    function HWMCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, HWMService, CommonService) {

        $scope.Obj = {
        };

        $scope.Obj.WasteGeneration = {
            WasteTypeId: 1,
            IsActive: true,
            EmployeeName: userContext.EmployeeName,
            EntryBy: userContext.UserId,
            FunctionalWasteTypeName: 'Hazardous Waste Generation',
            FunctionalWasteTypeId: 1
        };

        $scope.IsActive = true;
        $scope.sites = [];
        $scope.WasteCategories = [];
        $scope.WasteStorages = [];
        $scope.MaxDate = new Date();

        var GetWasteStorageDDL = function () {
            HWMService.GetWasteStorageDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteStorages = data.Result;
                }
            });
        };

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.GetFunctionAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Function, 0, 0);
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        var GetWasteTypeDDL = function () {
            HWMService.GetWasteTypeDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteCategories = data.Result;
                }
            });
        };

        $scope.fnWasteTypeChange = function (WasteGenerationDetail) {
            var category = WasteGenerationDetail.WasteCategoryId;

            if (category !== "undefined" && category !== undefined) {
                HWMService.GetWasteCategotyDetails(category).then(function (response) {
                    var data = response.data;
                    if (data.MessageType == messageTypes.Success) {
                        $scope.Obj.WasteGenerationDetail.UOM = data.Result.UnitShortCode;
                        $scope.Obj.WasteGenerationDetail.PhysicalState = data.Result.WasteStateId;
                        $scope.Obj.WasteGenerationDetail.PhysicalStateName = data.Result.WasteStateName;
                    }
                });
            }
            else {
                $scope.Obj.WasteGenerationDetail.UOM = "";
                $scope.Obj.WasteGenerationDetail.PhysicalState = "";
                $scope.Obj.WasteGenerationDetail.PhysicalStateName = "";
            }
        };

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

        $scope.SetFunction = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.Obj.WasteGeneration.FunctionLevelId = level.LevelId;
                $scope.Obj.WasteGeneration.FunctionLevelName = level.Name;
            }
        };

        $scope.SaveWasteGeneration = function (frmWasteGeneration, WasteGenerationObj) {

            console.log($rootScope.permission.CanWrite);
            if (!$rootScope.permission.CanWrite) {
                return;
            }

            console.log(frmWasteGeneration.$valid);
            if (frmWasteGeneration.$valid) {

                var param = angular.copy(WasteGenerationObj);

                HWMService.SaveWasteStorage(param).then(function (response) {
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

        // Clear Form
        $scope.ClearForm = function (frmUser) {
            $scope.user = {
                UserId: 0,
                UserName: '',
                Password: '',
                UserRole: [],
                SiteId: '',
                DepartmentId: '',
                IsActive: true,
                EmployeeId: '',
                Employee: {},
                UserArea: []
            };

            $scope.areas = [];
            frmUser.$setPristine();
            $("#ddlSiteLeveleId").focus();
            CommonFunctions.ScrollToTop();
        };

        $scope.GetWasteGenerationDetails = function (WasteGenerationId) {

            if (WasteGenerationId > 0) {
                HWMService.GetWasteGenerationDetails(WasteGenerationId).then(function (response) {
                    var data = response.data;

                    if (data.MessageType == messageTypes.Success) {
                        $scope.IsDisableOnEdit = true;
                        $scope.IsEdit = true;

                        $scope.Obj.WasteGeneration = data.Result.wasteGeneration;
                        $scope.Obj.WasteGenerationDetail = data.Result.lstWasteGenerationDetails[0];
                        $scope.Obj.MonitoringDate = new Date(data.Result.wasteGeneration.MonitoringDate + "Z");// data.Result.lstWasteGenerationDetails[0];
                        $scope.Obj.RequestedDate = new Date(data.Result.wasteGeneration.RequestedDate + "Z");//data.Result.lstWasteGenerationDetails[0];

                        if (data.Result.wasteGeneration.RequestedDate) {
                            $scope.Obj.WasteGeneration.RequestedDate = new Date(data.Result.wasteGeneration.RequestedDate + "Z");
                        }
                        if (data.Result.wasteGeneration.EPRAuthorizationIssueDate) {
                            $scope.Obj.WasteGeneration.EPRAuthorizationIssueDate = new Date(data.Result.wasteGeneration.EPRAuthorizationIssueDate + "Z");
                            //new Date(data.Result.wasteGeneration.EPRAuthorizationIssueDate);
                        }
                        if (data.Result.wasteGeneration.MonitoringDate) {
                            $scope.Obj.WasteGeneration.MonitoringDate = new Date(data.Result.wasteGeneration.MonitoringDate + "Z");
                            //new Date(data.Result.wasteGeneration.MonitoringDate);
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
            GetWasteStorageDDL();
        };

        Init();
    }
})();