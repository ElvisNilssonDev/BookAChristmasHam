using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAChristmasHam.Models;
using BookAChristmasHam.Managers;
using BookAChristmasHam.Managers;
using Spectre.Console;

//ShowAllHam()
//DeleteOrder()
//FilterOrder()
//UppdateOrder()


namespace BookAChristmasHam.UI.Menu.LoggedInMenu
{
    public class BusinessMenu
    {
        private readonly BusinessManager _businessManager;

        public BusinessMenu(BusinessManager businessManager)
        {
            _businessManager = businessManager;
        }

        private readonly BusinessManager _businessManager;

        public BusinessMenu(BusinessManager businessManager)
        {
            _businessManager = businessManager;
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
                        ShowMyOrders(user);
                    case "Show all your orders":
                        //Visa alla ordrar som är kopplade till företagets CompanyName
                        ShowMyOrders(user);
                        break;
                    case "Delete an order":
                        //Radera en order via dess bookingId
                        DeleteOrder(user);
                        //Radera en order via dess bookingId
                        DeleteOrder(user);
                        break;
                    case "Filter orders":
                        AnsiConsole.MarkupLine("[blue]FilterOrder()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "Update an order":
                        AnsiConsole.MarkupLine("[blue]UppdateOrder()...[/]");
                        AnsiConsole.MarkupLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "Logout":
                        runningbusiness = false; // Exit the loop
                        break;
                }

            }
        }



        //Visa alla ordrar kopplade till företaget
        // private void ShowMyOrders(User user)
        // {
        //     Console.Clear();
        //     AnsiConsole.Write(
        //         new FigletText("Your Orders")
        //         .Centered()
        //         .Color(Color.Red));

        //     var orders = _businessManager.GetMyOrders(user.Id).ToList();

        //     if (!orders.Any())
        //     {
        //         AnsiConsole.MarkupLine("[yellow]No orders found for your business.[/]");
        //         AnsiConsole.MarkupLine("Press any key to continue...");
        //         Console.ReadKey();
        //         return;
        //     }

        //     //Skapar en tabell för att visa bokningar
        //     var table = new Table();
        //     table.AddColumn("[bold]Booking ID[/]");
        //     table.AddColumn("[bold]User ID[/]");
        //     table.AddColumn("[bold]Christmas Ham ID[/]");
        //     table.AddColumn("[bold]Company Name[/]");

        //     foreach (var order in orders)
        //     {
        //         var companyName = _businessManager.GetCompanyName(order.BusinessId) ?? "Unknown";
        //         table.AddRow(
        //             order.Id.ToString(),
        //             order.UserId.ToString(),
        //             order.ChristmasHamId.ToString(),
        //             companyName
        //         );
        //     }

        //     AnsiConsole.Write(table);
        //     AnsiConsole.MarkupLine($"\n[green]Total orders: {orders.Count}[/]");
        //     AnsiConsole.MarkupLine("Press any key to continue...");
        //     Console.ReadKey();
        // }
        private void ShowMyOrders(User user)
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(
                    new FigletText("Your Orders")
                    .Centered()
                    .Color(Color.Red));

                var orders = _businessManager.GetMyOrders(user.Id).ToList();

                if (!orders.Any())
                {
                    AnsiConsole.MarkupLine("[yellow]No orders found for your business.[/]");
                    AnsiConsole.MarkupLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                //Tabell för att visa ordrar
                var table = new Table();
                table.AddColumn("[bold]Booking ID[/]");
                table.AddColumn("[bold]User ID[/]");
                table.AddColumn("[bold]Christmas Ham ID[/]");
                table.AddColumn("[bold]Company Name[/]");

                foreach (var order in orders)
                {
                    var companyName = _businessManager.GetCompanyName(order.BusinessId) ?? "Unknown";
                    table.AddRow(
                        order.Id.ToString(),
                        order.UserId.ToString(),
                        order.ChristmasHamId.ToString(),
                        companyName
                    );
                }

                //Visar tabellen
                AnsiConsole.Write(table);

                var choices = new List<object>(orders)
                {
                    "Cancel"
                };

                var selected = AnsiConsole.Prompt(
                    new SelectionPrompt<object>()
                        .Title("\n[green]Select an order:[/]")
                        .UseConverter(obj =>
                        {
                            if (obj is Booking order)
                            {
                                var company = _businessManager.GetCompanyName(order.BusinessId) ?? "Unknown";
                                return $"{order.Id} | User {order.UserId} | Ham {order.ChristmasHamId} | {company}";
                            }
                            return "[red]Cancel[/]";
                        })
                        .AddChoices(choices)
                );
                //Avbryt om användaren väljer att avbryta
                if (selected is string)
                    return;

                var selectedOrder = (Booking)selected;


                //Visa vilken som valdes
                Console.Clear();
                AnsiConsole.MarkupLine($"\n[bold green]You selected order ID: {selectedOrder.Id}[/]");
                var ham = _businessManager.GetHamById(selectedOrder.ChristmasHamId);
                if (ham != null)
                {
                    var hamDetails = ham.Data?.ToString() ?? "No details available.";
                    AnsiConsole.MarkupLine($"\n[green]Ham ID:[/] {ham.Id}");
                    AnsiConsole.MarkupLine($"[green]Ham Details: [/]{hamDetails}");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Ham details not found.[/]");
                }
                AnsiConsole.MarkupLine("\nPress any key to continue...");
                Console.ReadKey(true);
            }
        }


        //Radera en order via dess bookingId
        private void DeleteOrder(User user)
        {
            Console.Clear();
            AnsiConsole.Write(
                new FigletText("Delete Order")
                .Centered()
                .Color(Color.Red));

            var orders = _businessManager.GetMyOrders(user.Id).ToList();

            if (!orders.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No orders found for your business.[/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            //Låt användaren välja en order att radera
            var orderChoices = orders.Select(order =>
            {
                var companyName = _businessManager.GetCompanyName(order.BusinessId) ?? "Unknown";
                return $"Booking ID: {order.Id} | User ID: {order.UserId} | Ham ID: {order.ChristmasHamId} | Company: {companyName}";
            }).ToList();

            //Avbryta radering
            orderChoices.Add("[red]Cancel[/]");

            var selectedOrder = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]Select an order to delete:[/]")
                .PageSize(10)
                .AddChoices(orderChoices));

            //Avbryt om användaren väljer att avbryta
            if (selectedOrder == "[red]Cancel[/]")
            {
                return;
            }

            //Få fram bookingId från den valda strängen
            var bookingIdStr = selectedOrder.Split('|')[0].Replace("Booking ID:", "").Trim();
            if (!int.TryParse(bookingIdStr, out int bookingId))
            {
                AnsiConsole.MarkupLine("[red]Error: Could not parse booking ID.[/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            //Dubbelkolla med användaren innan radering
            var confirm = AnsiConsole.Confirm(
                $"[yellow]Are you sure you want to delete Booking ID: {bookingId}?[/]",
                false);

            if (!confirm)
            {
                AnsiConsole.MarkupLine("[blue]Deletion cancelled.[/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            //Radera ordern
            var isDeleted = _businessManager.DeleteOrder(bookingId);

            if (isDeleted)
            {
                AnsiConsole.MarkupLine($"[green]Order with Booking ID {bookingId} has been successfully deleted![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Failed to delete order with Booking ID {bookingId}.[/]");
            }

            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

