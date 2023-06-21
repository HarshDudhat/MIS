(function () {
    'use strict',

        angular.module("DorfKetalMVCApp").controller("MunicipalSolidWasteReportCtrl", [
            "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "MunicipalSolidWasteReportService",
            "CommonService", MunicipalSolidWasteReportCtrl
        ]);

    function MunicipalSolidWasteReportCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums,
        MunicipalSolidWasteReportService, CommonService) {

        $scope.MunicipalSolidWasteReport = {};
        $scope.MaxDate = new Date();
        $scope.names = [];

        $scope.MunicipalSolidWasteReport = {
            Year: '',
            Compostable: '',
            NonCompostable: '',
            FinYear: '',
            All: ''
        };
        $scope.MunicipalSolidWasteReportYear = [];

        var GetMunicipalSolidWasteReportYearDDL = function () {
            MunicipalSolidWasteReportService.GetMuncipalWasteGenerationYearDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.MunicipalSolidWasteReportYear = data.Result;
                }
            });
        };

        $scope.fnMunicipalSolidWasteReport = function (year, compostable) {

            if (year != "" && year != undefined) {
                MunicipalSolidWasteReportService.GetMunicipalSolidWasteReport(year, compostable).then(function (res) {
                    var data = res.data.Result;
                    $scope.names = data;
                });
            }
        };


        $scope.fnClearMunicipalSolidWasteReport = function () {

            $scope.names = [];
            $scope.MunicipalSolidWasteReport.Year = "";
            $scope.MunicipalSolidWasteReport.Compostable = 1;
        };

        // Init
        var Init = function () {

            GetMunicipalSolidWasteReportYearDDL();
            $scope.MunicipalSolidWasteReport.Compostable = 1;

        };

        Init();
    }
})();