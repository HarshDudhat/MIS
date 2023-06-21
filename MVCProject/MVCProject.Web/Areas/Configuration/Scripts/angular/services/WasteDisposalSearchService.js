angular.module("DorfKetalMVCApp")
    .service("WasteDisposalSearchService", ['$rootScope', '$http', function ($rootScope, $http) {
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

        list.GetWasteDisposal = function (params) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/HWM/GetWasteDisposalSearch',
                data: JSON.stringify(params)
            });
        };

        return list;
    }]);