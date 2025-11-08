using BookAChristmasHam.Managers;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.UI;
using BookAChristmasHam.UI.Authors;

class Program
{
    static void Main(string[] args)
    {
        // test
        var userStore = new DataStore<User>(PathService.GetDataFilePath("users.json"));
        var accountManager = new UserAccountManager(userStore);

        // Registrera ny användare
        var register = new Register(accountManager);
        register.Prompt();

        // Logga in efteråt
        var login = new Login(userStore);
        var user = login.Prompt();
    }
}
