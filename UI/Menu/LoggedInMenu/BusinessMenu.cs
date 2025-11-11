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
                var choice = AnsiConsole.Ask<int>(":");
                switch (choice)
                {
                    case 1:
                        // Visa mina bokningar
                        AnsiConsole.MarkupLine("[blue]Show all ham[/]");
                        AnsiConsole.MarkupLine("Tryck på valfri tangent för att fortsätta...");
                        Console.ReadKey();
                        break;
                    case 2:
                        runningbusiness = false; // Exit the loop
                        break;
                }

            }
        }
    }
}

