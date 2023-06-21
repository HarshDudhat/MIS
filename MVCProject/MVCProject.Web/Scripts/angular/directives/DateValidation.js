
// compareToDate directive
angular.module("DorfKetalMVCApp").directive('compareToDate', function () {
    return {
        require: "ngModel",
        scope: {
            intialDateValue: "=compareToDate",
            ngModel: '='
        },
        link: function (scope, element, attributes, ngModel) {

            ngModel.$validators.compareToDate = function (modelValue) {
                var CurrentDate = new Date(modelValue); //Year, Month, Date
                var intialDate = new Date(scope.intialDateValue); //Year, Month, Date
                if (new Date(intialDate.toDateString()) > new Date(CurrentDate.toDateString())) {
                    ngModel.$setViewValue(intialDate);
                }
                return true;
            };
            scope.$watch("intialDateValue", function () {
                ngModel.$validate();
            });
        }
    };
});


// dateLowerThan directive
angular.module("DorfKetalMVCApp").directive('dateLowerThan', ["$filter", '$rootScope', function ($filter, $rootScope) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            var validateDateRange = function (inputValue) {
                if (inputValue) {
                    var fromDate = $filter('toDateString')(inputValue, 'date');
                    var toDate = $filter('toDateString')(attrs.dateLowerThan, 'date');
                    var isValid = isValidDateRange(fromDate, toDate, $rootScope.defaultSettings.dateFormat);
                    ctrl.$setValidity('dateLowerThan', isValid);
                }
                return inputValue;
            };

            ctrl.$parsers.unshift(validateDateRange);
            ctrl.$formatters.push(validateDateRange);
            attrs.$observe('dateLowerThan', function () {
                validateDateRange(ctrl.$viewValue);
            });
        }
    };
} ]);



// dateGreaterThan directive
angular.module("DorfKetalMVCApp").directive('dateGreaterThan', ["$filter", '$rootScope', function ($filter, $rootScope) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            var validateDateRange = function (inputValue) {
                var fromDate = $filter('toDateString')(attrs.dateGreaterThan, 'date');
                var toDate = $filter('toDateString')(inputValue, 'date');
                var isValid = isValidDateRange(fromDate, toDate, $rootScope.defaultSettings.dateFormat);
                ctrl.$setValidity('dateGreaterThan', isValid);
                return inputValue;
            };

            ctrl.$parsers.unshift(validateDateRange);
            ctrl.$formatters.push(validateDateRange);
            attrs.$observe('dateGreaterThan', function () {
                validateDateRange(ctrl.$viewValue);

            });
        }
    };
} ]);




var isValidDate = function (dateStr, format) {
    if (dateStr == undefined)
        return false;
    //var dateTime = Date.parse(dateStr);
    var dateTime = moment(dateStr, format.toUpperCase()).toDate();
    if (isNaN(dateTime)) {
        return false;
    }
    return true;
};

var getDateDifference = function (fromDate, toDate, format) {
    var FromDate1 = moment.utc(fromDate, format.toUpperCase()).local().toDate();
    var ToDate1 = moment.utc(toDate, format.toUpperCase()).local().toDate();
    return ToDate1 - FromDate1;
};

var isValidDateRange = function (fromDate, toDate, format) {
    if (fromDate == "" || toDate == "")
        return true;
    if (isValidDate(fromDate, format) == false) {
        return false;
    }
    if (isValidDate(toDate, format) == true) {
        var days = getDateDifference(fromDate, toDate, format);
        if (days < 0) {
            return false;
        }
    }
    return true;
};