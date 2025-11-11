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
                        .Title("[bold]Choose an option:[/]")
                        .AddChoices("Log in", "Register new user", "Exit"));

                switch (choice)
                {
                    case "Log in":
                        var loginMenu = new LoginMenu(_accountManager);
                        var user = loginMenu.Prompt();
                        if (user != null)
                        {
                            AnsiConsole.MarkupLine($"[green]Logged in as {user.Name} ({user.Type})[/]");
                            return user;
                        }
                        break;

                    case "Register new user":
                        var registerMenu = new RegisterMenu(_accountManager);
                        registerMenu.Prompt();
                        break;

                    case "Exit":
                        AnsiConsole.MarkupLine("[yellow]The program is exiting...[/]");
                        return null;
                }
            }
        }
    }
}