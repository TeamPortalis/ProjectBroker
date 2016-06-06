using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBroker.Models.DBModel
{
    public class DBManager
    {
        public static projectbrokerEntities db = new projectbrokerEntities();

        public static string GenerateSalt()
        {
            byte[] saltString = new byte[72];
            var provider = RNGCryptoServiceProvider.Create();
            provider.GetBytes(saltString);
            return Convert.ToBase64String(saltString);
        }

        public static string ComputeHash(string password, string salt)
        {
            SHA512 hashed = SHA512.Create();
            var hashString = password + salt;
            MemoryStream s = new MemoryStream();
            StreamWriter writer = new StreamWriter(s);
            writer.Write(hashString);
            var messageDigest = hashed.ComputeHash(s);
            return Convert.ToBase64String(messageDigest);
        }

        
    }
}
