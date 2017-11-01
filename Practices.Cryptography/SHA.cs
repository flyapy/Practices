using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Practices.Cryptography
{
    public class SHA
    {
        public static string Encrypt(String srcStr)
        {
            String desStr = "";
            byte[] buf = Encoding.UTF8.GetBytes(srcStr);
            try
            {
                SHA256 sha = SHA256CryptoServiceProvider.Create();
                using (MemoryStream ms = new MemoryStream(buf))
                {
                    byte[] digest = sha.ComputeHash(ms);
                    desStr = ByteArray2HexString(digest);
                }
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return desStr;
        }

        public static string ByteArray2HexString(byte[] byteArray)
        {
            string str;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < byteArray.Length; i++)
            {
                str = (byteArray[i] & 0xFF).ToString("x2");
                sb.Append(str);
            }
            return sb.ToString();
        }
    }

}
