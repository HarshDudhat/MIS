(function () {
    'use strict'
    angular.module("DorfKetalMVCApp").controller("AttachmentsPopupCtrl", ["$scope", "$rootScope", "$uibModalInstance", "moduleId", "referenceId", "isReadOnly", "CommonService", function ($scope, $rootScope, $uibModalInstance, moduleId, referenceId, isReadOnly, CommonService) {
        $scope.isReadOnly = isReadOnly;
        $scope.moduleId = parseInt(moduleId);
        $scope.referenceId = referenceId;
        $scope.TotalCount = 0;
        //Init
        $scope.entity = {
            ReferenceId: referenceId,
            ModuleId: moduleId,
            DocumentsList: [],
            AttachmentsList: []
        }

        //Get Audit Compliance Attachments
        $scope.GetAttachments = function () {
            CommonService.GetAttachments($scope.moduleId, $scope.referenceId).then(function (res) {
                if (res && res.data) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                        $scope.entity.DocumentsList = data.Result;
                        $scope.TotalCount = data.Result.length;
                    } else {
                        toastr.error(data.Message, errorTitle);
                    }
                }
            });
        }();

        //Save  Attachments 
        $scope.SaveAttachments = function () {
            for (var i = 0; i < $scope.entity.AttachmentsList.length; i++) {
                $scope.entity.AttachmentsList[i].ModuleId = $scope.moduleId;
                $scope.entity.AttachmentsList[i].ReferenceId = $scope.referenceId;
            }

            CommonService.SaveAttachments($scope.entity).then(function (res) {
                if (res && res.data) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                        toastr.success(data.Message, successTitle);
                        $uibModalInstance.close(data.Total);
                    } else {
                        toastr.error(data.Message, errorTitle);
                    }
                }
            });
        };

        $scope.OKClick = function () {
            $scope.SaveAttachments();
        };

        $scope.CancelClick = function () {
            $uibModalInstance.close($scope.TotalCount);
        };
    }]);
})();