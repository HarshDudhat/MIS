//Custom Filter for Date ETC...
angular.module("MVCApp").filter('toDateString', ["$filter", "$rootScope", function ($filter, $rootScope) {
    return function (input, type) {
        if (input == undefined || input == null || input == '') {
            return input;
        }
        switch (type) {
            //For convert date to applied format                                       
            case 'date':
                return $filter('date')(input, $rootScope.defaultSettings.dateFormat);
                //For convert date to Local date with time format    
            case 'localDate':
                return $filter('date')(input, $rootScope.defaultSettings.dateTimeFormat);
                //For convert UTC Server date to Local date to applied format        
            case 'UtcTolocalDate':
                return $filter('date')(moment.utc(input).toDate(), $rootScope.defaultSettings.dateFormat);
                //For convert UTC Server date to Local date with time format  
            case 'UtcTolocalDateTime':
                return $filter('date')(moment.utc(input).toDate(), $rootScope.defaultSettings.dateTimeFormat);
            case 'UtcToLocalMonth':
                return $filter('date')(moment.utc(input).toDate(), $rootScope.defaultSettings.MonthFormat);
            case 'UtcToLocalMMM':
                return $filter('date')(moment.utc(input).toDate(), $rootScope.defaultSettings.MMMFormat);
            case 'UtcToLocalyy':
                return $filter('date')(moment.utc(input).toDate(), $rootScope.defaultSettings.yyFormat);
            case 'UtcToLocaldd':
                return $filter('date')(moment.utc(input).toDate(), $rootScope.defaultSettings.ddFormat);
            case 'UtcToLocalTime':
                return $filter('date')(moment.utc(input).toDate(), $rootScope.defaultSettings.TimeFormat);
            case 'UtcToLocalPostedFormat':
                return $filter('date')(moment.utc(input).toDate(), $rootScope.defaultSettings.datePostedFormat);
        }
    };
} ]);

//Custom Filter for Date ETC...
angular.module("MVCApp").filter('toDate', ["$filter", "$rootScope", function ($filter, $rootScope) {
    return function (input) {
        if (input == undefined || input == null || input == '') {
            return input;
        }
        return moment(input, $rootScope.defaultSettings.dateFormat.toUpperCase()).toDate();
    };
} ]);

//Custom Filter for Date ETC...
angular.module("MVCApp").filter('toDateTime', ["$filter", "$rootScope", function ($filter, $rootScope) {
    return function (input) {
        if (input == undefined || input == null || input == '') {
            return input;
        }
        return moment(input, $rootScope.defaultSettings.dateFormat.toUpperCase() + " hh:mm").toDate();
    };
} ]);


//Custom Filter for get comma-separated value from array
angular.module("MVCApp").filter('joinBy', function () {
    return function (input, delimiter, propName) {
        if (input == undefined || input == null || input == '') {
            return '';
        }
        if (propName == undefined || propName == null || propName == '') {
            return (input || []).join(delimiter || ',');
        } else {
            var arr = input.map(function (item) {
                return item[propName];
            });
            return (arr || []).join(delimiter || ',');
        }
    };
});



//Calculate Age From Date
angular.module("MVCApp").filter('ageFilter', function () {
    function calculateAge(birthday) { // birthday is a date
        var ageDifMs = Date.now() - birthday.getTime();
        if (ageDifMs < 0) {
            return 0;
        } else {
            var ageDate = new Date(ageDifMs); // miliseconds from epoch
            return Math.abs(ageDate.getUTCFullYear() - 1970);
        }
    }

    return function (birthdate) {
        if (angular.isDate(birthdate)) {
            return calculateAge(birthdate);
        } else {
            return 0;
        }
    };
});


//Roman Number Filter
angular.module("MVCApp").filter('romanNumber', function () {
    return function (num) {
        if (angular.isNumber(num)) {
            var digits = String(+num).split(""),
        key = ["", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM",
               "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC",
               "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"],
        roman = "",
        i = 3;
            while (i--)
                roman = (key[+digits.pop() + (i * 10)] || "") + roman;
            return Array(+digits.join("") + 1).join("M") + roman;
        } else {
            return number;
        }
    };
});

// set Decimal
angular.module("MVCApp").filter('setDecimal', function ($filter) {
    return function (input, places) {
        if (isNaN(input)) return input;
        // If we want 1 decimal place, we want to mult/div by 10
        // If we want 2 decimal places, we want to mult/div by 100, etc
        // So use the following to create that factor
        var factor = "1" + Array(+(places > 0 && places + 1)).join("0");
        return Math.round(input * factor) / factor;
    };
});

//Custom Filter for Count Characters Remaining...
angular.module("MVCApp").filter('countCharactersRemaining', ["$filter", function ($filter) {
    return function (input, totalLength) {
        if (input == undefined || input == null || input == '' ) {
            return totalLength;
        }

        return totalLength - input.length - (input.length ? input.split("\n").length - 1 : 0)
    };
} ]);