
using System.Linq.Expressions;
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using Spectre.Console;

namespace BookAChristmasHam.Managers
{
    // Hanterar bokningar för användare 
    public class UserManager
    {
        private readonly StorageService _storageService;
        private readonly UserAccountManager _userAccountManager;

        public UserManager(StorageService storage)
        {
            _storageService = storage;
            _userAccountManager = new UserAccountManager(storage);
        }

        public void PlaceOrder(User user, HamData hamData)
        {

            // hämta företagare
            var businessUsers = _userAccountManager.GetBusinesses().ToList();

            //  finns företagaren ?
            if (!businessUsers.Any())
            {
                AnsiConsole.MarkupLine("[red]No businesses available to deliver hams.[/]");
                return;
            }

            // slump generator.
            var random = new Random();

            // slump index
            var RandomBusinessUser = random.Next(businessUsers.Count);

            // Företag som ska leverera skinkan. Låt programmet välja vilket företag
            var selectedBusiness = businessUsers[RandomBusinessUser];

            // skapa skinkan
            var ham = new ChristmasHam(selectedBusiness.Id, hamData);

            // lägg till och spara, nextid ges via add
            _storageService.HamStore.Add(ham);
            _storageService.HamStore.SaveToJson();

            // skapa bokning
            var NewBooking = new Booking(user.Id, ham.Id, selectedBusiness.Id);

            // lägg till och spara, nextid ges via add
            _storageService.BookingStore.Add(NewBooking);
            _storageService.BookingStore.SaveToJson();

            // Bekräftelse
            AnsiConsole.MarkupLine($"\n[green]Booking created![/]");
            AnsiConsole.MarkupLine($"[blue]Ham:[/] {ham.Data}");
            AnsiConsole.MarkupLine($"[blue]Delivered by:[/] {selectedBusiness.CompanyName}");


        }

        public void SeeMyOrders(User user)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
            new FigletText("My Orders!")
            .Centered()
            .Color(Color.Yellow));

            // hämta bokningar från användaren
            var myBookings = _storageService.BookingStore.GetAll()
                .Where(b => b.UserId == user.Id);

            // finns bokningar från användaren?
            if (!myBookings.Any())
            {
                AnsiConsole.MarkupLine("[red]You have no bookings yet.[/]");
                return;
            }

            // hämta företagare
            var businessUsers = _userAccountManager.GetBusinesses().ToList();

            AnsiConsole.MarkupLine($"[blue]User info:[/] {user}");

            // loopa över listan myBookings
            foreach (var booking in myBookings)
            {
                // varje bokning innehåller en ChristmasHamId
                var hamdId = booking.ChristmasHamId;

                // hämta ham 
                var myHam = _storageService.HamStore.Get(hamdId);

                if (myHam == null)
                {
                    AnsiConsole.MarkupLine($"[red]Ham with ID {hamdId} not found.[/]");
                    continue;
                }

                var CustomerId = booking.UserId;
                // hämta företaget
                var businessId = booking.BusinessId;

                // Hämta företaget som matchar bokningens BusinessId
                var business = businessUsers.FirstOrDefault(b => b.Id == businessId);


                if (business == null)
                {
                    AnsiConsole.MarkupLine($"[red]Business with ID {businessId} not found.[/]");
                    continue;
                }

                var table = new Table();
                table.Border = TableBorder.Rounded; // try: None, Minimal, Double, Ascii2
                table.Expand();

                table.AddColumn("[yellow]Booking ID[/]");
                table.AddColumn("[blue]Ham[/]");
                table.AddColumn("[green]Delivered By[/]");
                table.AddColumn("[purple]CustomerId[/]"); // <-- NEW COLUMN

                table.AddRow(booking.Id.ToString(), myHam.Data.ToString(), business.CompanyName, booking.UserId.ToString());
                

                AnsiConsole.Write(table);

            }





        }










        //// USER-PRIVATE LÄGGER EN ORDER
        //public void BookHam(User user, int hamId)
        //{
        //    if (user.Type == UserType.Business)
        //    {
        //        AnsiConsole.MarkupLine("[red]Companies cannot book hams.[/]");
        //        return;
        //    }

        //    var ham = _hamStore.Get(hamId); // HÄMTER EN HAM MED VISS ID
        //    if (ham == null) return; 

        //    //var boo

        //    //var booking = new Booking // EN NY BOOKING SKAPAS
        //    //{
        //    //    UserId = user.Id, // vem som bokar, user-Private ska boka
        //    //    ChristmasHamId = ham.Id, 
        //    //    BusinessId = ham.BusinessId // varje ham ägs av ett unik företag
        //    //};

        //    //_bookingManager.AddBooking(booking); // ORDER LÄGGS FÖR user-P
        //    //AnsiConsole.MarkupLine("[green]The booking is complete![/]");
        //}

    }



}
