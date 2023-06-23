// -----------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject
{
    using System.Web.Optimization;
    using MVCProject.Models;

    /// <summary>
    /// Bundle Configuration
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Register Bundles
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterOuterBundles(bundles);
            RegisterLandingBundles(bundles);
            RegisterInnerBundles(bundles);
            RegisterOtherBundles(bundles);
        }

        /// <summary>
        /// Register Outer Bundles for public pages
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        public static void RegisterOuterBundles(BundleCollection bundles)
        {
            // Common CSS Bundle (outer)
            bundles.Add(new StyleBundle("~/css/outer")
                .Include(BundlePath.css.bootstrap, new CssRewriteUrlTransform())
                .Include(BundlePath.css.style)
                .Include(BundlePath.css.developer)
                .Include(BundlePath.css.toastr)
                .Include(BundlePath.css.icons, new CssRewriteUrlTransform()));

            // Common JS Bundle (outer)
            bundles.Add(new ScriptBundle("~/bundles/outer")
               .Include(BundlePath.jquery)
               .Include(BundlePath.jqPlugin.tether)
               .Include(BundlePath.js.bootstrapbundle)
               .Include(BundlePath.js.pace)
               .Include(BundlePath.js.common)
               .Include(BundlePath.jqPlugin.cryptography)
               //.Include(BundlePath.jqPlugin.bootstrap)
               .Include(BundlePath.jqPlugin.toastr)
               .Include(BundlePath.jqPlugin.moment)
               .Include(BundlePath.angular.js)
               .Include(BundlePath.angular.module.angularCookies)
               .Include(BundlePath.angular.module.uiBootstrap)
               .Include(BundlePath.angular.module.angularAnimate)
               .Include(BundlePath.angular.module.angularFilter)               
               .Include(BundlePath.angular.app)
               .Include(BundlePath.angular.commonFunctions)
               .Include(BundlePath.angular.directive.customDirectives)
               .Include(BundlePath.angular.customFilters)
               .Include("~/Scripts/angular/services/CommonService.js")
               .Include("~/Scripts/angular/services/AccountService.js"));
        }

        /// <summary>
        /// Register Landing Bundles for landing pages
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        public static void RegisterLandingBundles(BundleCollection bundles)
        {
            // LandingPage CSS Bundle (landing)
            bundles.Add(new StyleBundle("~/css/landing")
                .Include(BundlePath.css.bootstrap, new CssRewriteUrlTransform())
                .Include("~/Areas/Public/Content/css/HomePageStyle.css", new CssRewriteUrlTransform())
                .Include(BundlePath.css.icons, new CssRewriteUrlTransform()));

            // Common JS Bundle
            bundles.Add(new ScriptBundle("~/bundles/landing")
               .Include(BundlePath.jquery)
               .Include(BundlePath.jqPlugin.tether)
               .Include(BundlePath.js.detect)
               .Include(BundlePath.js.fastclick)
               .Include(BundlePath.js.common)
               .Include(BundlePath.jqPlugin.cryptography)
               //.Include(BundlePath.jqPlugin.bootstrap)
               .Include(BundlePath.jqPlugin.toastr)
               .Include(BundlePath.jqPlugin.moment)
               .Include(BundlePath.angular.js)
               .Include(BundlePath.angular.module.angularCookies)
               .Include(BundlePath.angular.module.uiBootstrap)
               .Include(BundlePath.angular.module.angularFilter)
               .Include(BundlePath.angular.module.textAngularRangy)
               .Include(BundlePath.angular.module.textAngularSanitize)
               .Include(BundlePath.angular.module.textAngular)
               .Include(BundlePath.angular.app)
               .Include(BundlePath.angular.commonFunctions)
               .Include(BundlePath.angular.customFilters)
               .Include(BundlePath.angular.directive.customDirectives)
               .Include("~/Scripts/angular/services/CommonService.js")
               .Include("~/Scripts/angular/services/AccountService.js")
               .Include(BundlePath.angular.directive.infiniteScroll));
        }

        /// <summary>
        /// Register Inner Bundles for logged in users pages
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        public static void RegisterInnerBundles(BundleCollection bundles)
        {
            // Common CSS Bundle (inner)
            bundles.Add(new StyleBundle("~/css/inner")
                .Include(BundlePath.css.bootstrap, new CssRewriteUrlTransform())
                .Include(BundlePath.css.style)
                .Include(BundlePath.css.developer)
                .Include(BundlePath.css.menu)
                .Include(BundlePath.css.angucomplete)
                .Include(BundlePath.css.daterangepicker)
                .Include(BundlePath.css.ngTable)
                .Include(BundlePath.css.toastr)
                .Include(BundlePath.css.icons, new CssRewriteUrlTransform())
                .Include(BundlePath.css.slidingtabpanel, new CssRewriteUrlTransform())
                .Include(BundlePath.css.switchery)
                .Include(BundlePath.css.colorpicker)
                .Include(BundlePath.css.imgareaselect, new CssRewriteUrlTransform())
                .Include(BundlePath.css.textAngular)
                .Include(BundlePath.css.spectrum)
                .Include(BundlePath.css.scrollableTable)
                .Include(BundlePath.css.tooltipster));

            // Common JS Bundle (inner)
            bundles.Add(new ScriptBundle("~/bundles/inner")
               .Include(BundlePath.jquery)
               .Include(BundlePath.jqPlugin.tether)
               .Include(BundlePath.js.bootstrapbundle)
               .Include(BundlePath.js.detect)
               .Include(BundlePath.js.fastclick)
               .Include(BundlePath.js.blockUI)
               .Include(BundlePath.js.wow)
               .Include(BundlePath.js.nicescroll)
               .Include(BundlePath.js.switchery)
               .Include(BundlePath.js.jsCore)
               .Include(BundlePath.js.jsApp)
               .Include(BundlePath.js.spectrum)
               .Include(BundlePath.js.fullscreen)
               .Include(BundlePath.js.waves)
               .Include(BundlePath.js.common)
               .Include(BundlePath.jqPlugin.cryptography)
               .Include(BundlePath.jqPlugin.bootstrap)
               .Include(BundlePath.jqPlugin.toastr)
               .Include(BundlePath.jqPlugin.bootbox)
               .Include(BundlePath.jqPlugin.moment)
               .Include(BundlePath.jqPlugin.lodash)
               .Include(BundlePath.jqPlugin.daterangepicker)
               .Include(BundlePath.jqPlugin.d3v3)
               .Include(BundlePath.jqPlugin.radar)
               .Include(BundlePath.jqPlugin.tooltipster)
               .Include(BundlePath.jqPlugin.d3)
               .Include(BundlePath.jqPlugin.nvd3)
               .Include(BundlePath.angular.js)
               .Include("~/Scripts/angular/angular-drag-and-drop-lists.js")
               .Include(BundlePath.angular.module.angularRoute)
               .Include(BundlePath.angular.module.angularCookies)
               .Include(BundlePath.angular.module.uiBootstrap)
               .Include(BundlePath.angular.module.angularAnimate)
               .Include(BundlePath.angular.module.ngTable)
               .Include(BundlePath.angular.module.ngBootbox)
               .Include(BundlePath.angular.module.angucomplete)
               .Include(BundlePath.angular.module.multiselect)
               .Include(BundlePath.angular.module.angularFilter)
               .Include(BundlePath.angular.module.fileUpload)
               .Include(BundlePath.angular.module.radarChart)
               .Include(BundlePath.angular.module.timePicker)
               .Include(BundlePath.angular.module.dateparser)
               .Include(BundlePath.angular.module.textAngularRangy)
               .Include(BundlePath.angular.module.textAngularSanitize)
               .Include(BundlePath.angular.module.textAngular)
               .Include(BundlePath.angular.module.switchery)
               .Include(BundlePath.angular.module.colorpicker)
               .Include(BundlePath.angular.module.pectrumColorpicker)
               .Include(BundlePath.angular.module.imgAreaSelect)
               .Include(BundlePath.angular.module.scrollableTable)
               .Include(BundlePath.angular.module.nicescroll)
               .Include(BundlePath.angular.module.draganddrop)
               .Include(BundlePath.angular.module.nvd3)
               .Include(BundlePath.angular.module.gridster)
               .Include(BundlePath.angular.app)
               .Include(BundlePath.angular.directive.textAngularControls)
               .Include(BundlePath.angular.commonFunctions)
               .Include(BundlePath.angular.commonEnums)
               .Include(BundlePath.angular.directive.angularDaterangePicker)
               .Include(BundlePath.angular.directive.customDirectives)
               .Include(BundlePath.angular.directive.dateValidation)
               .Include(BundlePath.angular.directive.validationDirectives)
               .Include(BundlePath.angular.directive.safeFileUploader)
               .Include(BundlePath.angular.directive.fileReader)
               .Include(BundlePath.angular.directive.autocompleteDropdown)
               .Include(BundlePath.angular.customFilters)
               .Include("~/Scripts/angular/controllers/MasterCtrl.js")
               .Include("~/Scripts/angular/services/CommonService.js")
               .Include("~/Scripts/angular/services/AccountService.js")
               .Include("~/Scripts/angular/services/FileService.js"));
        }

        /// <summary>
        /// Register Other Bundles page wise
        /// </summary>
        /// <param name="bundles">Bundle Collection</param>
        public static void RegisterOtherBundles(BundleCollection bundles)
        {
            // Account
            bundles.Add(new ScriptBundle("~/bundles/Account")
               .Include("~/Scripts/angular/controllers/AccountCtrl.js"));

            bundles.Add(new ScriptBundle("~/bundles/ResetPassword")
             .Include("~/Scripts/angular/controllers/ResetPasswordCtrl.js")
              .Include("~/Scripts/angular/services/ResetPasswordService.js"));

            // Home
            bundles.Add(new ScriptBundle("~/bundles/Demo")
               .Include("~/Scripts/angular/controllers/DemoCtrl.js"));

            // Component
            bundles.Add(new ScriptBundle("~/bundles/DashboardComponent")
               .Include("~/Scripts/angular/services/DashBoardService.js")
               .Include("~/Scripts/angular/services/WidgetCommonService.js")
               .Include("~/Scripts/angular/controllers/DashBoardCtrl.js")
               .Include("~/Scripts/angular/controllers/CustomWidgetCtrl.js")
               .Include("~/Scripts/angular/controllers/FilterWidgetCtrl.js")
               .Include(BundlePath.js.rgbcolor)
               .Include(BundlePath.js.canvg));
        }
    }
}