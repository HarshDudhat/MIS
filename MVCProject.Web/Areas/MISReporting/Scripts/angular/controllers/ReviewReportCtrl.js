(function () {
    'use strict';

    angular.module("MVCApp").controller('ReviewReportCtrl', [
        '$scope', '$rootScope', 'ngTableParams', 'CommonFunctions', 'FileService', 'ReviewReportService', ReviewReportCtrl
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
            };
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


        $scope.selectedMonth = '';
        $scope.minMonth = '';

        $scope.getCurrentYear = function () {
            return new Date().getFullYear().toString();
        };

        $scope.setCurrentMonth = function () {
            var currentDate = new Date();
            var currentYear = currentDate.getFullYear();
            var currentMonth = currentDate.getMonth() + 1;

            $scope.selectedMonth = currentYear + '-' + (currentMonth < 10 ? '0' + currentMonth : currentMonth);
            $scope.minMonth = currentYear + '-01';
        };

        $scope.setCurrentMonth();



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

       

       


    }
})();