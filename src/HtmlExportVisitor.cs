using System.Text;

namespace VisitorChallenge
{
    public class HtmlExportVisitor : IVisitor
    {
        private readonly StringBuilder _sb = new StringBuilder();

        public string GetHtml() => _sb.ToString();

        public void Visit(Paragraph paragraph)
        {
            _sb.Append($"<p style='font-family:{paragraph.FontFamily};font-size:{paragraph.FontSize}px'>{paragraph.Text}</p>");
        }

        public void Visit(Image image)
        {
            _sb.Append($"<img src='{image.Url}' width='{image.Width}' height='{image.Height}' alt='{image.Alt}' />");
        }

        public void Visit(Table table)
        {
            _sb.Append("<table>");
            foreach (var row in table.Cells)
            {
                _sb.Append("<tr>");
                foreach (var cell in row)
                {
                    _sb.Append($"<td>{cell}</td>");
                }
                _sb.Append("</tr>");
            }
            _sb.Append("</table>");
        }
    }
}
