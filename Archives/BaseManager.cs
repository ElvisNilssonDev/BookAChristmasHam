using BookAChristmasHam.Service;
using BookAChristmasHam.Interfaces;

namespace BookAChristmasHam.Archives
{
    // Abstrakt basklass för hantering av datalagring via DataStore<T>
    // Ger grundläggande CRUD-metoder och sparfunktionalitet till alla managers som ärver
    public abstract class BaseManager<T> where T : IHasId
    {
        protected readonly DataStore<T> _store;

        // Konstruktor som laddar data från JSON-fil vid start
        protected BaseManager(DataStore<T> store)
        {
            _store = store;
            _store.LoadFromJson();
        }

        // Hämtar alla objekt
        public IEnumerable<T> GetAll()
        {
            return _store.GetAll();
        }

        // Hämtar ett objekt baserat på Id
        public T? Get(int id)
        {
            return _store.Get(id);
        }

        // Lägger till ett nytt objekt
        public void Add(T item)
        {
            _store.Add(item);
        }

        // Uppdaterar ett befintligt objekt
        public bool Update(T item)
        {
            return _store.Update(item);
        }

        // Tar bort ett objekt baserat på Id
        public bool Delete(int id)
        {
            return _store.Delete(id);
        }

        // Sparar aktuell data till JSON-fil
        public void Save()
        {
            _store.SaveToJson();
        }
    }
}
