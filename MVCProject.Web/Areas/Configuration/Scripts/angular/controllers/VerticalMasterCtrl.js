(function () {
    'use strict';

    angular.module("MVCApp").controller('VerticalMasterCtrl', [
        '$scope', '$rootScope', 'ngTableParams', 'CommonFunctions', 'FileService', 'VerticalMasterService', VerticalMasterCtrl
        ]);

    //BEGIN VerticalMasterCtrl
    function VerticalMasterCtrl($scope, $rootScope, ngTableParams, CommonFunctions, FileService, VerticalMasterService) {
        /* Initial Declaration */
        $scope.sampleDate = new Date();
        var verticalDetailParams = {};

        $scope.verticalDetailScope = {
            VerticalId: 0,
            VerticalName: '',
            IsActive: true
        };
        $scope.isSearchClicked = false;
        $scope.lastStorageAudit = $scope.lastStorageAudit || {};
        $scope.operationMode = function () {
            return $scope.verticalDetailScope.VerticalId > 0 ? "Update" : "Save";
        };

        // BEGIN Add/Update Vertical details
        $scope.SaveVerticalDetails = function (verticalDetailScope, frmvertical) {
            debugger
            //if (!$rootScope.permission.CanWrite) { return; }
            if (frmvertical.$valid) {
                VerticalMasterService.SaveVerticalDetails(verticalDetailScope).then(function (res) {
                    if (res) {
                        var data = res.data;
                        if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                            $scope.ClearFormData(frmvertical);
                            toastr.success(data.Message, successTitle);
                            //$scope.GetAllDesignations();
                            $scope.tableParams.reload();
                        } else if (data.MessageType == messageTypes.Error) {// Error
                            toastr.error(data.Message, errorTitle);
                        } else if (data.MessageType == messageTypes.Warning) {// Warning
                            toastr.warning(data.Message, warningTitle);
                        }
                    }
                });
            }
        };

        // BEGIN Bind form data for edit Vertical
        $scope.EditVerticalDetails = function (verticalId) {
            VerticalMasterService.GetVerticalById(verticalId).then(function (res) {
                if (res) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success) {// Success
                        $scope.verticalDetailScope = data.Result;
                        $scope.lastStorageAudit = angular.copy(data.Result);
                        CommonFunctions.ScrollUpAndFocus("txtvertical");
                    } else if (data.MessageType == messageTypes.Error) {// Error
                        toastr.error(data.Message, errorTitle);
                    }
                }
                $rootScope.isAjaxLoadingChild = false;
            });
        };

        //  Create Excel Report of Designation
        //$scope.createReport = function () {
        //    if (!$rootScope.permission.CanWrite) { return; }
        //    var filename = "Designation_" + $rootScope.fileDateName + ".xls";
        //    CommonFunctions.DownloadReport('/Designations/CreateDesignationListReport', filename);
        //};

        //// Reset Designation form data; edit + reset will remove all changes made in edit mode.
        //$scope.resetDesignationDetails = function (frmDesignations) {
        //    if ($scope.operationMode() == "Update") {
        //        $scope.frmDesignations = angular.copy($scope.lastStorageGroup);
        //        frmDesignations.$setPristine();
        //    } else {
        //        $scope.clearData(frmDesignations);
        //    }
        //};

        // Clear Vertical form data.
        $scope.ClearFormData = function (frmvertical) {
            $scope.verticalDetailScope = {
                VerticalId: 0,
                VerticalName: '',
                IsActive: true
            };
            frmvertical.$setPristine();
            $("#txtvertical").focus();
            CommonFunctions.ScrollToTop();
        };

        //Load Vertical List
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: $rootScope.pageSize,
            sorting: { VerticalName: 'asc' }
        }, {
            getData: function ($defer, params) {
                if (verticalDetailParams == null) {
                    verticalDetailParams = {};
                }
                verticalDetailParams.Paging = CommonFunctions.GetPagingParams(params);
                verticalDetailParams.Paging.Search = $scope.isSearchClicked ? $scope.search : '';
                //Load Vertical List
                VerticalMasterService.GetAllVerticals(verticalDetailParams.Paging).then(function (res) {
                    var data = res.data;
                    if (res.data.MessageType == messageTypes.Success) {// Success
                        $defer.resolve(res.data.Result);
                        if (res.data.Result.length == 0) {
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