(function () {
    'use strict';

    angular.module("MVCApp").controller('ResetPasswordCtrl', [
            '$scope', '$rootScope', 'ResetPasswordService', 'CommonService', ResetPasswordCtrl
        ]);

    function ResetPasswordCtrl($scope, $rootScope, ResetPasswordService, CommonService) {

        $scope.isLogoLoaded = true;
        $scope.isDisabled = true;
        $scope.passwordModel = {
            NewPassword: "",
            ConfirmPassword: ""
        };

        var ValidateToken = function (userId, resetPasswordToken) {
            var model = {
                UserId: userId,
                ResetPasswordToken: resetPasswordToken,
                IsToCheckToken: true
            };

            ResetPasswordService.ResetPassword(model).then(function (res) {
                var data = res.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.isDisabled = false;
                } else {
                    toastr.error(data.Message, errorTitle);
                }
            });
        };

        $scope.ResetPassword = function (frmResetPassword) {
            if (frmResetPassword.$valid) {
                var model = {
                    UserId: $scope.userId,
                    ResetPasswordToken: $scope.resetPasswordToken,
                    IsToCheckToken: false,
                    NewPassword: $scope.passwordModel.NewPassword
                };

                ResetPasswordService.ResetPassword(model).then(function (res) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success) {
                        sessionStorage.setItem("resetPasswordSuccessMessage", data.Message);
                        window.location.href = "/account/login";
                    } else {
                        toastr.error(data.Message, errorTitle);
                    }
                });
            }
        };

        $scope.Init = function (userId, resetPasswordToken) {
            $scope.userId = userId;
            $scope.resetPasswordToken = resetPasswordToken;
            ValidateToken(userId, resetPasswordToken);
        };
    };
})();