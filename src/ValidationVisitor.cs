namespace VisitorChallenge
{
    public class ValidationVisitor : IVisitor
    {
        public bool IsValid { get; private set; } = true;

        public void Visit(Paragraph paragraph)
        {
            if (string.IsNullOrEmpty(paragraph.Text) || paragraph.Text.Length >= 1000)
            {
                IsValid = false;
            }
        }

        public void Visit(Image image)
        {
            if (string.IsNullOrEmpty(image.Url) || image.Width <= 0 || image.Height <= 0)
            {
                IsValid = false;
            }
        }

        public void Visit(Table table)
        {
            if (table.Rows <= 0 || table.Columns <= 0 || table.Cells.Count != table.Rows)
            {
                IsValid = false;
            }
        }
    }
}
