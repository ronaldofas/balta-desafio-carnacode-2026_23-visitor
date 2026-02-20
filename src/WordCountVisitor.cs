using System;

namespace VisitorChallenge
{
    public class WordCountVisitor : IVisitor
    {
        public int TotalWords { get; private set; }

        public void Visit(Paragraph paragraph)
        {
            TotalWords += paragraph.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public void Visit(Image image)
        {
            // Imagens não têm palavras
        }

        public void Visit(Table table)
        {
            foreach (var row in table.Cells)
            {
                foreach (var cell in row)
                {
                    TotalWords += cell.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }
        }
    }
}
