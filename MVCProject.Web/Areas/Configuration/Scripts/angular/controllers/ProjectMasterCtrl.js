(function () {
    'use strict';

    angular.module("MVCApp").controller('ProjectMasterCtrl', [
        '$scope', '$rootScope', 'ngTableParams', 'CommonFunctions', 'FileService', 'ProjectMasterService', ProjectMasterCtrl
        ]);

    //BEGIN ProjectMasterCtrl
    function ProjectMasterCtrl($scope, $rootScope, ngTableParams, CommonFunctions, FileService, ProjectMasterService) {
        /* Initial Declaration */
        $scope.sampleDate = new Date();
        var projectDetailParams = {};

        $scope.projectDetailScope = {
            ProjectId: 0,
            VerticalId:0,
            ProjectName: '',
            IsActive: true
        };
        $scope.isSearchClicked = false;
        $scope.lastStorageAudit = $scope.lastStorageAudit || {};
        $scope.operationMode = function () {
            return $scope.projectDetailScope.ProjectId > 0 ? "Update" : "Save";
        };

        // BEGIN Add/Update Project details
        $scope.SaveProjectDetails = function (projectDetailScope, frmproject) {
            
            //if (!$rootScope.permission.CanWrite) { return; }
            if (frmproject.$valid) {
                ProjectMasterService.SaveProjectDetails(projectDetailScope).then(function (res) {
                    if (res) {
                        var data = res.data;
                        if (data.MessageType == messageTypes.Success && data.IsAuthenticated) {
                            $scope.ClearFormData(frmproject);
                            toastr.success(data.Message, successTitle);
                            //$scope.GetAllDesignations();
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

        // BEGIN Bind form data for edit Project
        $scope.EditProjectDetails = function (projectId) {
            ProjectMasterService.GetProjectById(projectId).then(function (res) {
                if (res) {
                    var data = res.data;
                    if (data.MessageType == messageTypes.Success) {// Success
                        $scope.projectDetailScope = data.Result;
                        $scope.lastStorageAudit = angular.copy(data.Result);
                        CommonFunctions.ScrollUpAndFocus("txtproject");
                    } else if (data.MessageType == messageTypes.Error) {// Error
                        toastr.error(data.Message, errorTitle);
                    }
                }
                $rootScope.isAjaxLoadingChild = false;
            });
        };

        //  Create Excel Report of Designation
        //$scope.createReport = function () {
        //    if (!$rootScope.permission.CanWrite) { return; }
        //    var filename = "Designation_" + $rootScope.fileDateName + ".xls";
        //    CommonFunctions.DownloadReport('/Designations/CreateDesignationListReport', filename);
        //};

        //// Reset Designation form data; edit + reset will remove all changes made in edit mode.
        //$scope.resetDesignationDetails = function (frmDesignations) {
        //    if ($scope.operationMode() == "Update") {
        //        $scope.frmDesignations = angular.copy($scope.lastStorageGroup);
        //        frmDesignations.$setPristine();
        //    } else {
        //        $scope.clearData(frmDesignations);
        //    }
        //};

        // Clear Project form data.
        $scope.ClearFormData = function (frmproject) {
            $scope.projectDetailScope = {
                ProjectId: 0,
                VerticalId:0,
                ProjectName: '',
                IsActive: true
            };
            frmproject.$setPristine();
            $("#txtproject").focus();
            CommonFunctions.ScrollToTop();
        };

        //Load Project List
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: $rootScope.pageSize,
            sorting: { ProjectName: 'asc' }
        }, {
            getData: function ($defer, params) {
                if (projectDetailParams == null) {
                    projectDetailParams = {};
                }
                projectDetailParams.Paging = CommonFunctions.GetPagingParams(params);
                projectDetailParams.Paging.Search = $scope.isSearchClicked ? $scope.search : '';
                //Load Project List
                ProjectMasterService.GetAllProjects(projectDetailParams.Paging).then(function (res) {
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
                    CommonFunctions.SetFixHeader();
                });
            }
        });

        //Initalize the Vertical List
        $scope.Init = function () {
            $scope.VerticalScope();
            $scope.ProjectManager();
        }


        //Scope For Vertical List
        $scope.VerticalScope = function () {
            ProjectMasterService.GetVerticalList().then(function (res) {
                $scope.Verticals = res.data.Result;
            });
        }

        $scope.ProjectManager = function () {
            ProjectMasterService.GetProjectManager().then(function (res) {
                $scope.ProjectManager = res.data.Result;
            })
        }
    }
})();