using BookAChristmasHam.Managers;
using BookAChristmasHam.Models;
using Spectre.Console;
using BookAChristmasHam.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ShowAllHam()
//DeleteOrder()
//FilterOrder()
//UppdateOrder()


namespace BookAChristmasHam.UI.Menu.LoggedInMenu
{
    public class BusinessMenu
    {
        private readonly BusinessManager _businessManager;
        private readonly StorageService _storageService;

        public BusinessMenu(BusinessManager businessManager, StorageService storageService)
        {
            _businessManager = businessManager;
            _storageService = storageService;
        }

        public void DisplayBusinessMenu(User user)
        {
            bool runningbusiness = true;
            while (runningbusiness)
            {
                Console.Clear();
                // Visar meny för företagsanvändare
                AnsiConsole.Write(
                new FigletText("ChristmasHam!")
                .Centered()
                .Color(Color.Red));
                AnsiConsole.Write(
                new FigletText(user.CompanyName ?? "Business User")
                .Centered()
                .Color(Color.Green));
                var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]Select an option:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "Show all your orders",
                    "Delete an order",
                    "Filter orders",
                    "Update an order",
                    "Logout"
                }));
                switch (choice)
                {
                    case "Show all your orders":
                        //Visa alla ordrar som är kopplade till företagets CompanyName
                        _businessManager.ShowMyOrders(user);
                        break;
                    case "Delete an order":
                        //Radera en order via dess bookingId
                        _businessManager.DeleteOrder(user);
                        break;
                    case "Filter orders":
                        FilterOrders(user);
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "Update an order":
                        UpdateOrder(user);
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "Logout":
                        //Loggar ut användaren och återvänder till huvudmenyn
                        runningbusiness = false;
                        break;
                }

            }
        }
        private void FilterOrders(User user)//User BusinessUser
        {
            bool filtering = true;

            while(filtering)
            {
                var myOrderS = _businessManager.GetMyOrders(user.Id).ToList();

                var filterChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]Select a filter option: [/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                   "Alphabetic order(By Name)",
                   "Sorted by weeks",
                   "Filter by type of ham",
                   "Back to menu"

                }));
                switch (filterChoice)
                {
                    case "Alphabetic order(By Name)":
                        var userManager = new UserAccountManager(_storageService);
                        var allUsers = userManager.GetAllUsers().ToList();
                        //var myOrderS = _businessManager.GetMyOrders(user.Id).ToList();

                        var sorted = myOrderS.OrderBy(o => allUsers.First(u => u.Id == o.UserId).Name).ToList();

                        foreach(var order in sorted)
                        {
                            var customer = allUsers.First(u => u.Id == order.UserId);
                            var ham = _storageService.HamStore.Get(order.ChristmasHamId);
                            AnsiConsole.MarkupLine($"[yellow]{customer.Name}[/] - HamId: [green]{ham.Id}[/]");
                        }
                        Console.ReadKey();
                        break;

                    case "Sorted by weeks":

                        var orders = _businessManager.GetMyOrders(user.Id).ToList();
                        var hams = _businessManager.GetAllHams(user.Id).ToList();
                        var userAccountManager = new UserAccountManager(_storageService);
                        //var userManager = new UserAccountManager(_storageService);

                        var groupedWeeks = orders.GroupBy(o =>
                        {
                            var ham = hams.FirstOrDefault(h => h.Id == o.ChristmasHamId);
                            return ham?.Data.Week ?? -1;
                        });
                        foreach(var weekGroup in groupedWeeks.OrderBy(g=> g.Key))
                        {
                            AnsiConsole.MarkupLine($"[bold blue]=== Week {weekGroup.Key} ===[/]");
                            foreach(var order in weekGroup)
                            {
                                var customer = userAccountManager.GetUserById(order.UserId);
                                var customerName = customer?.Name ?? "Unknown";

                                var ham = hams.FirstOrDefault(h => h.Id == order.ChristmasHamId);
                                var hamInfo = ham != null ? $"HamId: {ham.Id}" : "Ham not found";

                                AnsiConsole.MarkupLine($"[yellow]{customer.Name}[/] - [blue] {hamInfo}[/]");
                            }
                        }
                        Console.ReadKey();
                        break;

                    case "Filter by type of ham":
                        var myOrders = _businessManager.GetMyOrders(user.Id).ToList();
                        FilterByHamProperties(user, myOrders);
                        break;
                    case "Back to menu":
                        filtering = false;
                        break;
                }
            }  
        }
        private void FilterByHamProperties(User user, List<Booking> orders)
        {
            var propertyChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green] Choose ham properties to filter: [/]")
                .AddChoices(new[]
                {
                    "Brined/Not Brined",
                    "Boneless/With Bones",
                    "Cooked/Uncooked",
                    "Back to menu"
                }));
              var hams = _businessManager.GetAllHams(user.Id);

            switch (propertyChoice)
            {
                case "Brined/Not Brined":
                    ShowGrouped(
                        "Brined",
                        hams.Where(h => h.Data.Brined),
                        "Not Brined",
                        hams.Where(h => !h.Data.Brined),
                        orders
                        ); break;
                case "Boneless/With Bones":
                    ShowGrouped(
                        "With Bones",
                        hams.Where(h => h.Data.HasBones),
                        "Boneless",
                        hams.Where(h => !h.Data.HasBones),
                        orders//ist för user här
                        ); break;
                case "Cooked/Uncooked":
                    ShowGrouped(
                        "Cooked",
                        hams.Where(h => h.Data.IsCooked),
                        "Uncooked",
                        hams.Where(h => !h.Data.IsCooked),
                        orders
                        ); break;
                case "Back to menu":
                    return;
            }
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
        }
        private void ShowGrouped(string labelYes, IEnumerable<ChristmasHam> yesList, string labelNo, IEnumerable<ChristmasHam> noList, List<Booking> orders)
        {
            //var orders = _businessManager.GetMyOrders(user.Id).ToList();

            AnsiConsole.MarkupLine($"[bold green]==={labelYes}===[/]");

            foreach(var ham in yesList)//här är ja
            {
                var order = orders.FirstOrDefault(o => o.ChristmasHamId == ham.Id);
                int week = ham.Data.Week;

                if (order == null)
                {
                    AnsiConsole.MarkupLine($"[green] HamId: {ham.Id} [/] | [blue] Week: {week} | [grey] No order found[/]");
                    continue;
                }
                AnsiConsole.MarkupLine($"[green]HamId:{ham.Id}[/] | Week: [yellow]{week}[/]");
            }
            Console.WriteLine();

            AnsiConsole.MarkupLine($"[bold red]==={labelNo}===[/]");//här nej
            foreach (var ham in noList)
            {
                var order = orders.FirstOrDefault(o => o.ChristmasHamId == ham.Id);

                if (order == null)
                {
                    continue;
                }
                //AnsiConsole.MarkupLine($"[green] HamId: {ham.Id} [/] | [grey] No order found[/]");
                int week = ham.Data.Week;
                AnsiConsole.MarkupLine($"[green]HamId:{ham.Id}[/] | Week: [yellow]{week}[/]");
            }

        }
        private void UpdateOrder(User user)
        {
            var orders = _businessManager.GetMyOrders(user.Id).ToList();//Skaffar orders för user

            if(!orders.Any())//Om inga ordrar finns, skriver ut meddelande o går tbxc
            {
                AnsiConsole.MarkupLine($"[yellow]No orders available for an update[/]");
                Console.ReadKey();
                return;
            }
            //Låter användaren välja en order, för att sedan hoppa till att ändra week
            var orderChoices = orders.Select(o =>
            {
                var ham = _businessManager.GetHamById(o.ChristmasHamId);//Skaffar skinkan som matchar id
                var week = ham?.Data.Week ?? -1;//tar fram nuvarande leverans vecka, o -1 om den inte hittas här
                return $"Booking ID: {o.Id} | Ham ID: {o.ChristmasHamId} | Week: {week}";
            }).ToList();
            orderChoices.Add("[red] Cancel[/]");

            var selected = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Select an order to update: ")//Ber användare välja order att uppdatera.
            .AddChoices(orderChoices));
            if (selected == "[red] Cancel[/]") return;//avslutas om användare avbryter

            var bookingIdStr = selected.Split('|')[0].Replace("Booking ID:", "").Trim();//tar bokning id från de som trycktes in

            if(!int.TryParse(bookingIdStr, out int bookingId))//felhantering, så de e ints
            {
                AnsiConsole.MarkupLine($"[red] Wrong, could not find the booking..[/]");
                Console.ReadLine();
                return;
            }
            var orderToUpdate = orders.First(o => o.Id == bookingId);
            var hamToUpdate = _businessManager.GetHamById(orderToUpdate.ChristmasHamId);

            if(hamToUpdate == null)
            {
                AnsiConsole.MarkupLine($"[red] Could NOT find order.[/]");
                Console.ReadKey();
                return;
            }
            //Här för o lägga ny vekca
            int newWeek = AnsiConsole.Prompt(new TextPrompt<int>($"Enter what week that you want your delivery: (Your current delivery week: {hamToUpdate.Data.Week}):"));

            hamToUpdate.Data.Week = newWeek;
            //här för att spara nya ändringen
            _businessManager.UpdateHam(hamToUpdate);

            AnsiConsole.MarkupLine($"[green]Your order is updated![/]");
            Console.ReadKey();
        }
    }
}

