using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Practices.Cryptography
{
    public class MD5
    {
        public static string Encode(string str)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
                int len = 0;
                for (int i = 0; i < sb.Length; i++)
                {
                    if (sb[i] != '0')
                    {
                        break;
                    }
                    len++;
                }
                sb.Remove(0, len);
                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
