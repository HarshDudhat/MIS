angular.module('MVCApp')
    .service('VerticalMasterService', ['$rootScope', '$http', function ($rootScope, $http) {
    var list = [];

    // Get All list of Vertical Details
        list.GetAllVerticals = function (verticalDetailParams) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/VerticalMaster/GetAllVerticals/',
            data: JSON.stringify(verticalDetailParams)
        });
    };
    //// Get All list of Designation Details
    //list.GetDesignationList = function (isGetAll) {
    //    return $http({
    //        method: 'GET',
    //        url: $rootScope.apiURL + '/Designations/GetDesignationList' + (angular.isDefined(isGetAll) ? '?isGetAll=' + isGetAll : '')
    //    });
    //};
    
   

    // Add/Update Vertical Details
        list.SaveVerticalDetails = function (verticalDetail) {
            
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/VerticalMaster/SaveVerticalDetails/',
            data: JSON.stringify(verticalDetail)
        });
    }

    // Get Vertical Items
        list.GetVerticalById = function (verticalId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/VerticalMaster/GetVerticalById?verticalId=' + verticalId
        });
    };

        // Get Site in-charge
        list.GetSiteinCharge = function () {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL + '/VerticalMaster/GetSiteinCharge/'
            });
        };
   

    return list;
} ]);