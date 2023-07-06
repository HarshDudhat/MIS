angular.module('MVCApp')
    .service('UserMasterService', ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        //dropdown for userrole
        list.getrolelist = function () {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/UserMaster/GetRoleDropDown'
            });
        };

       
        // Get All list of UserMaster Details
        list.GetAllUserMasters = function (userMasterDetailParams) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/UserMaster/GetAllUserMasters/',
                data: JSON.stringify(userMasterDetailParams)
            });
        };

        // Get All list of UserMaster Details
        list.GetUserMasterList = function (isGetAll) {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/UserMaster/GetUserMasterList' + (angular.isDefined(isGetAll) ? '?isGetAll=' + isGetAll : '')
            });
        };

        // Add/Update UserMaster Details
        list.SaveuserMasterDetails = function (userMasterDetail) {
            return $http({
                method: 'POST',
                url: $rootScope.apiURL + '/UserMaster/SaveUserMasterDetails',
                data: JSON.stringify(userMasterDetail)
            });
        }

        // Get UserMaster Items
        list.GetUserMasterById = function (UserId) {
            
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/UserMaster/GetUserMasterById?UserId=' + UserId
            });
        };

        return list;
    }]);




