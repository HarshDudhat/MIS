// -----------------------------------------------------------------------
// <copyright file="NewsMetaData.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
namespace MVCProject.Api.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// Holds News Master related property
    /// </summary>
    [MetadataType(typeof(NewsMetaData))]
    public partial class News
    {
        /// <summary>
        /// Gets or sets image of a slider.
        /// </summary>
        [DataMember]
        public Attachments Image { get; set; }

        /// <summary>
        /// Holds News meta data related property
        /// </summary>
        [DataContract]
        internal sealed class NewsMetaData
        {
            /// <summary>
            /// Gets or sets NewsHeader
            /// </summary>
            [Required]
            [StringLength(100)]
            public string NewsHeader { get; set; }

            /// <summary>
            /// Gets or sets ShortDescription
            /// </summary>
            [Required]
            [StringLength(500)]
            public string ShortDescription { get; set; }

            /// <summary>
            /// Gets or sets NewsDate
            /// </summary>
            [DataMember(IsRequired = true)]
            public DateTime NewsDate { get; set; }

            /// <summary>
            /// Gets or sets FullDescription
            /// </summary>
            [Required]
            public string FullDescription { get; set; }
        }
    }
}