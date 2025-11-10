using BookAChristmasHam.Models;
using Spectre.Console;

public class BusinessMenu
{
    private readonly Business _user; // 

    public BusinessMenu(Business user) // måste skicka in både user och user-B, busniessManager
    {
        _user = user;
    }

    public void Show()
    {
        AnsiConsole.MarkupLine($"[bold]Välkommen, {_user.Name} från {_user.CompanyName}![/]");
        // Lägg till menyval för företagsanvändare här
    }
}
