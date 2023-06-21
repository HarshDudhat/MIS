
(function () {
    'use strict';
    angular.module("DorfKetalMVCApp")
    .controller('CustomWidgetCtrl', ['$scope', '$rootScope', 'filterFilter', '$uibModal', '$timeout', 'DashBoardService', 'FileService', 'CommonEnums', 'CommonFunctions', 'WidgetCommonService',
    	function ($scope, $rootScope, filterFilter, $uibModal, $timeout, DashBoardService, FileService, CommonEnums, CommonFunctions, WidgetCommonService) {

    	    //Remove widget
    	    $scope.remove = function (widget) {
    	        if (!widget.CanWrite) { return; }
    	        var TotalWidget = $scope.dashboard.widgets.length;
    	        var DeletedWidgetCol = widget.col;
    	        var DeletedWidgetRow = widget.row;
    	        for (var i = 0; i < TotalWidget; i++) {
    	            if (($scope.dashboard.widgets[i].row == DeletedWidgetRow && $scope.dashboard.widgets[i].col > DeletedWidgetCol) || $scope.dashboard.widgets[i].row > DeletedWidgetRow) {
    	                if ($scope.dashboard.widgets[i].col == 0) {
    	                    $scope.dashboard.widgets[i].col = 3;
    	                    $scope.dashboard.widgets[i].row = $scope.dashboard.widgets[i].row - 1;
    	                }
    	                else {
    	                    $scope.dashboard.widgets[i].col = $scope.dashboard.widgets[i].col - 1;
    	                    $scope.dashboard.widgets[i].position = $scope.dashboard.widgets[i].position - 1;
    	                }
    	            }
    	        }
    	        $scope.dashboard.widgets.splice($scope.dashboard.widgets.indexOf(widget), 1);
    	    };

    	    //Open Settings popup
    	    $scope.openFilter = function (widget) {
    	        if (!widget.CanWrite) { return; }
    	        $uibModal.open({
    	            scope: $scope,
    	            templateUrl: 'WidgetSetting',
    	            controller: 'FilterWidgetCtrl',
    	            resolve: {
    	                widget: function () {
    	                    return widget;
    	                }
    	            }
    	        });
    	    };

    	    $scope.enum = angular.copy(CommonEnums);
    	    $scope.timelineRanges = [];
    	    $scope._objChartType = {
    	        ChartTypeId: 0,
    	        SegregationId: 0,
    	        SiteId: new Array(),
    	        NatureOfInjuryId: new Array(),
    	        BodyPartId: new Array(),
    	        ModuleId: new Array(),
    	        TaskPriorityId: new Array(),
    	        TaskStatusId: new Array(),
    	        TimeLineId: 0,
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
    	        SafetyObserverId: new Array()
    	    };


    	    $scope.filteredValues = {};
    	    $scope.filteredValues.TimeLineDateRange = $rootScope.dateRanges['Today'];


    	    $scope.MultiSelectiondropdownData = {
    	        SiteData: [],
    	        BodyPartData: [],
    	        NatureOfInjuryData: [],
    	        ModuleData: [],
    	        TaskPriority: [],
    	        TaskStatus: [],
    	        IncidentStatus: [],
    	        Department: [],
    	        AuditStatus: [],
    	        AuditTitle: [],
    	        ObservationStatus: [],
    	        Observer: [],
    	        InspectionStatus: [],
    	        Inspector: [],
    	        InjuryPotential: [],
    	        SafetyObservationType: [],
    	        SafetyObserver: []
    	    }

    	    $scope.SetTimeLine = function (widget, Range) {
    	        if (Range != null) {
    	            if (Range[0] != null && Range[1] != null) {
    	                $scope._objChartType.TimeLineStartDate = Range[0].toDate();
    	                $scope._objChartType.TimeLineEndDate = Range[1].toDate();
    	            }
    	            widget.TimeLineId = Range[2];
    	        } else {
    	            $scope._objChartType.TimeLineStartDate = new Date();
    	            $scope._objChartType.TimeLineEndDate = new Date();
    	            widget.TimeLineId = CommonEnums.ComponentSubFilter.CustomRange;
    	        }
    	    };

    	    function SelectedObject(AllValueListObject, SelectedValueArray) {
    	        var SelectedObject = new Array();
    	        if (SelectedValueArray != null && SelectedValueArray.length > 0) {
    	            $.each(AllValueListObject, function (index, value) {
    	                angular.forEach(SelectedValueArray, function (val) {
    	                    if (val == value.ComponentSubFilterId || val == value.SubFilterEnumValue) {
    	                        SelectedObject.push(AllValueListObject[index]);
    	                    }
    	                });
    	            });
    	        }
    	        return SelectedObject
    	    };


    	    $scope.UpdateChartType = function (componentSubFilterId) {
    	        $scope.selectedChartTypeId = componentSubFilterId;
    	    };

    	    //$scope.reloadRanges = true;
    	    //$scope.UpdateTimeRange = function (componentSubFilterId) {
    	    //    $scope.selectedSegregationId = componentSubFilterId;
    	    //    $scope.reloadRanges = false;
    	    //    $scope.newComponentdateRanges = angular.copy($rootScope.ComponentdateRanges);
    	    //    $scope.ComponentdateRanges = [];
    	    //    if (componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseWeekly) {
    	    //        angular.forEach($scope.newComponentdateRanges, function (val, index) {
    	    //            if (val[2] == CommonEnums.ComponentSubFilter.Last7Days || val[2] == CommonEnums.ComponentSubFilter.Last15Days || val[2] == CommonEnums.ComponentSubFilter.Last30Days || val[2] == CommonEnums.ComponentSubFilter.Last2Months) {
    	    //                $scope.ComponentdateRanges[index] = [eval(val[0]), eval(val[1]), val[2]];
    	    //            }
    	    //        });
    	    //    }
    	    //    else if (componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseMonthly) {
    	    //        angular.forEach($scope.newComponentdateRanges, function (val, index) {
    	    //            if (val[2] == CommonEnums.ComponentSubFilter.Last2Months || val[2] == CommonEnums.ComponentSubFilter.Last3Months || val[2] == CommonEnums.ComponentSubFilter.Last6Months || val[2] == CommonEnums.ComponentSubFilter.Last12Months || val[2] == CommonEnums.ComponentSubFilter.LastYear) {
    	    //                $scope.ComponentdateRanges[index] = [eval(val[0]), eval(val[1]), val[2]];
    	    //            }
    	    //        });
    	    //    }
    	    //    else {
    	    //        $scope.ComponentdateRanges = angular.copy($scope.allComponentdateRanges);
    	    //    }
    	    //    setTimeout(function () { $scope.reloadRanges = true; }, 10);

    	    //    $scope.timelineRanges = [];


    	    //    //$scope.timeline = $scope.timelineRanges[1].Range;
    	    //    if (componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseWeekly || componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseMonthly) {
    	    //        $scope.timelineRanges.push({ Name: 'Select', Range: null });
    	    //        for (var key in $scope.ComponentdateRanges) {
    	    //            $scope.timelineRanges.push({ Name: key, Range: $scope.ComponentdateRanges[key] });
    	    //        }
    	    //        //$scope.timeline = $scope.timelineRanges[1].Range;
    	    //        //$scope.timelineRanges.push({ Name: 'Select', Range: null });
    	    //    } else {
    	    //        $scope.timelineRanges.push({ Name: 'Select', Range: [null, null, 0] });
    	    //        for (var key in $scope.ComponentdateRanges) {
    	    //            $scope.timelineRanges.push({ Name: key, Range: $scope.ComponentdateRanges[key] });
    	    //        }
    	    //        $scope.timelineRanges.push({ Name: 'Custom Range', Range: null });
    	    //    }
    	    //};

    	    $scope.reloadRanges = true;
    	    $scope.UpdateTimeRange = function (componentSubFilterId) {
    	        $scope.selectedSegregationId = componentSubFilterId; // for Set Segration 
    	        $scope.reloadRanges = false;
    	        $scope.newComponentdateRanges = angular.copy($rootScope.ComponentdateRanges);
    	        $scope.ComponentdateRanges = [];
    	        if (componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseWeekly) {
    	            angular.forEach($scope.newComponentdateRanges, function (val, index) {
    	                if (val[2] == CommonEnums.ComponentSubFilter.Last7Days || val[2] == CommonEnums.ComponentSubFilter.Last15Days || val[2] == CommonEnums.ComponentSubFilter.Last30Days || val[2] == CommonEnums.ComponentSubFilter.Last2Months) {
    	                    $scope.ComponentdateRanges[index] = [eval(val[0]), eval(val[1]), val[2]];
    	                }
    	            });
    	        }
    	        else if (componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseMonthly) {
    	            angular.forEach($scope.newComponentdateRanges, function (val, index) {
    	                if (val[2] == CommonEnums.ComponentSubFilter.Last2Months || val[2] == CommonEnums.ComponentSubFilter.Last3Months || val[2] == CommonEnums.ComponentSubFilter.Last6Months || val[2] == CommonEnums.ComponentSubFilter.Last12Months || val[2] == CommonEnums.ComponentSubFilter.LastYear) {
    	                    $scope.ComponentdateRanges[index] = [eval(val[0]), eval(val[1]), val[2]];
    	                }
    	            });
    	        }
    	        else {
    	            $scope.ComponentdateRanges = angular.copy($scope.allComponentdateRanges);
    	        }
    	        setTimeout(function () { $scope.reloadRanges = true; }, 10);

    	        $scope.timelineRanges = [];
    	        $scope.timelineRanges.push({ Name: 'Select', Range: [null, null, 0] });
    	        for (var key in $scope.ComponentdateRanges) {
    	            $scope.timelineRanges.push({ Name: key, Range: $scope.ComponentdateRanges[key] });
    	        }
    	        $scope.timeline = $scope.timelineRanges[0].Range;
    	        if (componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseWeekly || componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseMonthly) {
    	            $scope.timeline = $scope.timelineRanges[0].Range;
    	        } else {
    	            $scope.timelineRanges.push({ Name: 'Custom Range', Range: null });
    	        }
    	    };

    	    $scope.FilterDate = function (componentSubFilterId) {
    	        if (componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseWeekly || componentSubFilterId == CommonEnums.ComponentSubFilter.TimelinewiseMonthly) {
    	            $('.range_inputs').hide();
    	            $(".ranges li:last").hide();
    	            $(".daterangepicker.show-calendar .calendar").hide();
    	        }
    	    };


    	    // Load Filter for Widget
    	    $scope.LoadFilterforWidget = function (widget) {
    	        $scope.ResponeseData = widget.Filter;
    	        $scope.ResWidget = widget;

    	        $scope.MultiSelectiondropdownData.BodyPartData = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: 17 }, true);
    	        $scope.MultiSelectiondropdownData.NatureOfInjuryData = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: 18 }, true);

    	        $scope.MultiSelectiondropdownData.SiteData = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.Site }, true);
    	        $scope.MultiSelectiondropdownData.ModuleData = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.ModuleType }, true);
    	        if ($scope.ResponeseData[0].ComponentId == CommonEnums.Component.TaskManagement) {
    	            $scope.MultiSelectiondropdownData.ModuleData = filterFilter($scope.MultiSelectiondropdownData.ModuleData, { SubFilterName: '!Equipment Inspection' });
    	        }
                if ($scope.ResponeseData[0].ComponentId == CommonEnums.Component.CAPAManagement) {
    	            $scope.MultiSelectiondropdownData.ModuleData = filterFilter($scope.MultiSelectiondropdownData.ModuleData, { SubFilterName: '!Task' });
    	            $scope.MultiSelectiondropdownData.ModuleData = filterFilter($scope.MultiSelectiondropdownData.ModuleData, { SubFilterName: '!Multi Task' });
    	            $scope.MultiSelectiondropdownData.ModuleData = filterFilter($scope.MultiSelectiondropdownData.ModuleData, { SubFilterName: '!Equipment Inspection' });
    	        }
    	        $scope.MultiSelectiondropdownData.TaskPriority = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.Taskpriority }, true);
    	        $scope.MultiSelectiondropdownData.TaskStatus = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.Taskstatus }, true);


    	        $scope.MultiSelectiondropdownData.IncidentStatus = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.IncidentStatus }, true);
    	        $scope.MultiSelectiondropdownData.Department = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.Department }, true);
    	        $scope.MultiSelectiondropdownData.AuditStatus = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.AuditStatus }, true);
    	        if ($scope.ResponeseData[0].ComponentId == CommonEnums.Component.AuditManagement) {
    	            $scope.MultiSelectiondropdownData.AuditStatus = filterFilter($scope.MultiSelectiondropdownData.AuditStatus, { SubFilterName: '!Draft' });
    	        }
    	        $scope.MultiSelectiondropdownData.AuditTitle = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.AuditTitleWise }, true);
    	        $scope.MultiSelectiondropdownData.ObservationStatus = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.ObservationStatus }, true);
    	        $scope.MultiSelectiondropdownData.Observer = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.ObserverWise }, true);
    	        $scope.MultiSelectiondropdownData.InspectionStatus = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.InspectionStatus }, true);
    	        if ($scope.ResponeseData[0].ComponentId == CommonEnums.Component.SiteInspection) {
    	            $scope.MultiSelectiondropdownData.InspectionStatus = filterFilter($scope.MultiSelectiondropdownData.InspectionStatus, { SubFilterName: '!Draft' });
    	        }
    	        $scope.MultiSelectiondropdownData.Inspector = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.InspectorWise }, true);
    	        $scope.MultiSelectiondropdownData.CAPAType = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.CAPAType }, true);
    	        $scope.MultiSelectiondropdownData.InjuryPotential = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.InjuryPotential }, true);
    	        $scope.MultiSelectiondropdownData.SafetyObservationType = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.SafetyObservationType }, true);
    	        $scope.MultiSelectiondropdownData.SafetyObserver = filterFilter(angular.copy($scope.ResponeseData), { ComponentFilterId: CommonEnums.ComponentFilter.SafetyObserverWise }, true);

    	        $scope._objChartType.ChartTypeId = widget.ChartTypeId;
    	        $scope.selectedChartTypeId = parseInt($scope._objChartType.ChartTypeId);
    	        $scope._objChartType.SegregationId = widget.SegregationId;
    	        $scope.selectedSegregationId = parseInt($scope._objChartType.SegregationId);
    	        $scope._objChartType.SiteId = SelectedObject($scope.MultiSelectiondropdownData.SiteData, widget.SiteId);
    	        $scope._objChartType.ModuleId = SelectedObject($scope.MultiSelectiondropdownData.ModuleData, widget.ModuleId);
    	        $scope._objChartType.TaskPriorityId = SelectedObject($scope.MultiSelectiondropdownData.TaskPriority, widget.TaskPriorityId);
    	        $scope._objChartType.TaskStatusId = SelectedObject($scope.MultiSelectiondropdownData.TaskStatus, widget.TaskStatusId);
    	        $scope._objChartType.IncidentStatusId = SelectedObject($scope.MultiSelectiondropdownData.IncidentStatus, widget.IncidentStatusId);
    	        $scope._objChartType.DepartmentId = SelectedObject($scope.MultiSelectiondropdownData.Department, widget.DepartmentId);
    	        $scope._objChartType.AuditStatusId = SelectedObject($scope.MultiSelectiondropdownData.AuditStatus, widget.AuditStatusId);
    	        $scope._objChartType.AuditTitleId = SelectedObject($scope.MultiSelectiondropdownData.AuditTitle, widget.AuditTitleId);
    	        $scope._objChartType.ObservationStatusId = SelectedObject($scope.MultiSelectiondropdownData.ObservationStatus, widget.ObservationStatusId);
    	        $scope._objChartType.ObserverId = SelectedObject($scope.MultiSelectiondropdownData.Observer, widget.ObserverId);
    	        $scope._objChartType.InspectionStatusId = SelectedObject($scope.MultiSelectiondropdownData.InspectionStatus, widget.InspectionStatusId);
    	        $scope._objChartType.InspectorId = SelectedObject($scope.MultiSelectiondropdownData.Inspector, widget.InspectorId);
    	        $scope._objChartType.CAPATypeId = SelectedObject($scope.MultiSelectiondropdownData.CAPAType, widget.CAPATypeId);
    	        $scope._objChartType.InjuryPotentialId = SelectedObject($scope.MultiSelectiondropdownData.InjuryPotential, widget.InjuryPotentialId);
    	        $scope._objChartType.SafetyObservationTypeId = SelectedObject($scope.MultiSelectiondropdownData.SafetyObservationType, widget.SafetyObservationTypeId);
    	        $scope._objChartType.SafetyObserverId = SelectedObject($scope.MultiSelectiondropdownData.SafetyObserver, widget.SafetyObserverId);

    	        $scope._objChartType.TimeLineId = widget.TimeLineId;
    	        $scope._objChartType.TimeLineStartDate = typeof (widget.TimeLineStartDate) == "string" ? moment(widget.TimeLineStartDate, "DD-MMM-YYYY").toDate() : new Date(widget.TimeLineStartDate);
    	        $scope._objChartType.TimeLineEndDate = typeof (widget.TimeLineEndDate) == "string" ? moment(widget.TimeLineEndDate, "DD-MMM-YYYY").toDate() : new Date(widget.TimeLineEndDate);



    	        angular.forEach($scope.ResponeseData, function (val, index) {
    	            if (val.ComponentFilterId == CommonEnums.ComponentFilter.TimeRange && val.ComponentSubFilterId != CommonEnums.ComponentSubFilter.CustomRange) {
    	                var t1 = val.DateRangeValue.split('&');
    	                $scope.ComponentdateRanges[val.SubFilterName] = [eval(t1[0]), eval(t1[1]), val.ComponentSubFilterId];
    	            }
    	        });
    	        $scope.allComponentdateRanges = angular.copy($scope.ComponentdateRanges);
    	        if (widget.SegregationId == CommonEnums.ComponentSubFilter.TimelinewiseWeekly) {
    	            $scope.newComponentdateRanges = angular.copy($rootScope.ComponentdateRanges);
    	            $scope.ComponentdateRanges = [];
    	            angular.forEach($scope.newComponentdateRanges, function (val, index) {
    	                if (val[2] == CommonEnums.ComponentSubFilter.Last7Days || val[2] == CommonEnums.ComponentSubFilter.Last15Days || val[2] == CommonEnums.ComponentSubFilter.Last30Days || val[2] == CommonEnums.ComponentSubFilter.Last2Months) {
    	                    $scope.ComponentdateRanges[index] = [eval(val[0]), eval(val[1]), val[2]];
    	                }
    	            });
    	        }
    	        else if (widget.SegregationId == CommonEnums.ComponentSubFilter.TimelinewiseMonthly) {
    	            $scope.newComponentdateRanges = angular.copy($rootScope.ComponentdateRanges);
    	            $scope.ComponentdateRanges = [];
    	            angular.forEach($scope.newComponentdateRanges, function (val, index) {
    	                if (val[2] == CommonEnums.ComponentSubFilter.Last2Months || val[2] == CommonEnums.ComponentSubFilter.Last3Months || val[2] == CommonEnums.ComponentSubFilter.Last6Months || val[2] == CommonEnums.ComponentSubFilter.Last12Months || val[2] == CommonEnums.ComponentSubFilter.LastYear) {
    	                    $scope.ComponentdateRanges[index] = [eval(val[0]), eval(val[1]), val[2]];
    	                }
    	            });
    	        }

    	        $scope.UpdateTimeRange($scope._objChartType.SegregationId);

    	        if (widget.TimeLineId > 0) {
    	            if (widget.TimeLineId == CommonEnums.ComponentSubFilter.CustomRange) {
    	                $scope.timeline = $scope.timelineRanges[$scope.timelineRanges.length - 1].Range;
    	            } else {
    	                //if()
    	                $scope.timeline = null;
    	                for (var i = 0; i < $scope.timelineRanges.length; i++) {
    	                    var Range = $scope.timelineRanges[i].Range;
    	                    if (angular.isDefined(Range) && Range != null && Range[0] != null && Range[1] != null) {
    	                        if (Range[0].format("DD-MM-YYYY") == moment(widget.TimeLineStartDate).format("DD-MM-YYYY") && Range[1].format("DD-MM-YYYY") == moment(widget.TimeLineEndDate).format("DD-MM-YYYY")) {
    	                            //if (Range[0].format("DD-MMM-YYYY") == widget.TimeLineStartDate && Range[1].format("DD-MMM-YYYY") == widget.TimeLineEndDate.substring(0, 11)) {
    	                            $scope.timeline = Range;
    	                        }
    	                    }
    	                }
    	            }
    	        } else {
    	            $scope.timeline = $scope.timelineRanges[0].Range;
    	        }
    	    };



    	    $scope.GetData = function (_objChartType, widget) {

//////       	        if ($scope._objChartType.TimeLineId == 0) {
//////    	            toastr.error("message", "errorTitle");
//////                    return;
//////                }

    	        widget.ChartTypeId = $scope.selectedChartTypeId;
    	        widget.SegregationId = $scope.selectedSegregationId
    	        widget.SiteId = $scope._objChartType.SiteId.map(function (item) { return item.ComponentSubFilterId; });
    	        widget.NatureOfInjuryId = $scope._objChartType.NatureOfInjuryId.map(function (item) { return item.ComponentSubFilterId; });
    	        widget.BodyPartId = $scope._objChartType.BodyPartId.map(function (item) { return item.ComponentSubFilterId; });
    	        widget.ModuleId = $scope._objChartType.ModuleId.map(function (item) { return item.ComponentSubFilterId; });
    	        widget.TaskPriorityId = $scope._objChartType.TaskPriorityId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.TaskStatusId = $scope._objChartType.TaskStatusId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.IncidentStatusId = $scope._objChartType.IncidentStatusId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.DepartmentId = $scope._objChartType.DepartmentId.map(function (item) { return item.ComponentSubFilterId; });
    	        widget.AuditStatusId = $scope._objChartType.AuditStatusId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.AuditTitleId = $scope._objChartType.AuditTitleId.map(function (item) { return item.ComponentSubFilterId; });
    	        widget.ObservationStatusId = $scope._objChartType.ObservationStatusId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.ObserverId = $scope._objChartType.ObserverId.map(function (item) { return item.ComponentSubFilterId; });
    	        widget.InspectionStatusId = $scope._objChartType.InspectionStatusId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.InspectorId = $scope._objChartType.InspectorId.map(function (item) { return item.ComponentSubFilterId; });
    	        widget.CAPATypeId = $scope._objChartType.CAPATypeId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.InjuryPotentialId = $scope._objChartType.InjuryPotentialId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.SafetyObservationTypeId = $scope._objChartType.SafetyObservationTypeId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.SafetyObserverId = $scope._objChartType.SafetyObserverId.map(function (item) { return item.SubFilterEnumValue; });
    	        widget.TimeLineStartDate = moment($scope._objChartType.TimeLineStartDate).format('DD-MMM-YYYY');
    	        widget.TimeLineEndDate = moment($scope._objChartType.TimeLineEndDate).format('DD-MMM-YYYY 23:59:59');

    	        if (widget.SiteId.length == 0) {
    	            $.each($scope.MultiSelectiondropdownData.SiteData, function (index, value) {
    	                widget.SiteId.push(parseInt(value.ComponentSubFilterId));
    	            });
    	            //widget.SiteId = $scope.MultiSelectiondropdownData.SiteData;
    	            //alert(widget.SiteId.length)
    	        }


    	        //    	        if (widget.SegregationId == CommonEnums.ComponentSubFilter.TimelinewiseWeekly && (widget.TimeLineId != CommonEnums.ComponentSubFilter.Last7Days && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last15Days && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last30Days && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last2Months)) {
    	        //    	            toastr.error(TimeLineDateRangeIsRequiredForWeekly, errorTitle);
    	        //    	            return false;
    	        //    	        }
    	        //    	        if (widget.SegregationId == CommonEnums.ComponentSubFilter.TimelinewiseMonthly && (widget.TimeLineId != CommonEnums.ComponentSubFilter.Last2Months && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last3Months && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last6Months && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last12Months && widget.TimeLineId != CommonEnums.ComponentSubFilter.LastYear)) {
    	        //    	            toastr.error(TimeLineDateRangeIsRequiredForMonthly, errorTitle);
    	        //    	            return false;
    	        //    	        }

    	        var SegregationName = "";
    	        if ($scope._objChartType.SegregationId > 0) {
    	            SegregationName = $.grep($scope.ResponeseData, function (f) {
    	                return f.ComponentSubFilterId == widget.SegregationId;
    	            })[0].SubFilterName;
    	        }

    	        //  IF  for Get Data in Table   ELSE Print Chart 
    	        if ((widget.Componentid == CommonEnums.Component.NoofDaysWithoutIncident || widget.Componentid == CommonEnums.Component.BodyPart || widget.Componentid == CommonEnums.Component.NatureOfInjury) && $scope.selectedChartTypeId == CommonEnums.ComponentSubFilter.Table) {
    	            var CurrentWidgetid = widget.content;
    	            $("#" + CurrentWidgetid + "table").show();
    	            $("#" + CurrentWidgetid + "Chart").hide();
    	            var table = $("#" + CurrentWidgetid + "table").children();
    	            table.html('');
    	            if (widget.Componentid == CommonEnums.Component.NoofDaysWithoutIncident) {
    	                SegregationName = "Description";
    	            }
    	            var tableHeading = "<thead><tr><th>" + (SegregationName == '' ? 'Segregation' : SegregationName) + "</th>";
    	            var tableBody = "<tbody>";
    	            //table.append("<tbody>");

    	            //Nature Of Injury Data in Table
    	            if (widget.Componentid == CommonEnums.Component.NatureOfInjury) {
    	                DashBoardService.GetNatureOfInjury(widget).then(function (response) {
    	                    if (response) {
    	                        var data = response.data.Result;
    	                        if (data.length > 0) {
    	                            if (data[0].stSegregation)
    	                                widget.chart.data = data;
    	                            $.each(data, function (i, value) {
    	                                for (var propName in value) {
    	                                    if (propName != 'stSegregation') {
    	                                        tableHeading = tableHeading + "<th>" + propName + "</th>";
    	                                    }
    	                                }
    	                                if (i == 0) {
    	                                    table.append(tableHeading + "</thead><tbody>");
    	                                }
    	                                for (var propName in value) {
    	                                    if (value[propName] != "No Data Available") {
    	                                        $("#a_" + widget.content).css('display', '');
    	                                        if (propName != 'stSegregation') {
    	                                            tableBody = tableBody + "<td>" + (value[propName] == null ? '0' : value[propName]) + "</td>";
    	                                        }
    	                                        else {
    	                                            if (i > 0) {
    	                                                tableBody = tableBody + "</tr>";
    	                                            }
    	                                            tableBody = tableBody + "<tr><td>" + value[propName] + "</td>";
    	                                        }
    	                                    }
    	                                    else {
    	                                        $("#a_" + widget.content).css('display', 'none');
    	                                        table.append("<tr><td align='center' style='text-align: center'>No Data Available</td>");
    	                                    }
    	                                }
    	                            });
    	                            table.append(tableBody);
    	                        } ///////
    	                        else {
    	                            $("#a_" + widget.content).css('display', 'none');
    	                            table.append("<tr><td align='center' style='text-align: center'>No Data Available</td>");
    	                        }
    	                    }
    	                    widget.TimeLineStartDate = angular.copy($scope._objChartType.TimeLineStartDate);
    	                    widget.TimeLineEndDate = angular.copy($scope._objChartType.TimeLineEndDate);
    	                });
    	            }

    	            // No of Days Without Incident Data in Table
    	            if (widget.Componentid == CommonEnums.Component.NoofDaysWithoutIncident) {
    	                DashBoardService.GetNoofDaysWithoutIncident(widget).then(function (response) {
    	                    if (response) {
    	                        var data = response.data.Result;
    	                        widget.chart.data = data;
    	                        $.each(data, function (i, value) {
    	                            for (var propName in value) {
    	                                if (propName != 'stSegregation') {
    	                                    tableHeading = tableHeading + "<th>" + propName + "</th>";
    	                                }
    	                            }
    	                            if (i == 0) {
    	                                table.append(tableHeading + "</thead><tbody>");
    	                            }
    	                            for (var propName in value) {
    	                                if (value[propName] != "No Data Available") {
    	                                    $("#a_" + widget.content).css('display', '');
    	                                    if (propName != 'stSegregation') {
    	                                        tableBody = tableBody + "<td>" + (value[propName] == null ? '0' : value[propName]) + "</td>";
    	                                    }
    	                                    else {
    	                                        if (i > 0) {
    	                                            tableBody = tableBody + "</tr>";
    	                                        }
    	                                        tableBody = tableBody + "<tr><td>" + value[propName] + "</td>";
    	                                    }
    	                                }
    	                                else {
    	                                    $("#a_" + widget.content).css('display', 'none');
    	                                    table.append("<tr><td align='center' style='text-align: center'>No Data Available</td>");
    	                                }
    	                            }
    	                        });
    	                        table.append(tableBody);
    	                    }
    	                    widget.TimeLineStartDate = angular.copy($scope._objChartType.TimeLineStartDate);
    	                    widget.TimeLineEndDate = angular.copy($scope._objChartType.TimeLineEndDate);
    	                });
    	            }

    	            // BodyPart Data in Table
    	            if (widget.Componentid == CommonEnums.Component.BodyPart) {
    	                DashBoardService.GetBodyPartData(widget).then(function (response) {
    	                    if (response) {
    	                        var data = response.data.Result;
    	                        widget.chart.data = data;
    	                        if (data.length > 0) {
    	                            $.each(data, function (i, value) {
    	                                for (var propName in value) {
    	                                    if (propName != 'stSegregation') {
    	                                        tableHeading = tableHeading + "<th>" + propName + "</th>";
    	                                    }
    	                                }
    	                                if (i == 0) {
    	                                    table.append(tableHeading + "</thead><tbody>");
    	                                }
    	                                for (var propName in value) {
    	                                    if (value[propName] != "No Data Available") {
    	                                        $("#a_" + widget.content).css('display', '');
    	                                        if (propName != 'stSegregation') {
    	                                            tableBody = tableBody + "<td>" + (value[propName] == null ? '0' : value[propName]) + "</td>";
    	                                        }
    	                                        else {
    	                                            if (i > 0) {
    	                                                tableBody = tableBody + "</tr>";
    	                                            }
    	                                            tableBody = tableBody + "<tr><td>" + value[propName] + "</td>";
    	                                        }
    	                                    }
    	                                    else {
    	                                        $("#a_" + widget.content).css('display', 'none');
    	                                        table.append("<tr><td align='center' style='text-align: center'>No Data Available</td>");
    	                                    }
    	                                }
    	                            });
    	                            table.append(tableBody);
    	                        } else {
    	                            $("#a_" + widget.content).css('display', 'none');
    	                            table.append("<tr><td align='center' style='text-align: center'>No Data Available</td>");
    	                        }
    	                    }
    	                    widget.TimeLineStartDate = angular.copy($scope._objChartType.TimeLineStartDate);
    	                    widget.TimeLineEndDate = angular.copy($scope._objChartType.TimeLineEndDate);
    	                });
    	            }
    	            table.append("</tbody>");
    	        }
    	        // IF - END: Table Data End
    	        // ELSE Print Chart
    	        else {
    	            var CurrentWidgetid = widget.content;
    	            $("#" + CurrentWidgetid + "table").hide();
    	            $("#" + CurrentWidgetid + "Chart").show();
    	            var chartType = "multiBarChart";

    	            if ($scope.selectedChartTypeId == CommonEnums.ComponentSubFilter.Column) {
    	                chartType = "multiBarChart";
    	                //chartType = "discreteBarChart"; 
    	                //widget.chart.options = WidgetCommonService.discreteBarChartOptions(SegregationName, chartType);
    	                widget.chart.options = WidgetCommonService.discreteChartOptions(SegregationName, chartType, widget.Componentid);
    	                WidgetCommonService.discreteBarChartData(widget);
    	            }

    	            if ($scope.selectedChartTypeId == CommonEnums.ComponentSubFilter.Bar) {
    	                chartType = "multiBarHorizontalChart";
    	                widget.chart.options = WidgetCommonService.discreteChartOptions(SegregationName, chartType);
    	                WidgetCommonService.discreteBarChartData(widget);
    	            }

    	            if ($scope.selectedChartTypeId == CommonEnums.ComponentSubFilter.Pie) {
    	                chartType = "pieChart";
    	                widget.chart.options = WidgetCommonService.discreteChartOptions(SegregationName, chartType);
    	                WidgetCommonService.discreteChartData(widget);
    	                widget.chart.api.refreshWithTimeout(400);
    	            }

    	            if ($scope.selectedChartTypeId == CommonEnums.ComponentSubFilter.Line) {
    	                chartType = "lineChart";
    	                widget.chart.options = WidgetCommonService.discreteChartOptions(SegregationName, chartType);
    	                WidgetCommonService.discreteChartData(widget);
    	            }

    	            //widget.chart.options = WidgetCommonService.discreteBarChartOptions(SegregationName, chartType);
    	            //WidgetCommonService.discreteBarChartData(widget);
    	            widget.chart.api.refreshWithTimeout(400);

    	            if (widget.Componentid == CommonEnums.Component.AuditManagement) {
    	                DashBoardService.GetAuditData(widget).then(function (response) {
    	                    if (response) {
    	                        var data = response.data.Result;
    	                        if (data) {
    	                            if (Object.keys(data[0]).length > 10) {
    	                                setTimeout(function () { $scope.minmaximizeWidget(widget) }, 1000); ;
    	                            }
    	                        }
    	                    }
    	                    widget.TimeLineStartDate = angular.copy($scope._objChartType.TimeLineStartDate);
    	                    widget.TimeLineEndDate = angular.copy($scope._objChartType.TimeLineEndDate);
    	                });
    	            }
    	        }
    	    };



    	    // We want to manually handle `window.resize` event in each directive.
    	    // So that we emulate `resize` event using $broadcast method and internally subscribe to this event in each directive
    	    // Define event handler
    	    $scope.events = {
    	        resize: function (e, scope) {
    	            $timeout(function () {
    	                scope.api.update()
    	            }, 500)
    	        }
    	    };

    	    $scope.init = function () {
    	        if ($scope.widget.Componentid != CommonEnums.Component.NoofDaysWithoutIncident) {

    	            if ($scope.widget.ChartTypeId == CommonEnums.ComponentSubFilter.Table && $scope.widget.chart.data != 'undefined') {
    	                setTimeout(function () {
    	                    var SegregationName = "";
    	                    if ($scope.widget.SegregationId > 0) {
    	                        SegregationName = $.grep($scope.widget.Filter, function (f) {
    	                            return f.ComponentSubFilterId == $scope.widget.SegregationId;
    	                        })[0].SubFilterName;
    	                    }

    	                    var CurrentWidgetid = $scope.widget.content;
    	                    $("#" + CurrentWidgetid + "table").show();
    	                    $("#" + CurrentWidgetid + "Chart").hide();
    	                    var table = $("#" + CurrentWidgetid + "table").children();
    	                    table.html('');
    	                    if ($scope.widget.Componentid == CommonEnums.Component.TaskManagement || $scope.widget.Componentid == CommonEnums.Component.CAPAManagement) {
    	                        table.append("<thead><tr><th>" + SegregationName + "</th><th>Open</th><th>Inprogress</th><th>Close</th></tr></thead>");
    	                        table.append("<tbody>");
    	                        var data = $scope.widget.chart.data;
    	                        if (data.length > 0) {
    	                            $("#a_" + $scope.widget.content).css('display', '');
    	                            $.each(data, function (i) {
    	                                table.append("<tr><td>" + data[i].stSegregation + "</td><td>" + data[i].Open + "</td><td>" + data[i].Inprogress + "</td><td>" + data[i].Close + "</td></tr>");
    	                            });
    	                        }
    	                        else {
    	                            $("#a_" + $scope.widget.content).css('display', 'none');
    	                            table.append("<tr><td colspan='4' align='center' style='text-align: center'>No Data Available</td>");
    	                        }
    	                        table.append("</tbody>");
    	                    }
    	                    if ($scope.widget.Componentid == CommonEnums.Component.AuditManagement || $scope.widget.Componentid == CommonEnums.Component.BBSObservation || $scope.widget.Componentid == CommonEnums.Component.SiteInspection || $scope.widget.Componentid == CommonEnums.Component.SafetyObservation) {

    	                        //|| $scope.widget.Componentid == CommonEnums.Component.NoofDaysWithoutIncident

    	                        if ($scope.widget.Componentid == CommonEnums.Component.NoofDaysWithoutIncident) {
    	                            SegregationName = "Description";
    	                        }

    	                        var tableHeading = "<thead><tr><th>" + (SegregationName == '' ? 'Segregation' : SegregationName) + "</th>";
    	                        var tableBody = "<tbody>";
    	                        var data = $scope.widget.chart.data;
    	                        $.each(data, function (i, value) {
    	                            for (var propName in value) {

    	                                if (propName != 'stSegregation') {
    	                                    tableHeading = tableHeading + "<th>" + propName + "</th>";
    	                                }
    	                            }
    	                            if (i == 0) {
    	                                table.append(tableHeading + "</thead><tbody>");
    	                            }
    	                            for (var propName in value) {
    	                                if (value[propName] != "No Data Available") {
    	                                    $("#a_" + $scope.widget.content).css('display', '');
    	                                    if (propName != 'stSegregation') {
    	                                        tableBody = tableBody + "<td>" + (value[propName] == null ? '0' : value[propName]) + "</td>";
    	                                    }
    	                                    else {
    	                                        if (i > 0) {
    	                                            tableBody = tableBody + "</tr>";
    	                                        }
    	                                        tableBody = tableBody + "<tr><td>" + value[propName] + "</td>";
    	                                    }
    	                                }
    	                                else {
    	                                    $("#a_" + $scope.widget.content).css('display', 'none');
    	                                    table.append("<tr><td align='center' style='text-align: center'>No Data Available</td>");
    	                                }
    	                            }
    	                        });
    	                        table.append(tableBody);
    	                    }
    	                    if ($scope.widget.Componentid == CommonEnums.Component.IncidentManagement) {
    	                        table.append("<thead><tr><th>" + SegregationName + "</th><th>FAC</th><th>MTC</th><th>FAT</th><th>LTA</th><th>Near Miss</th></tr></thead>");
    	                        table.append("<tbody>");
    	                        var data = $scope.widget.chart.data;
    	                        if (data.length > 0) {
    	                            $("#a_" + $scope.widget.content).css('display', '');
    	                            $.each(data, function (i) {
    	                                table.append("<tr><td>" + data[i].stSegregation + "</td><td>" + data[i].FAC + "</td><td>" + data[i].MTC + "</td><td>" + data[i].FAT + "</td><td>" + data[i].LTA + "</td><td>" + data[i].Nea_Miss + "</td></tr>");
    	                            });
    	                        }
    	                        else {
    	                            $("#a_" + $scope.widget.content).css('display', 'none');
    	                            table.append("<tr><td colspan='6' align='center' style='text-align: center'>No Data Available</td>");
    	                        }
    	                        table.append("</tbody>");
    	                    }
    	                }, 2000);
    	            }


    	            setTimeout(function () {
    	                var link = $(".gridster");
    	                var bottom = $(window).height() - link.height();
    	                bottom = $(".gridster").offset().top - bottom;
    	                //    	            $('html, body').animate({
    	                //    	                scrollTop: bottom
    	                //    	            }, 1000);
    	                //$(".tick > text").css("opacity", "1");
    	            }, 500)
    	        }
    	    } ();

    	    angular.element(window).on('resize', function (e) {
    	        $scope.$broadcast('resize');
    	        window.onresize = null;
    	    });

    	    //Minimize maximize widget
    	    $scope.minmaximizeWidget = function (widget) {
    	        var isAnyAlreadyMaximized = false;
    	        if (widget.isMaximized == false) {
    	            var currentWidgetIndex = $scope.dashboard.widgets.indexOf(widget);
    	            if ($scope.dashboard.widgets.length > 0) {
    	                //If any other widget is already maximized than,reset all widgets to their original stage first by using local storage 
    	                //    	                angular.forEach($scope.dashboard.widgets, function (value, key) {
    	                //    	                    if ($scope.dashboard.widgets[key].isMaximized == true) {
    	                //    	                        isAnyAlreadyMaximized = true;
    	                //    	                        angular.copy($scope.dashboardLocalstorage.widgets, $scope.dashboard.widgets);
    	                //    	                        $scope.$broadcast('resize');
    	                //    	                        $scope.dashboardLocalstorage.widgets = [];
    	                //    	                    }
    	                //    	                });

    	                //Get current passed widget after resetting widgets to their old state as widget index is changed because of above code of resetting widgets to original stage
    	                angular.forEach($scope.dashboard.widgets, function (value, key) {
    	                    if ($scope.dashboard.widgets[key].position == widget.position) {
    	                        widget = angular.copy($scope.dashboard.widgets[key]);
    	                        currentWidgetIndex = key;
    	                    }
    	                });
    	            }
    	            //Set maximized widget to first column and first row
    	            //$scope.dashboardLocalstorage.widgets.push(widget);
    	            $timeout(function () {
    	                var alreadyExist = true;
    	                angular.forEach($scope.dashboardLocalstorage.widgets, function (value, key) {
    	                    if ($scope.dashboardLocalstorage.widgets[key].widgetID == widget.widgetID) {
    	                        $scope.dashboardLocalstorage.widgets[key] = angular.copy(widget);
    	                        alreadyExist = false;
    	                    }
    	                });
    	                if (alreadyExist) {
    	                    $scope.dashboardLocalstorage.widgets.push(widget);
    	                }
    	                if (widget.Componentid == CommonEnums.Component.AuditManagement) {
    	                    widget.isMaximized = true;
    	                }
    	                $scope.dashboard.widgets[currentWidgetIndex].isMaximized = true;
    	                //$scope.dashboard.widgets[currentWidgetIndex].col = 0;
    	                $scope.dashboard.widgets[currentWidgetIndex].row = (widget.col > 0 ? widget.row + 1 : widget.row);
    	                $scope.dashboard.widgets[currentWidgetIndex].sizeX = 3;
    	                $scope.dashboard.widgets[currentWidgetIndex].sizeY = 2;
    	                $scope.$broadcast('resize');
    	            }, 500);
    	            //    	            if (widget.row >= 1) {
    	            //    	                $("html, body").animate({ scrollTop: $(document).height() }, 1000);
    	            //    	            }
    	            $('html, body').animate({
    	                scrollTop: $('#' + widget.content + 'Chart').parent().parent().offset().top
    	            }, 1000);
    	        }
    	        //Minimize widget to original stage
    	        else {
    	            //On minimize reset widgets to their original stage
    	            //angular.copy($scope.dashboardLocalstorage.widgets, $scope.dashboard.widgets);
    	            //$scope.dashboardLocalstorage.widgets=angular.copy($scope.dashboard.widgets);
    	            //angular.forEach($scope.dashboard.widgets, function (value, key) {
    	            //if (value.widgetID == widget.widgetID) {
    	            widget.isMaximized = false;
    	            widget.sizeX = 1;
    	            widget.sizeY = 1;
    	            angular.forEach($scope.dashboardLocalstorage.widgets, function (value, key) {
    	                if ($scope.dashboardLocalstorage.widgets[key].widgetID == widget.widgetID) {
    	                    widget.col = $scope.dashboardLocalstorage.widgets[key].col;
    	                }
    	            });
    	            //}
    	            setTimeout(function () {
    	                if (widget.chart.api) { widget.chart.api.update(); }
    	            }, 400);
    	            //});
    	            $scope.$broadcast('resize');
    	            //Clear local storage
    	            //$scope.dashboardLocalstorage.widgets = [];

    	        }
    	    };

    	    $scope.ExportToPng = function (divId, widgetName, canWrite) {
    	        if (!canWrite) { return; }

    	        //                $('div[name=Chart_' + divId + '] nvd3 svg').attr("width", "800px");
    	        //    	        $('div[name=Chart_' + divId + '] nvd3 svg').attr("height", "500px");

    	        //    	        $('div[name=Chart_' + divId + '] nvd3 svg path').css("fill", "none").css("stroke", "#000").css("stroke-opacity", ".75").css("shape-rendering", "crispEdges");
    	        //    	        $('div[name=Chart_' + divId + '] nvd3 svg g g g g g g line').css("stroke", "#000").css("stroke-width", "1px").css("opacity", "0.1");

    	        //    	        var svgString = new XMLSerializer().serializeToString(document.querySelector('div[name=Chart_' + divId + '] nvd3 svg'));
    	        //    	        var DOMURL = self.URL || self.webkitURL || self;
    	        //    	        var img = new Image();
    	        //    	        var svg = new Blob([svgString], { type: "image/svg+xml;charset=utf-8" });
    	        //    	        var url = DOMURL.createObjectURL(svg);
    	        //    	        img.onload = function () {
    	        //    	            var canvas = document.createElement('canvas');
    	        //    	            var imgWidth = $('div[name=Chart_' + divId + '] nvd3 svg').width();
    	        //    	            var imgHeight = $('div[name=Chart_' + divId + '] nvd3 svg').height();

    	        //    	            canvas.setAttribute('width', imgWidth);
    	        //    	            canvas.setAttribute('height', imgHeight);

    	        //    	            var ctx = canvas.getContext("2d");
    	        //    	            ctx.textAlign = "center";
    	        //    	            ctx.drawImage(img, 0, 0);

    	        //    	            var png = canvas.toDataURL("image/png");
    	        //    	            DOMURL.revokeObjectURL(png);

    	        //    	            var link = document.createElement("a");
    	        //    	            link.href = canvas.toDataURL("image/png");
    	        //    	            link.style = "visibility:hidden";
    	        //    	            link.download = widgetName + ".png";
    	        //    	            document.body.appendChild(link);
    	        //    	            link.click();
    	        //    	            document.body.removeChild(link);
    	        //    	            document.body.removeAttribute(canvas);
    	        //    	            $('div[name=Chart_' + divId + '] nvd3 svg g g g g g g line').css("stroke", "").css("stroke-width", "").css("opacity", "");
    	        //    	        };
    	        //    	        img.src = url;


    	        //$('div[name=Chart_' + divId + '] nvd3 svg').css("background-color", "#fff");
    	        $('div[name=Chart_' + divId + '] nvd3 svg path').css("fill", "none").css("stroke", "#000").css("stroke-opacity", ".75").css("shape-rendering", "crispEdges");
    	        $('div[name=Chart_' + divId + '] nvd3 svg g g g g g g line').css("stroke", "#000").css("stroke-width", "1px").css("opacity", "0.1");
    	        var svg = new XMLSerializer().serializeToString(document.querySelector('div[name=Chart_' + divId + '] nvd3 svg'));

    	        var imgWidth = $('div[name=Chart_' + divId + '] nvd3 svg').width();
    	        var imgHeight = $('div[name=Chart_' + divId + '] nvd3 svg').height();

    	        var canvas = document.createElement('canvas');
    	        canvas.setAttribute('width', imgWidth);
    	        canvas.setAttribute('height', imgHeight);


    	        document.getElementById(divId + 'Chart').appendChild(canvas);
    	        canvg(canvas, svg);

    	        var destinationCanvas = document.createElement("canvas");
    	        destinationCanvas.width = canvas.width;
    	        destinationCanvas.height = canvas.height;

    	        var destCtx = destinationCanvas.getContext('2d');
    	        destCtx.fillStyle = "#FFFFFF";
    	        destCtx.fillRect(0, 0, canvas.width, canvas.height);


    	        //    	        var devicePixelRatio = window.devicePixelRatio || 1;
    	        //    	        var backingStoreRatio = destCtx.webkitBackingStorePixelRatio ||
    	        //                        destCtx.mozBackingStorePixelRatio ||
    	        //                        destCtx.msBackingStorePixelRatio ||
    	        //                        destCtx.oBackingStorePixelRatio ||
    	        //                        destCtx.backingStorePixelRatio || 1;

    	        //    	        var ratio = devicePixelRatio / backingStoreRatio;
    	        //    	        if (devicePixelRatio !== backingStoreRatio) {

    	        //    	            // adjust the original width and height of the canvas
    	        //    	            destinationCanvas.width = imgWidth * ratio;
    	        //    	            destinationCanvas.height = imgHeight * ratio;

    	        //    	            // scale the context to reflect the changes above
    	        //    	            destCtx.scale(ratio, ratio);
    	        //    	        }
    	        destCtx.drawImage(canvas, 0, 0);

    	        function detectIE() {
    	            var ua = window.navigator.userAgent;

    	            var msie = ua.indexOf('MSIE ');
    	            if (msie > 0) {
    	                // IE 10 or older => return version number
    	                return true;
    	            }

    	            var trident = ua.indexOf('Trident/');
    	            if (trident > 0) {
    	                // IE 11 => return version number
    	                var rv = ua.indexOf('rv:');
    	                return true;
    	            }

    	            var edge = ua.indexOf('Edge/');
    	            if (edge > 0) {
    	                // Edge (IE 12+) => return version number
    	                return true;
    	            }

    	            // other browser
    	            return false;
    	        }



    	        function download(canvas, filename) {
    	            if (detectIE() != false) {
    	                download_in_ie(canvas, filename);
    	            }
    	            else {
    	                download_with_link(canvas, filename);
    	            }
    	        }

    	        // Works in IE
    	        function download_in_ie(canvas, filename) {
    	            if (window.navigator.userAgent.indexOf('MSIE 9.0') > 0) { // IE 9
    	                var html = "<p>Right-click on image below and Save-Picture-As</p>";
    	                html += "<img src='" + canvas.toDataURL() + "' alt='from canvas'/>";
    	                var tab = window.open();
    	                tab.document.write(html);
    	            } else {
    	                var blob = canvas.msToBlob();
    	                window.navigator.msSaveBlob(blob, filename);
    	            }
    	        }

    	        function download_with_link(canvas, filename) {
    	            var a = document.createElement('a')
    	            a.download = filename
    	            a.href = canvas.toDataURL("image/png")
    	            document.body.appendChild(a);
    	            a.click();
    	            a.remove();
    	        }
    	        download(destinationCanvas, widgetName.trim().replace(/ /g, "_") + "-" + $rootScope.fileDateName + '.png');
    	        canvas.parentNode.removeChild(canvas);
    	        $('div[name=Chart_' + divId + '] nvd3 svg g g g g g g line').css("stroke", "").css("stroke-width", "").css("opacity", "");
    	    };

    	    $scope.ExportToExcel = function (widget) {
    	        if (!widget.CanWrite) { return; }
    	        var filename = widget.name.trim() + "-" + $rootScope.fileDateName + ".xls";
    	        CommonFunctions.DownloadReport('/DashBoard/ExportToExcel', filename, widget);
    	    };

    	    ////

    	    $scope.ExportToPngWithoutPermission = function (divId, widgetName, canWrite) {

    	        // if (!canWrite) { return; }


    	        //Start: For Line Chart PNG Download
    	        $('div[name=Chart_' + divId + '] .nvd3 .nv-axis line').css("stroke", "#000").css("stroke-width", "1px").css("opacity", "0.1");
    	        $('div[name=Chart_' + divId + '] .nvd3 .nv-point-paths path').css("stroke", "#fff").css("stroke-width", "1px").css("fill", "none");
    	        $('div[name=Chart_' + divId + '] .nvd3 .nv-background').css("fill", "none");
    	        //End: For Line Chart PNG Download

    	        var svg = new XMLSerializer().serializeToString(document.querySelector('div[name=Chart_' + divId + '] nvd3 svg'));

    	        var imgWidth = $('div[name=Chart_' + divId + '] nvd3 svg').width();
    	        var imgHeight = $('div[name=Chart_' + divId + '] nvd3 svg').height();

    	        var canvas = document.createElement('canvas');
    	        canvas.setAttribute('width', imgWidth);
    	        canvas.setAttribute('height', imgHeight);


    	        document.getElementById(divId + 'Chart').appendChild(canvas);
    	        canvg(canvas, svg);

    	        var destinationCanvas = document.createElement("canvas");
    	        destinationCanvas.width = canvas.width;
    	        destinationCanvas.height = canvas.height;

    	        var destCtx = destinationCanvas.getContext('2d');
    	        destCtx.fillStyle = "#FFFFFF";
    	        destCtx.fillRect(0, 0, canvas.width, canvas.height);

    	        destCtx.drawImage(canvas, 0, 0);

    	        function detectIE() {
    	            var ua = window.navigator.userAgent;

    	            var msie = ua.indexOf('MSIE ');
    	            if (msie > 0) {
    	                // IE 10 or older => return version number
    	                return true;
    	            }

    	            var trident = ua.indexOf('Trident/');
    	            if (trident > 0) {
    	                // IE 11 => return version number
    	                var rv = ua.indexOf('rv:');
    	                return true;
    	            }

    	            var edge = ua.indexOf('Edge/');
    	            if (edge > 0) {
    	                // Edge (IE 12+) => return version number
    	                return true;
    	            }

    	            // other browser
    	            return false;
    	        }



    	        function download(canvas, filename) {
    	            if (detectIE() != false) {
    	                download_in_ie(canvas, filename);
    	            }
    	            else {
    	                download_with_link(canvas, filename);
    	            }
    	        }

    	        // Works in IE
    	        function download_in_ie(canvas, filename) {
    	            if (window.navigator.userAgent.indexOf('MSIE 9.0') > 0) { // IE 9
    	                var html = "<p>Right-click on image below and Save-Picture-As</p>";
    	                html += "<img src='" + canvas.toDataURL() + "' alt='from canvas'/>";
    	                var tab = window.open();
    	                tab.document.write(html);
    	            } else {
    	                var blob = canvas.msToBlob();
    	                window.navigator.msSaveBlob(blob, filename);
    	            }
    	        }

    	        function download_with_link(canvas, filename) {
    	            var a = document.createElement('a')
    	            a.download = filename
    	            a.href = canvas.toDataURL("image/png")
    	            document.body.appendChild(a);
    	            setTimeout(function () {
    	                a.click();
    	                a.remove();
    	            }, 500);
    	            
    	        }
    	        //var d = new Date();
    	        var date = moment(); //Get the current date
    	        date.format("DD-MMM-YYYY");
    	        download(destinationCanvas, widgetName.trim().replace(/ /g, "_") + '-' + date.format("DD-MMM-YYYY") + '.png');
    	        canvas.parentNode.removeChild(canvas);
    	        // $('div[name=Chart_' + divId + '] nvd3 svg g g g g g g line').css("stroke", "").css("stroke-width", "").css("opacity", "");
    	        //$('div[name=Chart_' + divId + '] nvd3 svg g g g g g g g path').css("stroke", "").css("stroke-width", "").css("opacity", "");


    	    };

    	    $scope.ExportToExcelWithoutPermission = function (widget) {
    	        //if (!widget.CanWrite) { return; }
    	        var filename = widget.name.trim() + "-" + $rootScope.fileDateName + ".xls";
    	        CommonFunctions.DownloadReport('/DashBoard/ExportToExcel', filename, widget);
    	    };

    	}
    ])
})();   