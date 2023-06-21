(function () {
    'use strict';
    angular.module("DorfKetalMVCApp")
    .controller('DashBoardCtrl', ['$scope', '$rootScope', 'DashBoardService', 'CommonEnums', 'WidgetCommonService', 'filterFilter',
            function ($scope, $rootScope, DashBoardService, CommonEnums, WidgetCommonService, filterFilter) {
                $scope.gridsterOptions = {
                    margins: [10, 10],
                    columns: 3,
                    swapping: true,
                    draggable: {
                        handle: 'h3'
                    },
                    resizable: {
                        enabled: true,
                        handles: ['n', 'e', 's', 'w', 'ne', 'se', 'sw', 'nw'],
                        start: function (event, $element, widget) { }, // optional callback fired when resize is started,
                        resize: function (event, $element, widget) {
                            if (widget.chart.api) widget.chart.api.update();
                        }, // optional callback fired when item is resized,
                        stop: function (event, $element, widget) {
                            setTimeout(function () {
                                if (widget.chart.api) widget.chart.api.update();
                            }, 400)
                        } // optional callback fired when item is finished resizing
                    }
                };

                $scope.SetAuthorizeMessage = function (authorizeMessage) {
                    if (authorizeMessage) {
                        toastr.warning(authorizeMessage, warningTitle);
                    }
                }
                $scope.dashboard = { widgets: [] };

                $scope.dashboardLocalstorage = { widgets: [] };
                $scope.clear = function () {
                    $scope.dashboard.widgets = [];
                    $scope.dashboardLocalstorage.widgets = [];
                };

                $scope.addWidget = function (ComponentId) {
                    DashBoardService.GetSettingByComponent(ComponentId).then(function (response) {
                        if (response) {
                            var data = response.data.Result;
                            var ComponentWiseCount = 0;
                            var counter = 0;
                            angular.forEach($scope.dashboard.widgets, function (val, index) {
                                if (val.Componentid == data[0].ComponentId) {
                                    if (val.Componentid == 1) {
                                        counter = val.name.replace("Module wise General Actions ", "");
                                    }
                                    else if (val.Componentid == 2) {
                                        counter = val.name.replace("Incident Statistics ", "");
                                    }
                                    else if (val.Componentid == 3) {
                                        counter = val.name.replace("Module wise CAPA Actions ", "");
                                    }
                                }
                                ComponentWiseCount = ((counter != "" && counter != 0) ? parseInt(counter) + 1 : 1);
                            });

                            var chartTypedata = filterFilter(angular.copy(data), { ComponentFilterId: CommonEnums.ComponentFilter.ChartType }, true);
                            var Segregationdata = filterFilter(angular.copy(data), { ComponentFilterId: CommonEnums.ComponentFilter.Segregation }, true);
                            $scope.dashboard.widgets.push({
                                widgetID: data[0].ComponentName + (ComponentWiseCount > 0 ? ComponentWiseCount : 1),
                                sizeX: 1,
                                sizeY: 1,
                                name: data[0].ComponentName + " " + (ComponentWiseCount > 0 ? ComponentWiseCount : 1),
                                content: $scope.dashboard.widgets.length+1,
                                isMaximized: false,
                                position: $scope.dashboard.widgets.length,
                                Componentid: data[0].ComponentId,
                                Filter: data,
                                SiteId: new Array(),
                                ModuleId: new Array(),
                                TaskPriorityId: new Array(),
                                TaskStatusId: new Array(),
                                TimeLineId: 0,
                                ChartTypeId: chartTypedata[0].ComponentSubFilterId,
                                SegregationId: Segregationdata[2].ComponentSubFilterId,
                                TimeLineStartDate: new Date(),
                                TimeLineEndDate: new Date(),
                                IncidentStatusId: new Array(),
                                DepartmentId: new Array(),
                                AuditStatusId: new Array(),
                                AuditTitleId: new Array(),
                                ObservationStatusId: new Array(),
                                ObserverId: new Array(),
                                InspectionStatusId: new Array(),
                                InspectorId: new Array(),
                                CAPATypeId: new Array(),
                                InjuryPotentialId: new Array(),
                                SafetyObservationTypeId: new Array(),
                                SafetyObserverId: new Array(),
                                CanRead: true,
                                CanWrite: true,
                                chart: {}
                            });

                        }
                        $scope.dashboard.widgets[$scope.dashboard.widgets.length - 1].chart.options = WidgetCommonService.discreteBarChartOptions(Segregationdata[2].SubFilterName, "multiBarChart");
                        WidgetCommonService.discreteBarChartData($scope.dashboard.widgets[$scope.dashboard.widgets.length - 1]);
                        $("html, body").animate({ scrollTop: $(document).height() }, 1000);
                    });

                };

                //Save widget settings to database
                $scope.GetComponentList = function () {
                    //setTimeout(function () {                    
                    DashBoardService.GetAllComponentByUser().then(function (response) {
                        if (response) {
                            var data = response.data;
                            //var chardata = discreteBarChartData();
                            for (var i = 0; i < data.Result.length; i++) {
                                $scope.dashboard.widgets.push({
                                    ComponentUserSaveSettId: data.Result[i].ComponentUserSaveSettId,
                                    sizeX: data.Result[i].ComponentWidth,
                                    sizeY: data.Result[i].ComponentHeight,
                                    name: data.Result[i].ComponentHeading,
                                    content: i,
                                    col: data.Result[i].ComponentPositionCol,
                                    row: data.Result[i].ComponentPositionRow,
                                    position: data.Result[i].Position,
                                    isMaximized: data.Result[i].IsMaximized,
                                    Componentid: data.Result[i].ComponentId,
                                    ChartTypeId: data.Result[i].ChartTypeId,
                                    SegregationId: data.Result[i].SegregationId,
                                    Filter: data.Result[i].ComponentSetting,
                                    SiteId: data.Result[i].SiteId,
                                    ModuleId: data.Result[i].ModuleId,
                                    TaskPriorityId: data.Result[i].TaskPriorityId,
                                    TaskStatusId: data.Result[i].TaskStatusId,
                                    TimeLineId: data.Result[i].TimeLineId,
                                    TimeLineStartDate: data.Result[i].TimeLineStartDate,
                                    TimeLineEndDate: data.Result[i].TimeLineEndDate,
                                    IncidentStatusId: data.Result[i].IncidentStatusId,
                                    DepartmentId: data.Result[i].DepartmentId,
                                    AuditStatusId: data.Result[i].AuditStatusId,
                                    AuditTitleId: data.Result[i].AuditTitleId,
                                    ObservationStatusId: data.Result[i].ObservationStatusId,
                                    ObserverId: data.Result[i].ObserverId,
                                    InspectionStatusId: data.Result[i].InspectionStatusId,
                                    InspectorId: data.Result[i].InspectorId,
                                    CAPATypeId: data.Result[i].CAPATypeId,
                                    InjuryPotentialId: data.Result[i].InjuryPotentialId,
                                    SafetyObservationTypeId: data.Result[i].SafetyObservationTypeId,
                                    SafetyObserverId: data.Result[i].SafetyObserverId,
                                    CanRead: data.Result[i].CanRead,
                                    CanWrite: data.Result[i].CanWrite,
                                    chart: {}
                                });


                                if (data.Result[i].ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                                    WidgetCommonService.discreteBarChartData($scope.dashboard.widgets[i]);
                                }
                                else {
                                    var SegregationName = "";
                                    if (data.Result[i].SegregationId > 0) {
                                        SegregationName = $.grep(data.Result[i].ComponentSetting, function (f) {
                                            return f.ComponentSubFilterId == data.Result[i].SegregationId;
                                        })[0].SubFilterName;
                                    }
                                    var chartType = "multiBarChart";
                                    if (data.Result[i].ChartTypeId == CommonEnums.ComponentSubFilter.Bar) {
                                        chartType = "multiBarHorizontalChart";
                                    }
                                    $scope.dashboard.widgets[i].chart.options = WidgetCommonService.discreteBarChartOptions(SegregationName, chartType);
                                    WidgetCommonService.discreteBarChartData($scope.dashboard.widgets[i]);
                                }
                            }
                        }
                    });
                    //}, 100);
                };

                $scope.Init = function () {
                    $scope.GetComponentList();
                } ();
                //Save widget settings to database
                $scope.save = function () {
                    $rootScope.isAjaxLoadingChild = true;

                    angular.forEach($scope.dashboard.widgets, function (value, key) {
                        if (typeof (value.TimeLineStartDate) != "string") {
                            value.TimeLineStartDate = angular.copy(moment(value.TimeLineStartDate).format('DD-MMM-YYYY'));
                            value.TimeLineEndDate = angular.copy(moment(value.TimeLineEndDate).format('DD-MMM-YYYY 23:59:59'));
                        }
                    });
                    DashBoardService.SaveComponentSetting($scope.dashboard.widgets).then(function (response) {
                        if (response) {
                            var data = response.data;
                            if (data.MessageType == messageTypes.Success) {// Success                            
                                toastr.success(data.Message, successTitle);
                            } else if (data.MessageType == messageTypes.Error) {// Error                            
                                toastr.error(data.Message, errorTitle);
                            }
                            $rootScope.isAjaxLoadingChild = false;
                        }
                    });
                };

                //Open Settings popup
                $scope.openSettings = function (widget) {
                    open({
                        scope: $scope,
                        templateUrl: '/views/dashboard/widget_settings',
                        controller: 'WidgetSettingsCtrl',
                        resolve: {
                            widget: function () {
                                return widget;
                            }
                        }
                    });
                };

            }
        ])
    // helper code
    .filter('object2Array', function () {
        return function (input) {
            var out = [];
            for (i in input) {
                out.push(input[i]);
            }
            return out;
        }
    });
})();