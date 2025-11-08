

using BookAChristmasHam.Models;
using BookAChristmasHam.Service;

namespace BookAChristmasHam.Managers
{
        

    public class UserAccountManager
    {
        // user-lagring 
        private readonly DataStore<User> _userStore;

        public UserAccountManager(DataStore<User> userStore)
        {
            _userStore = userStore;
            _userStore.LoadFromJson(); // hämta users från mappen Data/users.json
        }

        // checka user-info
        public User? Authenticate(string email, string password)
        {
            return _userStore.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        // Skapa ny användare
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


        // kan man lägga in fler fkner


    }
}