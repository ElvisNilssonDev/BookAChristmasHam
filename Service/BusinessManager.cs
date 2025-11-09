using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAChristmasHam.Models;

namespace BookAChristmasHam.Service
{
    // BusinessManager hanterar affärslogik relaterad till Business, ChristmasHam och Booking(order)
    public class BusinessManager
    {

        // lagringsinstans (_bookingStore) för Booking. Hanterar bokningar (List<Booking> _items). Innehåller metoder från DataStore-klassen.
        private readonly DataStore<Booking> _bookingStore;

        //konstruktorn
        public BusinessManager(DataStore<Booking> bookingStore)
        {
            _bookingStore = bookingStore;
        }

        //Radera bokning
        public bool DeleteOrder(int bookingId)
        {
            var booking = _bookingStore.GetAll().FirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
            {
                return false;
            }
            var result = _bookingStore.Delete(bookingId);
            if (result)
            {
                //_bookingStore.SaveToJson(); kommer sedan att lägga till denna metod (SaveToJson()) i DataStore-klassen
            }
            return result;
        }
        
        // Uppdatera bokning
        public bool UpdateBooking(Booking updatedBooking)
        {
            var result = _bookingStore.Update(updatedBooking);
            if (result)
            {
                //_bookingStore.SaveToJson(); // Jhon kommer lägga till denna metod (SaveToJson()) i DataStore-klassen
            }
            return result;
        }

        // FILTRERINGSMETODER FÖR BOKNINGAR. FILTERAR BOKNINGAR BASERAT PÅ OLIKA KRITERIER. 

        // Bokningar för ett företag
        public IEnumerable<Booking> GetBookingsForBusiness(int businessId)
        {
            return _bookingStore.GetAll()                   // hämtar alla bokningar
                .Where(b => b.BusinessId == businessId);    // filtrerar bokningar baserat på BusinessId
        }

        //Bokningar för en viss skinka
        public IEnumerable<Booking> GetBookingsForHam(int hamId)
        {
            return _bookingStore.GetAll()
                .Where(b => b.ChristmasHamId == hamId);
        }

        //hämtar bokning med specifikt Id ---->ej bra att vara att vara här
        public IEnumerable<Booking> GetBookingById(int id)
        {
            return _bookingStore.GetAll()
                .Where(b => b.Id == id);
        }

    }


}
