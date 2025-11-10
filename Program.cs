using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.Managers;
using BookAChristmasHam.UI.Menu.LoggRegMenu;
using BookAChristmasHam.UI.Menu.UserMenus;



class Program
{
    static void Main(string[] args)
    {



        var storage = new StorageService(); // Initierar lagringstjänst
        var accountManager = new UserAccountManager(storage); // Hanterar användarlogik
        var entryMenu = new EntryMenu(accountManager); // Startmeny för inloggning/registrering

        var user = entryMenu.Show(); // Visar meny och returnerar inloggad användare

        if (user == null)
        {
            // Programmet avslutas av användaren
            return;
        }

        //if (user.Type == UserType.Business)
        //{
        //    var businessMenu = new BusinessMenu((Business)user); // Meny för företagsanvändare
        //    businessMenu.Show();
        //}
        //else
        //{
        //    var privateMenu = new PrivateMenu(User user, UserManager userManager); // Meny för privat användare
        //    privateMenu.Show();
        //}




        // kalla på Account manager
        //var userAccountManager = new UserAccountManager();

        //// ladda in users.json
        //userAccountManager.ReadFile();


        //// sök väg för json-filen. Skapar mappen (Data) om den inte finns.
        //var usersFilePath = PathService.GetDataFilePath("users.json");

        //// slapa datalager för users
        //var userStore = new DataStore<User>(usersFilePath);

        //// försök ladda befintliga users från fil, users.json
        //userStore.LoadFromJson();


        // hantering av login/reg
        //var accountManager = new UserAccountManager(userStore);

        // visa entry meny
        //var entryMenu = new EntryMenu();
        //entryMenu.Show();






        // 
        //// Registrera ny användare
        //var register = new Register(accountManager);
        //register.Prompt();

        //// Logga in efteråt
        //var login = new Login(userStore);
        //var user = login.Prompt();
    }
}
