using BookAChristmasHam.Interfaces;
using System.Text.Json.Serialization;

namespace BookAChristmasHam.Models
{
    public class User : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; } // För inloggning

        [JsonConverter(typeof(JsonStringEnumConverter))] // printar ut sträng i json, istället för 0/1 för type.
        public UserType Type { get; set; }



    }

    // Två typer of users 
    public enum UserType
    {
        Private,
        Business
    }



}