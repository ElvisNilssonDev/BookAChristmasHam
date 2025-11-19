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
        public BookingManager(StorageService storage)
        {
            _bookingStore = storage.BookingStore;
        }


        // lägg/skapa en bokning (Create)
        public void AddBooking(Booking booking)
        {
            _bookingStore.Add(booking);
            _bookingStore.SaveToJson();
        }


        // Ta bort en order
        public bool DeleteBooking(int bookingId)
        {
            var isDeleted = _bookingStore.Delete(bookingId);
            if (isDeleted)
            {
                _bookingStore.SaveToJson();
            }
            return isDeleted;
        }


        // Uppdatera bokning
        public bool UpdateBooking(Booking updatedBooking)
        {
            var result = _bookingStore.Update(updatedBooking);
            if (result)
            {
                _bookingStore.SaveToJson();
            }
            return result;
        }


        // FILTRERING: FILTRERAR PÅ ALLA PROPERTIES (VAR FÖR SIG) I BOOKING-ClASS
        public IEnumerable<Booking> Filter(Func<Booking, bool> predicate)
        {
            return _bookingStore.GetAll().Where(predicate);
        }

        // ALLA BOKNIGAR från USER-Private(s) FÖR ALLA businessId(ers), EFTERSOM USER-BUISNESS(es) KAN INTE LÄGGA ORDER, SE USERMANAGER
        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookingStore.GetAll();
        }

        // Bokningar för ett företag via businessId
        public IEnumerable<Booking> GetBookingsByBusinessId(int businessId)
        {
            return _bookingStore.GetAll()                   // hämtar alla bokningar
                .Where(b => b.BusinessId == businessId);    // filtrerar bokningar baserat på BusinessId
        }


        // Hämta bokning via bookingId
        public Booking? GetBookingById(int bookingId)
        {
            return _bookingStore.Get(bookingId);
        }
    }
}
