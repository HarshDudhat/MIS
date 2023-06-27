angular.module('MVCApp')
    .service('DashboardService', ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.getAllList = function (reportParams) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/Dashboard/GetAllList',
                data: JSON.stringify(reportParams)
            });
        };

        list.getPendingList = function (reportParams) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/Dashboard/GetPendingList',
                data: JSON.stringify(reportParams)
            });
        };

        list.getSubmittedList = function (reportParams) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/Dashboard/GetSubmittedList',
                data: JSON.stringify(reportParams)
            });
        };

        list.getReviewedList = function (reportParams) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/Dashboard/GetReviewedList',
                data: JSON.stringify(reportParams)
            });
        };

        list.getprojectlist = function (VerticalId) {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/Dashboard/GetProjectList?VerticalId=' + VerticalId
            });
        };


        list.getCount = function (VerticalId) {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/Dashboard/GetCount'
            });
        };


        return list;
    }]);