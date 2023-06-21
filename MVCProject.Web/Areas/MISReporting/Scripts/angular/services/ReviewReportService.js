angular.module('MVCApp')
    .service('ReviewReportService', ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.getverticallist = function () {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/MISReport/GetVerticalList'
            });
        };

        return list;
    }]);