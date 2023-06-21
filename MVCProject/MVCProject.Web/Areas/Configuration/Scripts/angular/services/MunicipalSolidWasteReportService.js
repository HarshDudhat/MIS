angular.module("DorfKetalMVCApp")
    .service("MunicipalSolidWasteReportService", ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.GetMuncipalWasteGenerationYearDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetMuncipalWasteGenerationYearForDropdown",
                params: null
            });
        };

        list.GetMunicipalSolidWasteReport = function (year, compostable) {
            return $http({
                method: 'GET',
                dataType: 'json',
                url: $rootScope.apiURL + '/HWM/GetMunicipalSolidWasteReport?Year=' + year + '&compostable=' + compostable,
                data: ""
            });
        };

        return list;
    }]);