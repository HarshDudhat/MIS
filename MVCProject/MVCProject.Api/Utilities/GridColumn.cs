// -----------------------------------------------------------------------
// <copyright file="GridColumn.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Utilities
{
    #region Namespaces

    using System;

    #endregion

    /// <summary>
    /// Excel grid column
    /// </summary>
    public class GridColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumn" /> class
        /// </summary>
        public GridColumn()
        {
            this.Title = string.Empty;
            this.FieldName = string.Empty;
            this.Width = 256 * 30;
            this.IsSrNo = false;
            this.DataType = GridColumnDataType.String;
            this.HorizontalAlignment = HorizontalAlign.Center;
        }

        /// <summary>
        /// Gets or sets Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets FieldName
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets Width
        /// </summary>
        public float Width { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether column is serial number
        /// </summary>
        public bool IsSrNo { get; set; }

        /// <summary>
        /// Gets or sets DataType
        /// </summary>
        public GridColumnDataType DataType { get; set; }

        /// <summary>
        /// Gets or sets Horizontal alignment
        /// </summary>
        public HorizontalAlign HorizontalAlignment { get; set; }
    }
}