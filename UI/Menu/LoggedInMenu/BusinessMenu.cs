using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAChristmasHam.Models;
using BookAChristmasHam.Managers;
using Spectre.Console;

//ShowAllHam()
//DeleteOrder()
//FilterOrder()
//UppdateOrder()


namespace BookAChristmasHam.UI.Menu.LoggedInMenu
{
    public class BusinessMenu
    {
        private readonly BusinessManager _businessManager;

        public BusinessMenu(BusinessManager businessManager)
        {
            _businessManager = businessManager;
        }

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
                    "Show all your orders",
                    "Delete an order",
                    "Filter orders",
                    "Update an order",
                    "Logout"
                }));
                switch (choice)
                {
                    case "Show all your orders":
                        //Visa alla ordrar som är kopplade till företagets CompanyName
                        _businessManager.ShowMyOrders(user);
                        break;
                    case "Delete an order":
                        //Radera en order via dess bookingId
                        _businessManager.DeleteOrder(user);
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
                        //Loggar ut användaren och återvänder till huvudmenyn
                        runningbusiness = false;
                        break;
                }

            }
        }
    }
}

