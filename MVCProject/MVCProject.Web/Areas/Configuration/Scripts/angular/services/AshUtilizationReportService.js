angular.module("DorfKetalMVCApp")
    .service("AshUtilizationReportService", ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        // For Binding Financial Year
        list.GetMuncipalWasteGenerationYearDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetMuncipalWasteGenerationYearForDropdown",
                params: null
            });
        };

        // For Binding Report
        list.GetAshUtilizationReport = function (Year, SiteId, Type) {
            return $http({
                method: 'GET',
                dataType: 'json',
                url: $rootScope.apiURL + '/HWM/GetAshUtilizationReport?Year=' + Year + '&SiteId=' + SiteId + '&Type=' + Type,
                data: ""
            });
        };

        return list;
    }]);