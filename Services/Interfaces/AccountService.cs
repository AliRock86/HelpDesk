using HelpDesk.Models;

namespace HelpDesk.Services.Interfaces
{
    public interface AccountService
    {
        public User Login(string userName, string password);
        public byte[] GetSecureSalt();
        public string HashUsingPbkdf2(string password , byte[] salt);
    }
}
