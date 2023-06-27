(function () {
    'use strict';

    angular.module("MVCApp").controller('DashboardCtrl', [
        '$scope', '$rootScope', 'ngTableParams', 'CommonFunctions', 'FileService', 'DashboardService', '$timeout', DashboardCtrl
    ]);


    function DashboardCtrl($scope, $rootScope, ngTableParams, CommonFunctions, FileService, DashboardService) {
        /* Initial Declaration */
        $scope.flag = 1;
        var reportParams = {};
        $scope.GetAllList = function () {
            DashboardService.getAllList()
                .then(function (res) {
                    $scope.List = res.data.Result;
                });
        }

        $scope.GetCount = function () {
            DashboardService.getCount()
                .then(function (res) {
                    $scope.Count = res.data.Result;
                });
        }

        $scope.tableParams = new ngTableParams({
            page: 1,
            count: $rootScope.pageSize,
            sorting: { GroupName: 'asc' }
        }, {
            getData: function ($defer, params) {
                if (reportParams == null) {
                    reportParams = {};
                }
                reportParams.Paging = CommonFunctions.GetPagingParams(params);
                if ($scope.flag == 1) {
                    DashboardService.getAllList(reportParams.Paging).then(function (res) {
                        var data = res.data;
                        $scope.List = res.data.Result;
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
                else if ($scope.flag == 2) {
                    DashboardService.getSubmittedList(reportParams.Paging).then(function (res) {
                        var data = res.data;
                        $scope.List = res.data.Result;
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
                else if ($scope.flag == 3) {
                    DashboardService.getReviewedList(reportParams.Paging).then(function (res) {
                        var data = res.data;
                        $scope.List = res.data.Result;
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
                else if ($scope.flag == 4) {
                    DashboardService.getPendingList(reportParams.Paging).then(function (res) {
                        var data = res.data;
                        $scope.List = res.data.Result;
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
            }
        });

        $scope.show = function () {
            if (userContext.RoleId == 2 && $scope.flag == 2) {
                return true;
            }
            else if (userContext.RoleId == 3 && $scope.flag == 3) {
                return true;
            }

        }

        $scope.action = function (VerticalId, ProjectId, ReportDate) {
            if (userContext.RoleId == 2) {
                window.location.href = '../../MISReporting/ReviewReport#?VerticalId=' + VerticalId + '&ProjectId=' + ProjectId + '&ReportDate=' + ReportDate;
            }
            else if (userContext.RoleId == 3) {
                window.location.href = '../../MISReporting/ApproveReport#?VerticalId=' + VerticalId + '&ProjectId=' + ProjectId + '&ReportDate=' + ReportDate;
            }
        }

        //$scope.tableParams = new ngTableParams({
        //    page: 1,
        //    count: $rootScope.pageSize,
        //    sorting: { GroupName: 'asc' }
        //}, {
        //    getData: function ($defer, params) {
        //        if (reportParams == null) {
        //            reportParams = {};
        //        }
        //        reportParams.Paging = CommonFunctions.GetPagingParams(params);
        //        //projectDetailParams.Paging.Search = $scope.isSearchClicked ? $scope.search : '';
        //        //Load Project List
        //        DashboardService.getAllList(reportParams.Paging).then(function (res) {
        //            var data = res.data;
        //            $scope.List = res.data.Result;
        //            if (res.data.MessageType == messageTypes.Success) {// Success
        //                $defer.resolve(res.data.Result);
        //                if (res.data.Result.length == 0) {
        //                } else {
        //                    params.total(res.data.Result[0].TotalRecords);
        //                }
        //            } else if (res.data.MessageType == messageTypes.Error) {// Error
        //                toastr.error(res.data.Message, errorTitle);
        //            }
        //            $rootScope.isAjaxLoadingChild = false;
        //            CommonFunctions.SetFixHeader();
        //        });
        //    }
        //});

        $scope.Projectlist = function (VerticalId) {
            ApproveReportService.getprojectlist(VerticalId)
                .then(function (res) {
                    $scope.Project = res.data.Result;
                });
        }
    }
})();