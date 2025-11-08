#if false

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BookAChristmasHam.Models;
//using BookAChristmasHam.Service;

//namespace BookAChristmasHam.Managers
//{
//    // BusinessManager hanterar affärslogik relaterad till Business, ChristmasHam och Booking(order)
//    public class BusinessManager
//    { 
       
//        // lagringsinstans (_bookingStore) för Booking. Hanterar bokningar (List<Booking> _items). Innehåller metoder från DataStore-klassen.
//        private readonly DataStore<Booking> _bookingStore; 

//        //konstruktorn
//        public BusinessManager(DataStore<Booking> bookingStore)
//        {
//            _bookingStore = bookingStore;
//        }


//        // Uppdatera bokning
//        public bool UpdateBooking(Booking updatedBooking)
//        {
//            var result = _bookingStore.Update(updatedBooking);
//            if (result)
//            {
//                //_bookingStore.SaveToJson(); // Jhon kommer lägga till denna metod (SaveToJson()) i DataStore-klassen
//            }
//            return result;
//        }

//        // FILTRERINGSMETODER FÖR BOKNINGAR. FILTERAR BOKNINGAR BASERAT PÅ OLIKA KRITERIER. 

//        // Bokningar för ett företag. Svarar på frågan: Vilka företag har gjort Bokningar/beställt/order 
//        public IEnumerable<Booking> GetBookingsForBusiness(int businessId)
//        {
//            return _bookingStore.GetAll()                   // hämtar alla bokningar
//                .Where(b => b.BusinessId == businessId);    // filtrerar bokningar baserat på BusinessId
//        }

//        //Bokningar för en viss skinka
//        public IEnumerable<Booking> GetBookingsForHam(int hamId)
//        {
//            return _bookingStore.GetAll()
//                .Where(b => b.ChristmasHamId == hamId);
//        }

//        //hämtar bokning med specifikt Id ---->ej bra att vara att vara här
//        public IEnumerable<Booking> GetBookingById(int id)
//        {
//            return _bookingStore.GetAll()
//                .Where(b => b.Id == id);
//        }

//    }


//}

// PAUSAR MED DEN-KOMMER VI FÖRMODLIGEN ATT ERSÄTTA.
#endif