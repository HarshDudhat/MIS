angular.module("MVCApp")
.service('ResetToLoginService', ['$rootScope', '$http', function ($rootScope, $http) {
    var list = [];

    // Add/Update Update User Flag Details
    list.UpdateUserFlagDetails = function (userId) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Users/UpdateUserFlagDetails?userId=' + userId,
            data: JSON.stringify(userId)
        });
    }

    // Get Current Login Users List
    list.GetCurrentLoginUsers = function (currentUsersDetailParams) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Users/GetCurrentLoginUsers/',
            data: JSON.stringify(currentUsersDetailParams)
        });
    };

    return list;
} ]);