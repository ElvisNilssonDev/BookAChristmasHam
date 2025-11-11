using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAChristmasHam.Models;

//ShowAllHam()
//DeleteOrder()
//FilterOrder()
//UppdateOrder()


namespace BookAChristmasHam.UI.Menu.LoggedInMenu
{
    internal class BusinessMenu
    {
        public void DisplayBusinessMenu(Business businessUser)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== Business Menu ===");
                Console.WriteLine("1. Show all orders");
                Console.Write("Choose a option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        //ShowAllHam();
                        Console.WriteLine("Show all orders selected.");
                        break;
                }
            }
        }
    }

}
