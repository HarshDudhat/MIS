angular.module("DorfKetalMVCApp").service('DashBoardService', ["$http", "$rootScope", function ($http, $rootScope) {
 var list = {};


    // Save Component Setting
    list.SaveComponentSetting = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/SaveComponentSetting",
            data: JSON.stringify(data)
        });
    };

    list.GetAllComponentByUser = function () {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + "/Dashboard/GetAllComponentByUser",            
        });
    };

    list.GetSettingByComponent = function (ComponentId) {
        return $http({
            method: 'GET',
            url: $rootScope.apiURL + "/Dashboard/GetSettingByComponent",
            params: {ComponentId: ComponentId }
        });
    };

    list.GetTaskData = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetTaskData",
            data: JSON.stringify(data)
        });
    };

    list.GetCAPAData = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetCAPAData",
            data: JSON.stringify(data)
        });
    };

    list.GetIncidentData = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetIncidentData",
            data: JSON.stringify(data)
        });
    };

    list.GetAuditData = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetAuditData",
            data: JSON.stringify(data)
        });
    };

    list.GetBBSObservationData = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetBBSObservationData",
            data: JSON.stringify(data)
        });
    };

     list.GetSiteInspectionData = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetSiteInspectionData",
            data: JSON.stringify(data)
        });
    };

     list.GetSafetyObservationData = function (data) {
         return $http({
             method: 'POST',
             url: $rootScope.apiURL + "/Dashboard/GetSafetyObservationData",
             data: JSON.stringify(data)
         });
     };

     list.GetBodyPartData = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetBodyPartData",
            data: JSON.stringify(data)
        });
    };


    list.GetNoofDaysWithoutIncident = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetNoofDaysWithoutIncident",
            data: JSON.stringify(data)
        });
    };


    list.GetNatureOfInjury = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetNatureOfInjury",
            data: JSON.stringify(data)
        });
    };

    list.GetTRCFByMonth = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetTRCFByMonth",
            data: JSON.stringify(data)
        });
    };

    list.GetSeverityByMonthly = function (data) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + "/Dashboard/GetSeverityByMonthly",
            data: JSON.stringify(data)
        });
    };


     // Create Widget Excel Report
    list.ExportToExcel = function (WidgetsSetting) {
         return $http({
            method: 'POST',
            responseType: 'blob',
            url: $rootScope.apiURL + '/DashBoard/ExportToExcel',
            data: JSON.stringify(WidgetsSetting)
        });
    };
    return list;
} ]);