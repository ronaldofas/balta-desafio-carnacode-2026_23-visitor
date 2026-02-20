using System;

namespace VisitorChallenge
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE DOCUMENTOS (EXECUTOR) ===");
            Console.WriteLine("1. Executar Sistema Legado");
            Console.WriteLine("2. Executar Novo Sistema (Visitor)");
            Console.WriteLine("3. Executar Ambos para Comparação");
            Console.Write("\nEscolha uma opção: ");

            var choice = "1"; // Valor padrão para teste inicial

            // Console.ReadLine() não funciona bem em execuções automatizadas sem intervenção
            // No ambiente real, o usuário escolheria. Para este passo, vamos apenas mostrar que funciona.

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
                    Console.WriteLine("\n" + new string('=', 40) + "\n");
                    RunNewSystem();
                    break;
                default:
                    RunLegacy();
                    break;
            }
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
            Console.WriteLine("Em desenvolvimento...");
        }
    }
}
