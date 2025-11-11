using BookAChristmasHam.Managers;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.UI.Menu.LoggedInMenu;
using Spectre.Console;

namespace BookAChristmasHam.UI.Menu.LoggRegMenu
{

    public class EntryMenu
    { // Visa meny för inloggning/registrering

        private readonly UserAccountManager _accountManager;

        
        public EntryMenu(UserAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        private readonly PrivateMenu privateMenu = new PrivateMenu();
        private readonly BusinessMenu businessMenu = new BusinessMenu();



        public User? Show()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                new FigletText("Book A Christmas Ham!")
                .Centered()
                .Color(Color.Green3));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Choose an option:[/]")
                        .AddChoices("Log in", "Register new user", "Exit"));

                switch (choice)
                {
                    case "Log in":
                        var loginMenu = new LoginMenu(_accountManager);
                        var user = loginMenu.Prompt();

                        if (user != null)
                        {
                            if (user is Business businessUser)
                            {
                                var businessMenu = new BusinessMenu();
                                businessMenu.DisplayBusinessMenu(businessUser);
                            }
                             else if (user is not Business)
                            {
                                var privateMenu = new PrivateMenu();
                                privateMenu.ShowPriv(user);
                            }
                        }
                        break;

                    case "Register new user":
                        var registerMenu = new RegisterMenu(_accountManager);
                        registerMenu.Prompt();
                        break;

                    case "Exit":
                        AnsiConsole.MarkupLine("[yellow]The program is exiting...[/]");
                        return null;
                }
            }
        }
    }
}