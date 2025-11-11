using BookAChristmasHam.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAChristmasHam.UI.Menu.LoggedInMenu
{
    public class PrivateMenu
    {
        public void ShowPriv(User user)
        {
            bool runningpriv = true;

            while (runningpriv)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                new FigletText("ChristmasHam!")
                .Centered()
                .Color(Color.Red));
                var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]Select an option:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Order ChristmasHam",
                    "See Your Order",
                    "Logout"
                }));

                AnsiConsole.MarkupLine($"You selected: [yellow]{choice}[/]");
                switch (choice)
                {
                    case "Order ChristmasHam":
                        // Visa mina bokningar
                        AnsiConsole.MarkupLine("[blue]Visa mina bokningar - Funktionalitet kommer snart![/]");
                        AnsiConsole.MarkupLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        break;
                    case "See Your Order":
                        // Ta bort en bokning
                        AnsiConsole.MarkupLine("[blue]Ta bort en bokning - Funktionalitet kommer snart![/]");
                        AnsiConsole.MarkupLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        break;
                    case "Logout":
                        // Logga ut
                        runningpriv = false;
                        AnsiConsole.MarkupLine("[green]You have logged out! See you soon!.[/]");
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]Unauthorized choice!.[/]");
                        Console.ReadKey();
                        break;
                }
            }
        }
               
    }
}








