
using Spectre.Console;
using BookAChristmasHam.Models;
using BookAChristmasHam.Managers;


namespace BookAChristmasHam.UI.Authors
{

    public class Register // Rega ny användare
    {
        private readonly UserAccountManager _accountManager;

        public Register(UserAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public void Prompt()
        {
            AnsiConsole.MarkupLine("[bold underline]Registrera ny användare[/]");

            var name = AnsiConsole.Ask<string>("Ange ditt namn:");
            var email = AnsiConsole.Ask<string>("Ange din e-post:");

            if (_accountManager.EmailExists(email))
            {
                AnsiConsole.MarkupLine("[red]E-postadressen är redan registrerad.[/]");
                return;
            }

            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Ange ett lösenord:")
                    .PromptStyle("blue")
                    .Secret()
            );

            var type = AnsiConsole.Prompt(
                new SelectionPrompt<UserType>()
                    .Title("Välj användartyp:")
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
            AnsiConsole.MarkupLine("[green]Registreringen lyckades! Du kan nu logga in.[/]");
        }

    }//end class
}