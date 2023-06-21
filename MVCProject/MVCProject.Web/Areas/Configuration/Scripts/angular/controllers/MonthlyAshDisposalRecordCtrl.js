(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("MonthlyAshDisposalRecordCtrl", [
        "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "MonthlyAshDisposalRecordService", "CommonService", MonthlyAshDisposalRecordCtrl
    ]);

    function MonthlyAshDisposalRecordCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums, MonthlyAshDisposalRecordService, CommonService) {

        $scope.Obj = {
            Type: 'Battery'
        };
        $scope.MaxDate = new Date();

        $scope.Obj.AshWasteGeneration = {
            MuncipalWasteGenerationId: 0,
            SiteLeveleId: 0,
            RequestedDate: '',
            RequestedMonth: '',
            selectedyear: '',
            RequestedYear: '',
            CoalConsumed: 0,
            Ashcontentcoalper: 0,
            TotalAshGeneration: 0,
            FlyAshGeneration: 0,
            BottomAshGeneration: 0,
            FlyAshUtilized: 0,
            BottomAshUtilized: 0,
            FlyAshUtilizationper: 0,
            TotalAshUtilizationper: 0,
            CementMfdUtilization: 0,
            ClayMfdUtilization: 0,
            RoadDevUtilization: 0,
            Quaryyfilling: 0,
            AshpondDumping: 0,
            PonfAshLifting: 0,
            IsActive: true,
            EmployeeName: userContext.EmployeeName,
            EntryBy: userContext.UserId,
            UpdateDate: '',
            UpdateBy: ''
        };

        $scope.IsActive = true;
        $scope.sites = [];
        $scope.WasteCategories = [];
        $scope.WasteStorages = [];

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);
        $scope.GetFunctionAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Function, 0, 0);
        $scope.GetLevelAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        $scope.getYearfromDate = function (Requestedate) {
            var requesteyear = Requestedate.getFullYear();
            var finYear = (requesteyear) + " - " + (requesteyear + 1);
            $scope.Obj.AshWasteGeneration.selectedyear = requesteyear;
            $scope.Obj.AshWasteGeneration.finYear = finYear;
        };

        var GetMonthListForDropdownDDL = function () {
            MonthlyAshDisposalRecordService.GetMonthListForDropdownDDL().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.WasteCategories = data.Result;
                }
            });
        };

        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.Obj.AshWasteGeneration.SiteLeveleId = level.LevelId;
                $scope.Obj.AshWasteGeneration.SiteName = level.Name;
            }
        };

        $scope.ChangedSiteLeveleId = function (selected, actionToAdd, id) {
            $scope.Obj.AshWasteGeneration.SiteLeveleId = selected.originalObject.HierarchyId;
        };

        $scope.SaveAshWasteGeneration = function (frmAshWasteGeneration, AshWasteGenerationObj) {

            if (!$rootScope.permission.CanWrite) {
                return;
            }

            if (frmAshWasteGeneration.$valid) {

                var param = angular.copy(AshWasteGenerationObj);

                MonthlyAshDisposalRecordService.SaveStorage(param).then(function (response) {
                    var data = response.data;
                    if (data.MessageType == messageTypes.Success) {
                        toastr.success(data.Message, successTitle);

                        var d = new Date();
                        var year = d.getFullYear();

                        GetAshUtilizationReport(year, 0,  1);
                    }
                    else {
                        toastr.error(data.Message);
                    }
                });
            }
            else {
                toastr.error("Invalid data.");
            }
        };


        var GetAshUtilizationReport = function (Year, SiteId, Type) {

            MonthlyAshDisposalRecordService.GetAshUtilizationReport(Year, SiteId, Type).then(function (res) {
                var data = res.data.Result;
                $scope.names = data;
            });

        };


        // Init
        var Init = function () {

            GetMonthListForDropdownDDL();
            //();

            var d = new Date();
            var year = d.getFullYear();

            GetAshUtilizationReport(year, 0, 1);
        };

        Init();
    }
})();

