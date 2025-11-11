using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAChristmasHam.Models;
using Spectre.Console;

//ShowAllHam()
//DeleteOrder()
//FilterOrder()
//UppdateOrder()


namespace BookAChristmasHam.UI.Menu.LoggedInMenu
{
    public class BusinessMenu
    {
        public void DisplayBusinessMenu(Business businessUser)
        {
            bool runningbusiness = true;
            while (runningbusiness)
            {
                Console.Clear();
                // Visar meny för företagsanvändare
                AnsiConsole.MarkupLine($"[bold]Welcome, {businessUser.CompanyName}![/]");
                AnsiConsole.MarkupLine("[yellow]--- Business menu ---[/]");
                AnsiConsole.MarkupLine("[green]1.[/] Show all orders");
                AnsiConsole.MarkupLine("[green]2.[/] Delete an order");
                AnsiConsole.MarkupLine("[green]2.[/] Filter orders");
                AnsiConsole.MarkupLine("[green]2.[/] Update an order");
                AnsiConsole.MarkupLine("[green]0.[/] Log out");
                AnsiConsole.MarkupLine("---------------------");
                var choice = AnsiConsole.Ask<int>(":");
                switch (choice)
                {
                    case 1:
                        AnsiConsole.MarkupLine("[blue]ShowAllHam()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case 2:
                        AnsiConsole.MarkupLine("[blue]DeleteOrder()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case 3:
                        AnsiConsole.MarkupLine("[blue]FilterOrder()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case 4:
                        AnsiConsole.MarkupLine("[blue]UppdateOrder()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case 0:
                        runningbusiness = false; // Exit the loop
                        break;
                }

            }
        }
    }
}

