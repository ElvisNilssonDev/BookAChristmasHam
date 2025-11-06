
using System.Collections;

namespace BookAChristmasHam.Service
{
    // Generisk klass för datalagring
    public class DataStore<T>               // typbegränsar senare
    {

        // skapa lista för lagring av objekt
        private List<T> _items = new List<T>();

        // filväg för JSON-lagring
        private readonly string _filePath;

        // konstruktor 
        public DataStore(string filePath)
        {
            _filePath = filePath;
        }


        // Read-metod för att läsa från JSON-fil
        public IEnumerable<T> GetAll()              // IEnumerable istället för List. Mer flexibel, säkrare. Nackdel: måste skicka Tolist()
        {
            return _items;
        }



    }
}
// DataStore<T> - (skapa Lista och properties ) Read (Skapa även JSON) - @Ehsan 