angular.module('MVCApp')
    .service('ApproveReportService', ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.getverticallist = function () {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/ApproveReport/GetVerticalList'
            });
        };

        list.getprojectlist = function (VerticalId) {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/ApproveReport/GetProjectList?VerticalId=' + VerticalId
            });
        };


        list.getGroupData = function (ApproveReportDetailScope) {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/ApproveReport/GetGroupData?ProjectId=' + ApproveReportDetailScope.ProjectId + '&ReportDate=' + ApproveReportDetailScope.ReportDate
            });
        };

        list.approveMIS = function (GroupList) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/ApproveReport/ApproveMIS',
                data: JSON.stringify(GroupList)
            });
        };

        list.rejectMIS = function (GroupList) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/ApproveReport/RejectMIS',
                data: JSON.stringify(GroupList)
            });
        };


        return list;
    }]);