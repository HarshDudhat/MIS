// -----------------------------------------------------------------------
// <copyright file="BundlePath.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Models
{
    /// <summary>
    /// physical path of CSS and JS files
    /// </summary>
    public static class BundlePath
    {
        /// <summary>
        /// Bundle CSS Path
        /// </summary>
        public static BundleCSSPath css = new BundleCSSPath();

        /// <summary>
        /// jquery JS
        /// </summary>
        public static string jquery = "~/Scripts/jquery/lib/jquery.min.js";

        /// <summary>
        /// Bundle JS Path
        /// </summary>
        public static BundleJSPath js = new BundleJSPath();

        /// <summary>
        /// Bundle jQuery plugin Path
        /// </summary>
        public static BundleJqueryPluginPath jqPlugin = new BundleJqueryPluginPath();

        /// <summary>
        /// Bundle Angular Path
        /// </summary>
        public static BundleAngularPath angular = new BundleAngularPath();
    }
}