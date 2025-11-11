using BookAChristmasHam.Models;
using BookAChristmasHam.Service;

namespace BookAChristmasHam.Managers
{
       
    // hanterar 
    public class UserAccountManager
    {
        // user-lagring 
        private readonly DataStore<User> _userStore;

        //Konstruktor: tar emot StorageService för att hämta UserStore (användardata)
        public UserAccountManager(StorageService storage)
        {

            _userStore = storage.UserStore; // json-laddas in vid instans av UserAccountManager
        }


        ////Konstruktor
        //public UserAccountManager(DataStore<User> userStore)
        //{
        //    _userStore = userStore;
        //    _userStore.LoadFromJson(); // hämta users från mappen Data/users.json
        //}



        // checka user-info
        public User? Authenticate(string email, string password)
        {
            var user = _userStore.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);
            
            if (user != null && user.Type == UserType.Business)
            {
                // Konvertera till Business-objekt (annars blev det bara en User med Type = Business, så business menyn funkar inte)
                var business = new Business
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Username = user.Username,
                    Type = user.Type,
                    CompanyName = (user as Business)?.CompanyName ?? ""
                };
                return business;
            }
            
            return user;
        }

        // Skapa (add) ny användare
        public void Register(User user)
        {
            _userStore.Add(user);
        }

        //för att separera sparning
        public void Save()
        {
            _userStore.SaveToJson();
        }

        public bool EmailExists(string email)
        {
            return _userStore.GetAll().Any(u => u.Email == email);
        }


        // kan man lägga in fler fkner, ex mer CRUD om vi vill


    }
}