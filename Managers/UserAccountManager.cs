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
            _userStore = storage.UserStore;
        }
     
        // checka user-info
        public User? Authenticate(string email, string password)
        {
            var user = _userStore.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);
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
        
        // alla users
        public IEnumerable<User> GetAllUsers()
        {
            return _userStore.GetAll();
        }

        // allmän filter
        public IEnumerable<User> Filter(Func<User, bool> predicate)
        {
            return GetAllUsers().Where(predicate);
        }

        // filtrera ut företagare
        public IEnumerable<User> GetBusinesses()
        {
            return Filter(u => u.Type == UserType.Business);
        }
        //public int GetNextUserId()
        //{
        //    return _userStore.GetNextId();
        //}


    }
}