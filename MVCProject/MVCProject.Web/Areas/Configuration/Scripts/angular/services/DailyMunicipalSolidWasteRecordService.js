angular.module("DorfKetalMVCApp")
    .service("DailyMunicipalSolidWasteRecordService", ['$rootScope', '$http', function ($rootScope, $http) {
    var list = [];

        list.SaveDailyMunicipalSolidRecord = function (obj) {
        return $http({
            method: 'POST',
            dataType: 'json',
            url: $rootScope.apiURL + "/HWM/AddUpdateDailyMunicipalSolidWasteRecord",
            data: JSON.stringify(obj)
        });
    };

    //    list.UpdateWasteStorage = function (obj) {


    //        window.location = "";
    //    return $http({

            

    //        method: 'POST',
    //        dataType: 'json',
    //        url: $rootScope.apiURL + "/HWM/AddUpdateDailyBiomedicalWasteRecord",
    //        data: JSON.stringify(obj)

    //    });
    //};
    
    return list;
} ]);