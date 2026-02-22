using Nuodami4.PhiPlayer.ChartTools.Utils.Converter;
using System;

namespace Nuodami4.PhiPlayer.ChartTools.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine("Welcome to PhiPlayer Chart Converter\n");
            System.Console.ResetColor();

            string jsonPath = GetValidInput(
                "Enter the path to the source chart file: ",
                path => System.IO.File.Exists(path),
                "File not found. Please enter a valid file path."
            );

            string outputPath = GetValidInput(
                "Enter the path for the output file: ",
                path =>
                {
                    string directory = System.IO.Path.GetDirectoryName(path);
                    if (string.IsNullOrEmpty(directory))
                        return true;
                    if (!System.IO.Directory.Exists(directory))
                        System.IO.Directory.CreateDirectory(directory);
                    return true;
                },
                null
            );

            ChartFormat chartFormat = GetChartFormat();

            try
            {
                string jsonContent = System.IO.File.ReadAllText(jsonPath);
                Core.PhiPlayer.Chart.Root chart2 = null;

                switch (chartFormat)
                {
                    case ChartFormat.RePhiedit:
                        var rePhieditChart = Core.RePhiedit.Chart.Root.FromJson(jsonContent);
                        chart2 = rePhieditChart.ToPhiPlayer();
                        break;
                    case ChartFormat.Phigros:
                        var phiChart = Core.Phigros.Chart.Root.FromJson(jsonContent);
                        chart2 = phiChart.ToPhiPlayer();
                        break;
                    default:
                        throw new NotSupportedException($"Chart type {chartFormat} is not supported.");
                }

                System.IO.File.WriteAllText(outputPath, chart2.ToString());
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"\nConversion completed successfully. Output saved to: {outputPath}");
                System.Console.ResetColor();
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"\nAn error occurred during conversion: {ex.Message}");
                System.Console.ResetColor();
                System.Console.WriteLine("Press any key to exit...");
                System.Console.ReadKey();
                return;
            }

            System.Console.WriteLine("\nPress any key to exit...");
            System.Console.ReadKey();
        }

        private static string GetValidInput(string prompt, Func<string, bool> validator, string errorMessage)
        {
            string input;
            do
            {
                System.Console.Write(prompt);
                input = System.Console.ReadLine()?.Trim().Trim('"') ?? string.Empty;

                if (!string.IsNullOrEmpty(input) && validator(input))
                    break;

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(errorMessage);
                    System.Console.ResetColor();
                }
                else if (string.IsNullOrEmpty(input))
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Input cannot be empty. Please try again.");
                    System.Console.ResetColor();
                }
            } while (true);

            return input;
        }

        private static ChartFormat GetChartFormat()
        {
            System.Console.WriteLine("\nSupported chart types:");
            System.Console.WriteLine("  1. RePhiedit (170-)");
            System.Console.WriteLine("  2. Phigros");

            while (true)
            {
                System.Console.Write("Enter your choice (1 or 2): ");
                string input = System.Console.ReadLine()?.Trim();
                if (int.TryParse(input, out int choice) && (choice == 1 || choice == 2))
                {
                    return (ChartFormat)(choice - 1);
                }
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                System.Console.ResetColor();
            }
        }

        private enum ChartFormat
        {
            RePhiedit,
            Phigros
        }
    }
}