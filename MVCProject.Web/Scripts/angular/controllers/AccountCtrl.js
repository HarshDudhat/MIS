(function () {
    'use strict';

    angular.module("MVCApp").controller('AccountCtrl', [
            '$scope', '$rootScope', '$uibModal', 'AccountService', 'CommonFunctions', 'CommonService', AccountCtrl
        ]);

    //BEGIN AccountCtrl
    function AccountCtrl($scope, $rootScope, $uibModal, AccountService, CommonFunctions, CommonService) {

        $scope.applicationLogo = "/Content/images/company-logo.png";
        $scope.isLogoLoaded = true;

        //BEGIN Check login
        $scope.CheckLogin = function (isSessionExpired) {
            if (isSessionExpired) {
                toastr.warning(sessionHasBeenExpiredMsg ? sessionHasBeenExpiredMsg : '', warningTitle);
            }
            localStorage.setItem("logout", true);
            localStorage.removeItem("logout");

            var userdata = CommonFunctions.GetCookie("REM", userdata);
            if (userdata) {
                userdata = CommonFunctions.DecryptData(userdata);
                userdata = userdata.split('░');
                if (userdata.length > 1) {
                    $scope.loginFormData = {
                        UserName: userdata[0],
                        UserPassword: userdata[1],
                        Remember: true
                    }
                }
            } else {
                $scope.loginFormData = { Remember: false };
            }
        };
        //End Check login

        //BEGIN Do Login
        $scope.DoLogin = function (Login, frmLogin) {
            if (frmLogin.$invalid) {
                if (frmLogin.txtUserName.$error.required) {
                    frmLogin.txtUserName.$dirty = true;
                    $("#txtUserName").focus();
                } else if (frmLogin.txtPassword.$error.required) {
                    frmLogin.txtPassword.$dirty = true;
                    $("#txtPassword").focus();
                }
                return;
            }
            Login.TimeZoneMinutes = CommonFunctions.GetTimeZoneMinutes();
            AccountService.DoLogin(Login).then(function (res) {
                if (res) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                        CommonService.CreateSession(data.Result).then(function (response) {
                            $rootScope.isAjaxLoadingChild = true;
                            if (Login.Remember) {
                                var userdata = Login.UserName + "░" + Login.UserPassword;
                                userdata = CommonFunctions.EncryptData(userdata);
                                CommonFunctions.SetCookie("REM", userdata);
                            } else {
                                CommonFunctions.SetCookie("REM", "");
                            }
                            CommonFunctions.RedirectToDefaultUrl();
                        });
                    } else {
                        $("#txtUserName").focus();
                        toastr.error(data.Message, errorTitle);
                    }
                }
            });
        };
        //END Do Login

        $scope.OpenResetPasswordModel = function () {
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'template/ResetPasswordModel.html',
                controller: "ResetPasswordModel",
                size: 'sm',
                keyboard: true,
                backdrop: 'static',
                resolve: {}
            });
        };

        // Init
        $scope.Init = function () {
            if (sessionStorage.getItem("resetPasswordSuccessMessage")) {
                toastr.success(sessionStorage.getItem("resetPasswordSuccessMessage"));
                sessionStorage.removeItem("resetPasswordSuccessMessage");
            }
        } ();
    }
    //END AccountCtrl

    angular.module("MVCApp").controller('ResetPasswordModel', [
           "$scope", "$rootScope", "$filter", "$uibModalInstance", 'AccountService', ResetPasswordModel
        ]);

    //BEGIN AccountCtrl
    function ResetPasswordModel($scope, $rootScope, $filter, $uibModalInstance, AccountService) {
        $scope.user = { UserName: '' };

        $scope.SendResetPasswordMail = function (frmResetPassword) {
            if (frmResetPassword.$valid) {
                AccountService.SendResetPassword($scope.user).then(function (res) {
                    if (res) {
                        var data = res.data;
                        if (data.MessageType == messageTypes.Success) {
                            toastr.success(data.Message);
                            $uibModalInstance.dismiss('cancel');
                        } else {
                            toastr.error(data.Message, errorTitle);
                        }
                    }
                });
            }
        };

        $scope.cancelActionToPerform = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();