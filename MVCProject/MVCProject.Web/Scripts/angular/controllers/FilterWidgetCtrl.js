(function () {
    'use strict';
    angular.module("DorfKetalMVCApp")
    //Filter popup 
    .controller('FilterWidgetCtrl', ['$scope', '$timeout', 'filterFilter', '$rootScope', '$uibModalInstance', 'widget', 'DashBoardService', 'CommonEnums', 'CommonFunctions', 'WidgetCommonService',
	function ($scope, $timeout, filterFilter, $rootScope, $uibModalInstance, widget, DashBoardService, CommonEnums, CommonFunctions, WidgetCommonService) {
	    //Dismiss popup
	    $scope.enum = angular.copy(CommonEnums);

	    $scope.timelineRanges = [];
	    $scope._objChartType = {
	        ChartTypeId: 0,
	        SegregationId: 0,
	        SiteId: new Array(),
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
	        InjuryPotentialId: new Array(),
	        SafetyObservationTypeId: new Array(),
	        SafetyObserverId: new Array()
	    };



	    $scope.filteredValues = {};
	    $scope.filteredValues.TimeLineDateRange = $rootScope.dateRanges['Today'];

	    $scope.dismiss = function () {
	        $uibModalInstance.dismiss();
	    };

	    $scope.MultiSelectiondropdownData = {
	        SiteData: [],
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
	        CAPAType: [],
	        InjuryPotential: [],
	        SafetyObservationType: [],
	        SafetyObserver: []
	    }

	    $scope.SetTimeLine = function () {
	        if ($scope.timeline != null) {
	            if ($scope.timeline[0] != null && $scope.timeline[1] != null) {
	                $scope._objChartType.TimeLineStartDate = $scope.timeline[0].toDate();
	                $scope._objChartType.TimeLineEndDate = $scope.timeline[1].toDate();
	            }
	            widget.TimeLineId = $scope.timeline[2];
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
	    }

	    $scope.GetComponentSetting = function () {
	        $scope.ResponeseData = widget.Filter;
	        //Filling MultiDropDown data using filter
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

	        //$scope._objChartType.TimeLineStartDate = new Date(widget.TimeLineStartDate);
	        //$scope._objChartType.TimeLineEndDate = new Date(widget.TimeLineEndDate);


	        //$scope._objChartType.TimeLineStartDate = typeof (widget.TimeLineStartDate) == "string" ? moment(widget.TimeLineStartDate.format("DD-MMM-YYYY")).toDate() : new Date(widget.TimeLineStartDate);
	        //$scope._objChartType.TimeLineEndDate = typeof (widget.TimeLineEndDate) == "string" ? moment(widget.TimeLineEndDate.format("DD-MMM-YYYY")).toDate() : new Date(widget.TimeLineEndDate);
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
	                $scope.timeline = null;
	                for (var i = 0; i < $scope.timelineRanges.length; i++) {
	                    var Range = $scope.timelineRanges[i].Range;
	                    if (angular.isDefined(Range) && Range != null && Range[0] != null && Range[1] != null) {
	                        //if (Range[0].format("DD-MM-YYYY") == moment(widget.TimeLineStartDate).format("DD-MM-YYYY") && Range[1].format("DD-MM-YYYY") == moment(widget.TimeLineEndDate).format("DD-MM-YYYY")) {
	                        if (Range[0].format("DD-MM-YYYY") == moment(widget.TimeLineStartDate).format("DD-MM-YYYY") && Range[1].format("DD-MM-YYYY") == moment(widget.TimeLineEndDate).format("DD-MM-YYYY")||(Range[0].format("DD-MMM-YYYY") == widget.TimeLineStartDate && Range[1].format("DD-MMM-YYYY") == widget.TimeLineEndDate.substring(0, 11))) {
	                            $scope.timeline = Range;
	                        }
	                    }
	                }
	            }
	        } else {
	            $scope.timeline = $scope.timelineRanges[0].Range;
	        }
	    };

	    //End :: Get Component Setting

	    //Get Data of widget according filters 
	    $scope.GetData = function (_objChartType) {
	        widget.ChartTypeId = $scope.selectedChartTypeId;
	        widget.SegregationId = $scope.selectedSegregationId
	        widget.SiteId = $scope._objChartType.SiteId.map(function (item) { return item.ComponentSubFilterId; });
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
	        widget.SafetyObserverId = $scope._objChartType.SafetyObserverId.map(function (item) { return item.ComponentSubFilterId; });
	        widget.TimeLineStartDate = moment($scope._objChartType.TimeLineStartDate).format('DD-MMM-YYYY');
	        widget.TimeLineEndDate = moment($scope._objChartType.TimeLineEndDate).format('DD-MMM-YYYY 23:59:59');

	        if (widget.SegregationId == CommonEnums.ComponentSubFilter.TimelinewiseWeekly && (widget.TimeLineId != CommonEnums.ComponentSubFilter.Last7Days && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last15Days && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last30Days && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last2Months)) {
	            toastr.error(TimeLineDateRangeIsRequiredForWeekly, errorTitle);
	            return false;
	        }
	        if (widget.SegregationId == CommonEnums.ComponentSubFilter.TimelinewiseMonthly && (widget.TimeLineId != CommonEnums.ComponentSubFilter.Last2Months && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last3Months && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last6Months && widget.TimeLineId != CommonEnums.ComponentSubFilter.Last12Months && widget.TimeLineId != CommonEnums.ComponentSubFilter.LastYear)) {
	            toastr.error(TimeLineDateRangeIsRequiredForMonthly, errorTitle);
	            return false;
	        }

	        var SegregationName = "";
	        if ($scope._objChartType.SegregationId > 0) {
	            SegregationName = $.grep($scope.ResponeseData, function (f) {
	                return f.ComponentSubFilterId == widget.SegregationId;
	            })[0].SubFilterName;
	        }

	        if ((widget.Componentid == CommonEnums.Component.TaskManagement || widget.Componentid == CommonEnums.Component.CAPAManagement) && $scope.selectedChartTypeId == CommonEnums.ComponentSubFilter.Table) {
	            var CurrentWidgetid = widget.content;
	            $("#" + CurrentWidgetid + "table").show();
	            $("#" + CurrentWidgetid + "Chart").hide();
	            var table = $("#" + CurrentWidgetid + "table").children();
	            table.html('');
	            table.append("<thead><tr><th>" + SegregationName + "</th><th>Open</th><th>Inprogress</th><th>Close</th></tr></thead>");
	            table.append("<tbody>");
	            if (widget.Componentid == CommonEnums.Component.TaskManagement) {
	                DashBoardService.GetTaskData(widget).then(function (response) {
	                    if (response) {
	                        var data = response.data.Result;
	                        widget.chart.data = data;
	                        if (data.length > 0) {
	                            $("#a_" + widget.content).css('display', '');
	                            $.each(data, function (i) {
	                                table.append("<tr><td>" + data[i].stSegregation + "</td><td>" + data[i].Open + "</td><td>" + data[i].Inprogress + "</td><td>" + data[i].Close + "</td></tr>");
	                            });
	                        }
	                        else {
	                            $("#a_" + widget.content).css('display', 'none');
	                            table.append("<tr><td colspan='4' align='center' style='text-align: center'>No Data Available</td>");
	                        }
	                    }

	                    widget.TimeLineStartDate = angular.copy($scope._objChartType.TimeLineStartDate);
	                    widget.TimeLineEndDate = angular.copy($scope._objChartType.TimeLineEndDate);
	                });
	            }
	            else {
	                DashBoardService.GetCAPAData(widget).then(function (response) {
	                    if (response) {
	                        var data = response.data.Result;
	                        widget.chart.data = data;
	                        if (data.length > 0) {
	                            $("#a_" + widget.content).css('display', '');
	                            $.each(data, function (i) {
	                                table.append("<tr><td>" + data[i].stSegregation + "</td><td>" + data[i].Open + "</td><td>" + data[i].Inprogress + "</td><td>" + data[i].Close + "</td></tr>");
	                            });
	                        }
	                        else {
	                            $("#a_" + widget.content).css('display', 'none');
	                            table.append("<tr><td colspan='4' align='center' style='text-align: center'>No Data Available</td>");
	                        }
	                    }

	                    widget.TimeLineStartDate = angular.copy($scope._objChartType.TimeLineStartDate);
	                    widget.TimeLineEndDate = angular.copy($scope._objChartType.TimeLineEndDate);
	                });
	            }
	            table.append("</tbody>");
	        }
	        else if (widget.Componentid == CommonEnums.Component.IncidentManagement && $scope.selectedChartTypeId == CommonEnums.ComponentSubFilter.Table) {
	            var CurrentWidgetid = widget.content;
	            $("#" + CurrentWidgetid + "table").show();
	            $("#" + CurrentWidgetid + "Chart").hide();
	            var table = $("#" + CurrentWidgetid + "table").children();
	            table.html('');
	            table.append("<thead><tr><th>" + SegregationName + "</th><th>FAC</th><th>MTC</th><th>FAT</th><th>LTA</th><th>Near Miss</th></tr></thead>");
	            table.append("<tbody>");
	            DashBoardService.GetIncidentData(widget).then(function (response) {
	                if (response) {
	                    var data = response.data.Result;
	                    widget.chart.data = data;
	                    if (data.length > 0) {
	                        $("#a_" + widget.content).css('display', '');
	                        $.each(data, function (i) {
	                            table.append("<tr><td>" + data[i].stSegregation + "</td><td>" + data[i].FAC + "</td><td>" + data[i].MTC + "</td><td>" + data[i].FAT + "</td><td>" + data[i].LTA + "</td><td>" + data[i].Nea_Miss + "</td></tr>");
	                        });
	                    }
	                    else {
	                        $("#a_" + widget.content).css('display', 'none');
	                        table.append("<tr><td colspan='6' align='center' style='text-align: center'>No Data Available</td>");
	                    }
	                }

	                widget.TimeLineStartDate = angular.copy($scope._objChartType.TimeLineStartDate);
	                widget.TimeLineEndDate = angular.copy($scope._objChartType.TimeLineEndDate);
	            });
	            table.append("</tbody>");
	        }
	        else if ((widget.Componentid == CommonEnums.Component.AuditManagement || widget.Componentid == CommonEnums.Component.BBSObservation || widget.Componentid == CommonEnums.Component.SiteInspection || widget.Componentid == CommonEnums.Component.SafetyObservation) && $scope.selectedChartTypeId == CommonEnums.ComponentSubFilter.Table) {
	            var CurrentWidgetid = widget.content;
	            $("#" + CurrentWidgetid + "table").show();
	            $("#" + CurrentWidgetid + "Chart").hide();
	            var table = $("#" + CurrentWidgetid + "table").children();
	            table.html('');
	            var tableHeading = "<thead><tr><th>" + SegregationName + "</th>";
	            var tableBody = "<tbody>";
	            //table.append("<tbody>");
	            if (widget.Componentid == CommonEnums.Component.AuditManagement) {
	                DashBoardService.GetAuditData(widget).then(function (response) {
	                    if (response) {
	                        var data = response.data.Result;
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
	                    }

	                    widget.TimeLineStartDate = angular.copy($scope._objChartType.TimeLineStartDate);
	                    widget.TimeLineEndDate = angular.copy($scope._objChartType.TimeLineEndDate);
	                });
	            }
	            if (widget.Componentid == CommonEnums.Component.BBSObservation) {
	                DashBoardService.GetBBSObservationData(widget).then(function (response) {
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
	            if (widget.Componentid == CommonEnums.Component.SiteInspection) {
	                DashBoardService.GetSiteInspectionData(widget).then(function (response) {
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
	            if (widget.Componentid == CommonEnums.Component.SafetyObservation) {
	                DashBoardService.GetSafetyObservationData(widget).then(function (response) {
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
	            table.append("</tbody>");
	        }
	        else {
	            var CurrentWidgetid = widget.content;
	            $("#" + CurrentWidgetid + "table").hide();
	            $("#" + CurrentWidgetid + "Chart").show();
	            var chartType = "multiBarChart";
	            if ($scope.selectedChartTypeId == CommonEnums.ComponentSubFilter.Bar) {
	                chartType = "multiBarHorizontalChart";
	            }
	            widget.chart.options = WidgetCommonService.discreteBarChartOptions(SegregationName, chartType);
	            WidgetCommonService.discreteBarChartData(widget);
	            widget.chart.api.refreshWithTimeout(400);
//	            if (widget.Componentid == CommonEnums.Component.AuditManagement) {
//	                DashBoardService.GetAuditData(widget).then(function (response) {
//	                    if (response) {
//	                        var data = response.data.Result;
//	                        if (data) {
//	                            if (Object.keys(data[0]).length > 10) {
//	                                setTimeout(function () { $scope.minmaximizeWidget(widget) }, 1000); ;
//	                            }
//	                        }
//	                    }
//	                    widget.TimeLineStartDate = angular.copy($scope._objChartType.TimeLineStartDate);
//	                    widget.TimeLineEndDate = angular.copy($scope._objChartType.TimeLineEndDate);
//	                });
//	            }


	        }

	        $uibModalInstance.dismiss();
	    };
	    $scope.reloadRanges = true;
	    $scope.UpdateTimeRange = function (componentSubFilterId) {
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
	}

])
})();