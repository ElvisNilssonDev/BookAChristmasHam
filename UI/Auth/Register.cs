
using Spectre.Console;
using BookAChristmasHam.Models;
using BookAChristmasHam.Managers;


namespace BookAChristmasHam.UI.Authors
{ // hanterar Registrering

    public class Register // Rega ny användare
    {
        private readonly UserAccountManager _accountManager;

        public Register(UserAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public void Prompt()
        {
            AnsiConsole.MarkupLine("[bold underline]Register new user.[/]");

            var name = AnsiConsole.Ask<string>("Enter your name:");
            var email = AnsiConsole.Ask<string>("Enter your email:");

            if (_accountManager.EmailExists(email))
            {
                AnsiConsole.MarkupLine("[red]The email address is already registered.[/]");
                return;
            }

            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Choose a password.")
                    .PromptStyle("blue")
                    .Secret()
            );

            var type = AnsiConsole.Prompt(
                new SelectionPrompt<UserType>()
                    .Title("Select account type:")
                    .AddChoices(UserType.Private, UserType.Business)
            );

            var user = new User // ny user
            {
                Name = name,
                Email = email,
                Password = password,
                Type = type
            };

            _accountManager.Register(user); // skicka in till register som finns i Login
            _accountManager.Save(); // spara 
            AnsiConsole.MarkupLine("[green]Registration successful! You can now log in.[/]");
        }

    }//end class
}