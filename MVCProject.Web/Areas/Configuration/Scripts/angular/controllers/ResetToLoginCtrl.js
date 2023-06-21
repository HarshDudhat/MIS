(function () {
    'use strict';

    angular.module("MVCApp").controller('ResetToLoginCtrl', [
            '$scope', '$rootScope', 'ngTableParams', 'CommonFunctions', 'FileService', 'ResetToLoginService', ResetToLoginCtrl
        ]);

    //BEGIN ResetToLoginCtrl
    function ResetToLoginCtrl($scope, $rootScope, ngTableParams, CommonFunctions, FileService, ResetToLoginService) {
        /* Initial Declaration */
        $scope.sampleDate = new Date();
        var currentUsersDetailParams = {};
        $scope.resetToLoginDetailScope = {
            UserId: 0
        };
        $scope.lastStorageAudit = $scope.lastStorageAudit || {};
        $scope.operationMode = function () {
            return $scope.resetToLoginDetailScope.UserId > 0 ? "Update" : "Save";
        };

        // BEGIN Update User Flag Details
        $scope.UpdateUserFlagDetails = function (UserId) {
            if (!$rootScope.permission.CanWrite) { return; }
            ResetToLoginService.UpdateUserFlagDetails(UserId).then(function (res) {
                if (res) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                        //$scope.ClearFormData(frmResetToLogin);
                        toastr.success(data.Message, successTitle);
                        $scope.tableParams.reload();
                    } else {
                        toastr.error(data.Message, errorTitle);
                    }
                }
            });
        };

        //Load current Users List
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: $rootScope.pageSize,
            sorting: { UserName: 'asc' }
        }, {
            getData: function ($defer, params) {
                if (currentUsersDetailParams == null) {
                    currentUsersDetailParams = {};
                }
                currentUsersDetailParams.Paging = CommonFunctions.GetPagingParams(params);
                //Load current Users List
                ResetToLoginService.GetCurrentLoginUsers(currentUsersDetailParams.Paging).then(function (res) {
                    var data = res.data;
                    if (res.data.MessageType == messageTypes.Success) {// Success
                        $defer.resolve(res.data.Result);
                        if (res.data.Result.length == 0) {
                        } else {
                            params.total(res.data.Result[0].TotalRecords);
                        }
                    } else if (res.data.MessageType == messageTypes.Error) {// Error
                        toastr.error(res.data.Message, errorTitle);
                    }
                    $rootScope.isAjaxLoadingChild = false;
                });
            }
        });
    }
})();