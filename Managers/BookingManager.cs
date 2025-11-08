using BookAChristmasHam.Models;
using BookAChristmasHam.Service;


namespace BookAChristmasHam.Managers
{
    // HANTERAR BOKNINGAR OCH FILTRERINGAR (INOM BOOKIG-CLASS)
    public class BookingManager
    {
        // lagringsinstans (_bookingStore) för Booking. Hanterar bokningar (List<Booking> _items). Innehåller metoder från DataStore-klassen.
        private readonly DataStore<Booking> _bookingStore;

        // 
        public BookingManager(DataStore<Booking> bookingStore)
        {
            _bookingStore = bookingStore;
        }


        // lägg/skapa en bokning (Create)
        public void AddOrder(Booking booking)
        {
            _bookingStore.Add(booking);            // NATALIE FIXA EN Add() metod i DATASTORE
            _bookingStore.SaveToJson();            //JHON FIXAR EN SaveToJson() METOD I DATASTORE
        }


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


        // FILTRERING: FILTRERAR PÅ ALLA PROPERTIES (VAR FÖR SIG) I BOOKING-ClASS
        public IEnumerable<Booking> Filter(Func<Booking, bool> predicate)
        {
            return _bookingStore.GetAll().Where(predicate);
        }





    }
}
