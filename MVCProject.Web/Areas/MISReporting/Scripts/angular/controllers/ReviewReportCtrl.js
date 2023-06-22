(function () {
    'use strict';

    angular.module("MVCApp").controller('ReviewReportCtrl', [
        '$scope', '$rootScope', 'ngTableParams', 'CommonFunctions', 'FileService', 'ReviewReportService', '$timeout', ReviewReportCtrl
    ]);

    //BEGIN ReviewReportCtrl
    function ReviewReportCtrl($scope, $rootScope, ngTableParams, CommonFunctions, FileService, ReviewReportService) {
        /* Initial Declaration */
        $scope.sampleDate = new Date();
        /* var ReviewReportDetailParams = {};*/
        $scope.ClearFormData = function (frmReviewReport) {
            $scope.ReviewReportDetailScope = {
                VerticalId: 0,
                ProjectId: 0,
                ProjectName: '',
                VerticalName: '',
                selectedMonth:'',
            };

    /*        $scope.selectedMonth = null;*/
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

        $scope.Projectlist = function () {
            ReviewReportService.getprojectlist()
                .then(function (res) {
                    $scope.Project = res.data.Result;
                });
        }

        $scope.setDefaultMonth = function () {
            var currentDate = new Date();

            var currentMonth = currentDate.getMonth() + 1; // Add 1 because months are zero-based
            var currentYear = currentDate.getFullYear();

            $scope.selectedMonth = currentYear + '-' + (currentMonth < 10 ? '0' : '') + currentMonth;
            $scope.minMonth = '2000-01'; //select as per requirement
            $scope.maxDate = currentYear + '-' + (currentMonth < 10 ? '0' : '') + currentMonth;
        };
    }
})();