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

                      // Visa mina bokningar
                        AnsiConsole.MarkupLine("[blue]Visa mina bokningar - Funktionalitet kommer snart![/]");
                        AnsiConsole.MarkupLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        break;


                        // Ta bort en bokning
                        AnsiConsole.MarkupLine("[blue]Ta bort en bokning - Funktionalitet kommer snart![/]");
                        AnsiConsole.MarkupLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        break;

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








