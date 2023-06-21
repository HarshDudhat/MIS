// -----------------------------------------------------------------------
// <copyright file="LogOn.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// LogOn class for login page
    /// </summary>
    [DataContract]
    public class LogOn
    {
        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        [DataMember(IsRequired = true)]
        public string UserPassword { get; set; }

        /// <summary>
        /// Gets or sets Time Zone Minutes.
        /// </summary>
        [DataMember(IsRequired = true)]
        public int TimeZoneMinutes { get; set; }

        /// <summary>
        /// Gets or sets User Domain of only App User
        /// </summary>
        [DataMember]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets User Domain of only App User
        /// </summary>
        [DataMember]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        [DataMember]
        public bool IsMobileUser { get; set; }

        /// <summary>
        /// Gets or sets DevicePlatform.
        /// </summary>
        [DataMember]
        public string DevicePlatform { get; set; }

        /// <summary>
        /// Gets or sets AppVersion.
        /// </summary>
        [DataMember]
        public string AppVersion { get; set; }

    }
}