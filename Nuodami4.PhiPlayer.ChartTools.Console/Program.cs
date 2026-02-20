using Nuodami4.PhiPlayer.ChartTools.Utils;
using System;

namespace Nuodami4.PhiPlayer.ChartTools.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string jsonPath = GetUserInput("Please enter the chart file path: ");
            string outputPath = GetUserInput("Please enter the output file path: ");
            ChartFormat chartFormat = (ChartFormat)(int.Parse(GetUserInput("Supported chart types:\n 1. RePhiedit (170-) \nPlease enter the chart type: ")) - 1);

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
                        throw new NotSupportedException($"???? {chartFormat}");
                }

                System.IO.File.WriteAllText(outputPath, chart2.ToString());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"????: {ex.Message}");
            }
        }

        private static string GetUserInput(string prompt)
        {
            string input;
            do
            {
                System.Console.Write(prompt);
                input = System.Console.ReadLine()?.Trim() ?? string.Empty;
                if (!string.IsNullOrEmpty(input)) break;
            } while (true);
            System.Console.Write("\n");
            return input;
        }

        private enum ChartFormat
        {
            RePhiedit,
            Phigros
        }
    }
}
