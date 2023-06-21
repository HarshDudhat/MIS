angular.module("DorfKetalMVCApp").service("CommonFunctions", ["$rootScope", "$timeout", "$cookies", "$window", "$filter", CommonFunctions]);

function CommonFunctions($rootScope, $timeout, $cookies, $window, $filter) {
    var vm = this;

    // Download Report from API
    vm.DownloadReport = function (apiUrl, fileName, postData) {
        postData = angular.isDefined(postData) ? JSON.stringify(postData) : "";
        var params = { apiUrl: apiUrl, fileName: fileName, postData: postData };
        var form = document.createElement("form");
        form.setAttribute("method", "POST");
        form.setAttribute("action", "/Base/DownloadReport");
        form.setAttribute("target", "_blank");

        for (var key in params) {
            if (params.hasOwnProperty(key)) {
                var hiddenField = document.createElement("input");
                hiddenField.setAttribute("type", "hidden");
                hiddenField.setAttribute("name", key);
                hiddenField.setAttribute("value", params[key]);

                form.appendChild(hiddenField);
            }
        }

        document.body.appendChild(form);
        form.submit();
    };

    // Get TimeZone
    vm.GetTimeZone = function () {
        var timezone = "+05:30"; //India
        var zoneval = (0 - ((new Date()).getTimezoneOffset()) / 60).toFixed(1);
        var h = parseInt(zoneval.split('.')[0]);
        var m = parseInt(zoneval.split('.')[1]);
        var mStr = ((parseInt(m) / 10) * 60).toString();
        var sign = "";
        if (h > 0) {
            sign = "+";
        }
        if (h < 0) {
            sign = "-";
        }

        timezone = sign + ("0" + h).slice(-2) + ":" + mStr;
        return timezone;
    };

    //Get Local date.
    vm.GetLocalDate = function (utcdate) {
        var currentdate = new Date();
        var timezoneoffset = currentdate.getTimezoneOffset();
        var localDate = new Date(utcdate);
        localDate = new Date(localDate.getTime() - timezoneoffset * 60 * 1000);
        return localDate;
    };

    // Getting local time zone offset minutes
    vm.GetTimeZoneMinutes = function () {
        var timezone = "+05:30"; //India
        var zoneval = 0 - ((new Date()).getTimezoneOffset());
        return zoneval;
    };

    // Set Default Range
    vm.SetDefaultRange = function () {
        $timeout(function () {
            $(".ranges").each(function (i, obj) {
                $(obj).find("li:eq('1')").trigger('click');
            });
        }, 0);
    };

    // Set Default Range As Running Year
    vm.SetDefaultRangeRunningYear = function () {
        $timeout(function () {
            $(".ranges").each(function (i, obj) {
                $(obj).find("li:eq('1')").trigger('click');
            });
        }, 0);
    };

    // Set Default Range as Last Seven Days
    vm.SetLastSevendaysDefaultRange = function () {
        $timeout(function () {
            $(".ranges li:eq('2')").trigger('click');
        }, 0);
    };

    // Set Default Range as Last Seven Days
    vm.SetLast30DaysDefaultRange = function () {
        $timeout(function () {
            $(".ranges li:eq('3')").trigger('click');
        }, 0);
    };

    // Set Default Range as Last Seven Days
    vm.SetDefaultRangeNearmiss = function (index) {
        $timeout(function () {
            $(".ranges li:eq('" + index + "')").trigger('click');
        }, 0);
    };

    // Set value in Cookie
    vm.SetCookie = function (key, val) {
        $cookies.put($rootScope.cookieName + key, val, { path: "/" });
    };

    // get Cookie value
    vm.GetCookie = function (key) {
        return $cookies.get($rootScope.cookieName + key);
    };

    // Redirect to Login
    vm.RedirectToLoginPage = function (NoSession) {
        sessionStorage.removeItem("IsSmallMenu");
        localStorage.setItem("logout", true);
        vm.RedirectToLogout(NoSession);
    };

    // Redirect to logout
    vm.RedirectToLogout = function (NoSession) {
        $window.location.href = "/Account/Logout" + (NoSession ? "?noSession=y" : "");
    };

    // Redirect to Error Page
    vm.RedirectToErrorPage = function (Code) {
        $window.location.href = "/Error/ServerError/" + Code;
    };

    // Redirect to DefaultUrl
    vm.RedirectToDefaultUrl = function () {
        $window.location.href = "/Account/RedirectToDefaultUrl";
    };

    // Scroll to top
    vm.ScrollToTop = function () {
        $("html, body").animate({ scrollTop: 0 }, 500);
    };

    // Scroll to top
    vm.ScrollUp = function () {
        $("html, body").animate({ scrollTop: 0 }, 0);
    };

    // Scroll to Element and focus
    vm.ScrollUpAndFocus = function (id) {
        $("html, body").animate({ scrollTop: 0 }, 500);
        $("#" + id).focus();
    };

    // Encrypt Data
    vm.EncryptData = function (data) {
        if (data) {
            var key = CryptoJS.enc.Utf8.parse('acg7ay8h447825cg');
            var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
            var EencryptedData = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(data), key,
                {
                    keySize: 128 / 8,
                    iv: iv,
                    mode: CryptoJS.mode.CBC,
                    padding: CryptoJS.pad.Pkcs7
                }).toString();
            return encodeURIComponent(EencryptedData);
        } else {
            return "";
        }
    };

    // Decrypt Data
    vm.DecryptData = function (data) {
        if (data) {
            data = decodeURIComponent(data);
            var key = CryptoJS.enc.Utf8.parse('acg7ay8h447825cg');
            var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
            var DecryptedSession = CryptoJS.AES.decrypt(data, key,
                {
                    keySize: 128 / 8,
                    iv: iv,
                    mode: CryptoJS.mode.CBC,
                    padding: CryptoJS.pad.Pkcs7
                }).toString(CryptoJS.enc.Utf8);
            return DecryptedSession;
        } else {
            return "";
        }
    };

    //Encrypt Url
    vm.EncryptUrl = function (params) {
        var EncryptedParams = vm.EncryptData(params);
        return encodeURIComponent(encryptedParams);
    };

    // Get Paging Params
    vm.GetPagingParams = function (params, pageNo, pageCount, orderByColumn) {
        var PagingParams = {
            CurrentPageNumber: pageNo || 1,
            PageSize: pageCount || 10,
            IsAscending: true,
            OrderByColumn: orderByColumn
        };
        if (params != null) {
            PagingParams.CurrentPageNumber = params.page() || 1;
            PagingParams.PageSize = params.count() || 10;
            if (params.orderBy().length > 0) {
                PagingParams.IsAscending = params.orderBy().toString().charAt(0) == "+" ? true : false;;
                PagingParams.OrderByColumn = PagingParams.IsAscending == true ? params.orderBy()[0].replace("+", "") : params.orderBy()[0].replace("-", "") || "";
            }
        }
        return PagingParams;
    }

    // Get Date range
    vm.GetDateRange = function (rangepickerid, isTask) {
        var startRangeFormat = 'DD-MMM-YYYY';
        var endRangeFormat = 'DD-MMM-YYYY 23:59:59';
        var range = {
            startDate: moment().format(startRangeFormat),
            endDate: moment().format(endRangeFormat)
        };
        if (isTask) {
            range = {
                startDate: moment().subtract('days', 6).format(startRangeFormat),
                endDate: moment().subtract('days', 0).format(endRangeFormat)
            };
        }
        if ($(rangepickerid).val()) {
            var StartDate = $(rangepickerid).val().split(' ')[0];
            var EndDate = $(rangepickerid).val().split(' ')[2];
            var sDate = $filter("toDate")(StartDate);
            var eDate = $filter("toDate")(EndDate);
            range = {
                startDate: moment(sDate).format(startRangeFormat),
                endDate: moment(eDate).format(endRangeFormat)
            };
        }
        return range;
    }


    // Get Date range
    vm.GetDateRangeLast30daysDefault = function (rangepickerid, isTask) {
        var startRangeFormat = 'DD-MMM-YYYY';
        var endRangeFormat = 'DD-MMM-YYYY 23:59:59';
        var range = {
            startDate: moment().format(startRangeFormat),
            endDate: moment().format(endRangeFormat)
        };
        if (isTask) {
            range = {
                startDate: moment().subtract('days', 6).format(startRangeFormat),
                endDate: moment().subtract('days', 0).format(endRangeFormat)
            };
        }
        if ($(rangepickerid).val()) {
            var StartDate = $(rangepickerid).val().split(' ')[0];
            var EndDate = $(rangepickerid).val().split(' ')[2];
            var sDate = $filter("toDate")(StartDate);
            var eDate = $filter("toDate")(EndDate);
            range = {
                startDate: moment(sDate).format(startRangeFormat),
                endDate: moment(eDate).format(endRangeFormat)
            };
        }
        else {
            range = {
                startDate: moment().subtract('days', 29).format(startRangeFormat),
                endDate: moment().subtract('days', 0).format(endRangeFormat)
            };
        }
        return range;
    }

    // Get Date range
    vm.GetDateRangeLast12MonthsDefault = function (rangepickerid, isTask) {
        var startRangeFormat = 'DD-MMM-YYYY';
        var endRangeFormat = 'DD-MMM-YYYY 23:59:59';
        var range = {
            startDate: moment().format(startRangeFormat),
            endDate: moment().format(endRangeFormat)
        };
        if (isTask) {
            range = {
                startDate: moment().subtract('days', 6).format(startRangeFormat),
                endDate: moment().subtract('days', 0).format(endRangeFormat)
            };
        }
        if ($(rangepickerid).val()) {
            var StartDate = $(rangepickerid).val().split(' ')[0];
            var EndDate = $(rangepickerid).val().split(' ')[2];
            var sDate = $filter("toDate")(StartDate);
            var eDate = $filter("toDate")(EndDate);
            range = {
                startDate: moment(sDate).format(startRangeFormat),
                endDate: moment(eDate).format(endRangeFormat)
            };
        }
        else {
            range = {
                startDate: moment().subtract('months', 13).startOf('month').format(startRangeFormat),
                endDate: moment().endOf('month').format(endRangeFormat)
            };
        }
        return range;
    }

    // Get yearly range
    vm.GetDateRangeYearly = function (rangepickerid, isTask) {
        var startRangeFormat = 'DD-MMM-YYYY';
        var endRangeFormat = 'DD-MMM-YYYY 23:59:59';
        var range = {
            startDate: moment().format(startRangeFormat),
            endDate: moment().format(endRangeFormat)
        };
        if (isTask) {
            range = {
                startDate: moment().subtract('days', 29).format(startRangeFormat),
                endDate: moment().subtract('days', 0).format(endRangeFormat)
            };
        }
        if ($(rangepickerid).val()) {
            var StartDate = $(rangepickerid).val().split(' ')[0];
            var EndDate = $(rangepickerid).val().split(' ')[2];
            var sDate = $filter("toDate")(StartDate);
            var eDate = $filter("toDate")(EndDate);
            range = {
                startDate: moment(sDate).format(startRangeFormat),
                endDate: moment(eDate).format(endRangeFormat)
            };
        }
        else {
            range = {
                startDate: moment().subtract('years', 0).startOf('year').format(startRangeFormat),
                endDate: moment().subtract('years', 0).endOf('year').format(endRangeFormat)
            };
        }
        return range;
    }

    // Get month name from month number.
    vm.GetMonthName = function (monthNumber) {
        var months = new Array(12);
        months[0] = "January";
        months[1] = "February";
        months[2] = "March";
        months[3] = "April";
        months[4] = "May";
        months[5] = "June";
        months[6] = "July";
        months[7] = "August";
        months[8] = "September";
        months[9] = "October";
        months[10] = "November";
        months[11] = "December";

        return months[monthNumber];
    };

    // Set fix header width.
    vm.SetFixHeader = function () {
        setTimeout(function () {
            $('.ng-table-pager').each(function (i, el) {
                var pager = $(el);
                var scrollableTable = pager.closest("scrollable-table");
                pager.insertAfter(scrollableTable);
                pager.addClass('m-t-5');
            });
        }, 1000);
    };

    // Detect IE Browser
    vm.detectIE = function () {
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

    vm.ExportToPng = function (divId, nearmissType, chartName, chartType) {
        //Start: For Line Chart PNG Download    
        if (chartType == 'nvd3-bar') {
            $('div[name=' + divId + '] nvd3 svg path').css("fill", "none").css("stroke", "#000").css("stroke-opacity", ".75").css("shape-rendering", "crispEdges");
            $("div[name=' + divId + '] nvd3 .nv-bar text").css("font-size", "15px");
        }

        if (chartType == 'nvd3-line') {
            $('div[name=' + divId + '] .nvd3 .nv-point').css("fill", "none");
            $('div[name=' + divId + '] .nvd3 .nv-point-paths path').css("fill", "none");
            $('div[name=' + divId + '] .nvd3 .nv-background').css("fill", "none");
            $('div[name=' + divId + '] .nvd3 .nv-axis line').css("stroke", "#000").css("stroke-width", "1px").css("opacity", "0.1");
        }

        if (chartType == 'svg') {
            $('svg .node--leaf').css("fill", "white");
        }
        //End: For Line Chart PNG Download
        if (chartType == 'svg') {
            $('svg .node--leaf').css("fill", "white");
        }
        //End: For Line Chart PNG Download

        if (chartType == 'svg') {
            var svg = new XMLSerializer().serializeToString(document.querySelector('div[name=' + divId + '] svg'));

            var imgWidth = $('div[name=' + divId + '] svg').width();
            var imgHeight = $('div[name=' + divId + '] svg').height();
        }
        else {
            var svg = new XMLSerializer().serializeToString(document.querySelector('div[name=' + divId + '] nvd3 svg'));

            var imgWidth = $('div[name=' + divId + '] nvd3 svg').width();
            var imgHeight = $('div[name=' + divId + '] nvd3 svg').height();
        }

        var canvas = document.createElement('canvas');
        canvas.setAttribute('width', imgWidth);
        canvas.setAttribute('height', imgHeight);


        document.getElementById('' + divId + '').appendChild(canvas);
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
            a.click();
            a.remove();
        }
        download(destinationCanvas, chartName + '-[' + nearmissType + ']-' + new Date().toLocaleDateString().substr(0, 10) + '.png');
        canvas.parentNode.removeChild(canvas);
    };

    vm.ExportToPdf = function (divId, nearmissType, chartName, fileName, chartType, htmlContent) {
        var regex = new RegExp('"', 'g');
        htmlContent = htmlContent.replace(regex, '\'');

        //Start: For Line Chart PNG Download    
        if (chartType == 'nvd3-bar') {
            $('div[name=' + divId + '] nvd3 svg path').css("fill", "none").css("stroke", "#000").css("stroke-opacity", ".75").css("shape-rendering", "crispEdges");
            $("div[name=' + divId + '] nvd3 .nv-bar text").css("font-size", "15px");
        }

        if (chartType == 'nvd3-line') {
            $('div[name=' + divId + '] .nvd3 .nv-point').css("fill", "none");
            $('div[name=' + divId + '] .nvd3 .nv-point-paths path').css("fill", "none");
            $('div[name=' + divId + '] .nvd3 .nv-background').css("fill", "none");
            $('div[name=' + divId + '] .nvd3 .nv-axis line').css("stroke", "#000").css("stroke-width", "1px").css("opacity", "0.1");
        }

        if (chartType == 'svg') {
            $('svg .node--leaf').css("fill", "white");
        }
        //End: For Line Chart PNG Download

        if (chartType == 'svg') {
            var svg = new XMLSerializer().serializeToString(document.querySelector('div[name=' + divId + '] svg'));

            var imgWidth = $('div[name=' + divId + '] svg').width();
            var imgHeight = $('div[name=' + divId + '] svg').height();
        }
        else {
            var svg = new XMLSerializer().serializeToString(document.querySelector('div[name=' + divId + '] nvd3 svg'));

            var imgWidth = $('div[name=' + divId + '] nvd3 svg').width();
            var imgHeight = $('div[name=' + divId + '] nvd3 svg').height();
        }

        var canvas = document.createElement('canvas');
        canvas.setAttribute('width', imgWidth);
        canvas.setAttribute('height', imgHeight);


        document.getElementById('' + divId + '').appendChild(canvas);
        canvg(canvas, svg);

        var destinationCanvas = document.createElement("canvas");
        destinationCanvas.width = canvas.width;
        destinationCanvas.height = canvas.height;

        var destCtx = destinationCanvas.getContext('2d');
        destCtx.fillStyle = "#FFFFFF";
        destCtx.fillRect(0, 0, canvas.width, canvas.height);

        destCtx.drawImage(canvas, 0, 0);

        var image = canvas.toDataURL("image/png");
        image = image.replace('data:image/png;base64,', '');

        canvas.parentNode.removeChild(canvas);

        $.ajax({
            type: 'POST',
            url: '/AnalyticsReport/AnalyticsReport/GenerateAnalyticsReportPdf',
            data: '{ "imageData" : "' + image + '","chartName" : "' + chartName + '","fileName" : "' + fileName + '","nearmissType" : "' + nearmissType + '","htmlContent" : "' + htmlContent + '" }',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (result) {
                var a = document.createElement('a')
                a.download = fileName + '-[' + nearmissType + ']-' + new Date().toLocaleDateString().substr(0, 10) + '.pdf'
                a.target = '_blank';
                a.href = '../../Temp/' + fileName + '.pdf';
                document.body.appendChild(a);
                a.click();
                a.remove();
            }
        });
    };

    return vm;
}

angular.module("DorfKetalMVCApp").directive('allowDigitsOnly',
    function () {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {
                element.bind("keydown", function (event) {
                    //if (!((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode > 96 && event.keyCode < 105) || (event.keyCode == 8) || (event.keyCode == 9)) || event.shiftKey) {
                    if (!((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode >= 96 && event.keyCode <= 105) || (event.keyCode == 46) || (event.keyCode == 38) || (event.keyCode == 40) || (event.keyCode == 8) || (event.keyCode == 9)) || event.shiftKey) {

                        event.preventDefault();
                        return false;
                    }
                });
            }
        };
    });


angular.module("DorfKetalMVCApp").directive('allowNumbersOnly',
    function () {
        return {
            restrict: "A",
            link: function (scope, element, attrs) {
                element.bind("keydown", function (event) {
                    if (!((event.keyCode > 47 && event.keyCode < 58) || (event.keyCode >= 96 && event.keyCode <= 105) || (event.keyCode == 46) || (event.keyCode == 38) || (event.keyCode == 40) || (event.keyCode == 8) || (event.keyCode == 9)) || event.shiftKey) {
                        event.preventDefault();
                        return false;
                    }
                });
            }
        };
    });
