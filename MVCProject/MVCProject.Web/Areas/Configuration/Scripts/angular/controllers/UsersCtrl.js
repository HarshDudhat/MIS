(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").controller("UsersCtrl", [
            "$scope", "$rootScope", "$filter", "ngTableParams", "CommonFunctions", "FileService", "UsersService", UsersCtrl
        ]);

    function UsersCtrl($scope, $rootScope, $filter, ngTableParams, CommonFunctions, FileService, UsersService) {
        $scope.user = {
            UserId: 0,
            UserName: '',
            Password: '',
            UserRole: [],
            IsActive: true,
            EmployeeId: '',
            Employee: {},
            UserArea: []
        };
        $scope.roles = [];
        $scope.sites = [];
        $scope.departments = [];
        $scope.areas = [];
        $scope.IsActive = true;
        var isFirst = true;
        $scope.isSearchClicked = false;
        $scope.roleMultiSelectSettings = {
            idProp: 'RoleId',
            displayProp: 'RoleName',
            externalIdProp: 'RoleId',
            scrollable: true
        };
        $scope.areaMultiSelectSettings = {
            idProp: 'AreaId',
            displayProp: 'AreaName',
            externalIdProp: 'AreaId',
            scrollable: true
        };

        // Get Roles
        var GetRoles = function () {
            UsersService.GetRoles().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.roles = data.Result;
                }
            });
        };

        //Get sites
        var GetSites = function () {
            UsersService.GetSites().then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.sites = data.Result;
                }
            });
        };

        //Get departments -- for dropdows
        var GetDepartmentsBySite = function (siteId) {
            UsersService.GetDepartmentsBySite(siteId).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.departments = data.Result;
                }
            });
        };

        // Get Users Area
        var GetAreaBySite = function (siteId) {
            UsersService.GetAreaBySite(siteId).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.areas = data.Result;
                }
            });
        };

        // Get Users
        var GetUsers = function () {
            if ($scope.tableParams == undefined) {
                $scope.tableParams = new ngTableParams({
                    page: 1,
                    count: $rootScope.pageSize,
                    sorting: { UserName: 'asc' }
                }, {
                    getData: function ($defer, params) {
                        var isAscending = params.orderBy().toString().charAt(0) == "+" ? true : false;
                        var orderBy = isAscending == true ? params.orderBy()[0].replace("+", "") : params.orderBy()[0].replace("-", "");

                        var param = {};
                        param.ApplyPaging = true;
                        param.CurrentPageNumber = params.page();
                        param.PageSize = params.count();
                        param.OrderByColumn = orderBy;
                        param.IsAscending = isAscending;
                        param.Search = $scope.isSearchClicked ? $scope.search : '';

                        UsersService.GetUsers(param).then(function (response) {
                            var data = response.data;
                            if (data.MessageType == messageTypes.Success) {
                                params.total(data.Total);
                                $defer.resolve(data.Result);
                            }
                            else {
                                toastr.error(data.Message, "Error");
                            }
                            $rootScope.IsAjaxLoading = false;
                            CommonFunctions.SetFixHeader();
                        });

                        if (isFirst) {
                            isFirst = false;
                            var $parentDiv = $("[ng-table-pagination]").parent().parent();
                            var $pagination = $("[ng-table-pagination]").detach();
                            $parentDiv.append($pagination);
                        }
                    }
                });
            }
            else {
                $scope.tableParams.reload();
            }
        };

        // on site selection change - update users and department dropdown
        $scope.OnSiteChange = function (siteId) {
            if (siteId > 0) {
                $scope.departments = [];
                $scope.user.DepartmentId = '';
                GetDepartmentsBySite(siteId);
            }
        };

        // Get User
        $scope.GetUser = function (userId) {
            UsersService.GetUser(userId).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    $scope.user = data.Result;
                    $scope.user.ConfirmPassword = $scope.user.Password;
                    GetDepartmentsBySite($scope.user.SiteId);
                    GetAreaBySite($scope.user.SiteId);
                    CommonFunctions.ScrollUpAndFocus("txtUserName");
                }
                else {
                    toastr.error(data.Message);
                }
            });
        };

        // Save User
        $scope.SaveUser = function (frmUser, user) {
            if (!$rootScope.permission.CanWrite) { return; }
            if (frmUser.$valid) {
                if (user.UserRole.length == 0) {
                    toastr.warning("Select at least one role.");
                    return;
                }

                var param = angular.copy(user);

                var serviceCall;
                if (param.UserId > 0) {
                    serviceCall = UsersService.UpdateUser(param);
                }
                else {
                    serviceCall = UsersService.AddUser(param);
                }

                serviceCall.then(function (response) {
                    var data = response.data;
                    if (data.MessageType == messageTypes.Success) {
                        toastr.success(data.Message, successTitle);
                        GetUsers();
                        $scope.ClearForm(frmUser);
                    }
                    else {
                        toastr.error(data.Message);
                    }
                });
            } else {
                toastr.error("Invalid data.");
            }
        };

        // Clear Form
        $scope.ClearForm = function (frmUser) {
            $scope.user = {
                UserId: 0,
                UserName: '',
                Password: '',
                UserRole: [],
                SiteId: '',
                DepartmentId: '',
                IsActive: true,
                EmployeeId: '',
                Employee: {},
                UserArea: []
            };
            $scope.roleSearchFilter = "";
            $scope.areaSearchFilter = "";
            $scope.areas = [];
            frmUser.$setPristine();
            $("#txtUserName").focus();
            CommonFunctions.ScrollToTop();
        };

        //  Create Excel Report
        $scope.ExportToExcel = function () {
            if (!$rootScope.permission.CanWrite) { return; }
            UsersService.ExportToExcel().then(function (res) {
                var filename = "Users_" + $rootScope.fileDateName + ".xls"
                FileService.SaveBlob(res.data, filename, 'application/xls');
            });
        };

        // Select Employee
        $scope.SelectEmployee = function () {
            if (!$rootScope.permission.CanWrite) { return; }
            $scope.OpenPopupToSelectEmployees('Select Employee', [$scope.user.Employee], true, false, false, function (newSelectedEmployees) {
                if (angular.isDefined(newSelectedEmployees) && newSelectedEmployees.length > 0) {
                    $scope.user.Employee = angular.copy(newSelectedEmployees[0]);
                    $scope.user.EmployeeId = angular.copy($scope.user.Employee.EmployeeId);

                    UsersService.GetEmployee($scope.user.EmployeeId).then(function (response) {
                        var data = response.data;
                        $scope.user.UserName = angular.copy(response.data.Result.EmpContactNo);
                        if (data.MessageType == messageTypes.Success) {
                            GetAreaBySite(data.Result.SiteId); ;
                        }
                        else {
                            toastr.error(data.Message);
                        }
                    });
                }
            });
        };

        //Get Employee By Id 
        var GetEmployee = function (employeeId) {
            UsersService.GetEmployee(employeeId).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    return data.Result;
                }
                else {
                    toastr.error(data.Message);
                }
            });
        };

        // Init
        var Init = function () {
            GetSites();
            GetRoles();
            GetUsers();
        };

        Init();
    }
})();