using System.Text;

namespace VisitorChallenge
{
    public class PdfExportVisitor : IVisitor
    {
        private readonly StringBuilder _sb = new StringBuilder();

        public string GetPdf() => _sb.ToString();

        public void Visit(Paragraph paragraph)
        {
            _sb.Append($"PDF_TEXT({paragraph.Text}, {paragraph.FontFamily}, {paragraph.FontSize}) ");
        }

        public void Visit(Image image)
        {
            _sb.Append($"PDF_IMAGE({image.Url}, {image.Width}, {image.Height}) ");
        }

        public void Visit(Table table)
        {
            _sb.Append($"PDF_TABLE({table.Rows}, {table.Columns}, data...) ");
        }
    }
}
