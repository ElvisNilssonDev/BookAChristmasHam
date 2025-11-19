using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using Spectre.Console;

namespace BookAChristmasHam.UI.Printer
{
    public class HamTablePrinter
    {
        private readonly string _hamsFilePath;

        // Constructor
        public HamTablePrinter()
        {
            // Resolve the full path using PathService
            _hamsFilePath = PathService.GetDataFilePath("hams.json");
        }

        /// <summary>
        /// Prints all orders for a specific customer.
        /// </summary>
        public void PrintForCustomer(User customer)
        {
            var allHams = JsonService.ReadFromJsonFile<ChristmasHam>(_hamsFilePath) ?? new List<ChristmasHam>();
            var customerOrders = allHams
                .Where(h => h.Data != null && h.CustomerId == customer?.Id)
                .ToList();


            AnsiConsole.MarkupLine($"[bold yellow]🧑‍🎄 Your Orders, {customer.Username}[/]");
            PrintHamTable(customerOrders);
        }

        /// <summary>
        /// Prints all orders for a specific business.
        /// </summary>
        public void PrintForBusiness(Business business)
        {
            var allHams = JsonService.ReadFromJsonFile<ChristmasHam>(_hamsFilePath);

            var businessOrders = allHams
                .Where(h => h.BusinessId == business.Id)
                .ToList();

            AnsiConsole.MarkupLine($"[bold green]📦 Orders for {business.CompanyName}[/]");
            PrintHamTable(businessOrders);
        }

        /// <summary>
        /// Internal table printer.
        /// </summary>
        private void PrintHamTable(List<ChristmasHam> hams)
        {
            if (hams.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No orders found.[/]");
                return;
            }

            var table = new Table()
                .Border(TableBorder.Rounded)
                .ShowRowSeparators()
                .Title("[underline]Christmas Ham Orders[/]");

            table.AddColumn("Ham ID");
            table.AddColumn("Business ID");
            table.AddColumn("Weight");
            table.AddColumn("Brined");
            table.AddColumn("Bones");
            table.AddColumn("Cooked");
            table.AddColumn("Week");

            foreach (var ham in hams)
            {
                var d = ham.Data;
                table.AddRow(
                    ham.Id.ToString(),
                    ham.BusinessId.ToString(),
                    WeightLabel(d.WeightInterval),
                    d.Brined ? "Yes" : "No",
                    d.HasBones ? "With Bones" : "Boneless",
                    d.IsCooked ? "Cooked" : "Raw",
                    d.Week.ToString()
                );
            }

            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Converts WeightInterval enum to user-friendly label.
        /// </summary>
        private static string WeightLabel(WeightInterval wi)
        {
            return wi switch
            {
                WeightInterval.Kg3To4 => "3–4 kg",
                WeightInterval.Kg4To5 => "4–5 kg",
                WeightInterval.Kg5To6 => "5–6 kg",
                _ => "Unknown"
            };
        }
    }
}




