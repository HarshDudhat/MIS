(function () {
    'use strict';

    angular.module("MVCApp").controller('ReviewReportCtrl', [
        '$scope', '$rootScope', 'ngTableParams','$location', 'CommonFunctions', 'FileService', 'ReviewReportService', '$timeout', ReviewReportCtrl
    ]);

    //BEGIN ReviewReportCtrl
    function ReviewReportCtrl($scope, $rootScope, ngTableParams, $location, CommonFunctions, FileService, ReviewReportService) {
        /* Initial Declaration */
        $scope.sampleDate = new Date();
        $scope.ReviewReportDetailScope = {
            VerticalId: 0,
            ProjectId: 0,
            ProjectName: '',
            VerticalName: '',
            ReportDate: new Date(),
        };
        $scope.Init = function () {
            var params = $location.search();
            if (params != null) {
                $scope.ReviewReportDetailScope.VerticalId = JSON.parse(params.VerticalId);
                $scope.Projectlist($scope.ReviewReportDetailScope.VerticalId);
                $scope.ReviewReportDetailScope.ProjectId = JSON.parse(params.ProjectId);
                $scope.ReviewReportDetailScope.ReportDate = new Date(params.ReportDate);
            }
        }
        

        $scope.ClearFormData = function (frmReviewReport) {
            $scope.ReviewReportDetailScope = {
                VerticalId: 0,
                ProjectId: 0,
                ProjectName: '',
                VerticalName: '',
                ReportDate: new Date().getMonth() + 1,
            };
            $scope.GroupList = null;
            $scope.show = false;
            frmReviewReport.$setPristine();

        };

        $scope.resetReviewreportDetails = function (frmReviewReport) {
            if ($scope.operationMode() == "Update") {
                $scope.frmReviewReport = angular.copy($scope.lastStorageGroup);
                frmReviewReport.$setPristine();
            } else {            
                $scope.ClearFormData(frmReviewReport);
            }
        };

        //For vertical Dropdown
        $scope.Verticallist = function () {
            ReviewReportService.getverticallist()
                .then(function (res) {
                    $scope.Vertical = res.data.Result;
                });
        }

        $scope.Projectlist = function (VerticalId) {
            ReviewReportService.getprojectlist(VerticalId)
                .then(function (res) {
                    $scope.Project = res.data.Result;
                });
        }

        $scope.setDefaultMonth = function () {
            $scope.maxDate = new Date().toISOString().slice(0, 7);
        };

        $scope.search = function (ReviewReportDetailScope) {
            ReviewReportDetailScope.ReportDate = angular.copy(moment(ReviewReportDetailScope.ReportDate).format("YYYY-MM-DD"));
            ReviewReportService.getGroupData(ReviewReportDetailScope).then(function (res) {
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

        $scope.sendReview = function (GroupList) {
            console.log(GroupList)
            debugger
            ReviewReportService.reviewMIS(GroupList).then(function (res) {
                if (res) {
                    if (res.data.MessageType == messageTypes.Success) {
                        toastr.success(res.data.Message, successTitle);
                        window.location.href = '../../MISReporting/ReviewReport';
                    } else if (res.data.MessageType == messageTypes.Error) {
                        toastr.error(res.data.Message, errorTitle);
                    }   
                }
            })
        }

        $scope.rejectReview = function (GroupList) {
            ReviewReportService.rejectMIS(GroupList).then(function (res) {
                if (res) {
                    if (res.data.MessageType == messageTypes.Success) {
                        toastr.success(res.data.Message, successTitle);
                        window.location.href = '../../MISReporting/ReviewReport';
                    } else if (res.data.MessageType == messageTypes.Error) {
                        toastr.error(res.data.Message, errorTitle);
                    }
                }
            })
        }
    }
})();