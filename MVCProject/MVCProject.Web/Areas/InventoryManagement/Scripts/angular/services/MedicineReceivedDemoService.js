angular.module("DorfKetalMVCApp").service("MedicineReceivedDemoService", ["$http", "$rootScope", function ($http, $rootScope) {
    var list = {};

    list.GetMedicineReceivedDetails = function (filters) {
        return $http({
            method: 'POST',
            url: $rootScope.apiURL + '/Inventory/GetMedicineReceivedDetails',
            data: JSON.stringify(filters)
        });
    };

    list.MedicineAutoCompleteUrl = function () {
        return $rootScope.apiURL + '/Inventory/SearchForMedicineNameAutoComplete?medicineName=';
    };

    list.MedicineReceivedFromAutoCompleteUrl = function () {
        return $rootScope.apiURL + '/Inventory/SearchForMedicineReceivedFromAutoComplete?receivedFrom=';
    };


    list.GetMinimumQtyForReceivedStock = function (groupId, medicineId, expiryDate) {
        return $http({
            method: "GET",
            url: $rootScope.apiURL + '/Inventory/GetMinimumQtyForReceivedStock?GroupId=' + groupId + '&MedicineId=' + medicineId + '&ExpiryDate=' + expiryDate
        });
    };

    // Save ES Detail
    list.SaveMedicineReceivedDetails = function (MedicineReceivedDetails) {
        return $http({
            method: 'POST',
            dataType: 'json',
            url: $rootScope.apiURL + '/Inventory/SaveMedicineReceivedDetails',
            data: JSON.stringify(MedicineReceivedDetails)
        });
    };

    return list;
}]);