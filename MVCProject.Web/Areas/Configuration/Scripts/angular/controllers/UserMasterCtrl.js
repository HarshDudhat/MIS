(function () {
    'use strict';

    angular.module("MVCApp").controller('UserMasterCtrl', [
        '$scope', '$rootScope', 'ngTableParams', 'CommonFunctions', 'FileService', 'UserMasterService', UserMasterCtrl
    ]);

    //BEGIN UserMasterCtrl
    function UserMasterCtrl($scope, $rootScope, ngTableParams, CommonFunctions, FileService, UserMasterService) {
      

        /* Initial Declaration */
        $scope.sampleDate = new Date();
        var userMasterDetailParams = {};
        $scope.userMasterDetailScope = {
            UserId: 0,
            Email: '',
            Password: '',
            RoleId:0,
            IsActive: true,

        };
        $scope.isSearchClicked = false;
        $scope.lastStorageAudit = $scope.lastStorageAudit || {};
        $scope.operationMode = function () {
            return $scope.userMasterDetailScope.UserId > 0 ? "Update" : "Save";
        };

        console.log($scope.userMasterDetailScope);
        // BEGIN Add/Update UserMaster details
        $scope.SaveuserMasterDetails = function (userMasterDetailScope, frmUserMaster) {
            debugger
           if (userMasterDetailScope.Email == null || userMasterDetailScope.Email == "") {
                toastr.warning("User Name is  Required", warningTitle);
                $("#txtUserMaster").focus();
                return;
            }
            else if (userMasterDetailScope.Password == null || userMasterDetailScope.Password == "") {
                toastr.warning("Password is Required", warningTitle);
                $("#txtpassword").focus();
            }
            else if (userMasterDetailScope.RoleId == null || userMasterDetailScope.RoleId == "") {
                toastr.warning("UserRole is Required", warningTitle);
                $("#txtrolename").focus();
            }
            //if (!$rootScope.permission.CanWrite) { return; }
            if (frmUserMaster.$valid) {

                UserMasterService.SaveuserMasterDetails(userMasterDetailScope).then(function (res) {
                    if (res) {
                        debugger
                        var data = res.data;
                        if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                            $scope.ClearFormData(frmUserMaster);
                            toastr.success(data.Message, successTitle);
                            $scope.tableParams.reload();
                        } else if (data.MessageType == messageTypes.Error) {// Error
                            toastr.error(data.Message, errorTitle);
                        } else if (data.MessageType == messageTypes.Warning) {// Warning
                            toastr.warning(data.Message, warningTitle);
                        }
                    }
                });
            }
        };

        // BEGIN Bind form data for edit UserMaster
        $scope.EditUserMasterDetails = function (UserId) {
            
            UserMasterService.GetUserMasterById(UserId).then(function (res) {
                if (res) {
                    
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success) {// Success
                        $scope.userMasterDetailScope = data.Result;
                        $scope.lastStorageAudit = angular.copy(data.Result);
                        CommonFunctions.ScrollUpAndFocus("txtUserMaster");
                    } else if (data.MessageType == messageTypes.Error) {// Error
                        toastr.error(data.Message, errorTitle);
                    }
                }
                $rootScope.isAjaxLoadingChild = false;
            });
        };

     
        // Reset UserMaster form data; edit + reset will remove all changes made in edit mode.
        $scope.resetuserMasterDetails = function (frmUserMaster) {
            if ($scope.operationMode() == "Update") {
                $scope.frmUserMaster = angular.copy($scope.lastStorageGroup);
                frmUserMaster.$setPristine();
            } else {
                $scope.clearData(frmUserMaster);
            }
        };

        //Get the userRole Dropdown
        $scope.roleScope = function () {
            UserMasterService.getrolelist()
                .then(function (res) {
                    $scope.Role = res.data.Result;
                });
        }

        // Initialize Role List
        $scope.Init = function () {
            $scope.roleScope();
        }

        // Clear UserMaster form data.
        $scope.ClearFormData = function (frmUserMaster) {
            $scope.userMasterDetailScope = {
                UserId: 0,
                Email: '',
                Password: '',
                RoleId: 0,
                IsActive: true,
            };
            frmUserMaster.$setPristine();
            $("#txtUserMaster").focus();
            CommonFunctions.ScrollToTop();
        };

       
        //Load UserMaster List
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: $rootScope.pageSize,
            sorting: { Email: 'asc' }
        }, {
            getData: function ($defer, params) {
                if (userMasterDetailParams == null) {
                    userMasterDetailParams = {};
                }
                userMasterDetailParams.Paging = CommonFunctions.GetPagingParams(params);
                userMasterDetailParams.Paging.Search = $scope.isSearchClicked ? $scope.search : '';
                
                //Load Employee List
                UserMasterService.GetAllUserMasters(userMasterDetailParams.Paging).then(function (res) {
                    var data = res.data;
                    if (res.data.MessageType == messageTypes.Success) {// Success
                        $defer.resolve(res.data.Result.list);
                        if (res.data.Result.list.length == 0) {
                        } else {
                            params.total(res.data.Result.Total);
                        }
                    } else if (res.data.MessageType == messageTypes.Error) {// Error
                        toastr.error(res.data.Message, errorTitle);
                    }
                    $rootScope.isAjaxLoadingChild = false;
                    CommonFunctions.SetFixHeader();
                });
            }
        });
    }
})();