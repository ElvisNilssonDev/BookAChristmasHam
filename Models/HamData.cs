using System.Text.Json.Serialization;

namespace BookAChristmasHam.Models
{
    public class HamData
    {



        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WeightInterval WeightInterval { get; set; }  


        public bool Brined { get; set; }                    // Inlagd eller inte
        public bool HasBones { get; set; }                  // Med ben eller benfri
        public bool IsCooked { get; set; }                  // Kokt eller rå
        public int Week { get; set; }                       // Leveransvecka

        public HamData(WeightInterval weightInterval, bool brined, bool hasBones, bool isCooked, int week)
        {
            WeightInterval = weightInterval;
            Brined = brined;
            HasBones = hasBones;
            IsCooked = isCooked;
            Week = week;
        }

        public HamData() { } // Parameterlös konstruktor för serialisering/deserialisering

        public override string ToString()
        {
            return $"{GetWeightLabel()}, " +
                   $"{(Brined ? "Brined" : "Not brined")}, " +
                   $"{(HasBones ? "With bones" : "Boneless")}, " +
                   $"{(IsCooked ? "Cooked" : "Raw")}, " +
                   $"Delivery week: {Week}";
        }

        private string GetWeightLabel()
        {
            return WeightInterval switch
            {
                WeightInterval.Kg3To4 => "3–4 kg",
                WeightInterval.Kg4To5 => "4–5 kg",
                WeightInterval.Kg5To6 => "5–6 kg",
                _ => "Unknown weight"
            };
        }


    }

    public enum WeightInterval
    {
        Kg3To4,
        Kg4To5,
        Kg5To6
    }
}
