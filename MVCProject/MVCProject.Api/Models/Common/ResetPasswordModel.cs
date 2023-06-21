// -----------------------------------------------------------------------
// <copyright file="ResetPasswordModel.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.Common
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Reset Password Model
    /// </summary>
    [DataContract]
    public class ResetPasswordModel
    {
        /// <summary>
        /// Gets or sets User Id
        /// </summary>
        [Required]
        [DataMember(IsRequired = true)]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets Reset Password Token
        /// </summary>
        [Required]
        [DataMember(IsRequired = true)]
        public string ResetPasswordToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether token will be checked or not
        /// </summary>
        [DataMember]
        public bool IsToCheckToken { get; set; }

        /// <summary>
        /// Gets or sets new password
        /// </summary>
        [DataMember]
        public string NewPassword { get; set; }
    }
}