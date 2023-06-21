

(function () {
    'use strict'
    angular.module("DorfKetalMVCApp").controller("NotesPopupCtrl", ["$scope", "$rootScope", "$uibModalInstance", "moduleId", "referenceId", "isReadOnly", "CommonService", function ($scope, $rootScope, $uibModalInstance, moduleId, referenceId, isReadOnly, CommonService) {
        $scope.isReadOnly = isReadOnly;
        //Init
        $scope.Note = {
            ModuleUId: referenceId,
            ModuleId: moduleId,
            Note: '',
            IsActive: true
        };
        $scope.maxLength = 500;

        $scope.notes = [];

        //Get Note
        $scope.GetNotes = function () {
            CommonService.GetNotes($scope.Note.ModuleId, $scope.Note.ModuleUId).then(function (res) {
                if (res && res.data) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                        $scope.Notes = data.Result;
                    } else {
                        toastr.error(data.Message, errorTitle);
                    }
                }
            });
        } ();

        //Save Note 
        $scope.SaveNotes = function () {
            CommonService.SaveNotes($scope.Note).then(function (res) {
                if (res && res.data) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                        toastr.success(data.Message, successTitle);
                        $uibModalInstance.close();
                    } else {
                        toastr.error(data.Message, errorTitle);
                    }
                }
            });
        };

        $scope.OKClick = function () {
            $scope.SaveNotes();
        };

        $scope.CancelClick = function () {
            $uibModalInstance.close();
        };
    } ]);
})();