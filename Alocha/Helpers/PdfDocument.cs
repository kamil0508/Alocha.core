using Alocha.WebUi.Models.SowVM;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Helpers
{
    public class PdfDocument
    {
        private object _model { get; set; }
        protected Document _pdfDocument { get; set; }
        protected Font _titleFont { get; set; }
        protected Font _itemFont { get; set; }
        protected Font _boldFont { get; set; }
        protected BaseColor _tableHeaderGrayColor { get; set; }

        public PdfDocument(object model)
        {
            _model = model;
            _titleFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 16);
            _itemFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12);
            _boldFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12, Font.BOLD);
            _tableHeaderGrayColor = WebColors.GetRGBColor("#999999");
        }

        private PdfPTable TitleTable(string title)
        {
            var titleTable = new PdfPTable(1);
            titleTable.SpacingAfter = 10f;
            titleTable.DefaultCell.Border = Rectangle.NO_BORDER;

            var phrase = new Phrase(title, _titleFont);
            var cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.BackgroundColor = _tableHeaderGrayColor;

            titleTable.AddCell(cell);

            return titleTable;
        }

        private PdfPCell GetHeaderTableCell(string text)
        {
            var phrase = new Phrase(text, _boldFont);
            var cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = _tableHeaderGrayColor;
            cell.PaddingBottom = 5f;

            return cell;
        }

        private PdfPCell GetCellWithBorderAlignCenter(string text)
        {
            var phrase = new Phrase(text, _itemFont);
            var cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingBottom = 5f;

            return cell;
        }

        private PdfPCell GetNoteCell()
        {
            var phrase = new Phrase("Notatki:");
            var cell = new PdfPCell(phrase);
            cell.Colspan = 8;
            cell.FixedHeight = 50f;

            return cell;
        }

        public byte[] Generate()
        {
            var model = (IEnumerable<SowVM>)_model;
            using (MemoryStream ms = new MemoryStream())
            {
                _pdfDocument = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(_pdfDocument, ms);

                Rectangle pageSize = writer.PageSize;
                // Open the Document for writing
                _pdfDocument.Open();

                _pdfDocument.Add(TitleTable(string.Format("SPIS LOCH NA DZIEŃ: {0}", DateTime.Today.ToShortDateString())));

                var itemTable = new PdfPTable(8);
                itemTable.WidthPercentage = 100;

                itemTable.AddCell(GetHeaderTableCell("Numer"));
                itemTable.AddCell(GetHeaderTableCell("Status"));
                itemTable.AddCell(GetHeaderTableCell("Data zdarzenia"));
                itemTable.AddCell(GetHeaderTableCell("Data Inseminacji"));
                itemTable.AddCell(GetHeaderTableCell("Data oderwania"));
                itemTable.AddCell(GetHeaderTableCell("Data porodu"));
                itemTable.AddCell(GetHeaderTableCell("Data szczepienia"));
                itemTable.AddCell(GetHeaderTableCell("Szczepienie"));

                foreach (var item in model)
                {
                    var cell = GetCellWithBorderAlignCenter(item.Number);
                    cell.BackgroundColor = _tableHeaderGrayColor;
                    itemTable.AddCell(cell);
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.Status));


                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.DateHappening));

                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.DateInsimination));

                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.DateDetachment));

                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.DateBorn));

                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.VaccineDate));

                    if(item.Status != "Prośna")
                        itemTable.AddCell("");
                    if(item.Status == "Prośna")
                    {
                        if (item.IsVaccinated)
                            itemTable.AddCell(GetCellWithBorderAlignCenter("Tak"));
                        else
                        itemTable.AddCell(GetCellWithBorderAlignCenter("Nie"));
                    }

                    itemTable.AddCell(GetNoteCell());
                }
                _pdfDocument.Add(itemTable);

                _pdfDocument.Close();

                writer.Close();

                byte[] bytes = ms.ToArray();
                ms.Close();

                return bytes;
            }
        }
    }
}
