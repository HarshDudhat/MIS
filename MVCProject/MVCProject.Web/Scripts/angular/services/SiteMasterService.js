angular.module("DorfKetalMVCApp").service('SiteMasterService', ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    list.GetForDropdown = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/SiteMaster/GetForDropdown'
        });
    };

    list.GetSiteList = function (isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/SiteMaster/GetSiteList' + (angular.isDefined(isGetAll) ? '?isGetAll=' + isGetAll : '')
        });
    };

    list.GetSite = function (siteId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/SiteMaster/GetSite?siteid=' + siteId
        });
    };

    list.GetSiteDataBySiteId = function (siteId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/SiteMaster/GetSiteDataBySiteId?siteid=' + siteId
        });
    }

    return list;
} ]);