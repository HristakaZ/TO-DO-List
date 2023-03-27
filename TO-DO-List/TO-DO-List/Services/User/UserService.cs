using System.Text;
using System.Security.Cryptography;
using TO_DO_List.Services.Contracts;

namespace TO_DO_List.Services.User
{
    public class UserService : IUserService
    {
        public string HashPassword(string password)
        {
            byte[] hashBytes;
            using (var algorithm = SHA512.Create())
            {
                hashBytes = algorithm.ComputeHash(new UTF8Encoding().GetBytes(password));
            }

            return Convert.ToBase64String(hashBytes);
        }
    }
}
