using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Api.Models
{
    [MetadataType(typeof(SliderMetaData))]
    public partial class Slider
    {
        /// <summary>
        /// Gets or sets image of a slider.
        /// </summary>
        [DataMember]
        public Attachments Image { get; set; }

        // <summary>
        /// Gets or sets image path.
        /// </summary>
        [DataMember]
        public string AttachmentPath { get; set; }

        [DataContract]
        internal sealed class SliderMetaData
        {
            [Required]
            [StringLength(100)]
            public string SliderHeader { get; set; }

            [StringLength(25)]
            public string SliderHeaderColor { get; set; }

            [Required]
            [StringLength(500)]
            public string ShortDescription { get; set; }

            [StringLength(25)]
            public string ShortDescriptionColor { get; set; }

            [StringLength(50)]
            public string ButtonCaption { get; set; }

            [StringLength(25)]
            public string ButtonCaptionColor { get; set; }
            [StringLength(25)]
            public string ButtonCaptionBGColor { get; set; }

            [DataMember(IsRequired = true)]
            public int PriorityOrder { get; set; }

        }
    }
}