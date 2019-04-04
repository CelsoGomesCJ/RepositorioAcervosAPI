using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Utils
{
    public static class Utilidades
    {
        public static string ObtenhaHashSha256(string texto)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(texto);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
