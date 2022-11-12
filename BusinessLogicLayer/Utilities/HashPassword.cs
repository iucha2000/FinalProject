using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Utilities
{
    internal class HashPassword
    {
        public static byte[] encryptPassword(String password)
        {
            byte[] salt;

            new RNGCryptoServiceProvider().GetBytes(salt = new byte[password.Length]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(35);

            return hash;
        } 
    }
}
