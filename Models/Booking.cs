using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookAChristmasHam.Interfaces;

namespace BookAChristmasHam.Models
{
    public class Booking : IHasId
    {
        public int Id { get; set; }                // själva bokning
        public int UserId { get; set; }           // Vem har bokat
        public int ChristmasHamId { get; set; }   // Vilken skinka
        public int BusinessId { get; set; }      // vilket företaget som äger skinkan
    }

    // om vi vill lägga till fler properties i framtiden
    //public DateTime BookingDate { get; set; }
    //public int Quantity { get; set; }   = 0;


}
