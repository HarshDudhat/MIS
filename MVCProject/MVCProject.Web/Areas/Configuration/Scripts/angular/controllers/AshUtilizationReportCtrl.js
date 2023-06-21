(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("AshUtilizationReportCtrl", [
        "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "AshUtilizationReportService", "CommonService", AshUtilizationReportCtrl
    ]);

    function AshUtilizationReportCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, AshUtilizationReportService, CommonService) {

        $scope.AshUtilizationReport = {
            Year: '',
            SiteLeveleId: '',
            InstalledCapacity: '',
            SiteName: ''
        };
        $scope.MaxDate = new Date();

        $scope.sites = [];
        $scope.names = [];

        $scope.AshUtilizationReportYear = [];

        var GetAshUtilizationReportYearDDL = function () {
            AshUtilizationReportService.GetMuncipalWasteGenerationYearDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.AshUtilizationReportYear = data.Result;
                }
            });
        };

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.AshUtilizationReport.SiteLeveleId = level.LevelId;
                $scope.AshUtilizationReport.SiteName = level.Name;
            }
        };

        $scope.fnAshUtilizationReport = function (year) {
            GetAshUtilizationReport(year);
        };

        $scope.fnClearAshUtilizationReport = function () {
            $scope.names = [];
            $scope.AshUtilizationReport.Year = "";
            $scope.AshUtilizationReport.SiteLeveleId = "";
            $scope.AshUtilizationReport.SiteName = "";
            //$scope.txtConfigLevel_value = "";
            $("#txtConfigLevel_value").val("");
        };

        var GetAshUtilizationReport = function (Year) {
 
            var SiteLeveleId = $scope.AshUtilizationReport.SiteLeveleId;

            if (Year != "" && Year != undefined) {
                AshUtilizationReportService.GetAshUtilizationReport(Year, SiteLeveleId, 0).then(function (res) {
                    var data = res.data.Result;
                    $scope.names = data;
                });
            }
        };


        var GetAshUtilizationReportTotal = function (Year) {

            console.log("GetAshUtilizationReportTotal=" + Year);

            AshUtilizationReportService.GetAshUtilizationReport(Year, 0, 0, 2).then(function (res1) {
                var data1 = res1.data.Result;
                console.log(data1);

                $scope.Total = data1;
            });
        };

        // Init
        var Init = function () {
            GetAshUtilizationReportYearDDL();

        };

        Init();
    }
})();



