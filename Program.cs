using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.Managers;
using BookAChristmasHam.UI.Menu.LoggRegMenu;
using BookAChristmasHam.UI.Menu.UserMenus;



class Program
{
    static void Main(string[] args)
    {
        // hämta users
        var userStore = new DataStore<User>(PathService.GetDataFilePath("users.json"));

        // hantering av login/reg
        //var accountManager = new UserAccountManager(userStore);

        // visa entry meny
        var entryMenu = new EntryMenu();
        entryMenu.Show();






        // 
        //// Registrera ny användare
        //var register = new Register(accountManager);
        //register.Prompt();

        //// Logga in efteråt
        //var login = new Login(userStore);
        //var user = login.Prompt();
    }
}
