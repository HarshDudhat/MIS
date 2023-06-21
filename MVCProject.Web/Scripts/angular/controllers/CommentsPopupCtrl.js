(function () {
    'use strict'
    angular.module("MVCApp").controller("CommentsPopupCtrl", ["$scope", "$rootScope", "$uibModalInstance", "moduleId", "referenceId", "isReadOnly", "CommonService", function ($scope, $rootScope, $uibModalInstance, moduleId, referenceId, isReadOnly, CommonService) {
        $scope.isReadOnly = isReadOnly;
        //Init
        $scope.Comments = {
            ReferenceId: referenceId,
            ModuleId: moduleId,
            Comment: '',
            IsActive: true
        };
        $scope.maxLength = 500;

        $scope.commentsList = [];

        //Get Comments
        $scope.GetComments = function () {
            CommonService.GetComments($scope.Comments.ModuleId, $scope.Comments.ReferenceId).then(function (res) {
                if (res && res.data) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                        $scope.commentsList = data.Result;
                    } else {
                        toastr.error(data.Message, errorTitle);
                    }
                }
            });
        } ();

        //Save Comments 
        $scope.SaveComments = function () {
            CommonService.SaveComments($scope.Comments).then(function (res) {
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
            $scope.SaveComments();
        };

        $scope.CancelClick = function () {
            $uibModalInstance.close();
        };
    } ]);
})();