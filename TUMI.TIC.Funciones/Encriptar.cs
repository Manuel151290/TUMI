using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TUMI.TIC.Funciones
{
    public class Encriptar
    {
        public static string ObtenerSHA256(string cadena)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();
            byte[]  stream = sha256.ComputeHash(encoding.GetBytes(cadena));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:X2}", stream[i]);
            }
            return sb.ToString();
        }
    }
}
