using Spectre.Console;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;

namespace BookAChristmasHam.UI.Authors
{
    // user ska kunna logga in 
    public class Login // Alla user kan logga in, både user-p och user-b

    {   // lagring av user.
        private readonly DataStore<User> _userStore;

        public Login(DataStore<User> userStore)
        {
            _userStore = userStore;
        }

        public User? Prompt() // fråga efter user info, rega ny användare 
        {
            AnsiConsole.MarkupLine("[bold underline]Logga in[/]");

            var email = AnsiConsole.Ask<string>("Ange din e-post:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Ange ditt lösenord:")
                    .PromptStyle("blue")
                    .Secret()
            );

            // kollar om den som loggar in finns i systemet
            var user = _userStore.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                AnsiConsole.MarkupLine("[red]Felaktiga inloggningsuppgifter.[/]");
                return null;
            }

            AnsiConsole.MarkupLine($"[green]Välkommen, {user.Name}![/]");
            return user; // skickar ut user info
        }
    }


}


