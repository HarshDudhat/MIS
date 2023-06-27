angular.module("MVCApp").service('AccountService', ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    // Login
    list.DoLogin = function (Model) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Account/Login',
            data: JSON.stringify(Model)
        });
    };

    // Login
    list.DoLogOut = function () {
        return $http({
            method: 'GET',
            url: '/Account/LogOut'
        });
    };

    list.SendResetPassword = function (user) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Account/SendResetPassword',
            data: JSON.stringify(user)
        });
    }

    // Change current role of user
    list.ChangeRole = function (roleId) {
        return $http({
            method: 'PUT',
            url: $rootScope.apiURL + '/Account/ChangeRole?roleId=' + roleId
        });
    }

    // emaildid of user
    list.getuserlist = function (user) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Account/getusersdetails?user=' + user
        });
    };

    // Generate Security Code of User
    list.generatesecurecode = function (id) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Account/GetRandomString?id=' + id,
            data: JSON.stringify(id),
        });
    };

    // VerifyCode
    list.verifycodes = function (code) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Account/VerifyCode?code=' + code,
            data: JSON.stringify(code)
        });
    };

    // upadate pass
    list.updatepass = function (pass) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Account/UpdatePassword?pass=' + pass,
            data: JSON.stringify(pass)
        });
    };

    list.GetCurrentRole = function (roleId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Account/GetCurrentRole?RoleId=' + roleId
        });
    }

    return list;
} ]);