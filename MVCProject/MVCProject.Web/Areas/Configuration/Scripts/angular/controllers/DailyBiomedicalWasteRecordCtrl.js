(function () {
    'use strict',

        angular.module("DorfKetalMVCApp").controller("DailyBiomedicalWasteRecordCtrl", [
            "$scope", '$q', "$rootScope", '$ngBootbox', "$filter", "CommonFunctions", "CommonEnums", "DailyBiomedicalWasteRecordService",
            "CommonService", DailyBiomedicalWasteRecordCtrl
        ]);

    function DailyBiomedicalWasteRecordCtrl($scope, $q, $rootScope, $ngBootbox, $filter, CommonFunctions, CommonEnums,
        DailyBiomedicalWasteRecordService, CommonService) {

        $scope.BioMedicalWasteGeneration = {};
        $scope.MaxDate = new Date();

        $scope.BioMedicalWasteGeneration = {
            BioMedicalWasteGenerationId: 0,
            SiteLeveleId: 0,
            SiteName: '',
            RequestedDate: '',
            Quantity: 0,
            UOM: '',
            IsActive: true,
            EntryDate: '',
            EntryBy: userContext.EmployeeName,
            UpdateDate: '',
            UpdateBy: ''
        };

        $scope.IsActive = true;
        $scope.sites = [];

        $scope.GetSiteAutoCompleteUrl = CommonService.GetLevelAutoCompleteUrl('', CommonEnums.HierarchyLevel.Site, 0, 0);

        $scope.SetLevel = function (selected) {
            if (angular.isDefined(selected)) {
                var level = selected.originalObject;
                $scope.BioMedicalWasteGeneration.SiteLeveleId = level.LevelId;
                $scope.BioMedicalWasteGeneration.SiteName = level.Name;
            }
            else {
                $scope.BioMedicalWasteGeneration.SiteLeveleId = "";
                $scope.BioMedicalWasteGeneration.SiteName = "";
            }
        };

        $scope.fnWasteTypeChange = function (category) {
            $scope.BioMedicalWasteGenerationId.UOM = '';

            if (category) {
                GetWasteCategotyDetails(category);
            }
        };

        $scope.ChangedSiteLeveleId = function (selected, actionToAdd, id) {
            $scope.BioMedicalWasteGenerationId.SiteLeveleId = selected.originalObject.HierarchyId;
        };
        $scope.ChangedFunctionLevelId = function (selected, actionToAdd, id) {
            $scope.BioMedicalWasteGenerationId.FunctionLevelId = selected.originalObject.HierarchyId;
        };

        $scope.SaveBiomedicalWaste = function (frmBioMedicalWasteGenerationId, Obj) {

            console.log(Obj);
            console.log($rootScope.permission.CanWrite);


            if (!$rootScope.permission.CanWrite) {
                return;
            }

            console.log(Obj);
            if (frmBioMedicalWasteGenerationId.$valid) {

                var param = angular.copy(Obj);

                DailyBiomedicalWasteRecordService.SaveWasteStorage(param).then(function (response) {
                    var data = response.data;
                    if (data.MessageType == messageTypes.Success) {
                        toastr.success(data.Message, successTitle);
                        var origin = window.location.origin;   // Returns base URL (https://example.com)
                        setTimeout(function () {
                            location.href = origin + "/Configuration/HWM/BiomedicalWasteReport";
                        }, 3000);
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

        // Clear Form
        $scope.ClearForm = function (frmUser) {
            $scope.user = {
                UserId: 0,
                UserName: '',
                Password: '',
                UserRole: [],
                SiteId: '',
                DepartmentId: '',
                IsActive: true,
                EmployeeId: '',
                Employee: {},
                UserArea: []
            };

            $scope.areas = [];
            frmUser.$setPristine();
            $("#ddlSiteLeveleId").focus();
            CommonFunctions.ScrollToTop();
        };

        // Init
        var Init = function () {

        };

        Init();
    }
})();