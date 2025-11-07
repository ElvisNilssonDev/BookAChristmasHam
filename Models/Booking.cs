using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAChristmasHam.Models
{
    public class Booking : Interfaces.IHasId
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public int ChristmasHamId { get; set; }

        // om vi vill lägga till fler properties i framtiden
        //public DateTime BookingDate { get; set; }
        //public int Quantity { get; set; }   = 0;
    }
}
