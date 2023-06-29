(function () {
    'use strict';

    angular.module("MVCApp").controller('ApproveReportCtrl', [
        '$scope', '$rootScope', 'ngTableParams','$location', 'CommonFunctions', 'FileService', 'ApproveReportService', '$timeout', ApproveReportCtrl
    ]);

    //BEGIN ApproveReportCtrl
    function ApproveReportCtrl($scope, $rootScope, ngTableParams, $location, CommonFunctions, FileService, ApproveReportService) {
        /* Initial Declaration */
        $scope.sampleDate = new Date();
        $scope.ApproveReportDetailScope = {
            VerticalId: 0,
            ProjectId: 0,
            ProjectName: '',
            VerticalName: '',
            ReportDate: new Date(),
        };

        /* var ReviewReportDetailParams = {};*/
        $scope.ClearFormData = function (frmApproveReport) {
            $scope.ApproveReportDetailScope = {
                VerticalId: 0,
                ProjectId: 0,
                ProjectName: '',
                VerticalName: '',
                ReportDate: new Date().getMonth() + 1,
            };

    /*        $scope.selectedMonth = null;*/
            frmApproveReport.$setPristine();

        };

        $scope.Init = function () {
            var params = $location.search();
            if (params != null) {
                $scope.ApproveReportDetailScope.VerticalId = JSON.parse(params.VerticalId);
                $scope.Projectlist($scope.ApproveReportDetailScope.VerticalId);
                $scope.ApproveReportDetailScope.ProjectId = JSON.parse(params.ProjectId);
                $scope.ApproveReportDetailScope.ReportDate = new Date(params.ReportDate);
                //$scope.search($scope.ApproveReportDetailScope);
            }
        }


        $scope.resetApprovereportDetails = function (frmApproveReport) {
            if ($scope.operationMode() == "Update") {
                $scope.frmApproveReport = angular.copy($scope.lastStorageGroup);
                frmApproveReport.$setPristine();
            } else {            
                $scope.ClearFormData(frmApproveReport);
            }
        };

        //For vertical Dropdown
        $scope.Verticallist = function () {
            ApproveReportService.getverticallist()
                .then(function (res) {
                    $scope.Vertical = res.data.Result;
                });
        }

        $scope.Projectlist = function (VerticalId) {
            ApproveReportService.getprojectlist(VerticalId)
                .then(function (res) {
                    $scope.Project = res.data.Result;
                });
        }

        $scope.setDefaultMonth = function () {
            $scope.maxDate = new Date().toISOString().slice(0, 7);
        };

        $scope.search = function (ApproveReportDetailScope) {
            ApproveReportDetailScope.ReportDate = angular.copy(moment(ApproveReportDetailScope.ReportDate).format("YYYY-MM-DD"));
            ApproveReportService.getGroupData(ApproveReportDetailScope).then(function (res) {
                console.log(res.data.Result)
                $scope.GroupList = res.data.Result;
                if ($scope.GroupList.length > 0) {
                    $scope.show = true;
                }
                else {
                    $scope.show = false;
                }
            })
        }

        $scope.sendApprove = function (GroupList) {
            ApproveReportService.approveMIS(GroupList).then(function (res) {
                if (res) {
                    if (res.data.MessageType == messageTypes.Success) {
                        toastr.success(res.data.Message, successTitle);

                        window.location.href = '../../MISReporting/ApproveReport';
                    } else if (res.data.MessageType == messageTypes.Error) {
                        toastr.error(res.data.Message, errorTitle);
                    }   
                }
            })
        }

        $scope.rejectApprove = function (GroupList) {
            console.log($scope.GroupList)
            ApproveReportService.rejectMIS(GroupList).then(function (res) {
                if (res) {
                    if (res.data.MessageType == messageTypes.Success) {
                        toastr.success(res.data.Message, successTitle);
                        window.location.href = '../../MISReporting/ApproveReport';
                    } else if (res.data.MessageType == messageTypes.Error) {
                        toastr.error(res.data.Message, errorTitle);
                    }
                }
            })
        }
    }
})();