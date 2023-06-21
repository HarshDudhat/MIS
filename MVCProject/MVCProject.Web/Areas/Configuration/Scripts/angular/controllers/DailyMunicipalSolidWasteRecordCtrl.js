(function () {
    'use strict',

        angular.module("DorfKetalMVCApp").controller("DailyMunicipalSolidWasteRecordCtrl", [
            "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "DailyMunicipalSolidWasteRecordService",
            "CommonService", DailyMunicipalSolidWasteRecordCtrl
        ]);

    function DailyMunicipalSolidWasteRecordCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums,
        DailyMunicipalSolidWasteRecordService, CommonService) {

        $scope.MuncipalWasteGeneration = {};
        $scope.MaxDate = new Date();

        $scope.MuncipalWasteGeneration = {
            MuncipalWasteGenerationId: 0,
            SiteLeveleId: 0,
            RequestedDate: '',
            CompostableQuantity: '',
            NonCompostableQuantity: '',
            IsActive: true,
            EntryDate: '',
            EntryBy: userContext.EmployeeName,
            UpdateDate: '',
            UpdateBy: '',
            ComUOM: '',
            NonComUOM: ''
        };

        $scope.IsActive = true;
        $scope.sites = [];

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.MuncipalWasteGeneration.SiteLeveleId = level.LevelId;
                $scope.MuncipalWasteGeneration.SiteName = level.Name;
            }
        };

        $scope.ChangedSiteLeveleId = function (selected, actionToAdd, id) {
            $scope.MuncipalWasteGenerationId.SiteLeveleId = selected.originalObject.HierarchyId;
        };

        $scope.SaveBiomedicalWaste = function (frmMuncipalWasteGenerationId, Obj) {

            if (!$rootScope.permission.CanWrite) {
                return;
            }

            //if (frmMuncipalWasteGenerationId.$valid) {

            var param = angular.copy(Obj);

            DailyMunicipalSolidWasteRecordService.SaveDailyMunicipalSolidRecord(param).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    toastr.success(data.Message, successTitle);

                    //var pathname = window.location.pathname; // Returns path only (/path/example.html)
                    //var url = window.location.href;     // Returns full URL (https://example.com/path/example.html)
                    var origin = window.location.origin;   // Returns base URL (https://example.com)

                    setTimeout(function () {
                        location.href = origin + "/Configuration/HWM/MunicipalSolidWasteReport";
                    }, 3000);
                }
                else {
                    toastr.error(data.Message);
                }
            });
            //  }
            //else {
            //        toastr.error("Invalid data.");
            //    }
        };

        // Init
        var Init = function () {

        };

        Init();
    }
})();