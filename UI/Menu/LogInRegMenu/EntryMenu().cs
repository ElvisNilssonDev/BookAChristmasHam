using BookAChristmasHam.Managers;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.UI.Helper;
using BookAChristmasHam.UI.Menu.LoggedInMenu;
using Spectre.Console;

namespace BookAChristmasHam.UI.Menu.LoggRegMenu
{
    public class EntryMenu
    {
        // Visa meny för inloggning/registrering
        private readonly UserAccountManager _accountManager;
        private readonly StorageService _storageService;
        private readonly PrivateMenu _privateMenu;

        public EntryMenu(StorageService storage, UserAccountManager accountManager)
        {
            _accountManager = accountManager;
            _storageService = storage;
            _privateMenu = new PrivateMenu(storage);
        }

        public User? Show()
        {
            Thread.Sleep(1000);

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
                            // Visa vilken user-type som loggade in
                            LoadingUI.ShowLoginStatus(user);

                            // Ladda in data
                            _storageService.LoadHamAndBooking();
                            Thread.Sleep(1000);

                            // Kolla användartyp med switch-case
                            switch (user.Type)
                            {
                                case UserType.Business:
                                    var businessManager = new BusinessManager(_storageService);
                                    var businessMenu = new BusinessMenu(businessManager, _storageService);
                                    businessMenu.DisplayBusinessMenu(user);
                                    break;

                                case UserType.Private:
                                    _privateMenu.ShowPriv(user);
                                    break;

                                default:
                                    AnsiConsole.MarkupLine("[red]Unknown user type![/]");
                                    break;
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
