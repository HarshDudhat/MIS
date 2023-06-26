﻿(function () {
    'use strict';

    angular.module("MVCApp").controller('ApproveReportCtrl', [
        '$scope', '$rootScope', 'ngTableParams', 'CommonFunctions', 'FileService', 'ApproveReportService', '$timeout', ApproveReportCtrl
    ]);

    //BEGIN ApproveReportCtrl
    function ApproveReportCtrl($scope, $rootScope, ngTableParams, CommonFunctions, FileService, ApproveReportService) {
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
            console.log($scope.maxDate)
        };

        $scope.search = function (ApproveReportDetailScope) {
            ApproveReportDetailScope.ReportDate = angular.copy(moment(ApproveReportDetailScope.ReportDate).format("YYYY-MM-DD"));
            ApproveReportService.getGroupData(ApproveReportDetailScope).then(function (res) {
                console.log(res.data.Result)
                $scope.GroupList = res.data.Result;
            })
        }

        $scope.sendApprove = function (GroupList) {
            console.log(GroupList[0].FieldData);
            GroupList[0].ReportId = GroupList[0].FieldData[0].ReportId;
            ApproveReportService.approveMIS(GroupList).then(function (res) {
                if (res) {
                    if (res.data.MessageType == messageTypes.Success) {
                        toastr.success(res.data.Message, successTitle);
                        $scope.ClearFormData(frmApproveReport);
                        $scope.GroupList = null;
                        $scope.show = false;

                    } else if (res.data.MessageType == messageTypes.Error) {
                        toastr.error(res.data.Message, errorTitle);
                    }   
                }
            })
        }

        $scope.rejectApprove = function (GroupList) {
            console.log(GroupList[0].FieldData);
            GroupList[0].ReportId = GroupList[0].FieldData[0].ReportId;
            ApproveReportService.rejectMIS(GroupList).then(function (res) {
                if (res) {
                    if (res.data.MessageType == messageTypes.Success) {
                        toastr.success(res.data.Message, successTitle);
                        $scope.ClearFormData(frmApproveReport);
                        $scope.GroupList = null;
                        $scope.show = false;

                    } else if (res.data.MessageType == messageTypes.Error) {
                        toastr.error(res.data.Message, errorTitle);
                    }
                }
            })
        }
    }
})();