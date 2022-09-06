using HelpDesk.Models;
using HelpDesk.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace HelpDesk.Services
{
    public class AccountServiceImpl : AccountService
    {
        HelpDbContext _context = new HelpDbContext();
        public User Login(string userName, string password)
        {
            var user = _context.Users.Where(user => user.Active == true && user.UserName == userName).SingleOrDefault();

            if (user != null)
            {
                var passwordHash = HashUsingPbkdf2(password, Convert.FromBase64String(user.Salt));
                if (user.Password != passwordHash)
                {
                    return user;
                }
            }
            return user;
        }

        public byte[] GetSecureSalt()
        {
            return RandomNumberGenerator.GetBytes(32);
        }
        public string HashUsingPbkdf2(string password, byte[] salt)
        {
            byte[] derivedKey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, iterationCount: 100000, 32);

            return Convert.ToBase64String(derivedKey);
        }
    }
}
