
using System;
using System.Collections;
using System.Text.Json;
using System.Xml.Linq;

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
        public T? Get(int id)
        {
            return _items.FirstOrDefault(item => item.Id == id);
        }
        public bool Delete(int id)//ta bort objekt med angivet Id
        {
            var item = Get(id);//hitta objektet med det angivna Id
            if (item != null)
            {
                _items.Remove(item);//ta bort objektet från listan
                return true;
                //Kan ha en Save(); här sedan, för att spara om de krashar.
            }
            return false;//om objektet inte hittades
        }
        public bool Update(T item)//uppdatera objekt med angivet Id
        {
            var index = _items.FindIndex(i => i.Id == item.Id);//hitta index för objektet som ska uppdateras
            if (index != -1)//om objektet finns i listan
            {
                _items[index] = item;//uppdatera objektet i listan
                return true;
                //Kan ha en Save(); här också sedan, för att spara om de krashar.
            }
            return false;//om objektet inte hittades
        }

        //Laddar datan från JSON-filen
        public void Load()
        {
            try
            {
                if (File.Exists(_filePath)) //kolla om filen finns
                {
                    //Läser in JSON-filen som en string
                    var jsonString = File.ReadAllText(_filePath);

                    // Deserialisera stringen till en lista av T
                    var loadedItems = JsonSerializer.Deserialize<List<T>>(jsonString);

                    if (loadedItems != null) //kolla om deserialiseringen lyckades
                    {
                        _items = loadedItems;
                        Console.WriteLine($"Loaded {_items.Count} items from {_filePath}."); //Visa antal inlästa objekt
                    }
                    else //Om filen var tom eller ogiltig
                    {
                        Console.WriteLine($"The file {_filePath} was empty or invalid. Starting with an empty list.");
                        _items = new List<T>(); //Starta med en tom lista så att programmet fortsätter fungera
                    }
                }
                else //Om filen inte finns
                {
                    Console.WriteLine($"The file {_filePath} does not exist. Creating a new list.");
                    _items = new List<T>(); //Starta med en tom lista
                }
            }
            catch (Exception ex) //Fånga alla fel som kan uppstå under inläsningen
            {
                Console.WriteLine($"Error loading file {_filePath}: {ex.Message}"); //Visa felmeddelande
                _items = new List<T>(); //Starta med en tom lista för att programmet ska fortsätta fungera
            }
        }
    }


}
// DataStore<T> - (skapa Lista och properties ) Read (Skapa även JSON) - @Ehsan 