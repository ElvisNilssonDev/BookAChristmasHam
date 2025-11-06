
using System.Collections;

namespace BookAChristmasHam.Service
{
    //Generisk klass för datalagring

    // Förklaring av  where T : Interfaces.IHasId.

    //Global kontrakt för Id. Alla metoder som använder DataStore måste ha Id property och implementera IHasId
    //Detta möjligjör lagring av olika typer av objekt med Id property.
    //Alla metoder som använder DataStore måste ha Id property och implementera IHasId.
    //metoderna måste följa kontraktet. Om inte måste de ha egna kontrakt.
    //Möjliggöra generiska operationer.

    public class DataStore<T> where T : Interfaces.IHasId

    {   // skapa lista för lagring av objekt
        private List<T> _items = new List<T>();

        // filväg för JSON-lagring
        private readonly string _filePath;

        // konstruktor 
        public DataStore(string filePath)
        {
            _filePath = filePath;
        }


        // Hämtar alla objekt från lagringen
        public IEnumerable<T> GetAll()              // IEnumerable istället för List. Mer flexibel, säkrare. Nackdel: måste skicka Tolist()
        {
            return _items;
        }



    }
}
// DataStore<T> - (skapa Lista och properties ) Read (Skapa även JSON) - @Ehsan 