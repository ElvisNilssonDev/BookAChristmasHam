
using BookAChristmasHam.Models;
using BookAChristmasHam.Service;

namespace BookAChristmasHam.Managers
{
    // DESIGNPRINCIP: Direkt sparning efter varje ändring
    //
    // I denna applikation sparas data direkt till fil (via SaveToJson()) i varje CRUD-metod,
    // t.ex. Add(), Update(), Delete(). Detta är ett medvetet val för att undvika att data
    // förloras eller blir inkonsekvent om användaren hoppar mellan menyer utan att avsluta.
    //
    // Eftersom applikationen är menybaserad och användaren kan göra flera operationer
    // utan att explicit spara eller avsluta, säkerställer direkt sparning att:
    // - Alla ändringar är permanenta direkt
    // - Systemet alltid är synkroniserat med filen
    // - Ingen data går förlorad om användaren gör flera operationer i följd
    //
    // Detta upplägg är robust och passar menybaserade appar där användaren interagerar
    // direkt med datan utan batch-hantering eller ångra-funktion.




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

        //-------------
        //Fyll på med CRUD-OPERATION 
        //-------------

        //----------------------------------------------------------------------------------------------------
        //BokningsOperationer: ALLA ORDERS KOMMER FRÅN USER-PRIVATE, OCH NU HAR USER-BUISNESS TILLGÅNG TILL DE
        //----------------------------------------------------------------------------------------------------

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
        public IEnumerable<Booking> FilterOrder(Func<Booking, bool> predicate)
        {
            return _bookingManager.Filter(predicate);
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





        // ----HamOperationer

        // HÄMTA ALLA HAM via businessId. Alltså, varje företag har viss lista av hams i sin depå.
        // Och då behöver företaget ange deras businessId för att få se tillgång till just den. Det är en unik lista för varje företag.

        public IEnumerable<ChristmasHam> GetAllHams(int businessId)
        {
            return _hamStore.GetAll().Where(h => h.BusinessId == businessId);
        }


        //-optional, E.X. FÖRETAGET VILL MODIFFIERA DÅ KAN VI TA TILL CRUD-FUNKTIONER 

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








    }
}
