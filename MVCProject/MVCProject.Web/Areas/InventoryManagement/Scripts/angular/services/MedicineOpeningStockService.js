angular.module("DorfKetalMVCApp").service("MedicineOpeningStockService", ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    list.GetMedicineOpeningStock = function (filters) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Inventory/GetMedicineOpeningStock',
            data: JSON.stringify(filters)
        });
    };

    list.MedicineAutoCompleteUrl = function () {
        return $rootScope.apiURL + '/Inventory/SearchForMedicineNameAutoComplete?medicineName=';
    };

    list.GetMinimumQtyForOpeningStock = function (medicineId, expiryDate) {
        return $http({
            method: "GET",
            url: $rootScope.apiURL + '/Inventory/GetMinimumQtyForOpeningStock?MedicineId=' + medicineId + '&ExpiryDate=' + expiryDate
        });
    };

    // Save ES Detail
    list.SaveMedicineOpeningStock = function (OpeningStockDetail) {
        return $http({
            method: 'POST',
            dataType: 'json',
            url: $rootScope.apiURL + '/Inventory/SaveMedicineOpeningStock',
            data: JSON.stringify(OpeningStockDetail)
        });
    };

    return list;
} ]);