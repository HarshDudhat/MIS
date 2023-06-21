angular.module("DorfKetalMVCApp")
    .service("MonthlyAshDisposalRecordService", ['$rootScope', '$http', function ($rootScope, $http) {
        var list = [];
       
        list.GetMonthListForDropdownDDL = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/HWM/GetMonthListForDropdown",
                params: null
            });
        };

        //list.GetWasteStorageDDL = function () {
        //    return $http({
        //        method: "GET",
        //        url: $rootScope.apiURL + "/HWM/GetAshUtilizationReport?Year=2018",
        //        params: null
        //    });
        //};

        list.SaveStorage = function (obj) {
            return $http({
                method: 'POST',
                dataType: 'json',
                url: $rootScope.apiURL + "/HWM/AddUpdateMonthlyDisposal",
                data: JSON.stringify(obj)
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