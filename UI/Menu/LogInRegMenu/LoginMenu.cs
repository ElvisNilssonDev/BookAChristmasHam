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
            while (true) // Keep asking until login succeeds or user chooses to cancel
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

                    // Ask if they want to try again or go back
                    var retry = AnsiConsole.Confirm("Do you want to try again?");
                    if (!retry)
                        return null; // Go back to main menu
                    else
                        continue; // Loop again
                }

                // Validate business users
                if (user is Business businessUser && string.IsNullOrWhiteSpace(businessUser.CompanyName))
                {
                    AnsiConsole.MarkupLine("[red]Invalid business user – missing company name.[/]");
                    var retry = AnsiConsole.Confirm("Do you want to try again?");
                    if (!retry)
                        return null;
                    else
                        continue;
                }

                // Successful login
                return user;
            }
        }
    }
}
