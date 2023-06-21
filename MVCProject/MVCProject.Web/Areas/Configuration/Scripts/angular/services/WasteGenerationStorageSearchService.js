angular.module("DorfKetalMVCApp")
    .service("WasteGenerationStorageSearchService", ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.GetWasteCategotyDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteCategotyForDropdown",
                params: null
            });
        };

        list.GetWasteStateDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetWasteStateForDropdown",
                params: null
            });
        };

        list.GetStatusMasterDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetStatusMasterForDropdown",
                params: null
            });
        };

        list.GetWasteStorage = function (params) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/HWM/GetWasteStorageSearch',
                data: JSON.stringify(params)
            });
        };

        return list;
    }]);