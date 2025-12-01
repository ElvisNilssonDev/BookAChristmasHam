
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using Spectre.Console;

namespace BookAChristmasHam.Managers
{
    // HANTERAR HAM 
    public class BusinessManager // Hanterar Ham-operationer + booknings-operationer för företagsanvändare
    {
        // HAM-LAGRING + METODER
        private readonly DataStore<ChristmasHam> _hamStore;

        // lagringsinstans (_bookingStore) för Booking. Hanterar bokningar (List<Booking> _items). Innehåller metoder från DataStore-klassen.
        //private readonly DataStore<Booking> _bookingStore;
        private readonly BookingManager _bookingManager;

        //UserStore för att hämta CompanyName baserat på BusinessId
        private readonly DataStore<User> _userStore;

        //// KONSTRUKTOR
        //public BusinessManager(DataStore<ChristmasHam> hamStore, DataStore<Booking> bookingstore)
        //{
        //    _hamStore = hamStore;
        //    _bookingStore = bookingstore;
        //}

        // Konstruktor: tar emot StorageService
        public BusinessManager(StorageService storage)
        {
            _hamStore = storage.HamStore;
            _bookingManager = new BookingManager(storage);
            _userStore = storage.UserStore;
        }      
        // Ta bort en order
        public bool DeleteOrder(int bookingId)
        {
            // Hämta bokningen först
            var booking = _bookingManager.GetBookingById(bookingId);
            if (booking == null)
                return false;

            //Radera bokningen
            var DeleteBooking = _bookingManager.DeleteBooking(bookingId);
            if (!DeleteBooking)
                return false;

            //Radera skinkan som är kopplad till bokningen
            _hamStore.Delete(booking.ChristmasHamId);
            _hamStore.SaveToJson();
            return true;
        }

        // Uppdatera order
        public bool UpdateOrder(Booking updatedBooking)
        {
            return _bookingManager.UpdateBooking(updatedBooking);
        }


        // FILTRERING: FILTRERAR PÅ ALLA PROPERTIES (VAR FÖR SIG) I BOOKING-ClASS.
        public IEnumerable<Booking> FilterOrder(int businessId, Func<Booking, bool> predicate)
        {
            var myOrders = GetMyOrders(businessId);
            return myOrders.Where(predicate);            
        }

        // Företaget anger businessId, och ser bokningar som alla user-Private har lagt via UserManager BookHam
        public IEnumerable<Booking> GetMyOrders(int businessId)
        {
            return _bookingManager.GetBookingsByBusinessId(businessId);
        }

        //Hämta CompanyName baserat på BusinessId
        public string? GetCompanyName(int businessId)
        {
            var business = _userStore.Get(businessId);
            return business?.CompanyName;
        }

        //Hämta en specifik skinka via dess hamId (BusinessMenu)
        public ChristmasHam? GetHamById(int hamId)
        {
            return _hamStore.Get(hamId);
        }


        public IEnumerable<ChristmasHam> GetAllHams(int businessId)
        {
            return _hamStore.GetAll().Where(h => h.BusinessId == businessId);
        }

        // LÄGG TILL HAM,
        public void AddHam(ChristmasHam ham)
        {
            _hamStore.Add(ham);
            _hamStore.SaveToJson();

        }


        // TA BORT HAM
        public void DeleteHam(int hamId)
        {
            _hamStore.Delete(hamId);
            _hamStore.SaveToJson();
        }


        //Visa alla ordrar kopplade till företaget, för BusinessMenu.cs
        public void ShowMyOrders(User user)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Your Orders!")
                    .Centered()
                    .Color(Color.Red));

            var myOrders = GetMyOrders(user.Id)?.ToList();

            //Kolla om några ordrar finns
            if (!myOrders.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No orders found for your business.[/]");
                AnsiConsole.MarkupLine("\nPress any key to continue...");
                Console.ReadKey(true);
                return;
            }

            AnsiConsole.MarkupLine($"[green]{user.CompanyName}[/]");
            AnsiConsole.MarkupLine("");

            //Loopa igenom ordrarna och visa deras detaljer
            foreach (var order in myOrders)
            {
                //Hämta julskinkans detaljer
                var ham = GetHamById(order.ChristmasHamId);
                var hamDetails = ham?.Data?.ToString() ?? "No details available";

                //Skapa tabell för att visa orderdetaljer
                var table = new Table
                {
                    Border = TableBorder.Rounded,
                    Expand = true
                };

                table.AddColumn("[yellow]Booking ID[/]");
                table.AddColumn("[blue]Ham[/]");
                table.AddColumn("[purple]Customer ID[/]");

                table.AddRow(
                    order.Id.ToString(),
                    hamDetails,
                    order.UserId.ToString()
                );

                AnsiConsole.Write(table);
            }
            AnsiConsole.MarkupLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }



        //Radera en order via dess bookingId, för BusinessMenu.cs
        public void DeleteOrder(User user)
        {
            Console.Clear();
            AnsiConsole.Write(
                new FigletText("Delete Order")
                .Centered()
                .Color(Color.Red));

            var orders = GetMyOrders(user.Id).ToList();

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
                var companyName = GetCompanyName(order.BusinessId) ?? "Unknown";
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
            var isDeleted = DeleteOrder(bookingId);

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

        public User? GetUserById(int userId)
        {
            return _userStore.Get(userId);
        }
        public IEnumerable<(Booking booking, string email)>GetOrdersWithEmails(int businessId)
        {
            var bookings = _bookingManager.GetBookingsByBusinessId(businessId);
            return bookings.Select(b=>
            {
                var user = _userStore.Get(b.UserId);
                var email = user?.Email ?? "(Unknown)";
                return (booking: b, email: email);//namngivna tupler ist för item2 osv
            });
        }//hit ner
        public void UpdateHam(ChristmasHam ham)
        {
            _hamStore.Update(ham);
            _hamStore.SaveToJson();
        }



    }

}
