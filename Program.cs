using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.Managers;
using BookAChristmasHam.UI.Menu.LoggRegMenu;
using BookAChristmasHam.UI.Menu.UserMenus;



class Program
{
    static void Main(string[] args)
    {

        //// sök väg för json-filen. Skapar mappen (Data) om den inte finns.
        //var usersFilePath = PathService.GetDataFilePath("users.json");

        //// skapa lagring för users
        //var userStore = new DataStore<User>(usersFilePath);

        //// skapa en user hanterare och ladda in users 
        //var accountManager = new UserAccountManager(userStore);

        //// visa alla users
        //accountManager.

        var storage = new StorageService();
        var accountManager = new UserAccountManager(storage);

        var entryMenu = new EntryMenu(accountManager);
        var user = entryMenu.Show();


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
