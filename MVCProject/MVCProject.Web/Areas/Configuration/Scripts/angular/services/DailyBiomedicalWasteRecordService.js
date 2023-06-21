angular.module("DorfKetalMVCApp")
    .service("DailyBiomedicalWasteRecordService", ['$rootScope', '$http', function ($rootScope, $http) {
    var list = [];

    list.SaveWasteStorage = function (obj) {
        return $http({
            method: 'POST',
            dataType: 'json',
            url: $rootScope.apiURL + "/HWM/AddUpdateDailyBiomedicalWasteRecord",
            data: JSON.stringify(obj)
        });
    };

    list.UpdateWasteStorage = function (obj) {
        return $http({
            method: 'POST',
            dataType: 'json',
            url: $rootScope.apiURL + "/HWM/AddUpdateDailyBiomedicalWasteRecord",
            data: JSON.stringify(obj)
        });
    };
    
    return list;
} ]);