using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BookAChristmasHam.Managers;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using Spectre.Console;

namespace BookAChristmasHam.UI.Menu.LoggedInMenu
{
    public class PrivateMenu
    {

        private readonly UserManager _userManager;

        public PrivateMenu() { }

        public PrivateMenu(StorageService storage)
        {
            _userManager = new UserManager(storage);
        }


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
                        var hamData = AskForHamPreferences();
                        _userManager.PlaceOrder(user, hamData);

                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "See Your Order":
                        _userManager.SeeMyOrders(user);

                        AnsiConsole.MarkupLine("Press any key to continue...");
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
        } //



        public HamData AskForHamPreferences()
        {
            // Välj viktkategori med etiketter
            var weightChoices = new Dictionary<string, WeightInterval>
            {
                { "3–4 kg", WeightInterval.Kg3To4 },
                { "4–5 kg", WeightInterval.Kg4To5 },
                { "5–6 kg", WeightInterval.Kg5To6 }
            };

            var weightLabel = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose [green]weight interval[/]:")
                    .AddChoices(weightChoices.Keys));

            var weight = weightChoices[weightLabel];

            // Välj inläggning
            var brinedChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Should the ham be [green]brined[/]?")
                    .AddChoices("Brined", "Not brined"));

            var brined = brinedChoice == "Brined";

            // Välj ben
            var boneChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Should the ham be [green]with bones[/] or [green]boneless[/]?")
                    .AddChoices("With bones", "Boneless"));

            var hasBones = boneChoice == "With bones";

            // Välj tillagning
            var cookChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Should the ham be [green]cooked[/] or [green]raw[/]?")
                    .AddChoices("Cooked", "Raw"));

            var isCooked = cookChoice == "Cooked";

            // Leveransvecka med validering
            var week = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter [green]delivery week[/] (1–52):")
                    .Validate(week =>
                        week is >= 1 and <= 52
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Week must be between 1 and 52.[/]")));

            var hamData = new HamData(weight, brined, hasBones, isCooked, week);

            AnsiConsole.MarkupLine($"\n[blue]You selected:[/] {hamData}");

            return hamData;
        }









    }
}








