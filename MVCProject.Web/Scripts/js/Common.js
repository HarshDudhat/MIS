//String.format
String.prototype.format = function () {
    var formatted = this;
    for (var i = 0; i < arguments.length; i++) {
        var regexp = new RegExp('\\{' + i + '\\}', 'gi');
        formatted = formatted.replace(regexp, arguments[i]);
    }
    return formatted;
};

// Read query string parameter
function getQueryStringParameter(name, url) {
    if (!url) {
        url = window.location.href;
    }
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

// Generate UID
function generateUID() {
    // I generate the UID from two parts here 
    // to ensure the random number provide enough bits.
    var firstPart = (Math.random() * 46656) | 0;
    var secondPart = (Math.random() * 46656) | 0;
    firstPart = ("000" + firstPart.toString(36)).slice(-3);
    secondPart = ("000" + secondPart.toString(36)).slice(-3);
    return firstPart + secondPart;
}

/* Scroll to top after refresh page */
$(window).on('beforeunload', function () {
    $('.scrollArea').scrollTop(0);
    $('.scrollArea').scrollLeft(0);
    $(window).scrollTop(0);
});
/* --------------------------------- */
// Show Alert message
function showtoastr(message, type, title) {
    if (message) {
        var calltoastr = toastr.info;
        var defautlTitle = "";
        switch (type) {
            case messageTypes.Error:
                calltoastr = toastr.error;
                defautlTitle = errorTitle;
                break;
            case messageTypes.Success:
                calltoastr = toastr.success;
                defautlTitle = successTitle;
                break;
            case messageTypes.Warning:
                calltoastr = toastr.warning;
                defautlTitle = warningTitle;
                break;
            case messageTypes.Information:
                calltoastr = toastr.info;
                break;
            case messageTypes.NotFound:
                calltoastr = toastr.warning;
                defautlTitle = warningTitle;
                break;
            default:
                calltoastr = toastr.info;
        }

        if (title || defautlTitle) {
            calltoastr(message, title || defautlTitle);
        } else {
            calltoastr(message);
        }
    }
}

$(document).ready(function () {

    // Show Messages from sessionStorage
    if (sessionStorage.getItem("SuccessMessage") != null) {
        toastr.success(sessionStorage.getItem("SuccessMessage"), 'Success');
        sessionStorage.removeItem("SuccessMessage");
    }

    //tab previous click
    $(".st_prev").click(function () {
        var leftPos = $('.st_tabs_wrap').scrollLeft();
        $(".st_tabs_wrap").animate({ scrollLeft: leftPos - 120 }, 300);
    });

    //tab next click
    $(".st_next").click(function () {
        var leftPos = $('.st_tabs_wrap').scrollLeft();
        $(".st_tabs_wrap").animate({ scrollLeft: leftPos + 120 }, 300);
    });

    // set title on hover of validation message
    $(".form-group .label.label-danger span").on("mouseover", function () {
        $(this).attr("title", $(this).html());
    });
});

// For Menu
!function ($) {
    "use strict";

    /**
    Sidebar Module
    */
    var SideBar = function () {
        this.$body = $("body"),
        this.$sideBar = $('aside.left-panel'),
        this.$navbarToggle = $(".navbar-toggle"),
        this.$navbarItem = $("aside.left-panel nav.navigation > ul > li:has(ul) > a")
    };

    //initilizing 
    SideBar.prototype.init = function () {
        //on toggle side menu bar
        var $this = this;
        $(document).on('click', '.navbar-toggle', function () {
            $this.$sideBar.toggleClass('collapsed');
        });

        //on menu item clicking
        this.$navbarItem.click(function () {
            if ($this.$sideBar.hasClass('collapsed') == false || $(window).width() < 769) {
                $("aside.left-panel nav.navigation > ul > li > ul").slideUp(300);
                $("aside.left-panel nav.navigation > ul > li").removeClass('active');
                if (!$(this).next().is(":visible")) {
                    $(this).next().slideToggle(300, function () {
                        $("aside.left-panel:not(.collapsed)").getNiceScroll().resize();
                    });
                    $(this).closest('li').addClass('active');
                }
                return false;
            }
        });

        //adding nicescroll to sidebar
        if ($.isFunction($.fn.niceScroll)) {
            $("aside.left-panel:not(.collapsed)").niceScroll({
                cursorcolor: '#8e909a',
                cursorborder: '0px solid #fff',
                cursoropacitymax: '0.5',
                cursorborderradius: '0px'
            });
        }
    },

    //exposing the sidebar module
    $.SideBar = new SideBar, $.SideBar.Constructor = SideBar

} (window.jQuery),


//portlets
function ($) {
    "use strict";

    /**
    Portlet Widget
    */
    var Portlet = function () {
        this.$body = $("body"),
        this.$portletIdentifier = ".portlet",
        this.$portletCloser = '.portlet a[data-toggle="remove"]',
        this.$portletRefresher = '.portlet a[data-toggle="reload"]'
    };

    //on init
    Portlet.prototype.init = function () {
        // Panel closest
        var $this = this;
        $(document).on("click", this.$portletCloser, function (ev) {
            ev.preventDefault();
            var $portlet = $(this).closest($this.$portletIdentifier);
            var $portlet_parent = $portlet.parent();
            $portlet.remove();
            if ($portlet_parent.children().length == 0) {
                $portlet_parent.remove();
            }
        });

        // Panel Reload
        $(document).on("click", this.$portletRefresher, function (ev) {
            ev.preventDefault();
            var $portlet = $(this).closest($this.$portletIdentifier);
            // This is just a simulation, nothing is going to be reloaded
            $portlet.append('<div class="panel-disabled"><div class="loader-1"></div></div>');
            var $pd = $portlet.find('.panel-disabled');
            setTimeout(function () {
                $pd.fadeOut('fast', function () {
                    $pd.remove();
                });
            }, 500 + 300 * (Math.random() * 5));
        });
    },
    //
    $.Portlet = new Portlet, $.Portlet.Constructor = Portlet

} (window.jQuery),


//main app module
function ($) {
    "use strict";

    var DorfKetalApp = function () {
        this.pageScrollElement = "html, body",
        this.$body = $("body")
    };


    //initializing nicescroll
    DorfKetalApp.prototype.initNiceScrollPlugin = function () {
        //You can change the color of scroll bar here
        $.fn.niceScroll && $(".nicescroll").niceScroll({ cursorcolor: '#9d9ea5', cursorborderradius: '0px' });
    },


    //initilizing 
    DorfKetalApp.prototype.init = function () {
        this.initNiceScrollPlugin(),
        //creating side bar
        $.SideBar.init()
    },

    $.DorfKetalApp = new DorfKetalApp, $.DorfKetalApp.Constructor = DorfKetalApp

} (window.jQuery),

//initializing main application module
function ($) {
    "use strict";
    $.DorfKetalApp.init()
} (window.jQuery);

$(function () {

    $(".form-control").focus(function () {
        $(this).closest(".textbox-wrap").addClass("focused");
    }).blur(function () {
        $(this).closest(".textbox-wrap").removeClass("focused");
    });


    $(".colorBg a[href]").click(function () {
        var scrollElm = $(this).attr("href");

        $("html,body").animate({ scrollTop: $(scrollElm).offset().top }, 500);

        return false;
    });

});

var $ = jQuery.noConflict();
jQuery(document).ready(function ($) {
    backToTop.init();
});

var backToTop =
{
    /**
    * When the user has scrolled more than 100 pixels then we display the scroll to top button using the fadeIn function
    * If the scroll position is less than 100 then hide the scroll up button
    *
    * On the click event of the scroll to top button scroll the window to the top
    */
    init: function () {

        //Check to see if the window is top if not then display button
        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $('.backToTop').fadeIn();
            } else {
                $('.backToTop').fadeOut();
            }
        });

        // Click event to scroll to top
        $('.backToTop').click(function () {
            $('html, body').animate({ scrollTop: 0 }, 800);
            return false;
        });
    }
};

// Menu
var CURRENT_URL = window.location.href.split("?")[0],
    $BODY = $("body"),
    $MENU_TOGGLE = $("#menu_toggle"),
    $SIDEBAR_MENU = $("#sidebar-menu"),
    $SIDEBAR_FOOTER = $(".sidebar-footer"),
    $LEFT_COL = $(".left_col"),
    $RIGHT_COL = $(".right_col"),
    $NAV_MENU = $(".nav_menu"),
    $FOOTER = $("footer");
$(document).ready(function () {
    var e = function () {        
        var e = $BODY.outerHeight(),
            t = $BODY.hasClass("footer_fixed") ? 0 : $FOOTER.height(),
            n = $LEFT_COL.eq(1).height() + $SIDEBAR_FOOTER.height(),
            i = n > e ? n : e;
    };
    $SIDEBAR_MENU.find("ul.side-menu > li a").on("click", function (t) {
        var n = $(this).parent();
        if (!$(n).parent().hasClass("child_menu")) {
            n.is(".active")
                    ? (n.removeClass("active"),
                        $("ul:first", n).slideUp(function () { e() }),
                        $(this).find('span.fa').removeClass("fa-chevron-down").addClass("fa-chevron-right"))
                    : (n.parent().is(".child_menu") || ($SIDEBAR_MENU.find("li").removeClass("active"), $SIDEBAR_MENU.find("li ul").slideUp(function () { AdjustSideMenuHeight(false) })),
                        n.parent().find('li a span.fa').removeClass("fa-chevron-down").addClass("fa-chevron-right"),
                        n.addClass("active"),
                        $(this).find('span.fa').removeClass("fa-chevron-right").addClass("fa-chevron-down"),
                        $("ul:first", n).slideDown(function () { e(), AdjustSideMenuHeight(true) }))
        }
    });

    $SIDEBAR_MENU.find('a[href="' + CURRENT_URL + '"]').parent("li").addClass("current-page");
    $SIDEBAR_MENU.find("a").filter(function () {
        return this.href == CURRENT_URL
    }).parent("li").addClass("current-page").parents("ul").slideDown(function () {
        e(),
        $(this).parent("li.active").find("a").find("span.fa").removeClass("fa-chevron-right").addClass("fa-chevron-down");
    }).parent().addClass("active")
    , $(window).smartresize(function () {
        e()
    })
    , e()
});

var checkState = "";
$(document).ready(function () {
    $(".expand").on("click", function () {
        $(this).next().slideToggle(200), $expand = $(this).find(">:first-child"), "+" == $expand.text() ? $expand.text("-") : $expand.text("+")
    })
}), "undefined" != typeof NProgress && ($(document).ready(function () {
    NProgress.start()
}), $(window).load(function () {
    NProgress.done()
})),
    function (e, t) {
        var n = function (e, t, n) {
            var i;
            return function () {
                function c() {
                    n || e.apply(a, o), i = null
                }
                var a = this,
                    o = arguments;
                i ? clearTimeout(i) : n && e.apply(a, o), i = setTimeout(c, t || 100)
            }
        };
        jQuery.fn[t] = function (e) {
            return e ? this.bind("resize", n(e)) : this.trigger(t)
        }
    } (jQuery, "smartresize");

//menu_toggle

$(window).bind('storage', function (e) {
    if (e.originalEvent.key == "logout" && window.location.pathname.toLowerCase().indexOf("/account/login") == -1) {
        //localStorage.removeItem("logout");
        //window.location = "/Account/Login";
    }
}); 