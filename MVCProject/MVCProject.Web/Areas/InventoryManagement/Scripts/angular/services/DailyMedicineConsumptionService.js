angular.module("DorfKetalMVCApp").service("DailyMedicineConsumptionService", ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    list.GetDailyConsumptionStock = function (filters) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Inventory/GetDailyConsumptionStock',
            data: JSON.stringify(filters)
        });
    };

    list.MedicineAutoCompleteUrl = function (param) {
        //var defaultParam = {};
        //angular.extend(defaultParam, param);
        //var str = [];
        //for (var p in defaultParam) {
        //    if (!(angular.isArray(defaultParam[p]) && defaultParam[p].length == 0)) {
        //        if (defaultParam.hasOwnProperty(p)) {
        //            if (angular.isArray(defaultParam[p]) && defaultParam[p].length > 0) {
        //                for (var i = 0; i < defaultParam[p].length; i++) {
        //                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p][i]));
        //                }
        //            }
        //            else {
        //                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(defaultParam[p]));
        //            }
        //        }
        //    }
        //}

        var url = $rootScope.apiURL + '/Inventory/SearchForDeadMedicineNameAutoComplete?consumptionDate=' + param.toUTCString() + '&ChkExpired=true&medicineName=';
        return {
            url: url,
            httpMethod: $http({
                method: 'GET',
                url: url
            })
        }

        //return $rootScope.apiURL + '/Inventory/SearchForDeadMedicineNameAutoComplete?medicineName=';
    };

    // Save ES Detail
    list.SaveDailyConsumptionStock = function (OpeningStockDetail) {
        return $http({
            method: 'POST',
            dataType: 'json',
            url: $rootScope.apiURL + '/Inventory/SaveDailyConsumptionStock',
            data: JSON.stringify(OpeningStockDetail)
        });
    };

    return list;
} ]);