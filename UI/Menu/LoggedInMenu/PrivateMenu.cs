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
                Console.Clear();
                AnsiConsole.MarkupLine("[yellow]--- Book A christmasHam ---[/]");
                AnsiConsole.MarkupLine("[green]1.[/] Book a Ham");
                AnsiConsole.MarkupLine("[green]2.[/] Show My Order");
                AnsiConsole.MarkupLine("[green]0.[/] Loggout");
                AnsiConsole.MarkupLine("---------------------");
                var choice = AnsiConsole.Ask<int>(":");
                switch (choice)
                {
                    case 1:
                        // Visa mina bokningar
                        AnsiConsole.MarkupLine("[blue]Visa mina bokningar - Funktionalitet kommer snart![/]");
                        AnsiConsole.MarkupLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        break;
                    case 2:
                        // Ta bort en bokning
                        AnsiConsole.MarkupLine("[blue]Ta bort en bokning - Funktionalitet kommer snart![/]");
                        AnsiConsole.MarkupLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        break;
                    case 0:
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








