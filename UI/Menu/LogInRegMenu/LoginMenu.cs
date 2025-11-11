using BookAChristmasHam.Managers;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.UI.Menu.LoggedInMenu;
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
            AnsiConsole.MarkupLine("[bold underline]Log in[/]");

            var email = AnsiConsole.Ask<string>("Enter your email:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter your password:")
                    .PromptStyle("blue")
                    .Secret()
            );

            var user = _accountManager.Authenticate(email, password);

            if (user == null)
            {
                AnsiConsole.MarkupLine("[red]Incorrect login credentials.[/]");
                return null;
            }

            // Validera företagsanvändare
            if (user is Business businessUser && string.IsNullOrWhiteSpace(businessUser.CompanyName))
            {
                AnsiConsole.MarkupLine("[red]Invalid business user – missing company name.[/]");
                return null;
            }

            return user;
        }       
    }
}
