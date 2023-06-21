// -----------------------------------------------------------------------
// <copyright file="ApiResponse.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Generalized response class to send response to client.
    /// </summary>
    [DataContract]
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether request is authenticated or not.
        /// </summary>
        [DataMember]
        internal bool IsAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets final result needs to be returned to client.
        /// </summary>
        [DataMember]
        internal object Result { get; set; }

        /// <summary>
        /// Gets or sets custom message.
        /// </summary>
        [DataMember]
        internal string Message { get; set; }

        /// <summary>
        /// Gets or sets message type i.e. Success, Warning, Error and Info.
        /// </summary>
        [DataMember]
        internal int MessageType { get; set; }

        /// <summary>
        /// Gets or sets total number of record(s).
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        internal string Total { get; set; }
    }
}