using BookAChristmasHam.Managers;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using Spectre.Console;

namespace BookAChristmasHam.UI.Menu.LoggRegMenu
{
    public class LoginMenu
    { //Hantera inloggning	

        private readonly UserAccountManager _accountManager;

        public LoginMenu(UserAccountManager accountManager)
        {
            _accountManager = accountManager;
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

            var user = _accountManager.Authenticate(email, password);

            if (user == null)
            {
                AnsiConsole.MarkupLine("[red]Felaktiga inloggningsuppgifter.[/]");
                return null;
            }

            // Validera företagsanvändare
            if (user is Business businessUser && string.IsNullOrWhiteSpace(businessUser.CompanyName))
            {
                AnsiConsole.MarkupLine("[red]Ogiltig företagsanvändare – saknar företagsnamn.[/]");
                return null;
            }

            return user;
        }
    }
}
