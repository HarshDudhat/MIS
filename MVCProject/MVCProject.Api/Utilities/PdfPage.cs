// -----------------------------------------------------------------------
// <copyright file="PdfPage.cs" company="ASK-EHS">
// All copy rights reserved @ASK-EHS.
// </copyright>
// -----------------------------------------------------------------------

namespace MVCProject.Api.Utilities
{
    #region Namespaces

    using System;
    using System.Web;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using MVCProject.Api.Models;
    using MVCProject.Common.Resources;

    #endregion

    /// <summary>
    /// Help to generate footer having page number
    /// </summary>
    public class PdfPage : PdfPageEventHelper
    {
        /// <summary>
        /// Holds header.
        /// </summary>
        private string header;

        /// <summary>
        /// This is the base font used for the header or/and footer.
        /// </summary>
        private BaseFont baseFont = null;

        /// <summary>
        /// This is the content byte object of the writer. 
        /// </summary>
        private PdfContentByte contentByte;

        /// <summary>
        /// Date and time format.
        /// </summary>
        private string dateTimeFormat = string.Empty;

        /// <summary>
        /// Header template
        /// </summary>
        private PdfTemplate headerTemplate;

        /// <summary>
        /// Footer template.
        /// </summary>
        private PdfTemplate footerTemplate;

        /// <summary>
        /// Holds time for print. This keeps track of the creation time
        /// </summary>
        private DateTime printTime = DateTime.UtcNow;

        /// <summary>
        /// Holds report header.
        /// </summary>
        private string reportHeader = string.Empty;

        /// <summary>
        /// Holds report header logo.
        /// </summary>
        private string reportLogo = string.Empty;

        /// <summary>
        /// Holds time zone minutes.
        /// </summary>
        private int timeZoneMinutes = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfPage"/> class.
        /// </summary>
        /// <param name="reportHeader">Report header.</param>
        /// <param name="reportLogo">Report header logo.</param>
        public PdfPage(string reportHeader, string reportLogo)
        {
            UserContext userContext = SecurityUtility.ExtractUserContext(HttpContext.Current.Request);
            this.reportHeader = reportHeader;
            this.dateTimeFormat = Resource.DateTimeFormat;
            this.timeZoneMinutes = userContext.TimeZoneMinutes;

            if (System.IO.File.Exists(reportLogo))
                this.reportLogo = reportLogo;
            else
                this.reportLogo = this.GetReportLogo(reportLogo, userContext.CompanyDB);
        }

        /// <summary>
        /// Gets or sets header value.
        /// </summary>
        public string Header
        {
            get { return this.header; }
            set { this.header = value; }
        }

        /// <summary>
        /// On close event of PDF document.
        /// </summary>
        /// <param name="writer">PDF writer object.</param>
        /// <param name="document">Document object.</param>
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            this.headerTemplate.BeginText();
            this.headerTemplate.SetFontAndSize(this.baseFont, 9);
            this.footerTemplate.SetColorFill(BaseColor.BLACK);
            this.headerTemplate.SetTextMatrix(0, 0);
            this.headerTemplate.ShowText((writer.PageNumber).ToString());
            this.headerTemplate.EndText();
        }

        /// <summary>
        /// On end page event of PDF document.
        /// </summary>
        /// <param name="writer">PDF writer object.</param>
        /// <param name="document">Document object.</param>
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            Rectangle pageSize = document.PageSize;
            Rectangle a4Portrait = PageSize.A4;
            Rectangle a4Landscape = PageSize.A4.Rotate();
            float[] mainTableWidths;
            float top, right;

            if (pageSize.Width == a4Portrait.Width && pageSize.Height == a4Portrait.Height)
            {
                mainTableWidths = new float[] { 20f, 55f, 25f };
                right = 156;
                top = 65;
            }
            else //if (pageSize.Width == a4Landscape.Width && pageSize.Height == a4Landscape.Height)
            {
                mainTableWidths = new float[] { 110f, 475f, 135f };
                right = 168;
                top = 65;
            }

            PdfPTable mainTable = new PdfPTable(3);
            mainTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

            mainTable.DefaultCell.Border = Rectangle.BOX;
            mainTable.DefaultCell.BorderColor = BaseColor.DARK_GRAY;
            mainTable.SplitLate = false;
            mainTable.SetWidths(mainTableWidths);

            // Main header & logo.            
            Image image = null;
            try
            {
                image = Image.GetInstance(this.reportLogo);
            }
            catch
            {
                image = Image.GetInstance(HttpContext.Current.Server.MapPath("~/Content/images/") + "company-logo.png");
            }

            image.ScalePercent(10);
            PdfPCell pdfPCell = new PdfPCell();
           // pdfPCell.BorderWidthRight = 0.0f;
            pdfPCell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
            PDFHelper.AddImageInCell(pdfPCell, image, 90f, 30f, Rectangle.ALIGN_CENTER);
            mainTable.AddCell(pdfPCell);

            // Main report title.
            pdfPCell = new PdfPCell(PDFHelper.PhraseBNI(this.reportHeader, "B", 12));
            pdfPCell.SetLeading(1.2f,1.2f);
            pdfPCell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            pdfPCell.PaddingTop = 0f;
            pdfPCell.PaddingBottom = 5f;
            pdfPCell.PaddingLeft = 50f;
            mainTable.AddCell(pdfPCell);

            // Date and page number.
            pdfPCell = new PdfPCell(PDFHelper.PhraseBNI(Resource.ReportDate + " : " + DateTime.UtcNow.AddMinutes(this.timeZoneMinutes).ToString(this.dateTimeFormat), "N", 9));
            pdfPCell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            pdfPCell.VerticalAlignment = Rectangle.ALIGN_TOP;
           // pdfPCell.BorderWidthLeft = 0.0f;
            mainTable.AddCell(pdfPCell);
            mainTable.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 36, writer.DirectContent);

            string text = Resource.PageNo + " : " + writer.PageNumber + " " + Resource.Of + "";
            this.contentByte.BeginText();
            this.contentByte.SetFontAndSize(this.baseFont, 9);
            this.contentByte.SetColorFill(BaseColor.BLACK);
            this.contentByte.SetTextMatrix(document.PageSize.GetRight(right), document.PageSize.GetTop(top));
            this.contentByte.ShowText(text);
            this.contentByte.EndText();
            float len = this.baseFont.GetWidthPoint(text, 10);
            this.contentByte.AddTemplate(this.headerTemplate, document.PageSize.GetRight(right) + len, document.PageSize.GetTop(top));

            text = Resource.PDFFooterText;
            this.contentByte.BeginText();
            this.contentByte.SetFontAndSize(this.baseFont, 9);
            this.contentByte.SetColorFill(BaseColor.BLACK);
            this.contentByte.SetTextMatrix(document.PageSize.GetLeft(20), document.PageSize.GetBottom(10));
            this.contentByte.ShowText(text);
            this.contentByte.EndText();
            len = this.baseFont.GetWidthPoint(text, 10);
            this.contentByte.AddTemplate(this.footerTemplate, document.PageSize.GetLeft(20) + len, document.PageSize.GetBottom(10));
        }

        /// <summary>
        /// On open document event. 
        /// </summary>
        /// <param name="writer">PDF writer object.</param>
        /// <param name="document">Document object.</param>
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                this.printTime = DateTime.UtcNow;
                this.baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                this.contentByte = writer.DirectContent;
                this.headerTemplate = this.contentByte.CreateTemplate(100, 100);
                this.footerTemplate = this.contentByte.CreateTemplate(50, 50);
            }
            catch (DocumentException)
            {
                // handle exception here
            }
            catch (System.IO.IOException)
            {
                // handle exception here
            }
        }

        /// <summary>
        /// get report logo
        /// </summary>
        /// <param name="reportLogo">Report Logo</param>
        /// <param name="companyDB">Company Database Name</param>
        /// <returns>Report logo</returns>
        private string GetReportLogo(string reportLogo, string companyDB)
        {
            string attachmentPath = AppUtility.GetDirectoryPath(DirectoryPath.Attachment_ReportLogo, companyDB);
            string reportLogoPath = string.Format("{0}{1}", attachmentPath, reportLogo);
            return string.IsNullOrWhiteSpace(reportLogo) ? HttpContext.Current.Server.MapPath("~/Content/images/") + "company-logo.png" : reportLogoPath;
        }
    }
}