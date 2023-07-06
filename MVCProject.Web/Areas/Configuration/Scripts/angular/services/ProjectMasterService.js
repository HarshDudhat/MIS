angular.module('MVCApp')
    .service('ProjectMasterService', ['$rootScope', '$http', function ($rootScope, $http) {
    var list = [];

    // Get All list of Project Details
        list.GetAllProjects = function (projectDetailParams) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/ProjectMaster/GetAllProjects/',
            data: JSON.stringify(projectDetailParams)
        });
    };

    //Get All Vertical List
        list.GetVerticalList = function () {
            return $http({
                method: 'GET',
                url: $rootScope.apiURL +'/ProjectMaster/VeticalDropDown/'
            });
        }

   

    // Add/Update Project Details
        list.SaveProjectDetails = function (projectDetail) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/ProjectMaster/SaveProjectDetails/',
            data: JSON.stringify(projectDetail)
        });
    }

    // Get Project Items
        list.GetProjectById = function (projectId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/ProjectMaster/GetProjectById?projectId=' + projectId
        });
        };
        // Get Project Manager
        list.GetProjectManager = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + '/ProjectMaster/GetProjectManager/'
        });
    }

   

    return list;
} ]);