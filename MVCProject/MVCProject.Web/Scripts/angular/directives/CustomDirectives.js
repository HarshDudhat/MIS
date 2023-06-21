// Autofocus directive
angular.module("DorfKetalMVCApp").directive('ngFocus', ['$timeout', function ($timeout) {
    return {
        link: function (scope, element, attrs) {
            scope.$watch(attrs.ngFocus, function (val) {
                if (angular.isDefined(val) && val) {
                    $timeout(function () { element[0].focus(); });
                }
            }, true);

            element.bind('blur', function () {
                if (angular.isDefined(attrs.ngFocusLost)) {
                    scope.$apply(attrs.ngFocusLost);

                }
            });
        }
    };
} ]);

//table-responsive
angular.module("DorfKetalMVCApp").directive('tableResponsive', ['$timeout', '$window', function ($timeout, $window) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $timeout(function () {
                var elementHeight = attrs.height || attrs.tableResponsive || 520;
                element.wrap("<div class='table-responsive' style='max-height:" + elementHeight + "px;'>");
                //                scope.$watch(
                //                function () {
                var tableOb = element;
                tableOb.floatThead('destroy');
                setTimeout(function () {
                    tableOb.floatThead({
                        scrollContainer: function ($table) {
                            return $table.closest('.table-responsive');
                        }
                    });
                }, 100);
                //                });
            }, 100);
        }
    }
} ]);

//Check/UncheckAll Checkbox list
angular.module("DorfKetalMVCApp").directive('checkboxAll', function () {
    return function (scope, iElement, iAttrs) {
        var parts = iAttrs.checkboxAll.split('.');
        iElement.attr('type', 'checkbox');
        iElement.bind('change', function (evt) {
            scope.$apply(function () {
                var setValue = iElement.prop('checked');
                angular.forEach(scope.$eval(parts[0]), function (v) {
                    v[parts[1]] = setValue;
                });
            });
        });
        scope.$watch(parts[0], function (newVal) {
            var hasTrue = false;
            var hasFalse = false;
            angular.forEach(newVal, function (v) {
                if (v[parts[1]]) {
                    hasTrue = true;
                } else {
                    hasFalse = true;
                }
            });
            if (hasTrue && hasFalse) {
                iElement.prop('checked', false);
                iElement.prop("indeterminate", true);
            } else {
                iElement.prop("indeterminate", false);
                iElement.prop('checked', hasTrue);
            }
        }, true);
    };
});

//// fieldset directive// apply only in IE browser
//if (navigator.appVersion.toString().indexOf('.NET') > 0) {
//    angular.module("DorfKetalMVCApp").directive('fieldset', function () {
//        return {
//            restrict: 'E',
//            scope: {
//                ngDisabled: '='
//            },
//            link: function (scope, element) {
//                scope.$watch('ngDisabled', function (newval) {
//                    if (newval) {
//                        angular.element('input', element).attr('disabled', 'disabled');
//                        angular.element('a', element).attr('disabled', 'disabled');
//                        angular.element('button', element).attr('disabled', 'disabled');
//                        angular.element('select', element).attr('disabled', 'disabled');
//                        angular.element('textarea', element).attr('disabled', 'disabled');
//                    }
//                    else {
//                        angular.element('input', element).removeAttr('disabled', 'disabled');
//                        angular.element('a', element).removeAttr('disabled', 'disabled');
//                        angular.element('button', element).removeAttr('disabled', 'disabled');
//                        angular.element('select', element).removeAttr('disabled', 'disabled');
//                        angular.element('textarea', element).removeAttr('disabled', 'disabled');
//                    }
//                    angular.element('input[datepicker-popup]', element).attr('disabled', 'disabled');
//                    angular.element('[always-disable]', element).attr('disabled', 'disabled');
//                });
//            }
//        }
//    });
//}

//Maxlength for input type="number"
angular.module("DorfKetalMVCApp").directive("limitTo", [function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            var limit = parseInt(attrs.limitTo);
            angular.element(elem).on("keydown", function (e) {
                if (e.key.length == 1 && this.value.length == limit) return false;
            });
        }
    }
} ]);

//Numbers Only for input
angular.module("DorfKetalMVCApp").directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                // this next if is necessary for when using ng-required on your input. 
                // In such cases, when a letter is typed first, this parser will be called
                // again, and the 2nd time, the value will be undefined
                if (inputValue == undefined) return ''
                var transformedInput = inputValue.toString().replace(/[^0-9.]/g, '');
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }
                return transformedInput;
            });
        }
    };
});

// fallback Src for images
angular.module("DorfKetalMVCApp").directive('fallbackSrc', function () {
    var fallbackSrc = {
        link: function postLink(scope, iElement, iAttrs) {
            iElement.bind('error', function () {
                angular.element(this).attr("src", iAttrs.fallbackSrc);
            });
        }
    }
    return fallbackSrc;
});


angular.module("DorfKetalMVCApp").directive('countTo', ['$timeout', function ($timeout) {
    return {
        replace: false,
        scope: true,
        link: function (scope, element, attrs) {
            var e = element[0];
            var num, refreshInterval, duration, steps, step, countTo, value, increment;

            var calculate = function () {
                refreshInterval = 30;
                step = 0;
                scope.timoutId = null;
                countTo = parseInt(attrs.countTo) || 0;
                scope.value = parseInt(attrs.value, 10) || 0;
                duration = (parseFloat(attrs.duration) * 1000) || 0;

                steps = Math.ceil(duration / refreshInterval);
                increment = ((countTo - scope.value) / steps);
                num = scope.value;
            };

            var tick = function () {
                scope.timoutId = $timeout(function () {
                    num += increment;
                    step++;
                    if (step >= steps) {
                        $timeout.cancel(scope.timoutId);
                        num = countTo;
                        e.innerHTML = countTo;
                    } else {
                        e.innerHTML = Math.round(num);
                        tick();
                    }
                }, refreshInterval);

            };

            var start = function () {
                if (scope.timoutId) {
                    $timeout.cancel(scope.timoutId);
                }
                calculate();
                tick();
            };

            attrs.$observe('countTo', function (val) {
                if (val) {
                    start();
                }
            });

            attrs.$observe('value', function (val) {
                start();
            });

            return true;
        }
    }
} ]);

angular.module("DorfKetalMVCApp").directive("angularTooltip", [function () {
    return {
        link: function ($scope, $element, $attrs) {

            var elementId = new Date().getTime();
            $($element).attr('data-tooltip-content', '#' + elementId);
            //$("<div class='tooltip_templates' style='display:none;'><div id='" + elementId + "'>" + $attrs.content + "</div></div>").insertAfter($element);
            $($element).append("<div class='tooltip_templates' style='display:none;'><div id='" + elementId + "'>" + $attrs.content + "</div>");

            $($element).tooltipster({
                side: $attrs.side,
                animation: 'fade',
                delay: 200,
                theme: 'borderless',
                trigger: 'click'
            });

            $(window).keypress(function () {
                if ($($element).data('tooltipster-ns') != undefined) {
                    $($element).tooltipster('hide');
                }
            });
            $(window).mousedown(function () {
                if ($($element).data('tooltipster-ns') != undefined) {
                    $($element).tooltipster('hide');
                }
            });
        }
    };
} ]);
