using Spectre.Console;
using BookAChristmasHam.Models;
using BookAChristmasHam.Managers;

namespace BookAChristmasHam.UI.Menu.LoggRegMenu;

public class RegisterMenu
{ // Hantera registrering av ny användare.	

    private readonly UserAccountManager _accountManager;

    public RegisterMenu(UserAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public void Prompt()
    {
        AnsiConsole.MarkupLine("[bold underline]Register new user[/]");

        var name = AnsiConsole.Ask<string>("Enter your name:");
        var email = AnsiConsole.Ask<string>("Enter your email:");

        if (_accountManager.EmailExists(email))
        {
            AnsiConsole.MarkupLine("[red]The email address is already registered.[/]");
            return;
        }

        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Choose a password:")
                .PromptStyle("blue")
                .Secret()
        );

        var type = AnsiConsole.Prompt(
            new SelectionPrompt<UserType>()
                .Title("Select account type:")
                .AddChoices(UserType.Private, UserType.Business)
        );

        User newUser;

        if (type == UserType.Business)
        {
            var companyName = AnsiConsole.Ask<string>("Enter company name:");

            if (string.IsNullOrWhiteSpace(companyName))
            {
                AnsiConsole.MarkupLine("[red]Company name is required for business users.[/]");
                return;
            }

            newUser = new Business
            {
                Name = name,
                Email = email,
                Password = password,
                CompanyName = companyName
            };
        }
        else
        {
            newUser = new User
            {
                Name = name,
                Email = email,
                Password = password,
                Type = UserType.Private
            };
        }

        _accountManager.Register(newUser);
        _accountManager.Save(); 
        AnsiConsole.MarkupLine("[green]Registration successful![/]");
    }
}
