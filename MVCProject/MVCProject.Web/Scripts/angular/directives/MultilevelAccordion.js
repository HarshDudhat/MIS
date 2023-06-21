// multilevel Accordion
angular.module("DorfKetalMVCApp").directive('multilevelAccordion', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        terminal: true,
        scope: {
            nodes: '=ngModel',
            kids: '@',
            headingBulletField: '@',
            headingField: '@',
            selectedObject: '='
        },
        link: function ($scope, $element, $attrs) {
            if (angular.isArray($scope.nodes)) {
                $element.append(
                     '<uib-accordion close-others="false">'
                    + '<accordion-node ng-repeat="item in nodes" ng-model="item" childs="{{kids}}" selected-object="selectedObject" heading-field="{{headingField}}" heading-bullet-field="{{headingBulletField}}">'
                    +   '</accordion-node>'
                    +'</uib-accordion>'
                );
            }
            $compile($element.contents())($scope.$new());
        }
    };
} ]).directive('accordionNode', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        terminal: true,
        scope: {
            node: '=ngModel',
            childs: '@',
            headingBulletField: '@',
            headingField: '@',
            selectedObject: '='
        },
        link: function ($scope, $element, $attrs) {
            if (angular.isArray($scope.node[$scope.childs]) && $scope.node[$scope.childs].length > 0) {
                $element.append(
                     '<uib-accordion-group>'
                    +   '<uib-accordion-heading is-open="isNodeOpen">'
                    +       '<div ng-click="isNodeOpen=!isNodeOpen;selectedObject(node);" class="overflow-ellipsis" title="{{node[headingBulletField]}}  {{node[headingField]}}">'
                    +           '<i class="m-r-5 fa" ng-class="{\'fa-minus-circle\': isNodeOpen, \'fa-plus-circle\': !isNodeOpen}"></i>'
                    +           '<span class="font-600 m-r-5">{{node[headingBulletField]}}</span>'
                    +           '<span>{{node[headingField]}}</span>'
                    +       '</div>'
                    +   '</uib-accordion-heading>'
                    +   '<multilevel-accordion ng-model="node[childs]" kids="{{childs}}" selected-object="selectedObject" heading-field="{{headingField}}" heading-bullet-field="{{headingBulletField}}">'
                    +   '</multilevel-accordion>'
                    +'</uib-accordion-group>'
                );
            } else {
                $element.append(
                     '<div class="panel panel-default">'
                    +   '<div class="panel panel-heading">'
                    +       '<div class="panel-title PointerHand overflow-ellipsis m-l-10" title="{{node[headingBulletField]}}  {{node[headingField]}}" ng-click="selectedObject(node)">'
                    +       '<span class="font-600 m-r-5">{{node[headingBulletField]}}</span>'
                    +       '<span>{{node[headingField]}}</span>'
                    +       '</div>'
                    +   '</div>'
                    + '</div>'
                );
            }
            $compile($element.contents())($scope.$new());
        }
    };
} ])