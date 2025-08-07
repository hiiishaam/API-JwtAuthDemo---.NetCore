using JwtAuthDemo.Data;
using JwtAuthDemo.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthDemo.Services
{
    public class UserService
    {
        public AppDbContext _db;
        public UserService(AppDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// get users
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }
        public User AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }
        public User UpdateUser(int id, User user)
        {
            try
            {
                var existingUser = _db.Users.FirstOrDefault(p => p.Id == id);
                if (existingUser == null)
                {
                    return null;
                }
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;
                _db.Users.Update(existingUser);
                _db.SaveChanges();
                return existingUser;
            }
            catch (Exception ex)
            {
                throw new Exception("Error modify User: " + ex.Message);
            }

        }
        public void DeleteUser(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }
        public User GetUserByEmail(string email)
        {
            try {
                var utilisateur =  _db.Users.FirstOrDefault(u => u.Email == email);
                if (utilisateur ==null)
                {
                    throw new ArgumentException("Email cannot be null or empty", nameof(email));
                }
                return utilisateur;
            }
            catch (Exception ex)
            {
              throw new Exception("Error retrieving user by email: " + ex.Message);
            }
            
        }
    }
}


