using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using BookAChristmasHam.Managers;
using BookAChristmasHam.UI.Menu.LoggRegMenu;



class Program
{
    static void Main(string[] args)
    {



        var storage = new StorageService(); // Initierar lagringstjänst
        var accountManager = new UserAccountManager(storage); // Hanterar användarlogik
        var entryMenu = new EntryMenu(accountManager); // Startmeny för inloggning/registrering

        //var userManager = new UserManager(storage); 

        var user = entryMenu.Show(); // Visar meny och returnerar inloggad användare

       


    }
}
