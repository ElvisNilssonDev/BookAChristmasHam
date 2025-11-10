using BookAChristmasHam.Models;
using BookAChristmasHam.Managers;
using Spectre.Console;

namespace BookAChristmasHam.UI.Menu.UserMenus
{
    public class PrivateMenu
    {
        private readonly User _user;
        private readonly UserManager _userManager;

        public PrivateMenu(User user, UserManager userManager)
        {
            _user = user;
            _userManager = userManager;
        }

        public void Show()
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"[bold]Välkommen {_user.Name}! Vad vill du göra?[/]")
                        .AddChoices("Visa alla skinkor", "Filtrera skinkor", "Lägg en beställning", "Logga ut")
                );

                switch (choice)
                {
                    case "Visa alla skinkor":
                        ShowAllHams();
                        break;

                    case "Filtrera skinkor":
                        FilterAndShowHams();
                        break;

                    case "Lägg en beställning":
                        PlaceBooking();
                        break;

                    case "Logga ut":
                        AnsiConsole.MarkupLine("[yellow]Du har loggats ut.[/]");
                        return;
                }
            }
        }

        private void ShowAllHams()
        {
            var hams = _userManager.GetAvailableHams();

            if (!hams.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga skinkor tillgängliga.[/]");
                return;
            }

            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("ID");
            table.AddColumn("Vikt");
            table.AddColumn("Ben");
            table.AddColumn("Rimmad");
            table.AddColumn("Kokt");
            table.AddColumn("Vecka");

            foreach (var ham in hams)
            {
                var d = ham.Data;
                table.AddRow(
                    ham.Id.ToString(),
                    $"{d.Weight} kg",
                    d.HasBones ? "Ja" : "Nej",
                    d.Brined ? "Ja" : "Nej",
                    d.IsCooked ? "Ja" : "Nej",
                    $"v.{d.Week}"
                );
            }

            AnsiConsole.Write(table);
        }

        private void FilterAndShowHams()
        {
            var weightChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Välj viktintervall:")
                    .AddChoices("3-4 kg", "4-5 kg", "5-6 kg")
            );

            var boneChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Välj benalternativ:")
                    .AddChoices("Med ben", "Utan ben")
            );

            var brineChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Välj rimmning:")
                    .AddChoices("Rimmad", "Orimmad")
            );

            var cookChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Välj tillagning:")
                    .AddChoices("Kokt", "Okokt")
            );

            int minWeight = 0, maxWeight = 0;
            switch (weightChoice)
            {
                case "3-4 kg": minWeight = 3; maxWeight = 4; break;
                case "4-5 kg": minWeight = 4; maxWeight = 5; break;
                case "5-6 kg": minWeight = 5; maxWeight = 6; break;
            }

            bool hasBones = boneChoice == "Med ben";
            bool isBrined = brineChoice == "Rimmad";
            bool isCooked = cookChoice == "Kokt";

            var filtered = _userManager.GetAvailableHams()
                .Where(h =>
                    h.Data.Weight >= minWeight &&
                    h.Data.Weight <= maxWeight &&
                    h.Data.HasBones == hasBones &&
                    h.Data.Brined == isBrined &&
                    h.Data.IsCooked == isCooked
                ).ToList();

            if (!filtered.Any())
            {
                AnsiConsole.MarkupLine("[red]Inga skinkor matchar dina val.[/]");
                return;
            }

            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("ID");
            table.AddColumn("Vikt");
            table.AddColumn("Ben");
            table.AddColumn("Rimmad");
            table.AddColumn("Kokt");
            table.AddColumn("Vecka");

            foreach (var ham in filtered)
            {
                var d = ham.Data;
                table.AddRow(
                    ham.Id.ToString(),
                    $"{d.Weight} kg",
                    d.HasBones ? "Ja" : "Nej",
                    d.Brined ? "Ja" : "Nej",
                    d.IsCooked ? "Ja" : "Nej",
                    $"v.{d.Week}"
                );
            }

            AnsiConsole.Write(table);
        }

        private void PlaceBooking()
        {
            var hams = _userManager.GetAvailableHams().ToList();

            if (!hams.Any())
            {
                AnsiConsole.MarkupLine("[red]Det finns inga skinkor att boka just nu.[/]");
                return;
            }

            var hamChoice = AnsiConsole.Prompt(
                new SelectionPrompt<ChristmasHam>()
                    .Title("Välj en skinka att boka:")
                    .UseConverter(h => $"#{h.Id} – {h.Data.Weight}kg, v.{h.Data.Week}, {(h.Data.IsCooked ? "Kokt" : "Rå")}")
                    .AddChoices(hams)
            );

            _userManager.BookHam(_user, hamChoice.Id);
        }
    }
}