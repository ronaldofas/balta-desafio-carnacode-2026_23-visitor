// DESAFIO: Sistema de Relatórios para Estrutura de Documentos
// PROBLEMA: Um sistema de documentos tem diferentes tipos de elementos (Parágrafo, Imagem, Tabela)
// e precisa realizar múltiplas operações (exportar HTML, PDF, contar palavras, validar). O código
// atual adiciona cada operação como método em cada classe, violando Open/Closed Principle

using System;
using System.Collections.Generic;

namespace DesignPatternChallenge
{
    // Contexto: Sistema de documentos onde novos tipos de operações precisam ser
    // adicionados frequentemente, mas tipos de elementos são relativamente estáveis
    
    public abstract class DocumentElement
    {
        public abstract void Render();
    }

    public class Paragraph : DocumentElement
    {
        public string Text { get; set; }
        public string FontFamily { get; set; }
        public int FontSize { get; set; }

        public Paragraph(string text)
        {
            Text = text;
            FontFamily = "Arial";
            FontSize = 12;
        }

        public override void Render()
        {
            Console.WriteLine($"[Parágrafo] {Text}");
        }

        // Problema: Cada nova operação adiciona método aqui
        public string ExportToHtml()
        {
            return $"<p style='font-family:{FontFamily};font-size:{FontSize}px'>{Text}</p>";
        }

        public string ExportToPdf()
        {
            return $"PDF_TEXT({Text}, {FontFamily}, {FontSize})";
        }

        public int CountWords()
        {
            return Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Text) && Text.Length < 1000;
        }

        public int CalculateReadingTime()
        {
            int words = CountWords();
            return words / 200; // 200 palavras por minuto
        }

        // Adicionar nova operação = modificar todas as classes!
    }

    public class Image : DocumentElement
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Alt { get; set; }

        public Image(string url, int width, int height)
        {
            Url = url;
            Width = width;
            Height = height;
            Alt = "";
        }

        public override void Render()
        {
            Console.WriteLine($"[Imagem] {Url} ({Width}x{Height})");
        }

        // Problema: Mesmas operações, mas implementações diferentes
        public string ExportToHtml()
        {
            return $"<img src='{Url}' width='{Width}' height='{Height}' alt='{Alt}' />";
        }

        public string ExportToPdf()
        {
            return $"PDF_IMAGE({Url}, {Width}, {Height})";
        }

        public int CountWords()
        {
            return 0; // Imagens não têm palavras
        }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Url) && Width > 0 && Height > 0;
        }

        public int CalculateReadingTime()
        {
            return 0; // Imagens não afetam tempo de leitura
        }

        // Problema: Classe cresce horizontalmente com cada nova operação
    }

    public class Table : DocumentElement
    {
        public List<List<string>> Cells { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public Table(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Cells = new List<List<string>>();
            
            for (int i = 0; i < rows; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < columns; j++)
                {
                    row.Add($"Cell {i},{j}");
                }
                Cells.Add(row);
            }
        }

        public override void Render()
        {
            Console.WriteLine($"[Tabela] {Rows}x{Columns}");
        }

        public string ExportToHtml()
        {
            var html = "<table>";
            foreach (var row in Cells)
            {
                html += "<tr>";
                foreach (var cell in row)
                {
                    html += $"<td>{cell}</td>";
                }
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        public string ExportToPdf()
        {
            return $"PDF_TABLE({Rows}, {Columns}, data...)";
        }

        public int CountWords()
        {
            int total = 0;
            foreach (var row in Cells)
            {
                foreach (var cell in row)
                {
                    total += cell.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }
            return total;
        }

        public bool Validate()
        {
            return Rows > 0 && Columns > 0 && Cells.Count == Rows;
        }

        public int CalculateReadingTime()
        {
            int words = CountWords();
            return words / 200;
        }

        // Problema: Adicionar operação = modificar Paragraph, Image E Table
    }

    public class Document
    {
        public string Title { get; set; }
        public List<DocumentElement> Elements { get; set; }

        public Document(string title)
        {
            Title = title;
            Elements = new List<DocumentElement>();
        }

        public void AddElement(DocumentElement element)
        {
            Elements.Add(element);
        }

        // Problema: Cliente precisa saber tipo específico para chamar operação
        public string ExportToHtml()
        {
            var html = $"<html><head><title>{Title}</title></head><body>";
            
            foreach (var element in Elements)
            {
                // Problema: Type checking + casting
                if (element is Paragraph p)
                {
                    html += p.ExportToHtml();
                }
                else if (element is Image img)
                {
                    html += img.ExportToHtml();
                }
                else if (element is Table tbl)
                {
                    html += tbl.ExportToHtml();
                }
                // Adicionar novo tipo = modificar este método!
            }
            
            html += "</body></html>";
            return html;
        }

        public string ExportToPdf()
        {
            var pdf = $"PDF_DOCUMENT({Title})";
            
            foreach (var element in Elements)
            {
                if (element is Paragraph p)
                {
                    pdf += " " + p.ExportToPdf();
                }
                else if (element is Image img)
                {
                    pdf += " " + img.ExportToPdf();
                }
                else if (element is Table tbl)
                {
                    pdf += " " + tbl.ExportToPdf();
                }
            }
            
            return pdf;
        }

        public int CountTotalWords()
        {
            int total = 0;
            
            foreach (var element in Elements)
            {
                if (element is Paragraph p)
                {
                    total += p.CountWords();
                }
                else if (element is Image img)
                {
                    total += img.CountWords();
                }
                else if (element is Table tbl)
                {
                    total += tbl.CountWords();
                }
            }
            
            return total;
        }

        public bool ValidateAll()
        {
            foreach (var element in Elements)
            {
                if (element is Paragraph p && !p.Validate()) return false;
                if (element is Image img && !img.Validate()) return false;
                if (element is Table tbl && !tbl.Validate()) return false;
            }
            return true;
        }

        // Problema: Cada nova operação requer modificar Document também!
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Documentos ===\n");

            var doc = new Document("Relatório Anual");
            
            doc.AddElement(new Paragraph("Este é o relatório anual da empresa."));
            doc.AddElement(new Image("grafico.png", 800, 600));
            doc.AddElement(new Paragraph("Abaixo os resultados financeiros do ano:"));
            doc.AddElement(new Table(3, 4));
            doc.AddElement(new Paragraph("Conclusão do relatório com recomendações."));

            Console.WriteLine($"Documento: {doc.Title}");
            Console.WriteLine($"Elementos: {doc.Elements.Count}\n");

            Console.WriteLine("=== Operações no Documento ===");
            
            var totalWords = doc.CountTotalWords();
            Console.WriteLine($"Total de palavras: {totalWords}");
            
            var isValid = doc.ValidateAll();
            Console.WriteLine($"Documento válido: {isValid}");

            Console.WriteLine("\n=== Exportação HTML (amostra) ===");
            var html = doc.ExportToHtml();
            Console.WriteLine(html.Substring(0, Math.Min(200, html.Length)) + "...");

            Console.WriteLine("\n=== Exportação PDF (amostra) ===");
            var pdf = doc.ExportToPdf();
            Console.WriteLine(pdf.Substring(0, Math.Min(150, pdf.Length)) + "...");

            Console.WriteLine("\n=== PROBLEMAS ===");
            Console.WriteLine("✗ Cada nova operação requer modificar TODAS as classes de elementos");
            Console.WriteLine("✗ Operações relacionadas espalhadas em múltiplas classes");
            Console.WriteLine("✗ Type checking (is/as) repetido em cada operação do Document");
            Console.WriteLine("✗ Adicionar novo elemento = modificar todas operações existentes");
            Console.WriteLine("✗ Difícil manter coesão das operações");
            Console.WriteLine("✗ Viola Open/Closed Principle");
            Console.WriteLine("✗ Classes crescem horizontalmente (muitos métodos)");

            Console.WriteLine("\n=== Matriz de Complexidade ===");
            Console.WriteLine("3 tipos de elementos × 5 operações = 15 métodos");
            Console.WriteLine("Adicionar 1 operação = modificar 3 classes + Document");
            Console.WriteLine("Adicionar 1 elemento = implementar 5 operações + modificar Document");
            Console.WriteLine();
            Console.WriteLine("Operações futuras desejadas:");
            Console.WriteLine("• Exportar para Markdown");
            Console.WriteLine("• Exportar para LaTeX");
            Console.WriteLine("• Análise de SEO");
            Console.WriteLine("• Verificação de acessibilidade");
            Console.WriteLine("• Estatísticas de legibilidade");
            Console.WriteLine("• Extração de metadados");
            Console.WriteLine("→ Cada uma requer modificar Paragraph, Image, Table e Document!");

            Console.WriteLine("\n=== Requisitos Não Atendidos ===");
            Console.WriteLine("• Adicionar operações sem modificar elementos");
            Console.WriteLine("• Agrupar operações relacionadas em um lugar");
            Console.WriteLine("• Evitar type checking e casting");
            Console.WriteLine("• Permitir operações que trabalham com múltiplos tipos");

            // Perguntas para reflexão:
            // - Como adicionar operações sem modificar classes de elementos?
            // - Como agrupar operação relacionada em um único lugar?
            // - Como evitar type checking ao processar hierarquia?
            // - Como separar algoritmos de estrutura de dados?
        }
    }
}
