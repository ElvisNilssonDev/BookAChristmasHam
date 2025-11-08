using BookAChristmasHam.Interfaces;

namespace BookAChristmasHam.Models
{
    public class User : IHasId
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; } // För inloggning
        public UserType Type { get; set; }

        public string Name { get; set; }

  



    }


    public enum UserType
    {
        Private,
        Business
    }



}