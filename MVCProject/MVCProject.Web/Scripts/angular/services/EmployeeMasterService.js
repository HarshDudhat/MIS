angular.module("DorfKetalMVCApp").service('EmployeeMasterService', ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    list.EmployeeAutoCompleteUrl = function (siteId) {
        return $rootScope.apiURL + '/Employees/SearchForAutofill?hideLoader=1&siteId=' + siteId + '&name=';
    };

    list.EmployeeWithUserAutoCompleteUrl = function (siteId) {
        return $rootScope.apiURL + '/Employees/SearchForAutofillWithUser?hideLoader=1&siteId=' + siteId + '&name=';
    };
    list.EmployeeWithUserAutoCompleteByRoleGroupUrl = function (siteId, groupList, departmentId) {
        return $rootScope.apiURL + '/Employees/SearchForAutofillWithUserByRoleGroup?hideLoader=1&siteId=' + siteId + '&groupList=' + groupList + '&departmentId=' + departmentId + '&name=';
    }; 
    list.GetEmployee = function (employeeId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Employees/GetEmployee?employeeId=' + employeeId
        });
    };

    list.GetEmployeesToSelect = function (siteId, departmentId, nameStart, onlyUnallocated) {
        if (!(siteId > 0)) siteId = "";
        if (!(departmentId > 0)) departmentId = "";
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Employees/GetEmployeesToSelect?siteId=' + siteId + '&departmentId=' + departmentId + '&nameStart=' + nameStart + '&onlyUnallocated=' + onlyUnallocated
        });
    };

    list.GetEmployeeList = function (isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Employees/GetEmployeeList' + (angular.isDefined(isGetAll) ? '?isGetAll=' + isGetAll : '')
        });
    };

    list.GetEmployeeListBySiteList = function (siteList) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Employees/GetEmployeeListBySiteList?siteList=' + siteList
        });
    };

    list.GetEmployeeListBySite = function (siteId, isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Employees/GetEmployeeListBySite',
            params: (angular.isDefined(isGetAll) ? { siteIds: siteId, isGetAll: isGetAll } : { siteIds: siteId })
        });
    };

    list.GetEmployeeListRolewise = function (roleIdList, departmentId,siteId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Employees/GetEmployeeListRolewise?roleIdList=' + roleIdList + '&departmentId=' + departmentId + '&siteId=' + siteId
        });
    };

    list.GetEmployeeListGroupwise = function (groupList, departmentId, siteId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Employees/GetEmployeeListGroupwise?groupList=' + groupList + '&departmentId=' + departmentId + '&siteId=' + siteId
        });
    };

    return list;
} ]);