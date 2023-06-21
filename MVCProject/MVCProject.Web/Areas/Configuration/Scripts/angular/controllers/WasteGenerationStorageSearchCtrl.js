(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("WasteGenerationStorageSearchCtrl", [
        "$scope", '$q', "$rootScope", 'ngTableParams', '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "WasteGenerationStorageSearchService", "CommonService", WasteGenerationStorageSearchCtrl
    ]);

    function WasteGenerationStorageSearchCtrl($scope, $q, $rootScope, ngTableParams, $ngBootbox, $filter, CommonFunctions, CommonEnums, WasteGenerationStorageSearchService, CommonService) {

        $scope.MaxDate = new Date();

        $scope.WasteCategories = [];
        $scope.WasteType = [];
        $scope.StatusMaster = [];
        $scope.WasteStorageParams = {};

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.GetFunctionAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Function, 0, 0);
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        $scope.SetLevel = function (selected) {

            console.log(selected);

            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.WasteStorageParams.SiteLeveleId = level.LevelId;
                $scope.WasteStorageParams.SiteName = level.Name;
            }
            else {
                $scope.WasteStorageParams.SiteLeveleId = "";
                $scope.WasteStorageParams.SiteName = "";
            }
        };

        $scope.ChangedFunctionLevelId = function (selected) {

            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.WasteStorageParams.FunctionLevelId = level.LevelId;
                $scope.WasteStorageParams.FunctionLevelName = level.Name;
            }
            else {
                $scope.WasteStorageParams.FunctionLevelId = "";
                $scope.WasteStorageParams.FunctionLevelName = "";
            }
        };

        $scope.fnClearWasteGenerationStorage = function () {
            $scope.WasteStorageParams.StartDate = "";
            $scope.WasteStorageParams.EndDate = "";
            $("#txtSiteId_value").val("");
            $("#txtFunctionLevelId_value").val("");
            $scope.WasteStorageParams.WasteCategoryId = "";
            $scope.WasteStorageParams.StatusIds = "";
            $scope.tableParams.reload();
        };

        $scope.fnWasteGenerationStorage = function (WasteStorageParamsDetail) {
            $scope.tableParams.reload();
        };

        $scope.tableParams = new ngTableParams(
            {
                page: 1,
                count: $rootScope.pageSize,
                sorting: { WasteTypeName: 'asc' }
            }, {
                getData: function ($defer, params) {
                    if ($scope.WasteStorageParams == null) {
                        $scope.WasteStorageParams = {};
                    }
                    $scope.WasteStorageParams.Paging = CommonFunctions.GetPagingParams(params, 1, 50);
                    $scope.WasteStorageParams.pageSize = $rootScope.pageSize;
                    $scope.WasteStorageParams.CurrentPageNumber = 1;

                    WasteGenerationStorageSearchService.GetWasteStorage($scope.WasteStorageParams).then(function (res) {
                        console.log(res.data);

                        if (res.data.MessageType == messageTypes.Success) {// Success
                            $defer.resolve(res.data.Result);
                            if (res.data.Result.length == 0) {
                                console.log('No records found');
                            } else {
                                params.total(res.data.Result[0].TotalRecords);
                            }
                        } else if (res.data.MessageType == messageTypes.Error) {// Error
                            toastr.error(res.data.Message, errorTitle);
                        }
                        $rootScope.isAjaxLoadingChild = false;
                        CommonFunctions.SetFixHeader();
                    });
                }
            });
        var GetWasteCategotyDDL = function () {
            WasteGenerationStorageSearchService.GetWasteCategotyDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteCategories = data.Result;
                }
            });
        };

        var GetWasteStateDDL = function () {
            WasteGenerationStorageSearchService.GetWasteStateDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteType = data.Result;
                }
            });
        };

        var GetStatusMasterDDL = function () {
            WasteGenerationStorageSearchService.GetStatusMasterDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.StatusMaster = data.Result;
                }
            });
        };

        var Init = function () {
            GetWasteCategotyDDL();
            GetWasteStateDDL();
            GetStatusMasterDDL();
            //GetTableData();
        };

        Init();

    }
})();