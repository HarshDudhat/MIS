angular.module("DorfKetalMVCApp")
    .service("HWMDisposalService", ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.GetWasteTypeDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteCategotyForDropdown",
                params: null
            });
        };


        list.GetVehicleTypeDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetVehicleTypeForDropdown",
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

        list.UpdateWasteStorage = function (obj) {
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

        list.SaveAttachments = function (obj) {
            return $http({
                method: 'POST',
                dataType: 'json',
                url: $rootScope.apiURL + "/HWM/Attachments",
                data: JSON.stringify(obj)
            });
        };

        return list;
    }]);