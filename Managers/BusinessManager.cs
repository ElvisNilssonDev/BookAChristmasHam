
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
    public class BusinessManager // Hanterar Ham-operationer + booknings-operationer. 
    {
        // HAM-LAGRING + METODER
        private readonly DataStore<ChristmasHam> _hamStore;

        // lagringsinstans (_bookingStore) för Booking. Hanterar bokningar (List<Booking> _items). Innehåller metoder från DataStore-klassen.
        private readonly DataStore<Booking> _bookingStore;

       
        // KONSTRUKTOR
        public BusinessManager(DataStore<ChristmasHam> hamStore, DataStore<Booking> bookingstore)
        {
            _hamStore = hamStore;
            _bookingStore = bookingstore;
        }

        //-------------
        //Fyll på med CRUD-OPERATION 
        //-------------

        // ----BokningsOperationer

       
        // Ta bort en order
        public bool DeleteOrder(int bookingId)
        {
            var isDeleted = _bookingStore.Delete(bookingId);
            if (isDeleted)
            {
                //_bookingStore.SaveToJson();
            }
            return isDeleted;
        }


        // Uppdatera bokning
        public bool UpdateOrder(Booking updatedBooking)
        {
            var result = _bookingStore.Update(updatedBooking);
            if (result)
            {
                //_bookingStore.SaveToJson(); // Jhon kommer lägga till denna metod (SaveToJson()) i DataStore-klassen
            }
            return result;
        }


        // FILTRERING: FILTRERAR PÅ ALLA PROPERTIES (VAR FÖR SIG) I BOOKING-ClASS.
        public IEnumerable<Booking> Filter(Func<Booking, bool> predicate)
        {
            return _bookingStore.GetAll().Where(predicate);
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
