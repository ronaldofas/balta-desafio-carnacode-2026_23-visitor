using System;

namespace VisitorChallenge
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("====================================================================");
            Console.WriteLine("      SISTEMA DE DOCUMENTOS - COMPARAÇÃO: LEGADO VS VISITOR");
            Console.WriteLine("====================================================================\n");

            var choice = "3"; // Default para execução comparativa conforme solicitado
            if (args.Length > 0) choice = args[0];

            switch (choice)
            {
                case "1":
                    RunLegacy();
                    break;
                case "2":
                    RunNewSystem();
                    break;
                case "3":
                    RunLegacy();
                    Console.WriteLine("\n" + new string('*', 68));
                    Console.WriteLine("      MUDANDO PARA O NOVO SISTEMA (DESIGN PATTERN VISITOR)");
                    Console.WriteLine(new string('*', 68) + "\n");
                    RunNewSystem();
                    break;
                default:
                    goto case "3";
            }
            
            Console.WriteLine("\n====================================================================");
            Console.WriteLine("                FIM DA COMPARAÇÃO DE SISTEMAS");
            Console.WriteLine("====================================================================");
        }

        static void RunLegacy()
        {
            Console.WriteLine("--- Iniciando Legado ---\n");
            // Nota: Se a classe Program do Challenge.cs não for pública, 
            // precisaremos de um pequeno ajuste nela.
            DesignPatternChallenge.Program.Main(new string[0]);
        }

        static void RunNewSystem()
        {
            Console.WriteLine("--- Iniciando Novo Sistema (Visitor) ---\n");
            
            var doc = new Document("Relatório Anual (Novo)");
            
            doc.AddElement(new Paragraph("Este é o relatório anual da empresa (Novo)."));
            doc.AddElement(new Image("grafico_novo.png", 800, 600));
            doc.AddElement(new Table(2, 2));

            Console.WriteLine($"Documento Novo: {doc.Title}");
            
            // Usando o Visitor para exportação HTML
            var htmlVisitor = new HtmlExportVisitor();
            doc.Accept(htmlVisitor);
            
            // Usando o Visitor para contagem de palavras
            var wordVisitor = new WordCountVisitor();
            doc.Accept(wordVisitor);
            Console.WriteLine($"Total de palavras (Novo): {wordVisitor.TotalWords}");
            
            // Usando o Visitor para validação
            var validationVisitor = new ValidationVisitor();
            Console.WriteLine($"Documento válido (Novo): {validationVisitor.IsValid}");
            
            // Usando o Visitor para tempo de leitura
            var readingVisitor = new ReadingTimeVisitor();
            doc.Accept(readingVisitor);
            Console.WriteLine($"Tempo de leitura (Novo): {readingVisitor.ReadingTimeMinutes} min");
            
            Console.WriteLine("\n=== Novo: Exportação PDF (amostra) ===");
            var pdfVisitor = new PdfExportVisitor();
            doc.Accept(pdfVisitor);
            var pdf = pdfVisitor.GetPdf();
            Console.WriteLine(pdf.Substring(0, Math.Min(150, pdf.Length)) + "...");
            
            Console.WriteLine("\n=== Novo: Exportação HTML (amostra) ===");
            var html = htmlVisitor.GetHtml();
            Console.WriteLine(html.Substring(0, Math.Min(200, html.Length)) + "...");
        }
    }
}
