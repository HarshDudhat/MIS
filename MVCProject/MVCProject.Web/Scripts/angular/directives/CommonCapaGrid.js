(function () {
    'use strict'

    angular.module("DorfKetalMVCApp").service("CommonCapaGridService", ["$http", "$rootScope",
    function ($http, $rootScope) {
        var list = {};

        list.GetTasks = function (taskTypeId, taskReferenceId) {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/common/GetTasksByTaskType?taskTypeId=" + taskTypeId + "&taskReferenceId=" + taskReferenceId
            });
        };

        list.GetTaskStatus = function () {
            return $http({
                method: "GET",
                url: $rootScope.apiURL + "/Task/GetTaskStatus"
            });
        }

        list.AddCAPA = function (task) {
            return $http({
                method: "POST",
                url: $rootScope.apiURL + "/Common/AddCAPA",
                data: JSON.stringify(task)
            });
        };

        list.EditCAPA = function (task) {
            return $http({
                method: "PUT",
                url: $rootScope.apiURL + "/Common/EditCAPA",
                data: JSON.stringify(task)
            });
        };

        list.DeleteCAPA = function (taskId) {
            return $http({
                method: "DELETE",
                url: $rootScope.apiURL + "/Common/DeleteCAPA?taskId=" + taskId
            });
        };

        return list;
    } ]);

    angular.module("DorfKetalMVCApp").controller("CommonCapaGridCtrl", ["$scope", "$rootScope", "$filter", "$routeParams", "$uibModal", "CommonCapaGridService", "CommonEnums", "CommonFunctions",
    function ($scope, $rootScope, $filter, $routeParams, $uibModal, CommonCapaGridService, CommonEnums, CommonFunctions) {

        $scope.tasks = [];
        //$scope.saveLocal = false;
        $scope.TaskStatus = CommonEnums.TaskStatus;
        $scope.scrollHeight = 76;
        $scope.editCount = 0;
        $scope.addOptions = { show: false };
        $scope.taskToAdd = {};
        $scope.isEntryFirst = true;
        $scope.CommonAttachment = {
            paraAttachmentsList: [],
            AttachmentsList: [],
            DocumentsList: [],
            AuditRefId: 0,
            ModuleId: '',
            IsReadOnly: false
        }
        $scope.defaultSettings = $rootScope.defaultSettings;
        $scope.dateOptions = $rootScope.dateOptions;
        $scope.capaTableId = "tblCapa-" + Math.floor((1 + Math.random()) * 0x10000);
        $scope.SetFixHeader = function () {
        };

        $scope.filterCriteria = {
            SiteLevelIds: '',
            FunctionLevelIds: '',
            AgencyLevelIds: '',
            IncludeFunction: $scope.isFunctionResponsible,
            IncludeAgency: $scope.isAgencyResponsible
        }

        $scope.SetHeight = function () {
            if ($scope.tasks == null) {
                $scope.tasks = [];
            }
            if ($scope.tasks != 'undefined' && $scope.tasks.length > 0) {
                $scope.scrollHeight = 39 + (39 * $scope.tasks.length) + (8 * $scope.editCount);
                var height = $scope.addOptions.show ? 48 : 0;
            } else {
                $scope.scrollHeight = 39 + 39;
                var height = $scope.addOptions.show ? 12 : 0;
            }
            $scope.scrollHeight = $scope.scrollHeight + height;
            if ($scope.scrollHeight > 240) {
                $scope.scrollHeight = 240;
            }
        };

        $scope.$watch('siteId', function (newValue, oldValue) {
            //if (newValue === oldValue) {
            //    return;
            //}
            $scope.filterCriteria = {
                SiteLevelIds: $scope.siteId,
                FunctionLevelIds: '',
                AgencyLevelIds: '',
                IncludeFunction: $scope.isFunctionResponsible,
                IncludeAgency: $scope.isAgencyResponsible
            }
            //$scope.employeeAutoCompleteUrl = EmployeeMasterService.AutoCompleteResponsibleList($scope.filterCriteria);
        }, true);

        //$scope.employeeAutoCompleteUrl = EmployeeMasterService.AutoCompleteResponsibleList($scope.filterCriteria);
        $scope.responsibleAdd = function (selected, taskToAdd) {
            taskToAdd.IsTaskStatusDisable = false;
            if (angular.isDefined(selected)) {
                if (selected.originalObject.ResponsibleType != CommonEnums.TaskResponsibleType.Employee) {
                    taskToAdd.TaskStatusId = CommonEnums.TaskStatus.Active;
                    taskToAdd.IsTaskStatusDisable = true;
                }
                taskToAdd.responsibleAdd = selected;
            }
        };
        $scope.responsibleEdit = function (selected, taskToEdit) {
            taskToEdit.IsTaskStatusDisable = false;
            if (angular.isDefined(selected)) {
                if (selected.originalObject.ResponsibleType != CommonEnums.TaskResponsibleType.Employee) {
                    taskToEdit.TaskStatusId = CommonEnums.TaskStatus.Active;
                    taskToEdit.IsTaskStatusDisable = true;
                }
                taskToEdit.responsibleEdit = selected;
            }
        };


        $scope.$watch('tasks', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }

            $scope.SetHeight();
        }, true);

        var taskStatus = CommonEnums.toArray(CommonEnums.TaskStatus);
        $scope.TaskStatusEnum = angular.copy(taskStatus);
        $scope.taskStatus = $.map(taskStatus, function (val) { if (val.Id <= 3) return val; });
        $scope.taskPriorityList = CommonEnums.toArray(CommonEnums.TaskPriority);
        $scope.taskPriorityDaysList = CommonEnums.TaskPriorityDays;
        $scope.taskCAPATaskTypeList = CommonEnums.toArray(CommonEnums.CAPATaskType);

        $scope.PriorityChange = function (Task) {
            var currentDate = new Date();
            if (Task.TaskPriority > 0) {
                var PriorityName = $filter('filter')($scope.taskPriorityList, { Id: Task.TaskPriority })[0].Name;
                if (PriorityName) {
                    var DatysToAdd = $scope.taskPriorityDaysList[PriorityName];
                    currentDate.setDate(currentDate.getDate() + DatysToAdd);
                    var TDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());
                    Task.TaskDueDate = TDate;
                }
                else {
                    Task.TaskDueDate = null;
                }
            }
            else {
                Task.TaskDueDate = null;
            }
        };

        var GetTasks = function (taskTypeId, taskReferenceId) {
            if (angular.isDefined(taskTypeId) && angular.isDefined(taskReferenceId)) {
                CommonCapaGridService.GetTasks(taskTypeId, taskReferenceId).then(function (response) {
                    if (response && response.data) {
                        var data = response.data;
                        if (data.MessageType == messageTypes.Success) {
                            if (data.Result) {
                                $scope.tasks = data.Result;
                                $scope.tasks.TaskDueDate = new Date(data.Result.TaskDueDate + "Z");
                            }                            
                        }
                        else {
                            showtoastr(data.Message, data.MessageType);
                        }
                    }
                    $scope.SetHeight();
                    $scope.SetFixHeader();
                });
            }

        };

        var Init = function () {
            if (!angular.isDefined($scope.addType)) {
                $scope.addType = true;
            }
            if (!angular.isDefined($scope.addReviewer)) {
                $scope.addReviewer = true;
            }
            GetTasks($scope.taskTypeId, $scope.taskReferenceId);

            if (!angular.isDefined($scope.siteId)) {
                $scope.siteId = '';
            }
        };

        $scope.AddClick = function () {
            $scope.taskToAdd = {};
            $scope.taskToAdd.TaskStatusId = 1;
            $scope.taskToAdd.SiteLevelId = $scope.siteId;
            $scope.taskToAdd.TaskStartDate = angular.copy(moment($scope.startDate).format($rootScope.apiDateFormat));
            $scope.addOptions.show = !$scope.addOptions.show;
            $scope.SetHeight();
            $scope.SetFixHeader();

            setTimeout(function () {
                $("#txtTaskDescription" + $scope.capaTableId).focus()
            }, 200);

        };

        $scope.HideAddClick = function () {
            $scope.addOptions.show = !$scope.addOptions.show;
            $scope.SetHeight();
            $scope.SetFixHeader();
        };

        $scope.EditClick = function (options, t, taskToEdit) {
            t.IsEdit = true;
            options.editMode = !options.editMode;
            angular.copy(t, taskToEdit);
            $scope.editCount = $scope.editCount + 1;
            taskToEdit.TaskDueDate = $filter("toDate")($filter("toDateString")(taskToEdit.TaskDueDate, 'UtcTolocalDate'));
            $scope.SetHeight();
            $scope.SetFixHeader();

            if (taskToEdit.ResponsibleType != CommonEnums.TaskResponsibleType.Employee) {
                taskToEdit.IsTaskStatusDisable = true;
            }
            else {
                taskToEdit.IsTaskStatusDisable = false;
            }

            setTimeout(function () {
                $("#txtTaskDescription" + t.index).focus()
            }, 200);

        };



        $scope.OpenAttachments = function (task) {
            for (var i = 0; i < $scope.tasks.length; i++) {
                //$scope.tasks[i].index = i + 1;
                //$scope.tasks[i].AttachmentsList = [];
            }
            $scope.OpenAttachmentsPopup(CommonEnums.ModuleName.EHSObservation, task.index, $scope.readOnly, $scope.tasks);
        }

        // AttachmentsToCAPA Popup Controller function
        $scope.AttachmentsToCAPAPopupCtrl = function ($scope, $rootScope, $uibModalInstance, moduleId, referenceId, tasks, isReadOnly, CommonService) {
            $scope.isReadOnly = isReadOnly;
            $scope.moduleId = parseInt(moduleId);
            $scope.referenceId = referenceId;
            $scope.TotalCount = 0;
            //Init
            $scope.entity = {
                ModuleId: moduleId,
                DocumentsList: [],
                AttachmentsList: [],
                paraAttachmentsList: []
            }

            $scope.tasks = tasks;

            //Get Audit Compliance Attachments
            $scope.GetAttachments = function () {
                if ($scope.tasks[referenceId].AttachmentsList != undefined) {
                    var newAttachment = $filter('filter')($scope.tasks[referenceId].AttachmentsList, { 'IsDeleted': false });
                    if (newAttachment.length > 0) {
                        var i = 1;
                        $scope.entity.DocumentsList = [];

                        for (var a = 0; a < newAttachment.length; a++) {
                            // newAttachment[a].AttachmentId = i++;
                            $scope.entity.DocumentsList.push(newAttachment[a]);
                        }
                    }
                    else {
                        $scope.entity.DocumentsList = [];
                        $scope.entity.DocumentsList = $scope.tasks[referenceId].AttachmentsList;
                    }
                    $scope.tasks[referenceId].AttachmentsList = [];
                    $scope.tasks[referenceId].AttachmentsList = $scope.entity.DocumentsList;
                    $scope.tasks[referenceId].AttchmentCount = $scope.entity.DocumentsList.length;
                }
                else if ($scope.tasks[referenceId].DocumentsList != undefined) {
                    $scope.entity.DocumentsList = $scope.tasks[referenceId].DocumentsList;
                }
            } ();

            //Save  Attachments 
            $scope.SaveAttachments = function () {
                var temp = [];
                var paratemp = [];
                for (var i = 0; i < $scope.entity.DocumentsList.length; i++) {
                    $scope.entity.DocumentsList[i].ModuleId = $scope.moduleId;
                    $scope.entity.DocumentsList[i].ReferenceId = $scope.referenceId;
                    $scope.entity.DocumentsList[i].AttachmentPath = $scope.entity.DocumentsList[i].FileRelativePath;
                    temp.push($scope.entity.DocumentsList[i]);
                }
                var deleteAttachmentsList = $filter('filter')($scope.entity.AttachmentsList, { 'IsDeleted': true });
                $scope.entity.AttachmentsList = $filter('filter')($scope.entity.AttachmentsList, { 'IsDeleted': false });
                for (var i = 0; i < $scope.entity.AttachmentsList.length; i++) {
                    $scope.entity.AttachmentsList[i].ModuleId = $scope.moduleId;
                    $scope.entity.AttachmentsList[i].ReferenceId = $scope.referenceId;
                    $scope.entity.AttachmentsList[i].AttachmentPath = $scope.entity.AttachmentsList[i].FileRelativePath;
                    temp.push($scope.entity.AttachmentsList[i]);
                }
                var paraAttachmentsList = $filter('filter')(temp, { 'IsDeleted': false });
                paratemp = angular.copy(temp);
                for (var i = 0; i < paratemp.length; i++) {
                    if (paratemp[i].AttachmentId > 0) {
                        var objdeleted = $filter('filter')(deleteAttachmentsList, { 'AttachmentId': paratemp[i].AttachmentId });
                        var obj = $filter('filter')(paratemp, { 'AttachmentId': paratemp[i].AttachmentId });
                        if (objdeleted.length == 0) {
                            var index = -1;
                            temp.some(function (objtemp, i) {
                                return objtemp.AttachmentId === obj[0].AttachmentId ? index = i : false;
                            });
                            temp.splice(index, 1);
                        }
                        else {
                            var index = -1;
                            paraAttachmentsList.some(function (objtemp, i) {
                                return objtemp.FileName === obj[0].FileName ? index = i : false;
                            });
                            paraAttachmentsList.splice(index, 1);
                        }
                    }
                    else {
                        var objdeleted = $filter('filter')(deleteAttachmentsList, { 'FileName': paratemp[i].FileName });
                        if (objdeleted.length > 0) {
                            var index = -1;
                            temp.some(function (objtemp, i) {
                                return objtemp.FileName === objdeleted[0].FileName ? index = i : false;
                            });
                            temp.splice(index, 1);
                            index = -1;
                            paraAttachmentsList.some(function (objtemp, i) {
                                return objtemp.FileName === objdeleted[0].FileName ? index = i : false;
                            });
                            paraAttachmentsList.splice(index, 1);
                        }
                    }
                }
                //console.log($scope.entity);
                $scope.tasks[referenceId].AttachmentsList = [];
                $scope.tasks[referenceId].paraAttachmentsList = [];
                $scope.tasks[referenceId].AttachmentsList = paraAttachmentsList;
                $scope.tasks[referenceId].AttchmentCount = paraAttachmentsList.length;
                $scope.tasks[referenceId].paraAttachmentsList = temp;
                $scope.tasks[referenceId].paraAttchmentCount = temp.length;
                $uibModalInstance.close($scope.TotalCount);
            };

            $scope.OKClick = function () {
                $scope.SaveAttachments();
            };

            $scope.CancelClick = function () {
                $uibModalInstance.close($scope.TotalCount);
            };
        }

        //Open  Attachments Popup
        $scope.OpenAttachmentsPopup = function (moduleId, referenceId, isReadOnly, tasks) {
            var modalInstance = $uibModal.open({
                animation: '',
                templateUrl: '/Template/_AttachmentsPopup',
                controller: $scope.AttachmentsToCAPAPopupCtrl,
                size: 'lg',
                keyboard: true,
                resolve: {
                    moduleId: function () {
                        return moduleId
                    },
                    referenceId: function () {
                        return referenceId;
                    },
                    isReadOnly: function () {
                        return isReadOnly;
                    },
                    tasks: function () {
                        return tasks;
                    }
                }
            });

            modalInstance.result.then(function (Total) {
                //                if ((Total != 'undefined') && moduleId == CommonEnums.ModuleName.CommonInternalAudit) {
                //                    if (Total.length > 0) {
                //                        $rootScope.MasterAttachmentsCount = Total;
                //                    } else if (Total > 0) {
                //                        $rootScope.MasterAttachmentsCount = Total;
                //                    }
                //                    else {
                //                        $rootScope.MasterAttachmentsCount = 0;
                //                    }
                //                } else if (angular.isDefined(Total) && angular.isDefined(reference) && angular.isDefined(reference.AttchmentCount)) {
                //                    reference.AttchmentCount = Total;
                //                }

                //toastr.success(Result.Data, successTitle);
            }, function () {
                //toastr.warning('Modal dismissed at: ' + new Date(), warningTitle);
            });
        };

        $scope.HideEditClick = function (t) {
            t.IsEdit = false;
            $scope.editCount = $scope.editCount - 1;
            $scope.SetHeight();
        };

        $scope.SaveCapa = function (frm, task, responsible, reviewer, options, txtResponsibleId, txtReviewerId) {
            if (frm.$valid) {
                task = angular.copy(task);
                task.TaskReferenceId = $scope.taskReferenceId;
                task.TaskTypeId = $scope.taskTypeId;
                task.IsNewIncidentTask = false;

                task.SiteId = $scope.siteId;
                task.IsEdit = false;

                if ($scope.addType == false) {
                    task.TaskCAPAType = 1;
                }

                if ($scope.taskTypeId == CommonEnums.TaskType.Audit_CAPA) {
                    task.TaskSubject = "CAPA of Audit";
                }

                var isSameResposibleAndReviewer = false;

                if (angular.isDefined(responsible)) {
                    var employee = responsible.originalObject;
                    task.TaskAssignTo = employee.EmployeeId;
                    task.Responsible = employee.Name;
                    task.ResponsibleType = employee.ResponsibleType;
                }
                else if ($("#" + txtResponsibleId + "_value").val() != task.Responsible) {
                    task.TaskAssignTo = null;
                    //task.Responsible = $("#" + txtResponsibleId + "_value").val();
                }

                //if (task.TaskAssignTo == null) {
                //    toastr.warning("Responsible Person Name is required", warningTitle);
                //    return;
                //}
                if (task.TaskCAPAType == null) {
                    toastr.warning("Task type is required", warningTitle);
                    return;
                }

                if (angular.isDefined(reviewer)) {
                    var employee = reviewer.originalObject;
                    task.TaskReviewBy = employee.EmployeeId;

                }
                else if ($("#" + txtReviewerId + "_value").val() != task.Reviewer) {
                    task.TaskReviewBy = null;
                    task.Reviewer = $.trim($("#" + txtReviewerId + "_value").val());
                }

                var taskAssignToId = (typeof task.TaskAssignTo == "string") ? parseInt(task.TaskAssignTo.split(",").join("")) : task.TaskAssignTo;
                var taskReviewById = (typeof task.TaskReviewBy == "string") ? parseInt(task.TaskReviewBy.split(",").join("")) : task.TaskReviewBy;

                if (options.editMode == undefined) {
                    isSameResposibleAndReviewer = (!(!(taskReviewById > 0) && (task.Reviewer == "" || task.Reviewer == undefined) && !(taskAssignToId > 0) && (task.Responsible == "" || task.Responsible == undefined))) && (
                (taskReviewById > 0 && taskAssignToId > 0 && taskReviewById == taskAssignToId) || (task.Reviewer != undefined && task.Reviewer != "" && task.Responsible != undefined && task.Responsible != "" && task.Reviewer == task.Responsible));
                }
                else {
                    if (responsible == undefined || reviewer == undefined) {
                        isSameResposibleAndReviewer = (!(!(taskReviewById > 0) && (task.Reviewer == "" || task.Reviewer == undefined) && !(taskAssignToId > 0) && (task.Responsible == "" || task.Responsible == undefined))) && (
                (taskReviewById > 0 && taskAssignToId > 0 && taskReviewById == taskAssignToId) || (task.Reviewer != undefined && task.Reviewer != "" && task.Responsible != undefined && task.Responsible != "" && task.Reviewer == task.Responsible));
                    }
                    else {
                        if (responsible.title == reviewer.title) {
                            isSameResposibleAndReviewer = true;
                        }
                    }
                }

                if (isSameResposibleAndReviewer) {
                    toastr.warning("Responsible & reviewer person can not be same", warningTitle);
                    return;
                }

                if (angular.isDefined($scope.startDate) && moment(task.TaskDueDate).startOf("day").toDate() < moment($scope.startDate).startOf("day").toDate()) {
                    toastr.warning("Target date should be greater than or equal to " + moment($scope.startDate).format($rootScope.apiDateFormat) + " date", warningTitle);
                    return;
                }

                if (angular.isDefined($scope.currentDate) && task.TaskStatusId > 0 && (task.TaskStatusId == CommonEnums.TaskStatus.Active) && !(moment(task.TaskDueDate).startOf("day").toDate() >= moment($scope.currentDate).startOf("day").toDate())) {
                    toastr.warning("Target date should be greater than or equal to current date for active status", warningTitle);
                    return;
                }

                if (angular.isDefined($scope.currentDate) && task.TaskStatusId > 0 && (task.TaskStatusId == CommonEnums.TaskStatus.Completed || task.TaskStatusId == CommonEnums.TaskStatus.Close) && (moment(task.TaskDueDate).startOf("day").toDate() > moment($scope.currentDate).startOf("day").toDate())) {
                    toastr.warning("Target date should be less than or equal to current date for completed and close status", warningTitle);
                    return;
                }

                if (angular.isDefined(task.TaskDueDate) && task.TaskDueDate != '') {
                    //task.TaskDueDate = $filter('date')(task.TaskDueDate, 'yyyy-MM-dd');
                    task.TaskDueDate = angular.copy(moment(task.TaskDueDate).format($rootScope.apiDateFormat));
                    task.InitialTaskDueDate = angular.copy(task.TaskDueDate);
                }
                if (angular.isUndefined(task.TaskPriority) || task.TaskPriority == null) {
                    task.TaskPriority = CommonEnums.TaskPriority.Low;
                }

                if ($scope.saveLocal == true) {
                    if (task.TaskId > 0 || options.editMode) {
                        //task.Responsible = $("#" + txtResponsibleId + "_value").val();
                        //task.InitialResponsible = $("#" + txtResponsibleId + "_value").val();
                        //task.Reviewer = $("#" + txtReviewerId + "_value").val();
                        task.TaskPriority = task.TaskPriority == null ? CommonEnums.TaskPriority.Low : task.TaskPriority;
                        var oldTask = (task.TaskId > 0) ? $filter("filter")($scope.tasks, { TaskId: task.TaskId }, true)[0] : $filter("filter")($scope.tasks, { index: task.index }, true)[0];
                        angular.copy(task, oldTask);
                        options.editMode = false;
                        $scope.editCount = $scope.editCount - 1;
                    }
                    else {
                        //task.TaskId = data.Result;
                        //task.Responsible = $("#" + txtResponsibleId + "_value").val();
                        //task.InitialResponsible = $("#" + txtResponsibleId + "_value").val();
                        //task.Reviewer = $("#" + txtReviewerId + "_value").val();
                        task.TaskPriority = task.TaskPriority == null ? CommonEnums.TaskPriority.Low : task.TaskPriority;
                        $scope.tasks.push(task);
                        options.show = false;
                    }
                    $scope.SetHeight();
                    $scope.SetFixHeader();
                }
                else {

                    var serviceCall;
                    if (task.TaskId > 0) {
                        // update
                        serviceCall = CommonCapaGridService.EditCAPA(task);
                    }
                    else {
                        // add
                        serviceCall = CommonCapaGridService.AddCAPA(task);
                    }

                    serviceCall.then(function (response) {
                        if (response && response.data) {
                            var data = response.data;
                            showtoastr(data.Message, data.MessageType);
                            if (data.MessageType == messageTypes.Success) {
                                task.Responsible = $("#" + txtResponsibleId + "_value").val();
                                task.InitialResponsible = $("#" + txtResponsibleId + "_value").val();
                                task.Reviewer = $("#" + txtReviewerId + "_value").val();

                                if (task.TaskId > 0) {
                                    var oldTask = $filter("filter")($scope.tasks, { TaskId: task.TaskId }, true)[0];
                                    angular.copy(task, oldTask);
                                    options.editMode = false;
                                    $scope.editCount = $scope.editCount - 1;
                                }
                                else {
                                    task.TaskId = data.Result;
                                    $scope.tasks.push(task);
                                    options.show = false;
                                }
                            }
                        }
                        $scope.SetHeight();
                        $scope.SetFixHeader();
                    });
                }
            }
        };

        $scope.DeleteCapa = function (task) {
            //if (confirm("Are you sure you want to delete task?")) {
            if ($scope.saveLocal == true) {
                var i = $scope.tasks.indexOf(task);
                $scope.tasks.splice(i, 1);
                $scope.SetFixHeader();
                $scope.SetHeight();
            }
            else {
                CommonCapaGridService.DeleteCAPA(task.TaskId).then(function (response) {
                    if (response && response.data) {
                        var data = response.data;
                        if (data.MessageType == messageTypes.Success) {
                            var i = $scope.tasks.indexOf(task);
                            $scope.tasks.splice(i, 1);
                        }
                        else {
                            showtoastr(data.Message, data.MessageType);
                        }
                    }
                    $scope.SetHeight();
                    $scope.SetFixHeader();
                });
            }
        }
        //}

        Init();
        $scope.SetFixHeader();
    } ]);

    //CommonCapaGrid
    angular.module("DorfKetalMVCApp").directive("commonCapaGrid", function () {
        return {
            restrict: 'E',
            scope: {
                taskTypeId: "=",
                taskReferenceId: "=",
                siteId: "=",
                readOnly: "=",
                startDate: "=",
                currentDate: "=",
                moduleName: "=",
                saveLocal: "=",
                tasks: "=",
                displayFixHeader: '=',
                rowNo: '=',
                tabIndex: '=',
                addAttachment: '=',
                addType: '=',
                addReviewer: "=",
                isAgencyResponsible: "=",
                isFunctionResponsible: "=",
                isAutocompleteShowDetail: "@"
            },
            controller: "CommonCapaGridCtrl",
            templateUrl: "/Template/_CommonCapaGrid",
            compile: function (element, attrs) {
                if (!attrs.isAutocompleteShowDetail) { attrs.isAutocompleteShowDetail = false; }
            }
        };
    });

})();