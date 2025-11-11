using BookAChristmasHam.Managers;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using Spectre.Console;

namespace BookAChristmasHam.UI.Authors
{
    // user ska kunna logga in 
    public class Login // Alla user kan logga in, både user-p och user-b
    {
        //Alternativ inloggningsflöde

        private readonly UserAccountManager _accountManager;

        public Login(UserAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public User? Prompt() // fråga efter user info, rega ny användare 
        {
            AnsiConsole.MarkupLine("[bold underline]Log in[/]");

            var email = AnsiConsole.Ask<string>("Enter your email:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter your password:")
                    .PromptStyle("blue")
                    .Secret()
            );

            // kollar om den som loggar in finns i systemet


            var user = _accountManager.Authenticate(email, password);

            if (user == null)
            {
                AnsiConsole.MarkupLine("[red]Incorrect login credentials. Try again![/]");
                return null;
            }

            AnsiConsole.MarkupLine($"[green]Welcome, {user.Name}![/]");
            return user; // skickar ut user info
        }
    }


}


