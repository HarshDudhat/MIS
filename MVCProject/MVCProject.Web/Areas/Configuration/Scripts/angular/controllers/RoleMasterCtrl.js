(function () {
    'use strict';

    angular.module("DorfKetalMVCApp").controller('RoleMasterCtrl', [
            '$scope', '$rootScope', '$filter', 'RoleService', 'CommonFunctions', RoleMasterCtrl
        ]);

    // BEGIN RoleMasterCtrl
    function RoleMasterCtrl($scope, $rootScope, $filter, RoleService, CommonFunctions) {
        /* Initial Declaration */

        $scope.roleLeveles = [];
        $scope.roles = [];
        $scope.modules = [];
        $scope.role = {
            RoleId: '',
            RoleName: '',
            RoleLevelId: '',
            IsActive: 1,
            PageAccess: []
        };

        // Get Role Level
        $scope.GetRoleLevel = function () {
            RoleService.GetRoleLevel().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {// Success                
                    $scope.roleLeveles = data.Result;
                } else {// Error
                    toastr.error(data.Message, errorTitle);
                }
            });
        };

        // Get Roles
        $scope.GetRoles = function () {
            RoleService.GetRoles().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {// Success                
                    $scope.roles = data.Result;
                } else {// Error
                    toastr.error(data.Message, errorTitle);
                }
            });
        };

        // Get module pages
        $scope.GetModulePages = function () {
            RoleService.GetModulePages().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {// Success                
                    $scope.modules = data.Result;
                } else {// Error
                    toastr.error(data.Message, errorTitle);
                }
            });
        };

        // Init
        $scope.Init = function () {
            $scope.GetRoleLevel(true);
            $scope.GetRoles(true);
            $scope.GetModulePages();
        };
        $scope.Init();

        // Check/Uncheck pages
        $scope.CheckAll = function (module, prop) {
            angular.forEach(module.Pages, function (page) {
                page[prop] = module[prop];
                if (prop == 'CanWrite' && page.CanWrite == true) {
                    page.CanRead = true;
                    module.CanRead = true;
                } else if (prop == 'CanRead' && page.CanRead == false) {
                    page.CanWrite = false;
                    module.CanWrite = false;
                }
            });
        };

        // Set Check All
        $scope.SetCheckAll = function (page, module, prop) {
            module[prop] = $filter("filter")(module.Pages, JSON.parse('{"' + prop + '":true}'), true).length == module.Pages.length;
            if (prop == 'CanWrite' && page.CanWrite == true) {
                page.CanRead = true;
            } else if (prop == 'CanRead' && page.CanRead == false) {
                page.CanWrite = false;
            }
        };

        // Edit Role
        $scope.EditRole = function (roleId) {
            RoleService.EditRole(roleId).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {// Success                
                    $scope.role = data.Result;
                    $scope.SetPageAccess();
                    CommonFunctions.ScrollUpAndFocus("txtRoleName");
                } else {// Error
                    toastr.error(data.Message, errorTitle);
                }
            });
        };

        // Set Page Access
        $scope.SetPageAccess = function () {
            angular.forEach($scope.modules, function (module) {
                module.CanRead = false;
                module.CanWrite = false;
                angular.forEach(module.Pages, function (page) {
                    page.CanRead = false;
                    page.CanWrite = false;
                });
            });

            angular.forEach($scope.role.PageAccess, function (access) {
                angular.forEach($scope.modules, function (module) {
                    if (module.PageId == access.PageId) {
                        module.CanRead = access.CanRead ? true : false;
                        module.CanWrite = access.CanWrite ? true : false;
                    }

                    angular.forEach(module.Pages, function (page) {
                        if (page.PageId == access.PageId) {
                            page.CanRead = access.CanRead ? true : false;
                            page.CanWrite = access.CanWrite ? true : false;
                        }
                    });
                });
            });
        };

        // Save Role
        $scope.SaveRole = function (role, frmRole) {
            if (!$rootScope.permission.CanWrite) { return; }
            if (frmRole.$invalid) {
                return;
            }
            debugger

            role.PageAccesses = [];
            angular.forEach($scope.modules, function (module) {
                if (module.CanRead || module.CanWrite) {
                    role.PageAccesses.push(module);
                }
                angular.forEach(module.Pages, function (page) {
                    if (page.CanRead || page.CanWrite) {
                        role.PageAccesses.push(page);
                    }
                });
            });
            debugger

            RoleService.SaveRole(role).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {// Success                
                    toastr.success(data.Message, successTitle);
                    $scope.Init();
                    $scope.ClearForm(frmRole);
                    $("#txtRoleName").focus();
                } else {// Error
                    toastr.error(data.Message, errorTitle);
                }
            });
        };

        // Clear Form
        $scope.ClearForm = function (frmRole) {
            $scope.role = {
                RoleId: '',
                RoleName: '',
                RoleLevelId: '',
                IsActive: 1,
                PageAccess: []
            };
            $scope.SetPageAccess();
            frmRole.$setPristine();
            $("#txtRoleName").focus();
            CommonFunctions.ScrollToTop();
        };
    }
    // END RoleMasterCtrl
})();