angular.module("DorfKetalMVCApp")
    .service("HWBatteryWasteDisposalService", ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.GetWasteTypeDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteCategotyForDropdown",
                params: null
            });
        };

        list.GetWasteCategotyDetails = function (CategoryId) {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteCategotyDetails?WasteCategoryId=" + CategoryId,
            });
        };

        list.GetWasteStorageDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteStorageForDropdown",
                params: null
            });
        };

        list.SaveBatteryDisposal = function (obj) {
            return $http({
                method: 'POST',
                dataType: 'json',
                url: $rootScope.apiURL + "/HWM/AddWasteDisposal",
                data: JSON.stringify(obj)
            });
        };

        list.GetWasteDisposalDetails = function (WasteDisposalId) {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteDisposalDetails?WasteDisposalId=" + WasteDisposalId,
                params: null
            });
        };

        list.UpdateWasteDisposalDetails = function (obj) {
            return $http({
                method: 'POST',
                dataType: 'json',
                url: $rootScope.apiURL + "/HWM/UpdateWasteDisposalDetails",
                data: JSON.stringify(obj)
            });
        };

        return list;
    }]);