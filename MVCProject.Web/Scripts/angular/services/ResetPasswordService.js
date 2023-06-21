angular.module("MVCApp").service('ResetPasswordService', ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    // Login
    list.ResetPassword = function (model) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Account/ResetPassword',
            data: JSON.stringify(model)
        });
    };

    return list;
} ]);