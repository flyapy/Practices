using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

using System.Text;
using System.Threading.Tasks;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;



namespace Practices.Cryptography
{
    public class TripleDES
    {
        private static readonly string IV_PARAMETER_SPEC = "12345678";

        private static readonly string TRANSFORMATION = "DESede/CBC/PKCS5Padding";

        private static readonly string ALGORITHM = "DESede";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keyDigest">密钥的md5摘要，32位</param>
        /// <returns></returns>
        public static string Encrypt(string str, string keyDigest)
        {
            string res = null;
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.Mode = CipherMode.CBC;
                tripleDes.Padding = PaddingMode.PKCS7;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
                tripleDes.Key = Base64.Decode(keyDigest).Take(24).ToArray();
                
                //tdsp.Key = new byte[] { 123, 205, 59, 127, 87, 220, 127, 205, 157, 215, 125, 159, 245, 182, 244, 215, 199, 26, 235, 189, 252, 107, 95, 95 };
                tripleDes.IV = Encoding.UTF8.GetBytes(IV_PARAMETER_SPEC);  //ASCIIEncoding.ASCII.GetBytes(sKey);  
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, tripleDes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                res = Base64.Encode(ms.ToArray());
                ms.Close();
                return res;
            }
        }

        public static string Decrypt(string data, string keyDigest)
        {
            string res = null;
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.Mode = CipherMode.CBC;
                tripleDes.Padding = PaddingMode.PKCS7;
                byte[] inputByteArray = Base64.Decode(data);
                byte[] buf = new byte[inputByteArray.Length];
                tripleDes.Key = Base64.Decode(keyDigest).Take(24).ToArray();
                //tdsp.Key = new byte[] { 123, 205, 59, 127, 87, 220, 127, 205, 157, 215, 125, 159, 245, 182, 244, 215, 199, 26, 235, 189, 252, 107, 95, 95 };
                tripleDes.IV = Encoding.UTF8.GetBytes(IV_PARAMETER_SPEC);  //ASCIIEncoding.ASCII.GetBytes(sKey);  
                System.IO.MemoryStream ms = new System.IO.MemoryStream(inputByteArray);
                using (CryptoStream cs = new CryptoStream(ms, tripleDes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(buf, 0, buf.Length);
                }
                res = Encoding.UTF8.GetString(buf);
                ms.Close();
                return res;
            }
        }



        public static string InitKey()
        {
            return InitKey(new SecureRandom());
        }

        public static string InitKey(string seed)
        {
            SecureRandom rnd = new SecureRandom();
            if (seed != null)
            {
                rnd.SetSeed(Base64.Decode(seed));
            }
            return InitKey(rnd);
        }

        private static string InitKey(SecureRandom rnd)
        {
            KeyGenerationParameters keyGenParams = new KeyGenerationParameters(rnd, 24 * 8);
            CipherKeyGenerator keyGen = new CipherKeyGenerator();
            keyGen.Init(keyGenParams);
            byte[] buf = keyGen.GenerateKey();
            return Base64.Encode(buf);
        }

    }
}
