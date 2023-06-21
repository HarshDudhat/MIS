angular.module("DorfKetalMVCApp")
    .service("BiomedicalWasteReportService", ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];

        list.GetMuncipalWasteGenerationYearDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetMuncipalWasteGenerationYearForDropdown",
                params: null
            });
        };

        list.GetBiomedicalWasteReport = function (year) {
            return $http({
                method: 'GET',
                dataType: 'json',
                url: $rootScope.apiURL + '/HWM/GetBiomedicalWasteReport?Year=' + year,
                data: ""
            });
        };

        return list;
    }]);