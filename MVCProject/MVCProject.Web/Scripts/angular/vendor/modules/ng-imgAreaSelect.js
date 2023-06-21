//'use strict';

//angular.module("DorfKetalMVCApp").directive('ngImgAreaSelect', ['$timeout', function ($timeout) {
//    return {
//        restrict: 'EA',
//        scope: {
//            selection: '=',
//            ngImgAreaSelect: '='
//        },
//        link: function (scope, element, attrs) {
//            debugger;
//            if (element[0].tagName == 'IMG') {
//                //do your stuff
//                if (scope.selection === undefined) {
//                    scope.selection = {};
//                }
//                if (scope.ngImgAreaSelect === undefined) {
//                    scope.ngImgAreaSelect = {};
//                }
//                scope.ngImgAreaSelect.instance = true;
//                scope.ngImgAreaSelect.onSelectChange = function (img, selection) {
//                    debugger;
//                    scope.selection.x1 = selection.x1;
//                    scope.selection.x2 = selection.x2;
//                    scope.selection.y1 = selection.y1;
//                    scope.selection.y2 = selection.y2;
//                    scope.selection.width = selection.width;
//                    scope.selection.height = selection.height;
//                    scope.$apply();
//                };


//                var imgArea = angular.element(element).imgAreaSelect(scope.ngImgAreaSelect);

//                scope.$watch('selection', function () {
//                    imgArea.setSelection(scope.selection.x1, scope.selection.y1, scope.selection.x2, scope.selection.y2);
//                    imgArea.update();
//                }, true);
//            } else {
//                console.log("ng-imgAreaSelection attribute can only be used on img elements.");
//            }
//        }
//    };
//} ]);