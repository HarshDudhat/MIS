

angular.module("DorfKetalMVCApp").service('WidgetCommonService', ["$http", "$rootScope", "DashBoardService", "CommonEnums", function ($http, $rootScope, DashBoardService, CommonEnums) {
    var list = {};
    var xAxisHeading = [];
    var xAxisData = [];
    var tooltipData = [];

    //Creating chart option
    list.discreteBarChartOptions = function (SegrigationWise, chartType) {
        var leftmargin = 40;
        if (chartType == "multiBarHorizontalChart") {
            leftmargin = 90;
        }

        if (chartType == "pieChart") {

            return {
                chart: {
                    type: 'pieChart',
                    height: 500,
                    x: function (d) { return d.key; },
                    y: function (d) { return d.y; },
                    showLabels: true,
                    // noData: 'No Data', // show message if chart drawing data in not available
                    duration: 500,
                    labelThreshold: 0.05,
                    labelSunbeamLayout: true,
                    legend: {
                        margin: {
                            top: 10,
                            right: 10,
                            bottom: 10,
                            left: 10
                        },
                        maxKeyLength: 10 // Legend text length
                    }
                }
            }
        }
        else {
            return {
                chart: {
                    type: chartType,
                    margin: {
                        top: 10,
                        right: 10,
                        bottom: 65,
                        left: leftmargin
                    },
                    
                    valueFormat: function (d) {
                        return d;
                    },
                    clipEdge: true,
                    duration: 500,
                    stacked: false,
                    xAxis: {
                        axisLabel: SegrigationWise,
                        showMaxMin: false,
                        axisLabelDistance: 100,
                        rotateLabels: -25,
                        staggerLabels: false,
                        tickFormat: function (d) {
                            return d.length <= 10 ? d : d.substring(0, 10) + "..";
                            // return d.length <= 50 ? d : d.substring(0, 50) + "..";
                        }
                    },
                    yAxis: {
                        axisLabel: '',
                        axisLabelDistance: 8,
                        tickFormat: function (d) {
                            return d;
                        }
                    },
                    forceY: [0, 5],
                    showControls: false,
                    reduceXTicks: false,
                    useInteractiveGuideline: true,
                    interactiveLayer: { 
                       tooltip: {
                        enabled: true,
                        headerEnabled: false,
                        headerFormatter: function (d) { return d; },
                        valueFormatter: function (d) { return d == null ? 0 : d; }
                        } 
                    },
                    callback: function (chart) {
                        chart.multibar.dispatch.on('elementClick', function (e) {
                            //alert('dfsdf');
                        });
                    }
                },
                yDomain: [0, 10]//,
                //            title: {
                //                enable: true,
                //                text: SegrigationWise,
                //                css: {
                //                    "text-align": 'center',
                //                    "font-size": "15px",
                //                    margin: '0px'
                //                }
                //            }
            }

        }
    };


    list.discreteChartOptions = function (SegrigationWise, chartType, Componentid) {
        var leftmargin = 40;
        if (chartType == "multiBarHorizontalChart") {
            leftmargin = 90;
        }

        if (chartType == "pieChart") {
            return {
                chart: {
                    type: 'pieChart',
                    // height: 400,
                    x: function (d) { return d.key; },
                    y: function (d) { return d.y; },

                    showLabels: true,
                   // noData:'No Data',// show message if chart drawing data in not available
                    valueFormat: (d3.format('d')),
                    duration: 500,
                    labelThreshold: 0,
                    labelsOutside: true,
                    labelSunbeamLayout: true,
                    labelType: "value",
                    dispatch: {
                        renderEnd: function (d) {

                            for (var i in tooltipData) {
                                var gElem = $('.nv-series:contains("' + tooltipData[i].key + '")');
                                if (gElem.length > 0) {
                                    $(gElem[0]).attr('data-toggle', 'tooltip');
                                    $(gElem[0]).attr('data-placement', 'bottom');
                                    $(gElem[0]).attr('title', tooltipData[i].key);
                                }
                            }
                            $('[data-toggle="tooltip"]').tooltip();
                        }
                    },
                    legend: {
                        margin: {
                            top: 10,
                            right: 10,
                            bottom: 10,
                            left: 10
                        },
                        dispatch: {
                            legendMouseover: function (d) {
                                //$('.nv-series').attr('data-full-name', d.key);
                            }
                        },
                        maxKeyLength: 10 // Legend text length
                    }
                }
            }
        }
        // for Line Chart
        else if (chartType == "lineChart") {
            return {
                chart: {
                    type: 'lineChart',
                    // height: 450,
                    margin: {
                        top: 10,
                        right: 30,
                        bottom: 65,
                        left: 50//leftmargin
                    },
                    dispatch: {
                        renderEnd: function (d) {

                            for (var i in tooltipData) {
                                var gElem = $('.nv-series:contains("' + tooltipData[i].key + '")');
                                if (gElem.length > 0) {
                                    $(gElem[0]).attr('data-toggle', 'tooltip');
                                    $(gElem[0]).attr('data-placement', 'bottom');
                                    $(gElem[0]).attr('title', tooltipData[i].key);
                                }
                            }
                            $('[data-toggle="tooltip"]').tooltip();
                        }
                    },
                    labelThreshold: 0.05,
                    useInteractiveGuideline: false,  // true if want to show tool tip on entire line
                    interactiveLayer: {
                        tooltip: {
                            enabled: true // for Show tooltip on layer
                        }
                    },
                    showLegend: true,
                    x: function (d) { return d.x; },
                    y: function (d) { return d.y; },
                    xAxis: {
                        // axisLabelDistance:1,
                        showMaxMin: true,
                        orient: 'bottom',
                        //rotateLabels:90,
                        tickValues: xAxisData,
                        tickFormat: function (d) { return xAxisHeading[d]; }
                    }
                }
            }
        }
        else if (chartType == "multiBarChart" && Componentid == CommonEnums.Component.NoofDaysWithoutIncident) {

            return {
                chart: {
                    type: chartType,
                    margin: {
                        top: 10,
                        right: 10,
                        bottom: 65,
                        left: leftmargin
                    },
                    dispatch: {
                        renderEnd: function (d) {

                            for (var i in tooltipData) {
                                var gElem = $('.nv-series:contains("' + tooltipData[i].key + '")');
                                if (gElem.length > 0) {
                                    $(gElem[0]).attr('data-toggle', 'tooltip');
                                    $(gElem[0]).attr('data-placement', 'bottom');
                                    $(gElem[0]).attr('title', tooltipData[i].key);
                                }
                            }
                            $('[data-toggle="tooltip"]').tooltip();
                        }
                    },
                    showValues: true,
                    valueFormat: function (d) {
                        return d;
                    },
                    clipEdge: true,
                    duration: 500,
                    stacked: false,
                    interactiveLayer: {
                        tooltip: {
                            headerEnabled: false,
                            headerFormatter: function (d) {
                                return d;
                            }
                        }
                    },
                    tooltip: {
                        enabled: true,
                        headerEnabled:false,
                        headerFormatter: function (d) { return d; },
                        valueFormatter: function (d) { return d == null ? 0 : d; }
                    },
                    xAxis: {
                        axisLabel: SegrigationWise,
                        showMaxMin: false,
                        axisLabelDistance: 100,
                        //rotateLabels: -25,
                        staggerLabels: false,
                        tickFormat: function (d) {
                            // return d.length <= 10 ? d : d.substring(0, 10) + "..";
                            return d.length <= 50 ? d : d.substring(0, 50) + "..";
                        }
                    },
                    yAxis: {
                        axisLabel: '',
                        axisLabelDistance: 8,
                        tickFormat: function (d) {
                            return d;
                        }
                    },
                    forceY: [0, 5],
                    showControls: false,
                    reduceXTicks: false,
                    useInteractiveGuideline: false,
                    callback: function (chart) {
                        chart.multibar.dispatch.on('elementClick', function (e) {
                        });
                    }
                },
                yDomain: [0, 10]
            }


        }
        else {
            return {
                chart: {
                    type: chartType,
                    margin: {
                        top: 10,
                        right: 10,
                        bottom: 65,
                        left: leftmargin
                    },
                    dispatch: {
                        renderEnd: function (d) {

                            for (var i in tooltipData) {
                                var gElem = $('.nv-series:contains("' + tooltipData[i].key + '")');
                                if (gElem.length > 0) {
                                    $(gElem[0]).attr('data-toggle', 'tooltip');
                                    $(gElem[0]).attr('data-placement', 'bottom');
                                    $(gElem[0]).attr('title', tooltipData[i].key);
                                }
                            }
                            $('[data-toggle="tooltip"]').tooltip();
                        }
                    },
                    showValues: true,
                    valueFormat: function (d) {
                        return d;
                    },
                    clipEdge: true,
                    duration: 500,
                    stacked: false,
                    tooltip: {
                        enabled: true,
                        headerFormatter: function (d) { return d; },
                        valueFormatter: function (d) {return d==null ? 0 : d;} 
                    },
                    xAxis: {
                        axisLabel: SegrigationWise,
                        showMaxMin: false,
                        axisLabelDistance: 100,
                        rotateLabels: -25,
                        staggerLabels: false,
                        tickFormat: function (d) {
                            return d.length <= 10 ? d : d.substring(0, 10) + "..";
                            // return d.length <= 50 ? d : d.substring(0, 50) + "..";
                        }
                    },
                    yAxis: {
                        axisLabel: '',
                        axisLabelDistance: 8,
                        tickFormat: function (d) {
                            return d;
                        }
                    },
                    forceY: [0, 5],
                    showControls: false,
                    reduceXTicks: false,
                    useInteractiveGuideline: false,
                    callback: function (chart) {
                        chart.multibar.dispatch.on('elementClick', function (e) {
                        });
                    }
                },
                yDomain: [0, 10]
            }
        }
    };


    list.discreteChartData = function (widgetSetting) {

        //Start :: Get BodyPart Data
        if (widgetSetting.Componentid == CommonEnums.Component.BodyPart) {
            DashBoardService.GetBodyPartData(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                        
                    }
                    else {
                        //response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatPaiChartWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get BodyPart Data

        //Start :: Get Nature of INjury Wise Analysis
        if (widgetSetting.Componentid == CommonEnums.Component.NatureOfInjury) {
            DashBoardService.GetNatureOfInjury(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        // response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatPaiChartWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get Nature of INjury Wise Analysis

        //Start :: Get No of Days Without Incident
        if (widgetSetting.Componentid == CommonEnums.Component.NoofDaysWithoutIncident) {
            DashBoardService.GetNoofDaysWithoutIncident(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                        ////////////////
                        var CurrentWidgetid = widgetSetting.content;
                        $("#" + CurrentWidgetid + "table").show();
                        $("#" + CurrentWidgetid + "Chart").hide();
                        var table = $("#" + CurrentWidgetid + "table").children();
                        table.html('');
                        var tableHeading = "<thead><tr><th>" + "Description" + "</th>";
                        var tableBody = "<tbody>";
                        var data = response.data.Result;

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
                                    $("#a_" + widgetSetting.content).css('display', '');
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
                                    $("#a_" + widgetSetting.content).css('display', 'none');
                                    table.append("<tr><td align='center' style='text-align: center'>No Data Available</td>");
                                }
                            }
                        });
                        table.append(tableBody);
                        ///////
                    }
                    else {
                        // response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatPaiChartWidgetStSegration(response.data.Result);
                    }
                }
            });
        }
        //End :: Get Days of Incident

        //Start :: Get TRCF
        if (widgetSetting.Componentid == CommonEnums.Component.TRCFMonthlyAnalysis) {
            DashBoardService.GetTRCFByMonth(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        // response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatLineWidgetTRFC(response.data.Result);

                    }
                }
            });
        }
        //End :: Get TRCF

        //Start :: Get Severity
        if (widgetSetting.Componentid == CommonEnums.Component.SeverityMonthlyAnalysis) {
            DashBoardService.GetSeverityByMonthly(widgetSetting).then(function (response) {
                // alert(' for line chart'+ widgetSetting.Componentid);
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        // response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatLineWidgetSeverity(response.data.Result);
                    }
                }
            });
        }
        //End :: Get Severity

        return;
    };

    list.discreteBarChartData = function (widgetSetting) {
        //Start :: Get Task Management Data        
        if (widgetSetting.Componentid == CommonEnums.Component.TaskManagement) {
            DashBoardService.GetTaskData(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatTaskMgtWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get Task Management Data

        //Start :: Get Incident Management Data
        if (widgetSetting.Componentid == CommonEnums.Component.IncidentManagement) {
            DashBoardService.GetIncidentData(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatIncidentMgtWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get Incident Management Data

        //Start :: Get CAPA Management Data
        if (widgetSetting.Componentid == CommonEnums.Component.CAPAManagement) {
            DashBoardService.GetCAPAData(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatTaskMgtWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get CAPA Management Data

        //Start :: Get Audit Data
        if (widgetSetting.Componentid == CommonEnums.Component.AuditManagement) {
            DashBoardService.GetAuditData(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        if (response.data.Result[0]["stSegregation"] != "No Data Available") {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatAuditMgtWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get Audit Data

        //Start :: Get BBS Observation Data
        if (widgetSetting.Componentid == CommonEnums.Component.BBSObservation) {
            DashBoardService.GetBBSObservationData(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        if (response.data.Result[0]["stSegregation"] != "No Data Available") {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatAuditMgtWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get BBS Observation Data

        //Start :: Get Site Inspection Data
        if (widgetSetting.Componentid == CommonEnums.Component.SiteInspection) {
            DashBoardService.GetSiteInspectionData(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        if (response.data.Result[0]["stSegregation"] != "No Data Available") {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatAuditMgtWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get Site Inspection Data

        //Start :: Get BodyPart Data
        if (widgetSetting.Componentid == CommonEnums.Component.BodyPart) {
            DashBoardService.GetBodyPartData(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        //response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatAuditMgtWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get BodyPart Data


        //Start :: Get No of Days Without Incident
        if (widgetSetting.Componentid == CommonEnums.Component.NoofDaysWithoutIncident) {
            DashBoardService.GetNoofDaysWithoutIncident(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        // response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatAuditMgtWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get Days of Incident


        //Start :: Get Nature of INjury Wise Analysis
        if (widgetSetting.Componentid == CommonEnums.Component.NatureOfInjury) {
            DashBoardService.GetNatureOfInjury(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        // response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatAuditMgtWidget(response.data.Result);

                    }
                }
            });
        }
        //End :: Get Nature of INjury Wise Analysis


        //Start :: Get TRCF
        if (widgetSetting.Componentid == CommonEnums.Component.TRCFMonthlyAnalysis) {
            DashBoardService.GetTRCFByMonth(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        // response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatAuditMgtWidget(response.data.Result);

                    }
                }
            });
        }
        //End :: Get TRCF

        //Start :: Get Severity
        if (widgetSetting.Componentid == CommonEnums.Component.SeverityMonthlyAnalysis) {
            DashBoardService.GetSeverityByMonthly(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        // response.data.Result[0]["stSegregation"] != "No Data Available"
                        if (response.data.Result.length != 0) {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatAuditMgtWidget(response.data.Result);

                    }
                }
            });
        }
        //End :: Get Severity

        //Start :: Get Safety Observation Data
        if (widgetSetting.Componentid == CommonEnums.Component.SafetyObservation) {
            DashBoardService.GetSafetyObservationData(widgetSetting).then(function (response) {
                if (response) {
                    if (widgetSetting.ChartTypeId == CommonEnums.ComponentSubFilter.Table) {
                        widgetSetting.chart.data = response.data.Result;
                    }
                    else {
                        if (response.data.Result[0]["stSegregation"] != "No Data Available") {
                            $("#a_" + widgetSetting.content).css('display', '');
                            $("#ac_" + widgetSetting.content).css('display', '');
                        }
                        else {
                            $("#a_" + widgetSetting.content).css('display', 'none');
                            $("#ac_" + widgetSetting.content).css('display', 'none');
                        }
                        widgetSetting.chart.data = FormatAuditMgtWidget(response.data.Result);
                    }
                }
            });
        }
        //End :: Get Site Inspection Data
        return;
    };

    function FormatTaskMgtWidget(data) {
        var stSegregation = $.unique($.map(data, function (val) {
            return val.stSegregation
        }));

        var formatedData = [];
        $.each(["Open", "Inprogress", "Close"], function (index, value) {
            formatedData.push({
                key: value,
                values: $.map(stSegregation, function (area, i) {
                    if (value == "Open") {
                        return { x: area, y: data[i].Open };
                    }
                    else if (value == "Inprogress") {
                        return { x: area, y: data[i].Inprogress };
                    }
                    else {
                        return { x: area, y: data[i].Close };
                    }
                })
            });
        })

        return formatedData;
    }
    function FormatIncidentMgtWidget(data) {
        var stSegregation = $.unique($.map(data, function (val) {
            return val.stSegregation
        }));

        var formatedData = [];
        $.each(["FAC", "MTC", "FAT", "LTA", "Near Miss"], function (index, value) {
            formatedData.push({
                key: value,
                values: $.map(stSegregation, function (area, i) {
                    if (value == "FAC") {
                        return { x: area, y: data[i].FAC };
                    }
                    else if (value == "MTC") {
                        return { x: area, y: data[i].MTC };
                    }
                    else if (value == "FAT") {
                        return { x: area, y: data[i].FAT };
                    }
                    else if (value == "LTA") {
                        return { x: area, y: data[i].LTA };
                    }
                    else {
                        return { x: area, y: data[i].Nea_Miss };
                    }
                })
            });
        })

        return formatedData;
    }

    function FormatAuditMgtWidget(data) {
        var stSegregation = $.unique($.map(data, function (val) {
            return val.stSegregation
        }));
        var ColumnName = new Array();
        var formatedData = [];
        var t = 0;
        for (var propName in data[0]) {
            if (propName != 'stSegregation') {
                formatedData.push({
                    key: propName,
                    values: $.map(stSegregation, function (area, i) {
                        return { x: area, y: data[i][propName] };
                    })
                });
            }
        }

        tooltipData = angular.copy(formatedData);
        return formatedData;
    }


    function FormatPaiChartWidget(data) {
        var stSegregation = $.unique($.map(data, function (val) {
            return val.stSegregation
        }));
        var ColumnName = new Array();
        var formatedData = [];
        var t = 0;
        for (var propName in data[0]) {
            //if (propName != 'stSegregation') {
            formatedData.push({
                key: propName,
                y: data[0][propName]
            });
            // }
        }
        tooltipData = angular.copy(formatedData);
        return formatedData;
    }


    function FormatPaiChartWidgetStSegration(data) {
        var stSegregation = $.unique($.map(data, function (val) {
            return val.stSegregation
        }));
        var ColumnName = new Array();
        var formatedData = [];
        var t = 0;

        for (var i in data) {
            //if (propName != 'stSegregation') {
            formatedData.push({
                key: data[i].stSegregation,
                y: data[i].Days
            });
            // }
        }
        tooltipData = angular.copy(formatedData);
        return formatedData;
    }


    function FormatLineWidgetSeverity(data) {
        var stSegregation = $.unique($.map(data, function (val) {
            return val.stSegregation
        }));
        var ColumnName = new Array();
        var formatedData = [];
        var xyData = [];

        for (var i in data) {
            xAxisHeading.push(data[i].stSegregation);
            xAxisData.push(parseInt(i));
            xyData.push({
                x: parseInt(i),
                y: data[i].Severity
            });
        }

        formatedData.push({
            key: 'Severity',
            values: xyData
        });

        tooltipData = angular.copy(formatedData);
        return formatedData;
    };

    function FormatLineWidgetTRFC(data) {
        var stSegregation = $.unique($.map(data, function (val) {
            return val.stSegregation
        }));
        var ColumnName = new Array();
        var formatedData = [];
        var xyData = [];

        for (var i in data) {
            xAxisHeading.push(data[i].stSegregation);
            xAxisData.push(parseInt(i));
            xyData.push({
                x: parseInt(i),
                y: data[i]["TRCF Rate"]
            });
        }

        formatedData.push({
            key: 'TRCF Rate',
            values: xyData
        });

        tooltipData = angular.copy(formatedData);
        return formatedData;
    }

    return list;
} ]);

