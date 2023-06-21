// -----------------------------------------------------------------------
// <copyright file="ExcelHelper.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Utilities
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using NPOI.HPSF;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;
    using MVCProject.Api.Models;
    using MVCProject.Common.Resources;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    ///  Helper for extort data to excel
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// work book
        /// </summary>
        private HSSFWorkbook workBook;

        /// <summary>
        /// styles to be used in excel
        /// </summary>
        private ExcelStyle styles;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelHelper" /> class
        /// ExcelHelper constructor. 
        /// Creates new workbook and set company name and subject
        /// </summary>
        /// <param name="companyName">Company Name</param>
        /// <param name="subject">Subject of excel</param>
        public ExcelHelper(string companyName, string subject)
        {
            this.workBook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = companyName;
            this.WorkBook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = subject;
            this.WorkBook.SummaryInformation = si;

            this.ConfigureStyles();
        }

        /// <summary>
        /// Export single Grid Data to excel
        /// </summary>
        /// <param name="sheetTitle">Sheet title</param>
        /// <param name="filters">Filters to be displayed</param>
        /// <param name="gridColumns">Grid columns</param>
        /// <param name="data">Grid data</param>
        /// <param name="userContext">current user context</param>
        /// <param name="moduleName">module name value</param>
        public void ExportGridData(string sheetTitle, Dictionary<string, string> filters, List<GridColumn> gridColumns, List<Dictionary<string, object>> data, UserContext userContext, ModuleName? moduleName = null, string sheetHeaderTitle = null)
        {
            ISheet sheet = this.CreateSheet(sheetTitle);

            int rowNo = 0;

            this.SetSheetHeader(sheet, sheetHeaderTitle ?? sheetTitle, gridColumns.Count, rowNo, userContext.TimeZoneMinutes, out rowNo);

            if (filters != null)
            {
                rowNo++;
                rowNo++;
                this.SetFilters(sheet, filters, rowNo, out rowNo);
            }

            rowNo++;
            this.SetGridHeader(sheet, gridColumns, rowNo, out rowNo);

            if (moduleName == null)
            {
                this.SetGridData(sheet, gridColumns, data, rowNo, userContext, out rowNo);
            }
            else if (moduleName == ModuleName.SafetyObservation)
            {
                this.SetGridDataSafetyObservationSearch(sheet, gridColumns, data, rowNo, userContext, out rowNo);
            }
        }

        /// <summary>
        /// Set Grid Data
        /// </summary>
        /// <param name="sheet">ISheet type object</param>
        /// <param name="gridColumns">Grid columns</param>
        /// <param name="data">Grid data</param>
        /// <param name="rowNo">Row number to start with</param>
        /// <param name="userContext">current user context</param>
        /// <param name="lastRowNo">Last row where method reaches</param>
        public void SetGridDataSafetyObservationSearch(ISheet sheet, List<GridColumn> gridColumns, List<Dictionary<string, object>> data, int rowNo, UserContext userContext, out int lastRowNo)
        {
            // Populate the sheet with values from the grid data
            int i = -1;
            int y = 1;

            ICell cell;
            List<ICell> dateCells;
            List<ICell> dateTimeCells;
            foreach (Dictionary<string, object> item in data)
            {
                // Create a new row
                var row = sheet.CreateRow(++rowNo);
                dateCells = new List<ICell>();
                dateTimeCells = new List<ICell>();

                // Set values for the cells
                foreach (GridColumn col in gridColumns)
                {
                    cell = row.CreateCell(++i);
                    sheet.SetColumnWidth(i, (int)col.Width);

                    if (col.FieldName == "CAPAStatus")
                    {
                        if (Convert.ToString(item[col.FieldName]) == "Active")
                        {
                            cell.CellStyle = this.Styles.BackRedGroundCellStyle;
                        }
                        else if (Convert.ToString(item[col.FieldName]) == "In-Progress")
                        {
                            cell.CellStyle = this.Styles.BackYellowCellStyle;
                        }
                        else if (Convert.ToString(item[col.FieldName]) == "Completed")
                        {
                            cell.CellStyle = this.Styles.BackGreenCellStyle;
                        }
                        else if (Convert.ToString(item[col.FieldName]) == "Reopen")
                        {
                            cell.CellStyle = this.Styles.BackRedGroundCellStyle;
                        }
                        else if (Convert.ToString(item[col.FieldName]) == "Closed")
                        {
                            cell.CellStyle = this.Styles.BackGreenCellStyle;
                        }
                        else if (string.IsNullOrEmpty(Convert.ToString(item[col.FieldName])))
                        {
                            cell.CellStyle = this.Styles.NormalCellStyle;
                        }
                    }
                    else if (col.FieldName == "RiskLevel")
                    {
                        if (Convert.ToString(item[col.FieldName]) == "Low")
                        {
                            cell.CellStyle = this.Styles.BackGreenCellStyle;
                        }
                        else if (Convert.ToString(item[col.FieldName]) == "Medium")
                        {
                            cell.CellStyle = this.Styles.BackYellowCellStyle;
                        }
                        else if (Convert.ToString(item[col.FieldName]) == "High")
                        {
                            cell.CellStyle = this.Styles.BackOrangeCellStyle;
                        }
                        else if (Convert.ToString(item[col.FieldName]) == "Critical")
                        {
                            cell.CellStyle = this.Styles.BackRedGroundCellStyle;
                        }
                        else if (string.IsNullOrEmpty(Convert.ToString(item[col.FieldName])))
                        {
                            cell.CellStyle = this.Styles.NormalCellStyle;
                        }
                    }
                    else if (col.FieldName == "Days")
                    {
                        cell.SetCellFormula(string.Format("IF(OR(Q{0}=\"Active\", Q{0}=\"In-Progress\" , Q{0}=\"Completed\", Q{0}=\"Reopen\"), ( IF(DATE(YEAR(R3),MONTH(R3),DAY(R3)) - DATE(YEAR(B{0}),MONTH(B{0}),DAY(B{0}))< 1, 1, DATE(YEAR(R3),MONTH(R3),DAY(R3)) - DATE(YEAR(B{0}),MONTH(B{0}),DAY(B{0})) ) ), IF(Q{0}=\"Closed\",IF($R${0}-DATE(YEAR(B{0}),MONTH(B{0}),DAY(B{0})) < 1, 1, $R${0}-DATE(YEAR(B{0}),MONTH(B{0}),DAY(B{0}))), \" \"))", row.RowNum + 1));
                        cell.CellStyle = this.Styles.DaysCellStyle;

                    }
                    else if (col.FieldName == "Age")
                    {
                        cell.SetCellFormula(string.Format("IF(OR(Q{0}=\"Active\", Q{0}=\"In-Progress\" , Q{0}=\"Completed\", Q{0}=\"Reopen\", Q{0}=\"Closed\"), IF(O{0} <= 30, \" < 30 Days\", IF(AND(O{0} >= 31,O{0} <= 60), \"31 to 60 Days\",IF( AND(O{0} >= 61,O{0} <= 90), \"61 to 90 Days\", IF(O{0} >= 91, \"above 91 Days\", \" \")))), \" \")", row.RowNum + 1));
                        cell.CellStyle = this.Styles.NormalCellStyle;
                    }
                    else
                    {
                        cell.CellStyle = this.Styles.NormalCellStyle;
                    }

                    if (col.IsSrNo)
                    {
                        cell.SetCellValue(y);
                    }
                    else
                    {
                        switch (col.DataType)
                        {
                            case GridColumnDataType.String:
                                cell.SetCellValue(Convert.ToString(item[col.FieldName]));
                                break;
                            case GridColumnDataType.Date:
                            case GridColumnDataType.Datetime:
                                if (col.DataType == GridColumnDataType.Date)
                                {
                                    dateCells.Add(cell);
                                }
                                else if (col.DataType == GridColumnDataType.Datetime)
                                {
                                    dateTimeCells.Add(cell);
                                }

                                if (item[col.FieldName] != null)
                                {
                                    cell.SetCellValue(Convert.ToDateTime(item[col.FieldName]).AddMinutes(userContext.TimeZoneMinutes));
                                }
                                else
                                {
                                    cell.SetCellValue(string.Empty);
                                }

                                break;
                            case GridColumnDataType.Time:
                                if (item[col.FieldName] != null)
                                {
                                    cell.SetCellValue(Convert.ToDateTime(item[col.FieldName]).AddMinutes(userContext.TimeZoneMinutes).ToString("HH:mm"));
                                }

                                break;
                            case GridColumnDataType.Int:
                                if (item[col.FieldName] != null)
                                {
                                    cell.SetCellValue(Convert.ToInt32(item[col.FieldName]));
                                }
                                else
                                {
                                    cell.SetCellValue(string.Empty);
                                }

                                break;
                            case GridColumnDataType.Double:
                                if (item[col.FieldName] != null)
                                {
                                    cell.SetCellValue(Convert.ToDouble(item[col.FieldName]));
                                }
                                else
                                {
                                    cell.SetCellValue(string.Empty);
                                }

                                break;
                            default:
                                cell.SetCellValue(Convert.ToString(item[col.FieldName]));
                                break;
                        }
                    }
                }

                foreach (ICell dateCell in dateCells)
                {
                    dateCell.CellStyle = this.Styles.DateFormatStyle;
                }

                foreach (ICell dateTimeCell in dateTimeCells)
                {
                    dateTimeCell.CellStyle = this.Styles.DateTimeFormatDataStyle;
                }

                i = -1;

                y++;
            }

            lastRowNo = rowNo;
        }


        /// <summary>
        ///  Gets excel Work book object
        /// </summary>
        public HSSFWorkbook WorkBook
        {
            get
            {
                return this.workBook;
            }
        }

        /// <summary>
        ///  Gets styles to be used in excel
        /// </summary>
        public ExcelStyle Styles
        {
            get
            {
                return this.styles;
            }
        }

        /// <summary>
        /// Create Sheet in workbook
        /// </summary>
        /// <param name="sheetName">Excel sheet name</param>
        /// <returns>returns ISheet</returns>
        public ISheet CreateSheet(string sheetName)
        {
            return this.WorkBook.CreateSheet(sheetName.Replace("/", string.Empty));
        }

        /// <summary>
        /// Set Sheet Header
        /// </summary>
        /// <param name="sheet">ISheet type object</param>
        /// <param name="sheetHeader">Text used as sheet header</param>
        /// <param name="totalColumns">Total columns</param>
        /// <param name="rowNo">Row number to start with</param>
        /// <param name="timeZoneMinutes">clients Time Zone Minutes</param>
        /// <param name="lastRowNo">Last row where method reaches</param>
        public void SetSheetHeader(ISheet sheet, string sheetHeader, int totalColumns, int rowNo, int timeZoneMinutes, out int lastRowNo)
        {
            IRow reportHeader1 = sheet.CreateRow(rowNo);
            reportHeader1.CreateCell(0).SetCellValue(sheetHeader);

            rowNo = rowNo + 2;
            IRow reportHeader2 = sheet.CreateRow(rowNo);
            reportHeader2.CreateCell(totalColumns > 1 ? totalColumns - 2 : 0).SetCellValue("Report printed on: ");
            reportHeader2.CreateCell(totalColumns > 1 ? totalColumns - 1 : 1).SetCellValue(DateTime.UtcNow.AddMinutes(timeZoneMinutes));

            reportHeader1.Cells[0].CellStyle = this.Styles.BigCellStyle;
            reportHeader2.GetCell(totalColumns > 1 ? totalColumns - 2 : 0).CellStyle = this.Styles.ReportHeaderCellStyle;
            reportHeader2.GetCell(totalColumns > 1 ? totalColumns - 1 : 1).CellStyle = this.Styles.DateTimeFormatStyle;

            lastRowNo = rowNo;
        }

        /// <summary>
        /// Set Filter criteria
        /// </summary>
        /// <param name="sheet">ISheet type object</param>
        /// <param name="filters">Text used as sheet header</param>
        /// <param name="rowNo">Row number to start with</param>
        /// <param name="lastRowNo">Last row where method reaches</param>
        public void SetFilters(ISheet sheet, Dictionary<string, string> filters, int rowNo, out int lastRowNo)
        {
            bool hasFilter = false;
            int startRowNo = rowNo;
            rowNo = rowNo + 2;
            IRow reportHeader;
            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter.Value))
                {
                    reportHeader = sheet.CreateRow(rowNo);
                    reportHeader.CreateCell(0).SetCellValue(filter.Key);
                    reportHeader.CreateCell(1).SetCellValue(filter.Value);
                    hasFilter = true;
                    rowNo++;
                }
            }

            if (hasFilter)
            {
                IRow reportHeaderChild1 = sheet.CreateRow(startRowNo + 1);
                reportHeaderChild1.CreateCell(0).SetCellValue("Parameter ");
                reportHeaderChild1.CreateCell(1).SetCellValue("Value");

                reportHeaderChild1.Cells[0].CellStyle = this.Styles.ReportHeaderCellStyle;
                reportHeaderChild1.Cells[1].CellStyle = this.Styles.ReportHeaderCellStyle;

                IRow reportHeader3 = sheet.CreateRow(startRowNo);
                reportHeader3.CreateCell(0).SetCellValue("Filter parameter(s)");
                reportHeader3.Cells[0].CellStyle = this.Styles.ReportHeaderCellStyle;

                this.AddMergedRegion(sheet, new CellRangeAddress(startRowNo, startRowNo, 0, 1));
            }
            else
            {
                rowNo = rowNo - 2;
            }

            lastRowNo = rowNo;
        }

        /// <summary>
        /// Set Grid Header
        /// </summary>
        /// <param name="sheet">ISheet type object</param>
        /// <param name="gridColumns">Grid columns</param>
        /// <param name="rowNo">Row number to start with</param>
        /// <param name="lastRowNo">Last row where method reaches</param>
        public void SetGridHeader(ISheet sheet, List<GridColumn> gridColumns, int rowNo, out int lastRowNo)
        {
            // Create a header row
            var headerRow = sheet.CreateRow(rowNo);

            // Set the column names in the header row
            int j = -1;
            foreach (GridColumn li in gridColumns)
            {
                j++;
                headerRow.CreateCell(j).SetCellValue(li.Title);
            }

            this.SetRowStyle(this.Styles.BoldCellStyle, headerRow);

            // Freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, rowNo + 1, 0, rowNo + 1);

            lastRowNo = rowNo;
            this.AddMergedRegion(sheet, new CellRangeAddress(0, 0, 0, gridColumns.Count - 1));
        }

        /// <summary>
        /// Set Grid Data
        /// </summary>
        /// <param name="sheet">ISheet type object</param>
        /// <param name="gridColumns">Grid columns</param>
        /// <param name="data">Grid data</param>
        /// <param name="rowNo">Row number to start with</param>
        /// <param name="userContext">current user context</param>
        /// <param name="lastRowNo">Last row where method reaches</param>
        public void SetGridData(ISheet sheet, List<GridColumn> gridColumns, List<Dictionary<string, object>> data, int rowNo, UserContext userContext, out int lastRowNo)
        {
            // Populate the sheet with values from the grid data
            int i = -1;
            int y = 1;

            ICell cell;
            List<ICell> dateCells;
            List<ICell> dateTimeCells;
            foreach (Dictionary<string, object> item in data)
            {
                // Create a new row
                var row = sheet.CreateRow(++rowNo);
                dateCells = new List<ICell>();
                dateTimeCells = new List<ICell>();

                // Set values for the cells
                foreach (GridColumn col in gridColumns)
                {
                    cell = row.CreateCell(++i);
                    sheet.SetColumnWidth(i, (int)col.Width);

                    if (col.IsSrNo)
                    {
                        cell.SetCellValue(y);
                    }
                    else
                    {
                        switch (col.DataType)
                        {
                            case GridColumnDataType.String:
                                cell.SetCellValue(Convert.ToString(item[col.FieldName]));
                                break;
                            case GridColumnDataType.Date:
                            case GridColumnDataType.Datetime:
                                if (col.DataType == GridColumnDataType.Date)
                                {
                                    dateCells.Add(cell);
                                }
                                else if (col.DataType == GridColumnDataType.Datetime)
                                {
                                    dateTimeCells.Add(cell);
                                }

                                if (item[col.FieldName] != null)
                                {
                                    if (col.DataType == GridColumnDataType.Date)
                                        cell.SetCellValue(Convert.ToDateTime(item[col.FieldName]).AddMinutes(userContext.TimeZoneMinutes).ToString(Resource.DateFormat));
                                    else
                                        cell.SetCellValue(Convert.ToDateTime(item[col.FieldName]).AddMinutes(userContext.TimeZoneMinutes).ToString(Resource.DateTimeFormat));
                                }
                                else
                                {
                                    cell.SetCellValue(string.Empty);
                                }

                                break;
                            case GridColumnDataType.Time:
                                if (item[col.FieldName] != null)
                                {
                                    cell.SetCellValue(Convert.ToDateTime(item[col.FieldName]).AddMinutes(userContext.TimeZoneMinutes).ToString("HH:mm"));
                                }

                                break;
                            case GridColumnDataType.Int:
                                if (item[col.FieldName] != null)
                                {
                                    cell.SetCellValue(Convert.ToInt32(item[col.FieldName]));
                                }
                                else
                                {
                                    cell.SetCellValue(string.Empty);
                                }

                                break;
                            case GridColumnDataType.Double:
                                if (item[col.FieldName] != null)
                                {
                                    cell.SetCellValue(Convert.ToDouble(item[col.FieldName]));
                                }
                                else
                                {
                                    cell.SetCellValue(string.Empty);
                                }

                                break;
                            default:
                                cell.SetCellValue(Convert.ToString(item[col.FieldName]));
                                break;
                        }
                    }
                }

                this.SetRowStyle(this.Styles.NormalCellStyle, row);

                foreach (ICell dateCell in dateCells)
                {
                    dateCell.CellStyle = this.Styles.DateFormatStyle;
                }

                foreach (ICell dateTimeCell in dateTimeCells)
                {
                    dateTimeCell.CellStyle = this.Styles.DateTimeFormatDataStyle;
                }

                i = -1;

                y++;
            }

            lastRowNo = rowNo;
        }

        /// <summary>
        /// Set Grid Data
        /// </summary>
        /// <param name="sheet">ISheet type object</param>
        /// <param name="gridColumns">Grid columns</param>
        /// <param name="data">Grid data</param>
        /// <param name="rowNo">Row number to start with</param>
        /// <param name="userContext">current user context</param>
        /// <param name="lastRowNo">Last row where method reaches</param>
        public void SetGridDataByGroup(ISheet sheet, List<GridColumn> gridColumns, List<Dictionary<string, object>> data, int rowNo, UserContext userContext, string groupObject, string groupListObject, out int lastRowNo)
        {
            // Populate the sheet with values from the grid data
            int i = -1;
            int y = 1;

            ICell cell;
            List<ICell> dateCells;
            List<ICell> dateTimeCells;
            foreach (Dictionary<string, object> group in data)
            {
                i = -1;
                // Create a new row
                var row = sheet.CreateRow(++rowNo);
                dateCells = new List<ICell>();
                dateTimeCells = new List<ICell>();

                for (int col = 0; col < gridColumns.Count; col++)
                {
                    cell = row.CreateCell(++i);
                    if (col == 1)
                    {
                        cell.SetCellValue(Convert.ToString(group[groupObject]));
                    }
                    else
                    {
                        cell.SetCellValue(string.Empty);
                    }
                }

                this.SetRowStyle(this.Styles.BoldCellStyle, row);

                List<Dictionary<string, object>> groupdata = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(JsonConvert.SerializeObject(group[groupListObject]));

                foreach (Dictionary<string, object> item in groupdata)
                {
                    i = -1;
                    row = sheet.CreateRow(++rowNo);
                    dateCells = new List<ICell>();
                    dateTimeCells = new List<ICell>();
                    // Set values for the cells
                    foreach (GridColumn col in gridColumns)
                    {
                        cell = row.CreateCell(++i);
                        sheet.SetColumnWidth(i, (int)col.Width);

                        if (col.IsSrNo)
                        {
                            cell.SetCellValue(y);
                        }
                        else
                        {
                            switch (col.DataType)
                            {
                                case GridColumnDataType.String:
                                    cell.SetCellValue(Convert.ToString(item[col.FieldName]));
                                    break;
                                case GridColumnDataType.Date:
                                case GridColumnDataType.Datetime:
                                    if (col.DataType == GridColumnDataType.Date)
                                    {
                                        dateCells.Add(cell);
                                    }
                                    else if (col.DataType == GridColumnDataType.Datetime)
                                    {
                                        dateTimeCells.Add(cell);
                                    }

                                    if (item[col.FieldName] != null)
                                    {
                                        cell.SetCellValue(Convert.ToDateTime(item[col.FieldName]).AddMinutes(userContext.TimeZoneMinutes));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(string.Empty);
                                    }

                                    break;
                                case GridColumnDataType.Time:
                                    if (item[col.FieldName] != null)
                                    {
                                        cell.SetCellValue(Convert.ToDateTime(item[col.FieldName]).AddMinutes(userContext.TimeZoneMinutes).ToString("HH:mm"));
                                    }

                                    break;
                                case GridColumnDataType.Int:
                                    if (item[col.FieldName] != null)
                                    {
                                        cell.SetCellValue(Convert.ToInt32(item[col.FieldName]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(string.Empty);
                                    }

                                    break;
                                case GridColumnDataType.Double:
                                    if (item[col.FieldName] != null)
                                    {
                                        cell.SetCellValue(Convert.ToDouble(item[col.FieldName]));
                                    }
                                    else
                                    {
                                        cell.SetCellValue(string.Empty);
                                    }

                                    break;
                                default:
                                    cell.SetCellValue(Convert.ToString(item[col.FieldName]));
                                    break;
                            }
                        }
                    }


                    this.SetRowStyle(this.Styles.NormalCellStyle, row);

                    foreach (ICell dateCell in dateCells)
                    {
                        dateCell.CellStyle = this.Styles.DateFormatStyle;
                    }

                    foreach (ICell dateTimeCell in dateTimeCells)
                    {
                        dateTimeCell.CellStyle = this.Styles.DateTimeFormatDataStyle;
                    }

                    y++;
                }
            }

            lastRowNo = rowNo;
        }

        /// <summary>
        /// Set Export To Excel Cell Style
        /// </summary>
        /// <param name="cellStyle">Row cell style</param>
        /// <param name="row">Row on which style to be applied</param>
        public void SetRowStyle(ICellStyle cellStyle, NPOI.SS.UserModel.IRow row)
        {
            for (int i = 0; i < row.Cells.Count; i++)
            {
                row.Cells[i].CellStyle = cellStyle;
            }
        }

        /// <summary>
        /// Add Merged Region
        /// </summary>
        /// <param name="sheet">ISheet type object</param>
        /// <param name="region">CellRangeAddress region</param>
        public void AddMergedRegion(ISheet sheet, CellRangeAddress region)
        {
            sheet.AddMergedRegion(region);
        }

        /// <summary>
        /// Export single Grid Data to excel
        /// </summary>
        /// <param name="sheetTitle">Sheet title</param>
        /// <param name="filters">Filters to be displayed</param>
        /// <param name="gridColumns">Grid columns</param>
        /// <param name="data">Grid data</param>
        /// <param name="userContext">current user context</param>
        public void ExportGridData(string sheetTitle, Dictionary<string, string> filters, List<GridColumn> gridColumns, List<Dictionary<string, object>> data, UserContext userContext)
        {
            ISheet sheet = this.CreateSheet(sheetTitle);

            int rowNo = 0;

            this.SetSheetHeader(sheet, sheetTitle, gridColumns.Count, rowNo, userContext.TimeZoneMinutes, out rowNo);

            rowNo++;
            rowNo++;
            this.SetFilters(sheet, filters, rowNo, out rowNo);

            rowNo++;
            this.SetGridHeader(sheet, gridColumns, rowNo, out rowNo);

            this.SetGridData(sheet, gridColumns, data, rowNo, userContext, out rowNo);
        }

        /// <summary>
        /// Export single Grid Data to excel
        /// </summary>
        /// <param name="sheetTitle">Sheet title</param>
        /// <param name="filters">Filters to be displayed</param>
        /// <param name="gridColumns">Grid columns</param>
        /// <param name="data">Grid data</param>
        /// <param name="userContext">current user context</param>
        public void ExportGridDataByGroup(string sheetTitle, Dictionary<string, string> filters, List<GridColumn> gridColumns, List<Dictionary<string, object>> data, string groupObject, string groupListObject, UserContext userContext)
        {
            ISheet sheet = this.CreateSheet(sheetTitle);

            int rowNo = 0;

            this.SetSheetHeader(sheet, sheetTitle, gridColumns.Count, rowNo, userContext.TimeZoneMinutes, out rowNo);

            rowNo++;
            rowNo++;
            this.SetFilters(sheet, filters, rowNo, out rowNo);

            rowNo++;
            this.SetGridHeader(sheet, gridColumns, rowNo, out rowNo);

            this.SetGridDataByGroup(sheet, gridColumns, data, rowNo, userContext, groupObject, groupListObject, out rowNo);
        }

        /// <summary>
        /// Configure Styles
        /// </summary>
        private void ConfigureStyles()
        {
            IFont boldFont = this.WorkBook.CreateFont();
            boldFont.FontHeightInPoints = 11;
            boldFont.FontName = "Calibri";
            boldFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            IFont bigFont = this.WorkBook.CreateFont();
            bigFont.FontHeightInPoints = 20;
            bigFont.FontName = "Calibri";
            bigFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            this.styles = new ExcelStyle();

            this.Styles.BigCellStyle = this.WorkBook.CreateCellStyle();
            this.Styles.BigCellStyle.SetFont(bigFont);
            this.Styles.BigCellStyle.WrapText = true;
            this.Styles.BigCellStyle.ShrinkToFit = true;
            this.Styles.BigCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            this.Styles.BigCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            this.Styles.BigCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.None;
            this.Styles.BigCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.None;
            this.Styles.BigCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.None;
            this.Styles.BigCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.None;
            this.Styles.BigCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            this.Styles.BigCellStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

            this.Styles.BoldCellStyle = this.WorkBook.CreateCellStyle();
            this.Styles.BoldCellStyle.SetFont(boldFont);
            this.Styles.BoldCellStyle.WrapText = true;
            this.Styles.BoldCellStyle.ShrinkToFit = true;
            this.Styles.BoldCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            this.Styles.BoldCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            this.Styles.BoldCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.BoldCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.BoldCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.BoldCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.BoldCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            this.Styles.BoldCellStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

            this.Styles.BoldLeftCellStyle = this.WorkBook.CreateCellStyle();
            this.Styles.BoldLeftCellStyle.SetFont(boldFont);
            this.Styles.BoldLeftCellStyle.WrapText = true;
            this.Styles.BoldLeftCellStyle.ShrinkToFit = true;
            this.Styles.BoldLeftCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            this.Styles.BoldLeftCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            this.Styles.BoldLeftCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.BoldLeftCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.BoldLeftCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.BoldLeftCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.BoldLeftCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            this.Styles.BoldLeftCellStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

            IFont normalFont = this.WorkBook.CreateFont();
            normalFont.FontHeightInPoints = 11;
            normalFont.FontName = "Calibri";
            normalFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

            this.Styles.NormalCellStyle = this.WorkBook.CreateCellStyle();
            this.Styles.NormalCellStyle.ShrinkToFit = true;
            this.Styles.NormalCellStyle.SetFont(normalFont);
            this.Styles.NormalCellStyle.WrapText = true;
            this.Styles.NormalCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            this.Styles.NormalCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.NormalCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.NormalCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.NormalCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            IFont normalRedFont = this.WorkBook.CreateFont();
            normalRedFont.FontHeightInPoints = 11;
            normalRedFont.FontName = "Calibri";
            normalRedFont.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
            normalRedFont.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

            this.Styles.NormalRedCellStyle = this.WorkBook.CreateCellStyle();
            this.Styles.NormalRedCellStyle.ShrinkToFit = true;
            this.Styles.NormalRedCellStyle.SetFont(normalRedFont);
            this.Styles.NormalRedCellStyle.WrapText = true;
            this.Styles.NormalRedCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            this.Styles.NormalRedCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.NormalRedCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.NormalRedCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.NormalRedCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            this.Styles.NormalDarkCellStyle = this.WorkBook.CreateCellStyle();
            this.Styles.NormalDarkCellStyle.ShrinkToFit = true;
            this.Styles.NormalDarkCellStyle.SetFont(boldFont);
            this.Styles.NormalDarkCellStyle.WrapText = true;
            this.Styles.NormalDarkCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            this.Styles.NormalDarkCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.NormalDarkCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.NormalDarkCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.NormalDarkCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            this.Styles.ReportHeaderCellStyle = this.WorkBook.CreateCellStyle();
            this.Styles.ReportHeaderCellStyle.SetFont(boldFont);
            this.Styles.ReportHeaderCellStyle.WrapText = true;
            this.Styles.ReportHeaderCellStyle.ShrinkToFit = true;
            this.Styles.ReportHeaderCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            this.Styles.ReportHeaderCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            this.Styles.ReportHeaderCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            this.Styles.ReportHeaderCellStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

            this.Styles.DateFormatStyle = this.WorkBook.CreateCellStyle();
            this.Styles.DateFormatStyle.ShrinkToFit = true;
            this.Styles.DateFormatStyle.SetFont(normalFont);
            this.Styles.DateFormatStyle.WrapText = true;
            this.Styles.DateFormatStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            this.Styles.DateFormatStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.DateFormatStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.DateFormatStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.DateFormatStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            IDataFormat dataFormatDate = this.WorkBook.CreateDataFormat();
            this.Styles.DateFormatStyle.DataFormat = dataFormatDate.GetFormat(Resource.DateFormat);

            this.Styles.DateTimeFormatStyle = this.WorkBook.CreateCellStyle();
            IDataFormat dataFormatDateTime = this.WorkBook.CreateDataFormat();
            this.Styles.DateTimeFormatStyle.DataFormat = dataFormatDateTime.GetFormat(Resource.DateTimeFormat);

            this.Styles.DateTimeFormatDataStyle = this.WorkBook.CreateCellStyle();
            this.Styles.DateTimeFormatDataStyle.ShrinkToFit = true;
            this.Styles.DateTimeFormatDataStyle.SetFont(normalFont);
            this.Styles.DateTimeFormatDataStyle.WrapText = true;
            this.Styles.DateTimeFormatDataStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;
            this.Styles.DateTimeFormatDataStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.DateTimeFormatDataStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.DateTimeFormatDataStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            this.Styles.DateTimeFormatDataStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            IDataFormat dataFormatDate1 = this.WorkBook.CreateDataFormat();
            this.Styles.DateTimeFormatDataStyle.DataFormat = dataFormatDate.GetFormat(Resource.DateTimeFormat);
        }
    }
}