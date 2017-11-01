using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Practices.Cryptography
{
    /// <summary>
    /// Base64
    /// </summary>
    public class Base64
    {
        public static string Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static byte[] Decode(string data)
        {
            return Convert.FromBase64String(data);
        }

    }
}
