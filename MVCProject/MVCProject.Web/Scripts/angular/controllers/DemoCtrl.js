(function () {
    'use strict';

    angular.module("DorfKetalMVCApp").controller('DemoCtrl', [
            '$scope', '$rootScope', DemoCtrl
        ]);

    // BEGIN DemoCtrl
    function DemoCtrl($scope, $rootScope, AccountService, CommonFunctions, CommonService) {
        $scope.SelectItem = function (obj) {
            $scope.searhSite = obj.Name;
        };
        $scope.demodata = {
            Ids: [],
            TitleStatic: "Read only data",
            Editor: ''
        };
        $scope.MaxChar = 100;

        $scope.TrainerUrl = $rootScope.apiURL + "/TrainingSchedule/GetTrainerList?name=";
        $scope.TrainerUrlNoLoader = $rootScope.apiURL + "/TrainingSchedule/GetTrainerList?hideLoader&name=";
        $scope.AssignTrainer = function (data) {
            console.log(data.originalObject);
        };
        $scope.demodata.SiteId = 6;
        $scope.sites = [
        { Id: 1, Name: "Surat" },
        { Id: 2, Name: "Valsad" },
        { Id: 3, Name: "Baroda" },
        { Id: 4, Name: "Ahmedabad" },
        { Id: 5, Name: "Mumbai" },
        { Id: 6, Name: "Rajkot" },
        { Id: 7, Name: "Pune" }
        ];

        $scope.isAllFileUploaded = true;
        $scope.DocumentsList = [];
        $scope.AttachmentsList = [];
        $scope.DocumentsList2 = [];
        $scope.AttachmentsList2 = [];

        $scope.DeleteData = function (val) {
            alert(val);
        };


    }
    // END DemoCtrl

})();