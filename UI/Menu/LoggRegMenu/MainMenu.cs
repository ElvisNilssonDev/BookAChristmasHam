using Spectre.Console;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.Managers;

namespace BookAChristmasHam.UI.Menu.LoggRegMenu
{

    public class MainMenu
    {
        private readonly DataStore<User> _userStore;
        private readonly UserAccountManager _accountManager;

        public MainMenu()
        {
            _userStore = new DataStore<User>(PathService.GetDataFilePath("users.json"));
            _accountManager = new UserAccountManager(_userStore);
        }

        public void Show()
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
                        var loginMenu = new LoginMenu(_userStore);
                        var user = loginMenu.Prompt();
                        if (user != null)
                        {
                            AnsiConsole.MarkupLine($"[green]Inloggad som {user.Name} ({user.Type})[/]");
                            return;
                        }
                        break;

                    case "Registrera ny användare":
                        var registerMenu = new RegisterMenu(_accountManager);
                        registerMenu.Prompt();
                        break;

                    case "Avsluta":
                        AnsiConsole.MarkupLine("[yellow]Programmet avslutas...[/]");
                        return;
                }
            }
        }
    }
}