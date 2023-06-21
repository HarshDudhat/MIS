(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("WasteDisposalSearchCtrl", [
        "$scope", '$q', "$rootScope", 'ngTableParams', '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "WasteDisposalSearchService", "CommonService", WasteDisposalSearchCtrl
    ]);

    function WasteDisposalSearchCtrl($scope, $q, $rootScope, ngTableParams, $ngBootbox, $filter, CommonFunctions, CommonEnums, WasteDisposalSearchService, CommonService) {

        $scope.MaxDate = new Date();

        $scope.WasteCategories = [];
        $scope.WasteType = [];
        $scope.StatusMaster = [];
        $scope.WasteStorageParams = {};

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.GetFunctionAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Function, 0, 0);
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        $scope.SetLevel = function (selected) {
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


        $scope.fnWasteGenerationStorage = function (WasteStorageParamsDetail) {
            $scope.tableParamsDisposal.reload();
        };


        $scope.fnClearWasteGenerationStorage = function () {
            $scope.WasteStorageParams.StartDate="";
            $scope.WasteStorageParams.EndDate = "";
            $("#txtSiteId_value").val("");
            $scope.WasteStorageParams.WasteTypeId = "";
            $scope.WasteStorageParams.StatusIds = "";
            $scope.tableParamsDisposal.reload();
        };

        var WasteDisposalParams = {};

        $scope.tableParamsDisposal = new ngTableParams({
            page: 1,
            count: $rootScope.pageSize,
            sorting: { WasteDisposalId: 'dsc' }
        }, {
                getData: function ($defer, params) {
                    if (WasteDisposalParams == null) {
                        WasteDisposalParams = {};
                    }
                    $scope.WasteStorageParams.Paging = CommonFunctions.GetPagingParams(params, 1, 50);
                    $scope.WasteStorageParams.pageSize = $rootScope.pageSize;
                    $scope.WasteStorageParams.CurrentPageNumber = 1;

                WasteDisposalSearchService.GetWasteDisposal($scope.WasteStorageParams).then(function (res) {
                        var data = res.data;
                        console.log(data);

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
            WasteDisposalSearchService.GetWasteCategotyDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteCategories = data.Result;
                }
            });
        };

        var GetWasteStateDDL = function () {
            WasteDisposalSearchService.GetWasteStateDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteType = data.Result;
                }
            });
        };

        var GetStatusMasterDDL = function () {
            WasteDisposalSearchService.GetStatusMasterDDL().then(function (response) {
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
        };

        Init();

    }
})();