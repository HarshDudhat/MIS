angular.module("DorfKetalMVCApp")
    .service("eWasteDisposalService", ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.GetWasteDisposalDetails = function (WasteDisposalId) {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteDisposalDetails?WasteDisposalId=" + WasteDisposalId,
                params: null
            });
        };
                
        list.GetWasteTypeDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteCategotyForDropdown",
                params: null
            });
        };

        list.GetWasteItemsDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteCategotyForDropdown",
                params: null
            });
        };

        list.GetWasteWeigthUnitsDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetUOMForDropdown",
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

        list.SaveWasteDisposal = function (obj) {
            return $http({
                method: 'POST',
                dataType: 'json',
                url: $rootScope.apiURL + "/HWM/AddWasteDisposal",
                data: JSON.stringify(obj)
            });
        };

        list.SaveAttachments = function (obj) {
            return $http({
                method: 'POST',
                dataType: 'json',
                url: $rootScope.apiURL + "/HWM/Attachments",
                data: JSON.stringify(obj)
            });
        };

        list.GetVehicleTypeDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetVehicleTypeForDropdown",
                params: null
            });
        };

        return list;
    }]);