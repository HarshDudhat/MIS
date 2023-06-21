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
        $scope.ReviewReportDetailScope = {
            VerticalId: 0,
            ProjectId: 0,
            ProjectName:'',
           VerticalName:'',
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


    }
})();