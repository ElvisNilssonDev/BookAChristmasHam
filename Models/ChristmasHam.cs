using BookAChristmasHam.Interfaces;

namespace BookAChristmasHam.Models
{
    public class ChristmasHam : IHasId
    {
        public int Id { get; set; }           // ID för skinkan
        public int BusinessId { get; set; }   // ID för företaget som äger skinkan
        public HamData Data { get; set; }     // Egenskaper för skinkan


        // Konstruktor som används för att skapa ett ChrismasHam objekt
        // Här skickar man in alla värden när man skapar skinkan
        public ChristmasHam(int id, int businessId, HamData data)
        {
            Id = id;
            BusinessId = businessId;
            Data = data;
        }


    }
}