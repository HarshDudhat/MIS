(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("HWMWACCtrl", [
        "$scope", '$q', "$rootScope", 'ngTableParams', '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "HWMWACService", "CommonService", HWMWACCtrl
    ]);

    function HWMWACCtrl($scope, $q, $rootScope, ngTableParams, $ngBootbox, $filter, CommonFunctions, CommonEnums, HWMWACService, CommonService) {

        $scope.fnAshUtilizationReport = function (siteId) {
            GetAshUtilizationReport(siteId);
        };
        $scope.MaxDate = new Date();

        $scope.EditWasteStorage = function (obj) {
            
            if(obj.FunctionalWasteTypeId == "1"){
                window.location.href = "/Configuration/HWM/HWGenerationStorage?WasteGenerationId=" + obj.WasteGenerationId;
            }
            else if (obj.FunctionalWasteTypeId == "2") {
                window.location.href = "/Configuration/HWM/BatteryWasteGenerationStorage?WasteGenerationId=" + obj.WasteGenerationId;
            }
            else  if (obj.FunctionalWasteTypeId == "3") {
                window.location.href = "/Configuration/HWM/eWasteGenerationStorage?WasteGenerationId=" + obj.WasteGenerationId;
            }

        };

        $scope.EditWasteDisposal = function (obj) {
            
            if (obj.FunctionalWasteTypeId == "4") {
                window.location.href = "/Configuration/HWM/HWDisposal?WasteDisposalId=" + obj.WasteDisposalId;
            }
            else if (obj.FunctionalWasteTypeId == "5") {
                window.location.href = "/Configuration/HWM/BatteryWasteDisposal?WasteDisposalId=" + obj.WasteDisposalId;
            }
            else if (obj.FunctionalWasteTypeId == "6") {
                window.location.href = "/Configuration/HWM/eWasteDisposal?WasteDisposalId=" + obj.WasteDisposalId;
            }
        };

        var Init = function () {
        };

        Init();

        var WasteStorageParams = {};
        
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: $rootScope.pageSize,
            sorting: { DocumentNo: 'asc' }
        }, {
                getData: function ($defer, params) {
                    if (WasteStorageParams == null) {
                        WasteStorageParams = {};
                    }
                    WasteStorageParams.Paging = CommonFunctions.GetPagingParams(params, 1, 50);
                    WasteStorageParams.Paging.Search = $scope.isSearchClicked ? $scope.search : '';
                    
                    HWMWACService.GetWasteStorage(WasteStorageParams.Paging).then(function (res) {
                        var data = res.data;

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

        var WasteDisposalParams = {};
        
        $scope.tableParamsDisposal = new ngTableParams({
            page: 1,
            count: $rootScope.pageSize,
            sorting: { DocumentNo: 'asc' }
        }, {
                getData: function ($defer, params) {
                    if (WasteDisposalParams == null) {
                        WasteDisposalParams = {};
                    }
                    WasteDisposalParams.Paging = CommonFunctions.GetPagingParams(params, 1, 50);
                    WasteDisposalParams.Paging.Search = $scope.isSearchClickedDisposal ? $scope.searchDisposal : '';
        
                HWMWACService.GetWasteDisposal(WasteDisposalParams.Paging).then(function (res) {
                        var data = res.data;

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

    }
})();