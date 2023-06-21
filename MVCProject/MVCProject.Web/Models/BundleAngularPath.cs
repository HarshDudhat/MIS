// -----------------------------------------------------------------------
// <copyright file="BundleAngularPath.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Models
{
    /// <summary>
    /// Bundle Angular Path
    /// </summary>
    public class BundleAngularPath
    {
        /// <summary>
        /// Angular path
        /// </summary>
        private static string path = "~/Scripts/angular/";

        /// <summary>
        /// angular.js path
        /// </summary>
        public string js = path + "vendor/lib/angular.js";

        /// <summary>
        /// app.js
        /// </summary>
        public string app = path + "app.js";

        /// <summary>
        /// commonFunctions path
        /// </summary>
        public string commonFunctions = path + "common/functions.js";

        /// <summary>
        /// commonEnums path
        /// </summary>
        public string commonEnums = path + "common/enums.js";

        /// <summary>
        /// customFilters path
        /// </summary>
        public string customFilters = path + "filters/CustomFilters.js";

        /// <summary>
        /// Angular directive
        /// </summary>
        public BundleAngularDirectivePath directive = new BundleAngularDirectivePath();

        /// <summary>
        /// Angular module
        /// </summary>
        public BundleAngularModulePath module = new BundleAngularModulePath();
    }
}