// -----------------------------------------------------------------------
// <copyright file="ExcelStyle.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Utilities
{
    #region Namespaces

    using System;
    using NPOI.SS.UserModel;

    #endregion

    /// <summary>
    /// Excel Sheet Styles
    /// </summary>
    public class ExcelStyle
    {
        /// <summary>
        /// Gets or sets BigCellStyle
        /// </summary>
        public ICellStyle BigCellStyle { get; set; }

        /// <summary>
        /// Gets or sets BoldCellStyle
        /// </summary>
        public ICellStyle BoldCellStyle { get; set; }

        /// <summary>
        /// Gets or sets BoldLeftCellStyle
        /// </summary>
        public ICellStyle BoldLeftCellStyle { get; set; }

        /// <summary>
        /// Gets or sets NormalCellStyle
        /// </summary>
        public ICellStyle NormalCellStyle { get; set; }

        /// <summary>
        /// Gets or sets NormalRedCellStyle
        /// </summary>
        public ICellStyle NormalRedCellStyle { get; set; }

        /// <summary>
        /// Gets or sets NormalDarkCellStyle
        /// </summary>
        public ICellStyle NormalDarkCellStyle { get; set; }

        /// <summary>
        /// Gets or sets ReportHeaderCellStyle
        /// </summary>
        public ICellStyle ReportHeaderCellStyle { get; set; }

        /// <summary>
        /// Gets or sets DateFormatStyle
        /// </summary>
        public ICellStyle DateFormatStyle { get; set; }

        /// <summary>
        /// Gets or sets DateTimeFormatStyle
        /// </summary>
        public ICellStyle DateTimeFormatStyle { get; set; }

        /// <summary>
        /// Gets or sets DateTimeFormatDataStyle
        /// </summary>
        public ICellStyle DateTimeFormatDataStyle { get; set; }

        public ICellStyle BackRedGroundCellStyle { get; set; }

        public ICellStyle BackYellowCellStyle { get; set; }

        public ICellStyle BackGreenCellStyle { get; set; }

        public ICellStyle BackOrangeCellStyle { get; set; }

        public ICellStyle DaysCellStyle { get; set; }
    }
}