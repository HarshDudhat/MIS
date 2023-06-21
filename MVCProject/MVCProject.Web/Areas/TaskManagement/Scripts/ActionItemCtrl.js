(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("ActionItemCtrl", [
            "$scope", "$rootScope", "$filter", ActionItemCtrl
        ]);

    function ActionItemCtrl($scope, $rootScope, $filter) {
        $scope.currentDate = new Date();
        $scope.startDate = new Date();
        $scope.ActionItems = [];
        $scope.AttachmentsList = [];
        $scope.DocumentsList = [];
        $scope.isAllFileUploaded = false;
    }
})();