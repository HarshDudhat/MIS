
(function () {
    angular.module("DorfKetalMVCApp").controller("SetReminderPopupCtrl", ["$scope", "$rootScope", "$filter", "$uibModalInstance", "ngTableParams", "EmployeeMasterService", "EmailReminder", "title", "CommonFunctions",
    function ($scope, $rootScope, $filter, $uibModalInstance, ngTableParams, EmployeeMasterService, EmailReminder, title, CommonFunctions) {
        // Declare-Initialize Scope and Root Scope Variable   
        $scope.title = title;
        $scope.result = EmailReminder;
        $scope.isEditRecord = true;
        $scope.employeeAutoCompleteUrl = EmployeeMasterService.EmployeeAutoCompleteUrl($rootScope.userContext.SiteId);
        $scope.minDate = new Date();

        //Get Employee By Id 
        var GetEmployee = function (employeeId, callBack) {
            EmployeeMasterService.GetEmployee(employeeId).then(function (response) {
                var data = response.data;
                if (data.MessageType == messageTypes.Success) {
                    callBack(data.Result);
                }
                else {
                    toastr.error(data.Message);
                }
            });
        };

        //Disable Alerts
        $scope.DisableAlerts = function () {
            if ($scope.result.IsActive == false)
                $scope.result.IsActive = true;
            else
                $scope.result.IsActive = false;

            //$uibModalInstance.close($scope.result);
        };

        //// START action for reminder alert employees
        $scope.selectedEmployeeTableParams = new ngTableParams({
            useExternalSorting: true,
            sorting: { Name: 'desc' },
            page: 1,
            count: $rootScope.pageSize
        }, {
            counts: [],
            total: 0,
            getData: function ($defer, params) {

                params.settings().counts = 0;
                var isAscending = params.orderBy().toString().charAt(0) == "+" ? true : false;
                var orderBy = isAscending == true ? params.orderBy()[0].replace("+", "") : params.orderBy()[0].replace("-", "");
                var sortDirection = isAscending == true ? 'asc' : 'desc';

                var filterData = $filter('filter')($scope.result.EmailReminderEmployee, { IsEscalation: false });
                $defer.resolve(filterData, params.orderBy());

                $("div[ng-table-pagination=params]").hide();
                if (filterData.length > 0) {
                    $scope.scrollHeightSelected = 36 + 39.5 * filterData.length;
                    if ($scope.scrollHeightSelected > 232) {
                        $scope.scrollHeightSelected = 232;
                    }
                } else {
                    $scope.scrollHeightSelected = 71;
                }
            }
        });

        $scope.selectedReminderAlertEmployee = {};

        // set Escalation Employee By Name
        $scope.SetReminderAlertByName = function (selected) {
            if (angular.isDefined(selected)) {
                var employee = selected.originalObject;
                GetEmployee(employee.EmployeeId, function (employeeData) {
                    $scope.selectedReminderAlertEmployee.EmployeeId = employeeData.EmployeeId;
                    $scope.selectedReminderAlertEmployee.Name = employeeData.Name;
                    $scope.selectedReminderAlertEmployee.DesignationName = employeeData.DesignationName;
                    $scope.selectedReminderAlertEmployee.SiteName = employeeData.SiteName;
                    $scope.selectedReminderAlertEmployee.DepartmentName = employeeData.DepartmentName;
                });

                $scope.selectedReminderAlertEmployee.IsEscalation = false;
                $scope.selectedReminderAlertEmployee.IsActive = true;
            }
            else {
                $scope.selectedReminderAlertEmployee = {};
            }
        };

        // Add Escalation Employee
        $scope.AddReminderAlert = function (e) {
            $scope.result.EmailReminderEmployee.push($scope.selectedReminderAlertEmployee);

            $scope.selectedEmployeeTableParams.reload();
            $scope.options.showAddReminderAlerts = false;

            $scope.selectedReminderAlertEmployee = {};
        };
        //// END action for reminder alert employees

        //// START action for escalation employees
        $scope.selectedEscalationEmployeeTableParams = new ngTableParams({
            useExternalSorting: true,
            sorting: { Name: 'desc' },
            page: 1,
            count: $rootScope.pageSize
        }, {
            counts: [],
            total: 0,
            getData: function ($defer, params) {

                params.settings().counts = 0;
                var isAscending = params.orderBy().toString().charAt(0) == "+" ? true : false;
                var orderBy = isAscending == true ? params.orderBy()[0].replace("+", "") : params.orderBy()[0].replace("-", "");
                var sortDirection = isAscending == true ? 'asc' : 'desc';

                var filterData = $filter('filter')($scope.result.EmailReminderEmployee, { IsEscalation: true });
                $defer.resolve(filterData, params.orderBy());

                $("div[ng-table-pagination=params]").hide();
                if (filterData.length > 0) {
                    $scope.scrollHeightSelected1 = 36 + 39.5 * filterData.length;
                    if ($scope.scrollHeightSelected1 > 232) {
                        $scope.scrollHeightSelected1 = 232;
                    }
                } else {
                    $scope.scrollHeightSelected1 = 71;
                }
            }
        });

        $scope.selectedEscalationEmployee = {};

        // set Escalation Employee By Name
        $scope.SetEscalationEmployeeByName = function (selected) {
            if (angular.isDefined(selected)) {
                var employee = selected.originalObject;
                GetEmployee(employee.EmployeeId, function (employeeData) {
                    $scope.selectedEscalationEmployee.EmployeeId = employeeData.EmployeeId;
                    $scope.selectedEscalationEmployee.Name = employeeData.Name;
                    $scope.selectedEscalationEmployee.DesignationName = employeeData.DesignationName;
                    $scope.selectedEscalationEmployee.SiteName = employeeData.SiteName;
                    $scope.selectedEscalationEmployee.DepartmentName = employeeData.DepartmentName;
                });

                $scope.selectedEscalationEmployee.IsEscalation = true;
                $scope.selectedEscalationEmployee.IsActive = true;
            }
            else {
                $scope.selectedEscalationEmployee = {};
            }
        };

        // Add Escalation Employee
        $scope.AddEscalationEmployee = function (employee) {
            $scope.selectedEscalationEmployee.EscalationDate = new Date(employee.EscalationDate + "Z");

            $scope.result.EmailReminderEmployee.push($scope.selectedEscalationEmployee);

            $scope.selectedEscalationEmployeeTableParams.reload();
            $scope.options.showAddEscalationEmployees = false;

            $scope.selectedEscalationEmployee = {};
        };

        // Edit Escalation Employee
        $scope.onEditEscalationEmployee = function (employee) {
            employee.DefaultEscalationDate = employee.EscalationDate;

            var date = convertUTCDateToLocalDate(new Date(employee.EscalationDate));

            employee.EscalationDate = date;
            employee.isEdit = true;

            $scope.isEditRecord = false;
        };

        $scope.onCancelEditEscalationEmployee = function (employee) {
            employee.EscalationDate = employee.DefaultEscalationDate;
            employee.isEdit = false;

            $scope.editedEscalationEmployee = {};
            $scope.isEditRecord = true;
        };

        $scope.EditEscalationEmployee = function (employee) {
            $scope.selectedEscalationEmployeeTableParams.reload();

            $scope.isEditRecord = true;
            employee.isEdit = false;
        };
        //// END action for escalation employees


        $scope.cancelActionToPerform = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.AddReminder = function () {
            $uibModalInstance.close($scope.result);
        };

        $scope.DeleteSelectedEmployee = function (id, val) {

            $.each($scope.result.EmailReminderEmployee, function (i, el) {
                if (this.EmployeeId == id && this.IsEscalation == val) {
                    $scope.result.EmailReminderEmployee.splice(i, 1);
                }
            });

            $scope.selectedEmployeeTableParams.reload();
            $scope.selectedEscalationEmployeeTableParams.reload();
        };


        function convertUTCDateToLocalDate(date) {
            var newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

            var offset = date.getTimezoneOffset() / 60;
            var hours = date.getHours();

            newDate.setHours(hours - offset);

            return newDate;
        }

        // Init()
        $scope.Init = function () {
        } ();
    } ]);


})();