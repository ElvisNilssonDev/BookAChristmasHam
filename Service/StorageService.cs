using BookAChristmasHam.Models;

namespace BookAChristmasHam.Service
{// Använder StorageService för att undvika duplicerad lagring och hålla data synkroniserad


    // Centraliserar all lagring i applikationen
    public class StorageService
    {
        // Lagring för användare
        public DataStore<User> UserStore { get; }

        // Lagring för skinkor
        public DataStore<ChristmasHam> HamStore { get; }

        // Lagring för bokningar
        public DataStore<Booking> BookingStore { get; }

        // Konstruktor: skapar och laddar alla datakällor från mappen Data
        public StorageService()
        {
            UserStore = CreateStore<User>("users.json");
            HamStore = CreateStore<ChristmasHam>("hams.json");
            BookingStore = CreateStore<Booking>("bookings.json");
        }

        // Hjälpmetod: skapar och laddar en DataStore från fil
        private DataStore<T> CreateStore<T>(string filename) where T : Interfaces.IHasId
        {
            var store = new DataStore<T>(PathService.GetDataFilePath(filename)); // generisk store
            store.LoadFromJson(); // Läser in data från JSON-fil
            return store;
        }
    }
}
