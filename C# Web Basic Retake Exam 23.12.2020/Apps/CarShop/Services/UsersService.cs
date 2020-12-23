using CarShop.Data;

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CarShop.Data.Models;

namespace CarShop.Services
{
    public class UsersService: IUsersService
    {
        private readonly ApplicationDbContext _db;

        public UsersService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Create(string username, string email, string password, string userType)
        {
            bool isMechanic = userType == "Mechanic";

            var user = new User
            {
                Email = email,
                Username = username,
                Password = ComputeHash(password),
                IsMechanic = isMechanic
            };
            this._db.Users.Add(user);
            this._db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var hashPassword = ComputeHash(password);

            var user = this._db.Users.FirstOrDefault(
                x => x.Username == username && x.Password == hashPassword);
            return user?.Id;
        }

        public bool IsUserMechanic(string Userid)
        {
            var isMechanic = this._db.Users.FirstOrDefault(x => x.Id == Userid).IsMechanic;

            return isMechanic;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this._db.Users.Any(x => x.Username == username);
        }

        public bool IsEmailAvailable(string email)
        {
            return !this._db.Users.Any(x => x.Email == email);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
