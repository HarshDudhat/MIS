//Declare Main and sub modules
var DorfKetalApp = angular.module("MVCApp", angularmodule);

//BEGIN App Run
angular.module("MVCApp").run(["$rootScope", DorfKetalMVCAppRun]);
function DorfKetalMVCAppRun($rootScope) {

    //Init Common Objects
    $rootScope.apiURL = baseApiURL + "/api";
    $rootScope.apiAttachmentsURL = baseApiURL + "/Attachments";

    $rootScope.sessionToken = typeof sessionToken == "undefined" ? '' : sessionToken;
    $rootScope.permission = typeof permission == "undefined" ? {
        PageId: 0,
        CanRead: false,
        CanWrite: false
    } : permission;

    $rootScope.pageSize = 10;
    $rootScope.isAjaxLoadingMaster = false;
    $rootScope.isAjaxLoadingChild = false;

    $rootScope.cookieName = "SZ" + window.location.port;
    $rootScope.apiDateFormat = "DD-MMM-YYYY HH:mm";

    $rootScope.maxDate = new Date(2020, 5, 22);
    $rootScope.fileDateName = moment().format('DD-MMM-YYYY');

    $rootScope.GetFileNameByDate = function () {
        return moment().format('DDMMMYYYY_HHmmss'); ;
    };

    $rootScope.switchOptions = { color: '#10c469', secondaryColor: '#ff4242', size: 'small' };

    $rootScope.dateOptions = {
        formatYear: 'yyyy',
        startingDay: 1
    };
    $rootScope.dateRangelocale = {
        applyLabel: dateRangeApplyLabel,
        cancelLabel: dateRangeCancelLabel,
        fromLabel: dateRangeFromLabel,
        toLabel: dateRangeToLabel,
        customRangeLabel: dateRangeCustomRangeLabel
    }

    //DateRanges
    $rootScope.ComponentdateRanges = {}; //It is use for component page only

    $rootScope.dateRanges = {};
    $rootScope.dateRanges[dateRangeToday] = [moment(), moment()];
    $rootScope.dateRanges[dateRangeYesterday] = [moment().subtract('days', 1), moment().subtract('days', 1)];
    $rootScope.dateRanges[dateRangeLast7Days] = [moment().subtract('days', 6), moment()];
    $rootScope.dateRanges[dateRangeLast30Days] = [moment().subtract('days', 29), moment()];
    $rootScope.dateRanges[dateRangeThisMonth] = [moment().startOf('month'), moment().endOf('month')];
    $rootScope.dateRanges[dateRangeThisYear] = [moment().startOf('year'), moment().endOf('year')];

    $rootScope.taskDateRanges = {};
    $rootScope.taskDateRanges[dateRangeToday] = [moment(), moment()];
    $rootScope.taskDateRanges[dateRangeYesterday] = [moment().subtract('days', 1), moment().subtract('days', 1)];
    $rootScope.taskDateRanges[dateRangeLast7Days] = [moment().subtract('days', 6), moment()];
    $rootScope.taskDateRanges[dateRangeLast30Days] = [moment().subtract('days', 29), moment()];
    $rootScope.taskDateRanges[dateRangeThisMonth] = [moment().startOf('month'), moment().endOf('month')];
    $rootScope.taskDateRanges[dateRangeThisYear] = [moment().startOf('year'), moment().endOf('year')];

    $rootScope.nearmissDateRanges = {};
    $rootScope.nearmissDateRanges[dateRangeLast30Days] = [moment().subtract('days', 29), moment()];
    $rootScope.nearmissDateRanges[dateRangeLast12Months] = [moment().subtract('months', 13).startOf('month'), moment().endOf('month')];
    $rootScope.nearmissDateRanges[dateRangeThisMonth] = [moment().startOf('month'), moment().endOf('month')];
    $rootScope.nearmissDateRanges[dateRangeThisYear] = [moment().startOf('year'), moment().endOf('year')];

    $rootScope.nearmissDateRanges1 = {};
    $rootScope.nearmissDateRanges1[dateRangeLastYear] = [moment().subtract('years', 1).startOf('year'), moment().subtract('years', 1).endOf('year')];
    $rootScope.nearmissDateRanges1[dateRangeThisYear] = [moment().startOf('year'), moment().endOf('year')];

    // date Picker Range
    $rootScope.datePickerRange = [
            { Name: dateRangeToday, startDate: new Date(), endDate: new Date() },
            { Name: dateRangeYesterday, startDate: moment().subtract('days', 1).toDate(), endDate: moment().subtract('days', 1).toDate() },
            { Name: dateRangeLast7Days, startDate: moment().subtract('days', 6).toDate(), endDate: new Date() },
            { Name: dateRangeLast30Days, startDate: moment().subtract('days', 29).toDate(), endDate: new Date() },
            { Name: dateRangeThisMonth, startDate: moment().startOf('month').toDate(), endDate: moment().endOf('month').toDate() },
            { Name: dateRangeThisYear, startDate: moment().startOf('year').toDate(), endDate: moment().endOf('year').toDate() }
        ];

    //Multi Select Options
    $rootScope.defaultTextSettings = {
        checkAll: checkAll,
        uncheckAll: uncheckAll,
        selectionCount: checked,
        buttonDefaultText: select,
        dynamicButtonTextSuffix: checked
    };
    $rootScope.multiSelectSettings = {
        idProp: 'Id',
        displayProp: 'Name',
        externalIdProp: 'Id',
        scrollable: true
    };

    //for Component dropdown filter 
    $rootScope.ComponentmultiSelectSettings = {
        idProp: 'ComponentSubFilterId',
        displayProp: 'SubFilterName',
        externalIdProp: 'ComponentSubFilterId',
        scrollable: true
    };

    // Multi select setting for Enum Value
    $rootScope.MultiSelectEnumValueSettings = {
        idProp: 'SubFilterEnumValue',
        displayProp: 'SubFilterName',
        externalIdProp: 'SubFilterEnumValue',
        scrollable: true
    };

    //for Module wise capa 
    $rootScope.moduleMultiSelectSettings = {
        idProp: 'TaskType',
        displayProp: 'Name',
        externalIdProp: 'TaskType',
        scrollable: true
    };

    $rootScope.defaultSettings = {
        loginTryTime: 3,
        dateFormat: "dd-MMM-yyyy",
        dateTimeFormat: "dd-MMM-yyyy HH:mm",
        MonthFormat: "MMMM-yyyy",
        MMMFormat: "MMM",
        yyFormat: "yy",
        ddFormat: "dd",
        TimeFormat: " HH:mm",
        datePostedFormat: "MMMM dd, yyyy"
    };

    //user Context of logged user
    $rootScope.userContext = {
        UserName: '',
        CompanyDB: '',
        CompanyId: -1,
        UserId: -1,
        RoleId: -1,
        RoleGroupId: [],
        RoleLevel: -1,
        SiteId: -1,
        DepartmentId: -1,
        EmployeeId: -1,
        EmployeeName: '',
        ProfilePicturePath: '',
        ApplicationLogo: '',
        DepartmentLabel: '',
        Designation: '',
        Department: '',
        SiteName: '',
        IsCorporateSite: false,
        AllowSubArea: '',
        AllowNearmiss: '',
        AllowRiskBehaviour: '',
        AllowHazard: '',
        AllowInvestigation: '',
        AllowCAPA: '',
        AllowCAPAProgress: '',
        AllowReport: '',
        AllowSearch:'',
        AllowAnalytics: ''
    };

    $("#LayoutBody").show();

}
//End  App Run

//BEGIN App config
angular.module("MVCApp").config(["$httpProvider", SAFEZydusAppConfig]);
function SAFEZydusAppConfig($httpProvider) {
    $httpProvider.interceptors.push(["$rootScope", "CommonFunctions", Interceptors]);
}

function Interceptors($rootScope, CommonFunctions) {
    return {
        request: function (config) {
            if (config.url.indexOf($rootScope.apiURL) >= 0 && config.url.indexOf('hideLoader') < 0) {
                $rootScope.isAjaxLoadingMaster = true;
                $rootScope.isAjaxLoadingChild = true;
            }
            config.headers['__RequestAuthToken'] = $rootScope.sessionToken || undefined;
            if (config.url.contains($rootScope.apiURL) && $rootScope.sessionToken) {
                $.ajax({
                    type: "GET",
                    url: "/Account/HasSession",
                    async: false,
                    success: function (data) {
                        if (data.toLowerCase() == "false") {
                            config.headers['__RequestAuthToken'] = undefined;
                        }
                    }
                });
            }
            if (config.method == 'GET') {
                if (config.url.indexOf('template/') == 0 || config.url.indexOf('ng-table/') == 0 || config.url.indexOf('angucomplete-alt') >= 0) {
                    return config;
                }
            }
            return config;
        },
        response: function (response) {
            if (response.config.url.indexOf($rootScope.apiURL) >= 0) {
                $rootScope.isAjaxLoadingMaster = false;
                $rootScope.isAjaxLoadingChild = false;
            }
            if (angular.isDefined(response) && angular.isDefined(response.data) && angular.isDefined(response.data.IsAuthenticated) && !response.data.IsAuthenticated) {
                //CommonFunctions.RedirectToLoginPage(true);
            }
            return response;
        },
        responseError: function (response) {
            if (response.config.url.indexOf($rootScope.apiURL) >= 0) {
                $rootScope.isAjaxLoadingMaster = false;
                $rootScope.isAjaxLoadingChild = false;
            }
            if (response.status === 500) {
                if (response.config.url.toLowerCase().indexOf($rootScope.apiURL + "/Account/LogOut".toLowerCase()) >= 0) {
                    CommonFunctions.RedirectToLoginPage(false);
                } else {
                    CommonFunctions.RedirectToErrorPage(response.status);
                }
            } else if (response.status == 401 || (response.status == 400 && !response.data.IsAuthenticated)) {
               // CommonFunctions.RedirectToLoginPage(true);
            }
        }
    };
}
//END App config