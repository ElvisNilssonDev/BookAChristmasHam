using Spectre.Console;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;

namespace BookAChristmasHam.UI.Menu.LoggRegMenu
{

    public class LoginMenu
    {
        private readonly DataStore<User> _userStore;

        public LoginMenu(DataStore<User> userStore)
        {
            _userStore = userStore;
        }

        public User? Prompt()
        {
            AnsiConsole.MarkupLine("[bold underline]Logga in[/]");

            var email = AnsiConsole.Ask<string>("Ange din e-post:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Ange ditt lösenord:")
                    .PromptStyle("blue")
                    .Secret()
            );

            var user = _userStore.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                AnsiConsole.MarkupLine("[red]Felaktiga inloggningsuppgifter.[/]");
                return null;
            }

            return user;
        }
    }
}