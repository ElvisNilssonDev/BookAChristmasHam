using Spectre.Console;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.Managers;

namespace BookAChristmasHam.UI.Menu.LoggRegMenu
{

    public class EntryMenu
    { // Visa meny för inloggning/registrering

        private readonly UserAccountManager _accountManager;

        public EntryMenu(UserAccountManager accountManager)
        {
            _accountManager = accountManager;
        }



        public User? Show()
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Välj ett alternativ:[/]")
                        .AddChoices("Logga in", "Registrera ny användare", "Avsluta"));

                switch (choice)
                {
                    case "Logga in":
                        var loginMenu = new LoginMenu(_accountManager);
                        var user = loginMenu.Prompt();
                        if (user != null)
                        {
                            AnsiConsole.MarkupLine($"[green]Inloggad som {user.Name} ({user.Type})[/]");
                            return user;
                        }
                        break;

                    case "Registrera ny användare":
                        var registerMenu = new RegisterMenu(_accountManager);
                        registerMenu.Prompt();
                        break;

                    case "Avsluta":
                        AnsiConsole.MarkupLine("[yellow]Programmet avslutas...[/]");
                        return null;
                }
            }
        }
    }
}