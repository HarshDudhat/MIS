angular.module('MVCApp')
    .service('ReviewReportService', ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.getverticallist = function () {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/MISReport/GetVerticalList'
            });
        };

        list.getprojectlist = function () {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/MISReport/GetProjectList'
            });
        };

        return list;
    }]);