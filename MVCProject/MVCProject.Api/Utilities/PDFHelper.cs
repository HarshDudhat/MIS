// -----------------------------------------------------------------------
// <copyright file="PDFHelper.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Utilities
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using MVCProject.Api.Models;
    using MVCProject.Common.Resources;

    #endregion

    /// <summary>
    /// PDF helper class.
    /// </summary>
    public class PDFHelper
    {
        /// <summary>
        /// Adds image inside cell.
        /// </summary>
        /// <param name="cell">Cell in which image needs to be added.</param>
        /// <param name="image">Image which needs to be added.</param>
        /// <param name="fitWidth">Fit width.</param>
        /// <param name="fitHight">Fit height.</param>
        /// <param name="alignment">Image alignment in cell.</param>
        public static void AddImageInCell(PdfPCell cell, iTextSharp.text.Image image, float fitWidth, float fitHight, int alignment)
        {
            image.ScaleToFit(fitWidth, fitHight);
            image.Alignment = alignment;
            cell.AddElement(image);
        }

        /// <summary>
        /// Sets cell's value.
        /// </summary>
        /// <param name="value">Value to be set.</param>
        /// <param name="isTopBorderDark">A value indicating whether top border needs to be set darken or not.</param>
        /// <param name="isLeftBorderDark">A value indicating whether left border needs to be set darken or not.</param>
        /// <param name="isRightBorderDark">A value indicating whether right border needs to be set darken or not.</param>
        /// <param name="isBottomBorderDark">A value indicating whether bottom border needs to be set darken or not.</param>
        /// <param name="textAlign">Text alignment inside cell.</param>
        /// <param name="isBold">A value indicating whether text needs to be set bold or not.</param>
        /// <param name="isTableHeading">A value indicating whether value is table heading or not.</param>
        /// <param name="colspan">Column spanning value.</param>
        /// <param name="rowSpan">Row spanning value.</param>
        /// <returns>Returns PDF cell.</returns>
        public static PdfPCell PdfCellValue(string value, bool isTopBorderDark, bool isLeftBorderDark, bool isRightBorderDark, bool isBottomBorderDark, string textAlign = "Left", bool isBold = false, bool isTableHeading = false, int colspan = 1, int rowSpan = 1, bool showTopBorder = true, bool showLeftBorder = true, bool showRightBorder = true, bool showBottomBorder = true)
        {
            //Full path to the Unicode Arial file
            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "webdings.ttf");
            //Create a base font object making sure to specify IDENTITY-H
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //Create a specific font object
            Font f = new Font(bf, 12, Font.NORMAL);

            Font font10Bold = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Font font10Normal = FontFactory.GetFont("Arial", 10, Font.NORMAL);


            PdfPCell pdfPCell = new PdfPCell();
            Phrase phrase = new Phrase();
            if (isBold)
            {
                phrase.Add(new Chunk(value, font10Bold));
            }
            else
            {
                phrase.Add(new Chunk(value, font10Normal));
            }

            Paragraph paragraph = new Paragraph(phrase);
            paragraph.Leading = 15f;
            if (!string.IsNullOrEmpty(textAlign) && textAlign.ToLower().Equals("center"))
            {
                paragraph.Alignment = Element.ALIGN_CENTER;
            }
            else if (!string.IsNullOrEmpty(textAlign) && textAlign.ToLower().Equals("right"))
            {
                paragraph.Alignment = Element.ALIGN_RIGHT;
            }
            else
            {
                paragraph.Alignment = Element.ALIGN_LEFT;
            }

            pdfPCell = new PdfPCell();
            pdfPCell.AddElement(paragraph);
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BorderWidth = 0.5f;
            pdfPCell.UseAscender = true;
            pdfPCell.Padding = 7;
            // pdfPCell.BorderColor = BaseColor.LIGHT_GRAY;
            // pdfPCell.BorderColor = BaseColor.WHITE;

            if (colspan > 1)
            {
                pdfPCell.Colspan = colspan;
            }

            if (rowSpan > 1)
            {
                pdfPCell.Rowspan = rowSpan;
            }

            if (isTopBorderDark)
            {
                pdfPCell.BorderWidthTop = 0.5f;
                pdfPCell.BorderColorTop = BaseColor.BLACK;
            }
            else
            {
                pdfPCell.BorderWidthTop = 0.5f;
                pdfPCell.BorderColorTop = BaseColor.LIGHT_GRAY;
            }

            if (!showTopBorder)
            {
                pdfPCell.BorderWidthTop = 0f;
            }

            if (isLeftBorderDark)
            {
                pdfPCell.BorderWidthLeft = 0.5f;
                pdfPCell.BorderColorLeft = BaseColor.BLACK;
            }
            else
            {
                pdfPCell.BorderWidthLeft = 0.5f;
                pdfPCell.BorderColorLeft = BaseColor.LIGHT_GRAY;
            }

            if (!showLeftBorder)
            {
                pdfPCell.BorderWidthTop = 0f;
            }

            if (isRightBorderDark)
            {
                pdfPCell.BorderWidthRight = 0.5f;
                pdfPCell.BorderColorRight = BaseColor.BLACK;
            }
            else
            {
                pdfPCell.BorderWidthRight = 0.5f;
                pdfPCell.BorderColorRight = BaseColor.LIGHT_GRAY;
            }

            if (!showRightBorder)
            {
                pdfPCell.BorderWidthTop = 0f;
            }

            if (isBottomBorderDark)
            {
                pdfPCell.BorderWidthBottom = 0.5f;
                pdfPCell.BorderColorBottom = BaseColor.BLACK;
            }
            else
            {
                pdfPCell.BorderWidthBottom = 0.5f;
                pdfPCell.BorderColorBottom = BaseColor.LIGHT_GRAY;
            }

            if (!showBottomBorder)
            {
                pdfPCell.BorderWidthTop = 0f;
            }

            if (isTableHeading)
            {
                pdfPCell.BackgroundColor = new BaseColor(218, 218, 218);
            }

            return pdfPCell;
        }

        /// <summary>
        /// Sets cell's value.
        /// </summary>
        /// <param name="Symbolvalue">Symbolvalue to be set.</param>
        /// <param name="value">Value to be set.</param>
        /// <param name="isTopBorderDark">A value indicating whether top border needs to be set darken or not.</param>
        /// <param name="isLeftBorderDark">A value indicating whether left border needs to be set darken or not.</param>
        /// <param name="isRightBorderDark">A value indicating whether right border needs to be set darken or not.</param>
        /// <param name="isBottomBorderDark">A value indicating whether bottom border needs to be set darken or not.</param>
        /// <param name="textAlign">Text alignment inside cell.</param>
        /// <param name="isBold">A value indicating whether text needs to be set bold or not.</param>
        /// <param name="isTableHeading">A value indicating whether value is table heading or not.</param>
        /// <param name="colspan">Column spanning value.</param>
        /// <param name="rowSpan">Row spanning value.</param>
        /// <returns>Returns PDF cell.</returns>
        public static PdfPCell PdfCellValue(string Symbolvalue, string value, bool isTopBorderDark, bool isLeftBorderDark, bool isRightBorderDark, bool isBottomBorderDark, string textAlign = "Left", bool isBold = false, bool isTableHeading = false, int colspan = 1, int rowSpan = 1)
        {
            //Full path to the Unicode Arial file
            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "webdings.ttf");

            //Create a base font object making sure to specify IDENTITY-H
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //Create a specific font object
            Font f = new Font(bf, 12, Font.NORMAL);

            Font font10Bold = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Font font10Normal = FontFactory.GetFont("Arial", 10, Font.NORMAL);

            PdfPCell pdfPCell = new PdfPCell();
            Phrase phrase = new Phrase();

            if (Symbolvalue != null || Symbolvalue != "")
            {
                phrase.Add(new Chunk(Symbolvalue, f));
            }

            if (isBold)
            {
                phrase.Add(new Chunk(value, font10Bold));
            }
            else
            {

                phrase.Add(new Chunk(value, font10Normal));
                //phrase.Add(new Chunk(value, f));
                //phrase.Add(new Chunk("√"));
                //phrase.Add(new Chunk("$"));
            }

            Paragraph paragraph = new Paragraph(phrase);
            paragraph.Leading = 15f;
            if (!string.IsNullOrEmpty(textAlign) && textAlign.ToLower().Equals("center"))
            {
                paragraph.Alignment = Element.ALIGN_CENTER;
            }
            else if (!string.IsNullOrEmpty(textAlign) && textAlign.ToLower().Equals("right"))
            {
                paragraph.Alignment = Element.ALIGN_RIGHT;
            }
            else
            {
                paragraph.Alignment = Element.ALIGN_LEFT;
            }

            pdfPCell = new PdfPCell();
            pdfPCell.AddElement(paragraph);
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BorderWidth = 0.5f;
            pdfPCell.UseAscender = true;
            pdfPCell.Padding = 7;
            pdfPCell.BorderColor = BaseColor.LIGHT_GRAY;
            if (colspan > 1)
            {
                pdfPCell.Colspan = colspan;
            }

            if (rowSpan > 1)
            {
                pdfPCell.Rowspan = rowSpan;
            }

            if (isTopBorderDark)
            {
                pdfPCell.BorderWidthTop = 0.5f;
                pdfPCell.BorderColorTop = BaseColor.BLACK;
            }

            if (isLeftBorderDark)
            {
                pdfPCell.BorderWidthLeft = 0.5f;
                pdfPCell.BorderColorLeft = BaseColor.BLACK;
            }

            if (isRightBorderDark)
            {
                pdfPCell.BorderWidthRight = 0.5f;
                pdfPCell.BorderColorRight = BaseColor.BLACK;
            }

            if (isBottomBorderDark)
            {
                pdfPCell.BorderWidthBottom = 0.5f;
                pdfPCell.BorderColorBottom = BaseColor.BLACK;
            }

            if (isTableHeading)
            {
                pdfPCell.BackgroundColor = new BaseColor(218, 218, 218);
            }

            return pdfPCell;
        }

        /// <summary>
        /// Set pdf cell style.
        /// </summary>
        /// <param name="Value">value.</param>
        /// <param name="link">link.</param>
        /// <param name="TopBorderDark">Top border dark color(boolean).</param>
        /// <param name="LeftBorderDark">Left border dark color(boolean).</param>
        /// <param name="RightBorderDark">Right border dark color(boolean).</param>
        /// <param name="BottomBorderDark">Bottom border dark color(boolean).</param>
        /// <param name="TextAlign">Text alignment.</param>
        /// <param name="IsBold">Bold(boolean).</param>
        /// <param name="IsTableHeading">Table heading(boolean).</param>
        /// <param name="Colspan">Column span.</param>
        /// <param name="RowSpan">Row span.</param>
        /// <returns>Returns pdfpcell</returns>
        public static Anchor PdfTCellValue_Link(string Value, string link, bool TopBorderDark, bool LeftBorderDark, bool RightBorderDark, bool BottomBorderDark, string TextAlign = "Left", bool IsBold = false, bool IsTableHeading = false, int Colspan = 1, int RowSpan = 1)
        {
            Font urlFont = FontFactory.GetFont("Arial", 10, Font.UNDERLINE, BaseColor.BLUE);
            Anchor anchor = new Anchor(Value, urlFont);
            anchor.Reference = link;
            return anchor;
        }

        /// <summary>
        /// Set pdf cell style.
        /// </summary>
        /// <param name="value">value.</param>
        /// <param name="link">link.</param>
        /// <param name="isTopBorderDark">isTopBorderDark.</param>
        /// <param name="isLeftBorderDark">isLeftBorderDark.</param>
        /// <param name="isRightBorderDark">isRightBorderDark.</param>
        /// <param name="isBottomBorderDark">isBottomBorderDark.</param>
        /// <param name="textAlign">textAlign.</param>
        /// <param name="IsBold">IsBold.</param>
        /// <param name="IsTableHeading">IsTableHeading.</param>
        /// <param name="colspan">colspan.</param>
        /// <param name="rowSpan">rowSpan.</param>
        /// <returns></returns>
        public static PdfPCell PdfTCellValue_Link_Link(string value, string link, bool isTopBorderDark, bool isLeftBorderDark, bool isRightBorderDark, bool isBottomBorderDark, string textAlign = "Left", bool IsBold = false, bool IsTableHeading = false, int colspan = 1, int rowSpan = 1)
        {
            Font urlFont = FontFactory.GetFont("Arial", 10, Font.UNDERLINE, BaseColor.BLUE);
            Anchor anchor = new Anchor(value, urlFont);
            anchor.Reference = link;
            //return anchor;

            //Full path to the Unicode Arial file
            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "webdings.ttf");
            //Create a base font object making sure to specify IDENTITY-H
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //Create a specific font object
            Font f = new Font(bf, 12, Font.NORMAL);

            Font font10Bold = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Font font10Normal = FontFactory.GetFont("Arial", 10, Font.NORMAL);


            PdfPCell pdfPCell = new PdfPCell();
            Phrase phrase = new Phrase();
            if (IsBold)
            {
                //phrase.Add(new Chunk(value, font10Bold));
                phrase.Add(anchor);
            }
            else
            {
                // phrase.Add(new Chunk(value, font10Normal));
                phrase.Add(anchor);
            }

            Paragraph paragraph = new Paragraph(phrase);
            paragraph.Leading = 15f;
            if (!string.IsNullOrEmpty(textAlign) && textAlign.ToLower().Equals("center"))
            {
                paragraph.Alignment = Element.ALIGN_CENTER;
            }
            else if (!string.IsNullOrEmpty(textAlign) && textAlign.ToLower().Equals("right"))
            {
                paragraph.Alignment = Element.ALIGN_RIGHT;
            }
            else
            {
                paragraph.Alignment = Element.ALIGN_LEFT;
            }

            pdfPCell = new PdfPCell();
            pdfPCell.AddElement(paragraph);
            pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfPCell.BorderWidth = 0.5f;
            pdfPCell.UseAscender = true;
            pdfPCell.Padding = 7;
            pdfPCell.BorderColor = BaseColor.LIGHT_GRAY;
            if (colspan > 1)
            {
                pdfPCell.Colspan = colspan;
            }

            if (rowSpan > 1)
            {
                pdfPCell.Rowspan = rowSpan;
            }

            if (isTopBorderDark)
            {
                pdfPCell.BorderWidthTop = 0.5f;
                pdfPCell.BorderColorTop = BaseColor.BLACK;
            }

            if (isLeftBorderDark)
            {
                pdfPCell.BorderWidthLeft = 0.5f;
                pdfPCell.BorderColorLeft = BaseColor.BLACK;
            }

            if (isRightBorderDark)
            {
                pdfPCell.BorderWidthRight = 0.5f;
                pdfPCell.BorderColorRight = BaseColor.BLACK;
            }

            if (isBottomBorderDark)
            {
                pdfPCell.BorderWidthBottom = 0.5f;
                pdfPCell.BorderColorBottom = BaseColor.BLACK;
            }

            //if (isTableHeading)
            //{
            //    pdfPCell.BackgroundColor = new BaseColor(218, 218, 218);
            //}

            return pdfPCell;
        }

        /// <summary>
        /// Phrase BNI.
        /// </summary>
        /// <param name="headerValue">Header value.</param>
        /// <param name="fontStyle">Font style.</param>
        /// <param name="size">Font size.</param>
        /// <returns>Returns phrase.</returns>
        public static Phrase PhraseBNI(string headerValue, string fontStyle, int size = 12)
        {
            Font font12Bold = FontFactory.GetFont("Arial", size, Font.BOLD);
            Font font12Normal = FontFactory.GetFont("Arial", size, Font.NORMAL);
            Font font12Italic = FontFactory.GetFont("Arial", size, Font.ITALIC);
            Phrase phrase = new Phrase();

            if (fontStyle.Equals("I"))
            {
                phrase.Add(new Chunk(headerValue, font12Italic));
            }
            else if (fontStyle.Equals("B"))
            {
                phrase.Add(new Chunk(headerValue, font12Bold));
            }
            else
            {
                phrase.Add(new Chunk(headerValue, font12Normal));
            }

            return phrase;
        }

        /// <summary>
        /// Sets table width.
        /// </summary>
        /// <param name="number">Number to be multiplied to set width.</param>
        /// <param name="numberOfFixedColumns">Number of fixed columns.</param>
        /// <param name="isFirstColumnSerialNumber">A value indicating whether first column is serial number or not.</param>
        /// <param name="isSecondColumnSerialNumber">A value indicating whether second column is serial number or not.</param>
        /// <returns>Returns floating point value.</returns>
        public static float[] SetTableWidth(int number, int numberOfFixedColumns, bool isFirstColumnSerialNumber, bool isSecondColumnSerialNumber)
        {
            float[] arr = new float[number];
            for (int i = 0; i < number; i++)
            {
                int val = 20 * number / 5;
                if (i == 0 && isFirstColumnSerialNumber)
                {
                    arr[i] = val;
                    continue;
                }

                val = 20 * number / 5;
                if (i == 1 && isSecondColumnSerialNumber)
                {
                    arr[i] = val;
                    continue;
                }

                val = 60 * number / 5;
                if (i >= 0 && i <= numberOfFixedColumns)
                {
                    arr[i] = val;
                    numberOfFixedColumns = numberOfFixedColumns - 1;
                    continue;
                }

                arr[i] = 100f;
            }

            return arr;
        }

        /// <summary>
        /// Set table width for filter.
        /// </summary>
        /// <param name="number">Number of filters.</param>
        /// <returns>Returns array of type <see cref="float"/>.</returns>
        public static float[] SetTableWidthForFilter(int number)
        {
            float[] arr = new float[number];
            for (int i = 0; i < number; i++)
            {
                if (i == 0)
                {
                    arr[i] = 30f;
                    continue;
                }

                arr[i] = 100f;
            }

            return arr;
        }

        /// <summary>
        /// Export grid data to PDF
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="orientation">orientation of page</param>
        /// <param name="documentTitle">Document title</param>
        /// <param name="reportLogo">report logo</param>
        /// <param name="filters">filters to display</param>
        /// <param name="gridColumns">Grid columns</param>
        /// <param name="data">Grid data</param>
        /// <param name="userContext">User context</param>
        /// <returns>PDF content as byte array</returns>
        public static byte[] ExportGridData(Rectangle pageSize, Orientation orientation, string documentTitle, string reportLogo, Dictionary<string, string> filters, List<GridColumn> gridColumns, List<Dictionary<string, object>> data, UserContext userContext)
        {
            using (MemoryStream output = new MemoryStream())
            {
                //// document
                Document document = new Document(new Rectangle(288f, 144f), 10, 10, 10, 10);
                PdfPCell pdfPCell = new PdfPCell();

                //// Setting Page
                string saveAsFileName = documentTitle + ".pdf";
                MVCProject.Api.Utilities.PdfPage page = new MVCProject.Api.Utilities.PdfPage(documentTitle, reportLogo);
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, output);
                pdfWriter.PageEvent = page;

                document.Open();
                document.SetMargins(20f, 20f, 80f, 40f);

                if (orientation == Orientation.Landscape)
                {
                    document.SetPageSize(pageSize.Rotate());
                }
                else if (orientation == Orientation.Portrait)
                {
                    document.SetPageSize(pageSize);
                }
                else
                {
                    document.SetPageSize(pageSize);
                }

                document.NewPage();

                //// Empty Table
                PdfPTable emptyTable = new PdfPTable(1);
                emptyTable.WidthPercentage = 100;
                pdfPCell = new PdfPCell(new Phrase(" "));
                pdfPCell.BorderWidthTop = 0.0f;
                pdfPCell.BorderWidthBottom = 0.0f;
                pdfPCell.BorderWidthLeft = 0.0f;
                pdfPCell.BorderWidthRight = 0.0f;
                emptyTable.AddCell(pdfPCell);
                // document.Add(emptyTable);

                //// Sub Header
                PdfPTable subGroupHeaderTable = new PdfPTable(2);
                float[] subGroupTableWidths = { 217f, 1000f };
                subGroupHeaderTable.WidthPercentage = 100;
                subGroupHeaderTable.DefaultCell.Border = Rectangle.BOX;
                subGroupHeaderTable.DefaultCell.BorderColor = BaseColor.DARK_GRAY;
                subGroupHeaderTable.SplitLate = false;
                subGroupHeaderTable.SetWidths(subGroupTableWidths);

                // display filters
                bool isFirst = true;
                foreach (string key in filters.Keys)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        subGroupHeaderTable.AddCell(PDFHelper.PdfCellValue(key + " : ", true, true, false, false));
                        subGroupHeaderTable.AddCell(PDFHelper.PdfCellValue(filters[key], true, false, true, false));
                    }
                    else
                    {
                        subGroupHeaderTable.AddCell(PDFHelper.PdfCellValue(key + " : ", false, true, false, false));
                        subGroupHeaderTable.AddCell(PDFHelper.PdfCellValue(filters[key], false, false, true, false));
                    }
                }

                //// Report Generated on
                //string reportDate = DateTime.UtcNow.AddMinutes(userContext.TimeZoneMinutes).ToString(Resource.DateTimeFormat);
                //if (filters.Keys.Count > 0)
                //{
                //    subGroupHeaderTable.AddCell(PDFHelper.PdfCellValue(Resource.ReportGeneratedon + " : ", false, true, false, false));
                //    subGroupHeaderTable.AddCell(PDFHelper.PdfCellValue(reportDate, false, false, true, false));
                //}
                //else
                //{
                //    subGroupHeaderTable.AddCell(PDFHelper.PdfCellValue(Resource.ReportGeneratedon + " : ", true, true, false, false));
                //    subGroupHeaderTable.AddCell(PDFHelper.PdfCellValue(reportDate, true, false, true, false));
                //}

                document.Add(subGroupHeaderTable);

                //// Init Table Design
                float[] dataHeaderWidths = gridColumns.Select(x => x.Width).ToArray();
                PdfPTable table = new PdfPTable(gridColumns.Count);
                table.WidthPercentage = 100;
                table.DefaultCell.Border = Rectangle.BOX;
                table.DefaultCell.BorderColor = BaseColor.DARK_GRAY;
                table.SplitLate = false;
                table.SetWidths(dataHeaderWidths);
                table.HeaderRows = 1;

                //// Set Heading
                isFirst = true;
                foreach (GridColumn column in gridColumns)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        // table.AddCell(PDFHelper.PdfCellValue(column.Title, true, true, true, true, column.HorizontalAlignment.ToString(), true, true));
                        table.AddCell(PDFHelper.PdfCellValue(column.Title, true, true, true, true, "Center", true, true));
                    }
                    else
                    {
                        //table.AddCell(PDFHelper.PdfCellValue(column.Title, true, false, true, true, column.HorizontalAlignment.ToString(), true, true));
                        table.AddCell(PDFHelper.PdfCellValue(column.Title, true, false, true, true, "Center", true, true));
                    }
                }

                // set data rows
                for (int i = 0; i < data.Count; i++)
                {
                    //// Set Employee Data rows

                    int j = 0;
                    foreach (GridColumn column in gridColumns)
                    {
                        string value;
                        if (column.IsSrNo)
                        {
                            value = (i + 1).ToString();
                        }
                        else if (column.DataType == GridColumnDataType.Date && data[i][column.FieldName] != null)
                        {
                            value = Convert.ToDateTime(data[i][column.FieldName]).AddMinutes(userContext.TimeZoneMinutes).ToString(Resource.DateFormat);
                        }
                        else if (column.DataType == GridColumnDataType.Datetime && data[i][column.FieldName] != null)
                        {
                            value = Convert.ToDateTime(data[i][column.FieldName]).AddMinutes(userContext.TimeZoneMinutes).ToString(Resource.DateTimeFormat);
                        }
                        else
                        {
                            value = Convert.ToString(data[i][column.FieldName]);
                        }

                        table.AddCell(PDFHelper.PdfCellValue(value, false, j == 0, j == gridColumns.Count - 1, data.Count == i + 1, column.HorizontalAlignment.ToString()));

                        j++;
                    }
                }

                document.Add(table);
                document.Close();

                return output.ToArray();
            }
        }
    }
}