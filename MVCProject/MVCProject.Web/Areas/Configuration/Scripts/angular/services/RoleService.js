angular.module("DorfKetalMVCApp")
.service('RoleService', ['$rootScope', '$http', function ($rootScope, $http) {
    var list = [];

    // Get role levels
    list.GetRoleLevel = function (onlyActive) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Role/GetRoleLevel?isActiveOnly=' + (onlyActive ? 'true' : 'false')
        });
    };

    // Get roles
    list.GetRoles = function (onlyActive) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Role/GetRoles?isActiveOnly=' + (onlyActive ? 'true' : 'false')
        });
    };

    // Get role to edit
    list.EditRole = function (roleId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Role/EditRole?roleId=' + roleId
        });
    };

    // Get module pages
    list.GetModulePages = function (onlyActive) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Role/GetModulePages'
        });
    };

    // Save Role
    list.SaveRole = function (model) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Role/SaveRole/',
            data: JSON.stringify(model)
        });
    };

    return list;
} ]);