(function () {
    'use strict';

    angular.module("MVCApp").controller('DesignationCtrl', [
            '$scope', '$rootScope', 'ngTableParams', 'CommonFunctions', 'FileService', 'DesignationService', DesignationCtrl
        ]);

    //BEGIN DesignationCtrl
    function DesignationCtrl($scope, $rootScope, ngTableParams, CommonFunctions, FileService, DesignationService) {
        /* Initial Declaration */
        $scope.sampleDate = new Date();
        var designationDetailParams = {};
        $scope.designationDetailScope = {
            DesignationId: 0,
            DesignationName: '',
            IsActive: true
        };
        $scope.isSearchClicked = false;
        $scope.lastStorageAudit = $scope.lastStorageAudit || {};
        $scope.operationMode = function () {
            return $scope.designationDetailScope.DesignationId > 0 ? "Update" : "Save";
        };

        // BEGIN Add/Update Designation details
        $scope.SaveDesignationDetails = function (designationDetailScope, frmDesignations) {
            debugger
            //if (!$rootScope.permission.CanWrite) { return; }
            if (frmDesignations.$valid) {
                DesignationService.SaveDesignationDetails(designationDetailScope).then(function (res) {
                    if (res) {
                        var data = res.data;
                        if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                            $scope.ClearFormData(frmDesignations);
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

        // BEGIN Bind form data for edit Designation
        $scope.EditDesignationDetails = function (designationId) {
            DesignationService.GetDesignationById(designationId).then(function (res) {
                if (res) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success) {// Success
                        $scope.designationDetailScope = data.Result;
                        $scope.lastStorageAudit = angular.copy(data.Result);
                        CommonFunctions.ScrollUpAndFocus("txtDesignation");
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

        // Reset Designation form data; edit + reset will remove all changes made in edit mode.
        $scope.resetDesignationDetails = function (frmDesignations) {
            if ($scope.operationMode() == "Update") {
                $scope.frmDesignations = angular.copy($scope.lastStorageGroup);
                frmDesignations.$setPristine();
            } else {
                $scope.clearData(frmDesignations);
            }
        };

        // Clear Designation form data.
        $scope.ClearFormData = function (frmDesignations) {
            $scope.designationDetailScope = {
                DesignationId: 0,
                DesignationName: '',
                IsActive: true
            };
            frmDesignations.$setPristine();
            $("#txtDesignation").focus();
            CommonFunctions.ScrollToTop();
        };

        //Load Designation List
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: $rootScope.pageSize,
            sorting: { DesignationName: 'asc' }
        }, {
            getData: function ($defer, params) {
                if (designationDetailParams == null) {
                    designationDetailParams = {};
                }
                designationDetailParams.Paging = CommonFunctions.GetPagingParams(params);
                designationDetailParams.Paging.Search = $scope.isSearchClicked ? $scope.search : '';
                //Load Employee List
                DesignationService.GetAllDesignations(designationDetailParams.Paging).then(function (res) {
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