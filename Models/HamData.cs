
namespace BookAChristmasHam.Models
{
    // om vi ska filtrera, kan vara lättare skärskilja data för ChristmasHam. Läggs in i ChristmasHam-class
    public class HamData
    {
        public int Weight { get; set; }       // Vikt i kg
        public bool Brined { get; set; }      // Inlagd eller inte
        public bool HasBones { get; set; }    // Med ben eller benfri
        public bool IsCooked { get; set; }    // Kokt eller rå
        public int Week { get; set; }         // Leveransvecka

        public HamData(int weight, bool brined, bool hasBones, bool isCooked, int week)
        {
            Weight = weight;
            Brined = brined;
            HasBones = hasBones;
            IsCooked = isCooked;
            Week = week;

        }
    }
}