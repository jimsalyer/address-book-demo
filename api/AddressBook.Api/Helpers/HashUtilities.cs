using System.Security.Cryptography;
using System.Text;

namespace AddressBook.Api.Helpers
{
    public static class HashUtilities
    {
        public static void CreateHash(string value, out byte[] valueHash, out byte[] valueSalt)
        {
            using var hmac = new HMACSHA512();
            valueSalt = hmac.Key;
            valueHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
        }

        public static bool VerifyHash(string value, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
