// -----------------------------------------------------------------------
// <copyright file="ForgotPasswordModel.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.Common
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Forgot Password Model
    /// </summary>
    [DataContract]    
    public class ForgotPasswordModel
    {
        /// <summary>
        /// Gets or sets User Name
        /// </summary>
        [DataMember(IsRequired = true)]
        public string UserName { get; set; }
        
        /// <summary>
        /// Gets or sets company name of only App User
        /// </summary>        
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets User Domain of only App User
        /// </summary>
        [DataMember]
        public string Domain { get; set; }
        
        /// <summary>
        /// reset password from email or SMS
        /// </summary>        
        public bool FromEmail { get; set; }
    }
}