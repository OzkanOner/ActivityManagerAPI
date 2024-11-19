using System.Text;

namespace ActivityManagerAPI.Helpers
{
    public static class PasswordHelper
    {
        public static bool VerifyPasswordHash(string password, string storedHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return storedHash == Convert.ToBase64String(computedHash);
            }
        }
    }
}