(function () {
    'use strict';

    angular
        .module('angular-nicescroll', [])
        .directive('ngNicescroll', ngNicescroll);

    ngNicescroll.$inject = ['$rootScope', '$parse'];

    function ngNicescroll($rootScope, $parse) {
        return {
            link: function (scope, element, attrs, controller) {
                var niceOption = scope.$eval(attrs.niceOption);

                var niceScroll = $(element).niceScroll(niceOption);
                var nice = $(element).getNiceScroll();

                if (attrs.niceScrollObject) $parse(attrs.niceScrollObject).assign(scope, nice);

                // on scroll
                $(element).scroll(function (data) {
                    if ($(data.target).hasClass("scroll-view")) {
                        if ($('.nav-sm ul.side-menu li').hasClass("active")) {
                            var eleOffset = $('.nav-sm ul.side-menu li.active').offset();
                            $('.nav-sm ul.side-menu li.active ul.child_menu').css("top", eleOffset.top - $(window).scrollTop() + "px");
                        }
                    }
                });

                // on scroll end
                niceScroll.onscrollend = function (data) {
                    if (this.newscrolly >= this.page.maxh) {
                        if (attrs.niceScrollEnd) scope.$evalAsync(attrs.niceScrollEnd);
                    }
                    if (data.end.y <= 0) { // at top
                        if (attrs.niceScrollTopEnd) scope.$evalAsync(attrs.niceScrollTopEnd);
                    }
                };

                scope.$on('$destroy', function () {
                    if (angular.isDefined(niceScroll.version)) {
                        niceScroll.remove();
                    }
                });
            }
        };
    }


})();
