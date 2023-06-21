

(function () {
    'use strict'
    angular.module("DorfKetalMVCApp").controller("MessagePopupCtrl", ["$scope", "$rootScope", "$uibModalInstance", "Text", "Header", function ($scope, $rootScope, $uibModalInstance, Text, Header) {
        $scope.Message = {
            Text: Text,
            Header: Header
        };
        $scope.CancelClick = function () {
            $uibModalInstance.close();
        };
    }]);
})();