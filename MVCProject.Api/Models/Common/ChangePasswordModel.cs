
// -----------------------------------------------------------------------
// <copyright file="ForgotPasswordModel.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.Common
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Change Password Model
    /// </summary>
    [DataContract]
    public class ChangePasswordModel
    {
        /// <summary>
        /// Gets or sets User Name
        /// </summary>        
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets old password
        /// </summary>        
        [DataMember]
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets old password
        /// </summary>        
        [DataMember]
        public string NewPassword { get; set; }
    }
}