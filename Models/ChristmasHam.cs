using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAChristmasHam.Models
{
    // Klassen ChrismasHam representerar en julskinka som kan beställas av kunden
    public class ChristmasHam
    {
        // Unikt id för skinkan
        private int Id { get; set; }

        // Vikten på skinkan i kg
        public int Weight { get; set; }

        // True om skinkan är inlagd
        public bool Brined { get; set; }

        // True om skinkan har ben
        public bool HasBones { get; set; }

        // True om skinkan är kokt
        public bool IsCooked { get; set; }

        // Veckonummer då de vill ha skinkan
        public int Week { get; set; }


        // Konstruktor som används för att skapa ett ChrismasHam objekt
        // Här skickar man in alla värden när man skapar skinkan
        public ChristmasHam( int id, int weight, bool brined, bool hasBones, bool isCooked, int week) {
            
            Id = id;
            Weight = weight;
            Brined = brined;
            HasBones = hasBones;
            IsCooked = isCooked;
            Week = week;

            }


    }
}
