using BookAChristmasHam.Models;
using Spectre.Console;

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
            // skapa user store
            // skapa user store
            UserStore = CreateStore<User>("users.json");
            UserStore.LoadFromJson(); // laddar users direkt

            // Skapa övriga stores, men vänta med att ladda data
            UserStore.LoadFromJson(); // laddar users direkt

            // Skapa övriga stores, men vänta med att ladda data
            HamStore = CreateStore<ChristmasHam>("hams.json");      // Vänta med att ladda      // Vänta med att ladda
            BookingStore = CreateStore<Booking>("bookings.json");   // Vänta med att ladda

   // Vänta med att ladda


        }

        // Hjälpmetod: skapar och laddar en DataStore från fil
        private DataStore<T> CreateStore<T>(string filename) where T : Interfaces.IHasId
        {
            var store = new DataStore<T>(PathService.GetDataFilePath(filename)); // generisk store
            //store.LoadFromJson(); // Läser in data från JSON-fil
            return store;
        }
        public void LoadHamAndBooking()
        {
            BookingStore.LoadFromJson();
            HamStore.LoadFromJson();
        }
    }
}
