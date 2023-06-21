angular.module("MVCApp")
.service('ApplicationService', ['$rootScope', '$http', function ($rootScope, $http) {
    var list = [];

    // Get Application Configuration
    list.GetApplicationConfiguration = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/ApplicationConfiguration/GetApplicationConfiguration'
        });
    };

    // Save Application Configuration
    list.SaveApplicationConfiguration = function (applicationConfiguration, siteId) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/ApplicationConfiguration/SaveApplicationConfiguration?siteId=' + siteId,
            data: JSON.stringify(applicationConfiguration)
        });
    };

    // Upload Logo to server
    list.UploadLogo = function (formData) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Upload/UploadFile?databaseName=' + $rootScope.userContext.CompanyDB,
            data: formData,
            headers: {
                'Content-Type': undefined
            }
        });
    };

    // Update Application Logo in session
    list.UpdateApplicationLogo = function (logo) {
        return $http({
            method: 'PUT',
            url: '/Configuration/Application/UpdateApplicationLogo?logo=' + logo
        });
    };

    return list;
} ]);