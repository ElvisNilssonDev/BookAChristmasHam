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
        public void DisplayBusinessMenu(User user)
        {
            bool runningbusiness = true;
            while (runningbusiness)
            {
                Console.Clear();
                // Visar meny för företagsanvändare
                AnsiConsole.Write(
                new FigletText("ChristmasHam!")
                .Centered()
                .Color(Color.Red));
                AnsiConsole.Write(
                new FigletText(user.CompanyName ?? "Business User")
                .Centered()
                .Color(Color.Green));
                var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]Select an option:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Show all orders",
                    "Delete an order",
                    "Filter orders",
                    "Update an order",
                    "Logout"
                }));
                AnsiConsole.MarkupLine($"You selected: [yellow]{choice}[/]");
                switch (choice)
                {
                    case "Show all orders":
                    //Visa alla ordrar som är kopplade till företagets CompanyName
                        AnsiConsole.MarkupLine("[blue]ShowAllHam()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "Delete an order":
                    //Radera en order via dess bookingId
                        AnsiConsole.MarkupLine("[blue]DeleteOrder()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "Filter orders":
                        AnsiConsole.MarkupLine("[blue]FilterOrder()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "Update an order":
                        AnsiConsole.MarkupLine("[blue]UppdateOrder()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "Logout":
                        runningbusiness = false; // Exit the loop
                        break;
                }

            }
        }
    }
}

