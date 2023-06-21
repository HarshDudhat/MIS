
/**
 * Anularjs Module for pop up timepicker
 */
angular.module('timepickerPop', ['ui.bootstrap'])
.directive("timeFormat",["$filter",function ($filter) {
    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            showMeridian: '=',
        },
        link: function (scope, element, attrs, ngModel) {
            var parseTime = function (viewValue) {

                if (!viewValue) {
                    ngModel.$setValidity('time', true);
                    return null;
                } else if (angular.isDate(viewValue) && !isNaN(viewValue)) {
                    ngModel.$setValidity('time', true);
                    return viewValue;
                } else if (angular.isString(viewValue)) {
                    var timeRegex = /^(0?[0-9]|1[0-2]):[0-5][0-9] ?[a|p]m$/i;
                    if (!scope.showMeridian) {
                        timeRegex = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/;
                    }
                    if (!timeRegex.test(viewValue)) {
                        ngModel.$setValidity('time', false);
                        return undefined;
                    } else {
                        ngModel.$setValidity('time', true);
                        var date = new Date();
                        var sp = viewValue.split(":");
                        var apm = sp[1].match(/[a|p]m/i);
                        if (apm) {
                            sp[1] = sp[1].replace(/[a|p]m/i, '');
                            if (apm[0].toLowerCase() == 'pm') {
                                sp[0] = sp[0] + 12;
                            }
                        }
                        date.setHours(sp[0], sp[1]);
                        return date;
                    };
                } else {
                    ngModel.$setValidity('time', false);
                    return undefined;
                };
            };

            ngModel.$parsers.push(parseTime);

            var showTime = function (data) {
                parseTime(data);
                var timeFormat = (!scope.showMeridian) ? "HH:mm" : "hh:mm a";
                return $filter('date')(data, timeFormat);
            };
            ngModel.$formatters.push(showTime);
            scope.$watch('showMeridian', function (value) {
                var myTime = ngModel.$modelValue;
                if (myTime) {
                    element.val(showTime(myTime));
                }

            });

        }
    };
}])
.directive('timepickerPop',["$document", function ($document) {
    return {
        restrict: 'E',
        transclude: false,
        scope: {
            inputTime: "=",
            showMeridian: "=",
            tabIndex:"=",
            inputName: "@",
            ngRequired : "=",
            ngDisabled:"=",
            ngReadonly:"=",
            ngOpen:'='
        },
        controller:['$scope', '$element', function ($scope, $element) {
            $scope.isOpen = false;

            if (typeof $scope.ngOpen == "function") {
                $scope.$watch('isOpen', function (newValue, oldValue) {
                    if (newValue === oldValue || newValue == false) {
                        return;
                    }
                    $scope.ngOpen();
                }, true);
            }

            $scope.toggle = function () {
                $scope.isOpen = !$scope.isOpen;
            };

            $scope.open = function () {
                $scope.isOpen = true;
            };
        }],
        link: function (scope, element, attrs) {
            scope.$watch("inputTime", function (value) {
                if (!scope.inputTime) {
                    element.addClass('has-error');
                } else {
                    element.removeClass('has-error');
                }

            });

            element.bind('click', function (event) {
                event.preventDefault();
                event.stopPropagation();
            });

            $document.bind('click', function (event) {
                scope.$apply(function () {
                    scope.isOpen = false;
                });
            });

        },
        template: function(tElement, tAttrs) {
            return "<input type='text' class='form-control hidden' name='{{inputName}}' ng-required='ngRequired' ng-readonly='ngReadonly' ng-disabled='ngDisabled' ng-model='inputTime'  time-format show-meridian='showMeridian' />"
                + "<uib-timepicker  ng-hide='ngDisabled' "+(tAttrs.tabIndex?("tabindex='" + tAttrs.tabIndex + "' "):"")+ "ng-model='inputTime' show-meridian='showMeridian' show-spinners='false' hour-step='1' minute-step='1' ></uib-timepicker>"

                 + "<table class='uib-timepicker' ng-if='ngDisabled'>"
                 + "   <tbody>"
                 + "        <tr>"
                 + "            <td class='form-group uib-time hours'>"
                 + "                <input placeholder='HH' ng-model='inputTime' class='form-control text-center' type='text' disabled='disabled' uib-datepicker-popup='HH'>"
                 + "            </td>"
                 + "            <td class='uib-separator'>:</td>"
                 + "            <td class='form-group uib-time minutes'>"
                 + "                <input placeholder='MM' ng-model='inputTime' class='form-control text-center' type='text' disabled='disabled' uib-datepicker-popup='mm'>"
                 + "            </td>"
                 + "        </tr>"
                 + "    </tbody>"
                 + "</table>";
        }
    };
}]);