(function () {
    'use strict',

        angular.module("DorfKetalMVCApp").controller("BiomedicalWasteReportCtrl", [
            "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "BiomedicalWasteReportService",
            "CommonService", BiomedicalWasteReportCtrl
        ]);

    function BiomedicalWasteReportCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums,
        BiomedicalWasteReportService, CommonService) {

        $scope.BiomedicalWasteReport = {};

        $scope.BiomedicalWasteReport = {
            Year: ''
        };
        $scope.MaxDate = new Date();

        $scope.BiomedicalWasteReportYear = [];

        var GetBiomedicalWasteReportYearDDL = function () {
            BiomedicalWasteReportService.GetMuncipalWasteGenerationYearDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.BiomedicalWasteReportYear = data.Result;
                }
            });
        };

        $scope.fnBiomedicalWasteReport = function (year) {

            if (year != "" && year != undefined) {
                BiomedicalWasteReportService.GetBiomedicalWasteReport(year).then(function (res) {
                    var data = res.data.Result;
                    $scope.names = data;
                });
            }
        };


        $scope.fnClearBiomedicalWasteReport = function () {

            $scope.names = [];
            $scope.BiomedicalWasteReport.Year = "";

        };

        // Init
        var Init = function () {

            GetBiomedicalWasteReportYearDDL();
            $scope.BiomedicalWasteReport.Compostable = 1;

        };

        Init();
    }
})();