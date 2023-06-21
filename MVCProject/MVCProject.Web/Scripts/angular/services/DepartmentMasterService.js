angular.module("DorfKetalMVCApp").service('DepartmentMasterService', ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    list.GetDepartmentsBySite = function (siteId, isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/DepartmentMaster/GetDepartmentsBySite',
            params: (angular.isDefined(isGetAll) ? { siteIds: siteId, isGetAll: isGetAll} : { siteIds: siteId })
        });
    };

    // Get Department List
    list.GetDepartmentList = function (isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/DepartmentMaster/GetDepartmentList' + (angular.isDefined(isGetAll) ? '?isGetAll=' + isGetAll : '')
        });
    };


    return list;
} ]);