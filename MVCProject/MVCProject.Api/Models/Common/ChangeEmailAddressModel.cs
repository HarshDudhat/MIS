// -----------------------------------------------------------------------
// <copyright file="ChangeEmailAddressModel.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models.Common
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Change Email Address Model
    /// </summary>
    [DataContract]
    public class ChangeEmailAddressModel
    {
        /// <summary>
        /// Gets or sets User Name
        /// </summary> 
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets  old Email Address
        /// </summary>        
        [DataMember]
        public string OldEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets new Email Address
        /// </summary>        
        [DataMember]
        public string NewEmailAddress { get; set; }
    }
}