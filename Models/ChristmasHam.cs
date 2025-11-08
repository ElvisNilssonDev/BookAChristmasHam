using BookAChristmasHam.Interfaces;

namespace BookAChristmasHam.Models
{
    public class ChristmasHam : IHasId
    {
        public int Id { get; set; }           // ID för skinkan
        public int BusinessId { get; set; }   // ID för företaget som äger skinkan
        public HamData Data { get; set; }     // Egenskaper för skinkan
    }
}