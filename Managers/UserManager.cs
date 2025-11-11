
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;
using Spectre.Console;

namespace BookAChristmasHam.Managers
{
    // HÄR ÄR DET TÄNKT ATT USER-PRIVATE SER ALLA HAMS OCH LÄGGER EN ORDER (skapar bokningar)
    public class UserManager
    {
        // HÄMTAR LAGRING FÖR HAM SOM EXISTERAR i _hamStore+CRUD-FUNKTIONER
        private readonly DataStore<ChristmasHam> _hamStore;

        // HÄMTAR BOKNINGAR SOM HANTERAS AV BookingManager MEN FINNS I _bookingManager
       // private readonly DataStore<Booking> _bookingstore;

        private readonly BookingManager _bookingManager;

        // KONSTROKTUR
        public UserManager(StorageService storage)
        {
            _hamStore = storage.HamStore;
            _bookingManager = new BookingManager(storage);
        }

        // VISAR ALLA HAMS TILL USER-PRIVATE (P)
        public IEnumerable<ChristmasHam> GetAvailableHams()
        {
            return _hamStore.GetAll();
        }

        // USER-PRIVATE LÄGGER EN ORDER
        public void BookHam(User user, int hamId)
        {
            if (user.Type == UserType.Business)
            {
                AnsiConsole.MarkupLine("[red]Companies cannot book hams.[/]");
                return;
            }

            var ham = _hamStore.Get(hamId); // HÄMTER EN HAM MED VISS ID
            if (ham == null) return; 

            var booking = new Booking // EN NY BOOKING SKAPAS
            {
                UserId = user.Id, // vem som bokar, user-Private ska boka
                ChristmasHamId = ham.Id, 
                BusinessId = ham.BusinessId // varje ham ägs av ett unik företag
            };

            _bookingManager.AddBooking(booking); // ORDER LÄGGS FÖR user-P
            AnsiConsole.MarkupLine("[green]The booking is complete![/]");
        }

    }



}
