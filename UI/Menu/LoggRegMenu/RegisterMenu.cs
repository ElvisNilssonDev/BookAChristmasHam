using Spectre.Console;
using BookAChristmasHam.Models;
using BookAChristmasHam.Managers;

namespace BookAChristmasHam.UI.Menu.LoggRegMenu;

public class RegisterMenu
{
    private readonly UserAccountManager _accountManager;

    public RegisterMenu(UserAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public void Prompt()
    {
        AnsiConsole.MarkupLine("[bold underline]Registrera ny användare[/]");

        var name = AnsiConsole.Ask<string>("Ange ditt namn:");
        var email = AnsiConsole.Ask<string>("Ange din e-post:");
        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Välj ett lösenord:")
                .PromptStyle("blue")
                .Secret()
        );

        var type = AnsiConsole.Prompt(
            new SelectionPrompt<UserType>()
                .Title("Välj användartyp:")
                .AddChoices(UserType.Private, UserType.Business)
        );

        var newUser = new User
        {
            Name = name,
            Email = email,
            Password = password,
            Type = type
        };

        _accountManager.Register(newUser);
        _accountManager.Save(); // om du har en Save-metod
        AnsiConsole.MarkupLine("[green]Registrering lyckades![/]");
    }
}
