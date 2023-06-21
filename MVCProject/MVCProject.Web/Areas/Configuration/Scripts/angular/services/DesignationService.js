angular.module('DorfKetalMVCApp')
.service('DesignationService', ['$rootScope', '$http', function ($rootScope, $http) {
    var list = [];

    // Get All list of Designation Details
    list.GetAllDesignations = function (designationDetailParams) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Designations/GetAllDesignations/',
            data: JSON.stringify(designationDetailParams)
        });
    };
    // Get All list of Designation Details
    list.GetDesignationList = function (isGetAll) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Designations/GetDesignationList' + (angular.isDefined(isGetAll) ? '?isGetAll=' + isGetAll : '')
        });
    };
    
   

    // Add/Update Designation Details
    list.SaveDesignationDetails = function (designationDetail) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Designations/SaveDesignationDetails',
            data: JSON.stringify(designationDetail)
        });
    }

    // Get Designation Items
    list.GetDesignationById = function (designationId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/Designations/GetDesignationById?designationId=' + designationId
        });
    };

    // Create Excel Report
    list.CreateExcelReport = function () {
        return $http({
            method: 'GET',
            responseType: 'blob',
            url: $rootScope.apiURL + '/Designations/CreateDesignationListReport'
        });
    };

    return list;
} ]);