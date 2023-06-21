// -----------------------------------------------------------------------
// <copyright file="BundleAngularDirectivePath.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------
namespace MVCProject.Models
{
    /// <summary>
    /// Bundle Angular module Path
    /// </summary>
    public class BundleAngularDirectivePath
    {
        /// <summary>
        /// Angular directives path
        /// </summary>
        private static string path = "~/Scripts/angular/directives/";

        /// <summary>
        /// Custom directives
        /// </summary>
        public string customDirectives = path + "CustomDirectives.js";

        /// <summary>
        /// Infinite scroll
        /// </summary>
        public string infiniteScroll = path + "ng-infinite-scroll.js";

        /// <summary>
        /// Text angular controls
        /// </summary>
        public string textAngularControls = path + "textAngular-Controls.js";

        /// <summary>
        /// Date range picker
        /// </summary>
        public string angularDaterangePicker = path + "angularDaterangePicker.js";

        /// <summary>
        /// Date validation
        /// </summary>
        public string dateValidation = path + "DateValidation.js";

        /// <summary>
        /// Validation Directives
        /// </summary>
        public string validationDirectives = path + "ValidationDirectives.js";

        /// <summary>
        /// Safe file upload
        /// </summary>
        public string safeFileUploader = path + "SafeFileUploader.js";

        /// <summary>
        /// File reader
        /// </summary>
        public string fileReader = path + "FileReader.js";

        /// <summary>
        /// Autocomplete Dropdown
        /// </summary>
        public string autocompleteDropdown = path + "AutocompleteDropdown.js";
    }
}