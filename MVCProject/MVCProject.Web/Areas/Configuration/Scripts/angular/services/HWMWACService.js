angular.module("DorfKetalMVCApp")
    .service("HWMWACService", ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.GetWasteStorage = function (params) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/HWM/GetWasteStorage',
                data: JSON.stringify(params)
            });
        };

        list.GetWasteDisposal = function (params) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/HWM/GetWasteDisposal',
                data: JSON.stringify(params)
            });
        };
        return list;
    }]);