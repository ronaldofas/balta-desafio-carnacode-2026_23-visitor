using System;

namespace VisitorChallenge
{
    public class ReadingTimeVisitor : IVisitor
    {
        private int _totalWords;
        public int ReadingTimeMinutes => _totalWords / 200;

        public void Visit(Paragraph paragraph)
        {
            _totalWords += paragraph.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public void Visit(Image image)
        {
            // Imagens não afetam tempo de leitura
        }

        public void Visit(Table table)
        {
            int tableWords = 0;
            foreach (var row in table.Cells)
            {
                foreach (var cell in row)
                {
                    tableWords += cell.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }
            _totalWords += tableWords;
        }
    }
}
