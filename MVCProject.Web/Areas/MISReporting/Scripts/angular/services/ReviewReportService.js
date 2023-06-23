angular.module('MVCApp')
    .service('ReviewReportService', ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.getverticallist = function () {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/ReviewReport/GetVerticalList'
            });
        };

        list.getprojectlist = function () {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/ReviewReport/GetProjectList'
            });
        };


        list.getGroupData = function (ReviewReportDetailScope) {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/ReviewReport/GetGroupData?ProjectId=' + ReviewReportDetailScope.ProjectId + '&ReportDate=' + ReviewReportDetailScope.ReportDate
            });
        };

        list.reviewMIS = function (GroupList) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/ReviewReport/ReviewMIS',
                data: JSON.stringify(GroupList)
            });
        };

        list.rejectMIS = function (GroupList) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/ReviewReport/RejectMIS',
                data: JSON.stringify(GroupList)
            });
        };


        return list;
    }]);