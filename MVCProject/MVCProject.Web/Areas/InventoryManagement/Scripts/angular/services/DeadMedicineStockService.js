angular.module("DorfKetalMVCApp").service("DeadMedicineStockService", ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    list.GetDeadMedicineStock = function (filters) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Inventory/GetDeadMedicineStock',
            data: JSON.stringify(filters)
        });
    };

    list.MedicineAutoCompleteUrl = function (param) {
        var url = $rootScope.apiURL + '/Inventory/SearchForDeadMedicineNameAutoComplete?consumptionDate=' + param.toUTCString() + '&ChkExpired=false&medicineName=';
        return {
            url: url,
            httpMethod: $http({
                method: 'GET',
                url: url
            })
        }
    };

    // Save ES Detail
    list.SaveDeadMedicineStock = function (OpeningStockDetail) {
        return $http({
            method: 'POST',
            dataType: 'json',
            url: $rootScope.apiURL + '/Inventory/SaveDeadMedicineStock',
            data: JSON.stringify(OpeningStockDetail)
        });
    };

    return list;
} ]);