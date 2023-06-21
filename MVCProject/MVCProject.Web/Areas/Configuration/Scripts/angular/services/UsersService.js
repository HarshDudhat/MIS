angular.module("DorfKetalMVCApp")
.service("UsersService", ['$rootScope', '$http', function ($rootScope, $http) {
    var list = [];

    list.GetUsers = function (params) {
        return $http({
            method: "GET",
            url: $rootScope.apiURL + "/Users/GetUsers",
            params: params
        });
    };

    // Get roles
    list.GetRoles = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Role/GetRoles?isActiveOnly=true'
        });
    };

    // Get User By UserId
    list.GetUser = function (userId) {
        return $http({
            method: "GET",
            url: $rootScope.apiURL + "/Users/GetUser?userId=" + userId
        });
    };

    // Add user
    list.AddUser = function (user) {
        return $http({
            method: "POST",
            url: $rootScope.apiURL + "/Users/AddUser",
            data: JSON.stringify(user)
        });
    };

    //Update user
    list.UpdateUser = function (user) {
        return $http({
            method: "POST",
            url: $rootScope.apiURL + "/Users/UpdateUser",
            data: JSON.stringify(user)
        });
    };

    // Export To Excel
    list.ExportToExcel = function () {
        return $http({
            method: "GET",
            responseType: "blob",
            url: $rootScope.apiURL + "/Users/ExportToExcel"
        });
    };

    // Get Sites for dropdown
    list.GetSites = function () {
        return $http({
            method: "GET",
            url: $rootScope.apiURL + '/SiteMaster/GetForDropdown'
        });
    };

    // Get Departments By Site
    list.GetDepartmentsBySite = function (siteId) {
        return $http({
            method: "GET",
            url: $rootScope.apiURL + '/DepartmentMaster/GetDepartmentsBySite?siteIds=' + siteId
        });
    };

    // Get Users Area
    list.GetAreaBySite = function (siteId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/AreaMaster/GetArea?siteId=' + siteId
        });
    };

    list.GetEmployee = function (employeeId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Employees/GetEmployee?employeeId=' + employeeId
        });
    };

    return list;
} ]);