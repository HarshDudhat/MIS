(function (root, factory) {
    'use strict';
    if (typeof module !== 'undefined' && module.exports) {
        // CommonJS
        module.exports = factory(require('angular'));
    } else if (typeof define === 'function' && define.amd) {
        // AMD
        define(['angular'], factory);
    } else {
        // Global Variables
        factory(root.angular);
    }
}(window, function (angular) {
    'use strict';
    angular.module('autocomplete-dropdown-alt', []).directive('autocompleteDropdown', ['$templateCache', '$filter',

    function ($templateCache, $filter) {
        //  constants
        var KEY_TAB = 9;
        var KEY_EN = 13;
        var KEY_ES = 27;
        var KEY_UP = 38;
        var KEY_DW = 40;
        var MAX_LENGTH = 8000;
        var TEXT_NORESULTS = 'No results found';
        var TEMPLATE_URL = '/autocomplete-dropdown/index.html';
        $templateCache.put(TEMPLATE_URL,
            '<div uib-dropdown is-open="status.isopen">' +
            '    <input type="text" autocomplete="off"' +
            '       ng-model="searhItem"' +
            '       class="{{inputClass}}"' +
            '       id="{{id}}_value"' +
            '       name="{{name}}"' +
            '       tabindex="{{tabindex}}"' +
            '       ng-disabled="ngDisabled"' +
            '       placeholder="{{placeholder}}"' +
            '       maxlength="{{maxLength}}"' +
            '       ng-change="ClearModel()"' +
            '       uib-dropdown-toggle />' +
            '    <span class="drop-caret"></span>' +
            '    <ul class="dropdown-menu" style="min-width: 100%" uib-dropdown-menu role="menu" aria-labelledby="{{id}}_value" > ' +
            '        <li role="menuitem" ng-repeat="o in options | filter:searhItem">' +
            '            <a href="#" ng-click="SelectItem(o)">' +
            '               <span data-ng-bind-html="o[textField] | highlight:searhItem:matchClass"></span>' +
            '           </a>' +
            '        </li>' +
            '        <li ng-if="(options | filter:searhItem).length == 0">' +
            '            <a>' +
            '               <span class="muted">' + TEXT_NORESULTS + '</span>' +
            '           </a>' +
            '        </li>' +
            '    </ul>' +
            '</div>' +
            '<a id="lnkOpenList" ng-click="OpenList($event);" style="dispaly:none;"></a>'
        );

        function link(scope, elem, attrs, ctrl) {

            var formName = elem.closest("form").attr("name");
            var validationElm = scope.$parent[formName][scope.name];
            var inputId = scope.id + "_value";

            scope.SetValidity = function (valid) {
                if (scope.ngRequired) {
                    validationElm.$setValidity('required', valid);
                }
            }

            scope.OpenList = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                scope.status.isopen = true;
            }

            scope.SetValidity(false);
            scope.maxLength = attrs.maxLength ? attrs.maxLength : MAX_LENGTH;
            scope.SelectItem = function (item) {
                if (item) {
                    scope.searhItem = item[scope.textField];
                    scope.ngModel = item[scope.valueField];
                    scope.SetValidity(true);
                } else {
                    scope.SetValidity(false);
                }
            }

            scope.$on('autocomplete-dropdown:clearInput', function (event, elementId) {
                scope.searhItem = '';
                scope.ngModel = '';
            });

            scope.ClearModel = function () {
                scope.ngModel = undefined;
                scope.SetValidity(false);
            }

            scope.$watch("ngModel", function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    if (newVal) {
                        var selectedObject = $filter('filter')(scope.options, JSON.parse("{\"" + scope.valueField + "\":" + (scope.ngModel ? scope.ngModel : "\"\"") + "}"), true)[0];
                        if (selectedObject) {
                            scope.SelectItem(selectedObject);
                        }
                    } else {
                        scope.ClearModel();
                    }
                    if (typeof scope.ngChange == 'function') {
                        scope.ngChange();
                    }
                }
            });

            scope.$watch("options", function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    if (newVal) {
                        var selectedObject = $filter('filter')(scope.options, JSON.parse("{\"" + scope.valueField + "\":" + (scope.ngModel ? scope.ngModel : "\"\"") + "}"), true)[0];
                        if (selectedObject) {
                            scope.SelectItem(selectedObject);
                        }
                    } else {
                        scope.ClearModel();
                        scope.searhItem = "";
                    }
                    if (typeof scope.ngChange == 'function') {
                        scope.ngChange();
                    }
                }
            });

            if (scope.ngModel) {
                var selectedObject = $filter('filter')(scope.options, JSON.parse("{\"" + scope.valueField + "\":" + (scope.ngModel ? scope.ngModel : "\"\"") + "}"), true)[0];
                if (selectedObject) {
                    scope.SelectItem(selectedObject);
                }
            }

            var inputField = elem.find('input');

            if (scope.ngFocus) {
                setTimeout(function () {
                    $("#" + inputId).focus();
                }, 100);
            }
            // for IE8 quirkiness about event.which
            function ie8EventNormalizer(event) {
                return event.which ? event.which : event.keyCode;
            }
            function keydownHandler(event) {
                var which = ie8EventNormalizer(event);
                var optionsList = $("#" + inputId).parent().find("li a");
                var activeOptionsList = $("#" + inputId).parent().find("li a.active");

                $("#lnkOpenList").click();
                if (which === KEY_DW) {
                    event.preventDefault();
                    if (activeOptionsList.length == 0 || optionsList.last().hasClass("active")) {
                        optionsList.last().removeClass("active");
                        optionsList.first().addClass("active");
                    } else {
                        var activeFound = false;
                        var classAdded = false;
                        optionsList.each(function (index) {
                            if (!classAdded) {
                                if (activeFound) {
                                    if (!$(this).hasClass("active")) {
                                        $(this).addClass("active");
                                        classAdded = true;
                                        activeFound = true;
                                    }
                                } else {
                                    activeFound = $(this).hasClass("active");
                                    $(this).removeClass("active");
                                }
                            }
                        });
                    }
                } else if (which === KEY_UP) {
                    event.preventDefault();
                    if (activeOptionsList.length == 0 || optionsList.first().hasClass("active")) {
                        optionsList.first().removeClass("active");
                        optionsList.last().addClass("active");
                    } else {
                        var activeFound = false;
                        var classAdded = false;
                        for (var i = optionsList.length - 1; i >= 0; i--) {
                            var elm = $(optionsList.get(i));
                            if (!classAdded) {
                                if (activeFound) {
                                    if (!elm.hasClass("active")) {
                                        elm.addClass("active");
                                        classAdded = true;
                                        activeFound = true;
                                    }
                                } else {
                                    activeFound = elm.hasClass("active");
                                    elm.removeClass("active");
                                }
                            }
                        }
                    }
                } else if (which === KEY_TAB || which === KEY_EN) {
                    if (activeOptionsList.length > 0) {
                        activeOptionsList.first().click();
                    } else if (optionsList.length > 0) {
                        optionsList.first().click();
                    }
                }

            }
            inputField.on('keydown', keydownHandler);
        }

        return {
            restrict: 'A',
            require: "ngModel",
            scope: {
                ngModel: '=',
                ngRequired: '=',
                ngFocus: '=',
                ngDisabled: '=',
                options: '=',
                ngChange: '&',
                matchClass: '@',
                textField: '@',
                valueField: '@',
                tabindex: '@',
                inputClass: '@',
                maxLength: '@',
                placeholder: '@',
                id: '@',
                name: '@'
            },
            templateUrl: function (element, attrs) {
                return attrs.templateUrl || TEMPLATE_URL;
            },
            compile: function () {
                return link;
            }
        };
    }]);

    angular.module('autocomplete-dropdown-alt').filter('highlight', ['$sce', function ($sce) {
        return function (str, termsToHighlight, matchClass) {
            if (termsToHighlight === undefined || !matchClass) {
                return $sce.trustAsHtml(str);
            }
            var myRegExp = new RegExp('(' + termsToHighlight + ')', 'gi');
            return $sce.trustAsHtml(str.replace(myRegExp, '<span class="' + matchClass + '">$1</span>'));
        };
    }]);

}));