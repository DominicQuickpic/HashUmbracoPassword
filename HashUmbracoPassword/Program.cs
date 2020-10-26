using System;
using System.Security.Cryptography;
using System.Text;

namespace HashUmbracoPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            var passwordSalted = "CyUlw8OmjDgSIOXHdMPxTENXz8o=";
            string salt;

            string a = StoredPassword(passwordSalted, out salt);
            string b = EncryptOrHashPassword("sss", salt);
        }

        private static string EncryptOrHashPassword(string pass, string salt)
        {
            return LegacyEncodePassword(pass);
        }

        private static string LegacyEncodePassword(string password)
        {
            string result = password;

            HashAlgorithm hashAlgorithm = GetHashAlgorithm(password);
            return result = Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.Unicode.GetBytes(password)));
        }

        private static HashAlgorithm GetHashAlgorithm(string password)
        {
            HMACSHA1 hMACSHA = new HMACSHA1();
            hMACSHA.Key = Encoding.Unicode.GetBytes(password);
            return hMACSHA;
        }

        private static string StoredPassword(string storedString, out string salt)
        {
            string text = GenerateSalt();
            salt = storedString.Substring(0, text.Length);
            return storedString.Substring(text.Length);
        }

        private static string GenerateSalt()
        {
            byte[] array = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(array);
            return Convert.ToBase64String(array);
        }
    }
}
