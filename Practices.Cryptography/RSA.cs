using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Encodings;


namespace Practices.Cryptography
{
    public class RSA
    {
        public const string ENCODING = "UTF-8";
        public const string KEY_ALGORITHM = "RSA";
	    public const string SIGNATURE_ALGORITHM = "SHA1withRSA";
	    private const string TRANSFORMATION = "RSA/ECB/PKCS1Padding";

	    private const string PUBLIC_KEY = "RSAPublicKey";
	    private const string PRIVATE_KEY = "RSAPrivateKey";

	    /** RSA最大加密明文大小 */
	    private const int MAX_ENCRYPT_BLOCK = 117;

        /** RSA最大解密密文大小 */
        private const int MAX_DECRYPT_BLOCK = 128;



        public struct RSAKEY
        {
            /// <summary>
            /// 公钥
            /// </summary>
            public string PublicKey { get; set; }
            
            /// <summary>
            /// 私钥
            /// </summary>
            public string PrivateKey { get; set; }
        }

        public static RSAKEY GenerateRSAKey()
        {
            //RSA密钥对的构造器
            RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();
            //RSA密钥构造器的参数
            RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(0x3),
                new Org.BouncyCastle.Security.SecureRandom(),
                1024,  //密钥长度
                25);
            //用参数初始化密钥构造器
            keyGenerator.Init(new KeyGenerationParameters(new SecureRandom(), 1024));
            //产生密钥对
            AsymmetricCipherKeyPair keyPair = keyGenerator.GenerateKeyPair();
            //获取公钥和密钥
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;
            SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);

            Asn1Object asn1ObjectPublic = subjectPublicKeyInfo.ToAsn1Object();
            byte[] publicInfoByte = asn1ObjectPublic.GetEncoded("UTF-8");
            Asn1Object asn1ObjectPrivate = privateKeyInfo.ToAsn1Object();
            byte[] privateInfoByte = asn1ObjectPrivate.GetEncoded("UTF-8");
            RSAKEY item = new RSAKEY()
            {
                PublicKey = Convert.ToBase64String(publicInfoByte),
                PrivateKey = Convert.ToBase64String(privateInfoByte)
            };
            return item;
        }

        public static AsymmetricKeyParameter GetPublicKeyParameter(string s)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            byte[] publicInfoByte = Convert.FromBase64String(s);
            Asn1Object pubKeyObj = Asn1Object.FromByteArray(publicInfoByte);//这里也可以从流中读取，从本地导入
            AsymmetricKeyParameter pubKey = PublicKeyFactory.CreateKey(publicInfoByte);
            return pubKey;
        }

        public static AsymmetricKeyParameter GetPrivateKeyParameter(string s)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            byte[] privateInfoByte = Convert.FromBase64String(s);
            // Asn1Object priKeyObj = Asn1Object.FromByteArray(privateInfoByte);//这里也可以从流中读取，从本地导入
            // PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);
            AsymmetricKeyParameter priKey = PrivateKeyFactory.CreateKey(privateInfoByte);
            return priKey;
        }

        public static string EncryptByPrivateKey(string s, string key)
        {
            //非对称加密算法，加解密用
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());

            //加密
            try
            {
                engine.Init(true, GetPrivateKeyParameter(key));
                byte[] byteData = System.Text.Encoding.UTF8.GetBytes(s);
                //var resultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                var resultData = SegmentEncrypt(engine, byteData);
                return Convert.ToBase64String(resultData);
                //Console.WriteLine("密文（base64编码）:" + Convert.ToBase64String(testData) + Environment.NewLine);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string DecryptByPublicKey(string s, string key)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            //非对称加密算法，加解密用
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());

            //解密
            try
            {
                engine.Init(false, GetPublicKeyParameter(key));
                byte[] byteData = Convert.FromBase64String(s);
                //var resultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                var resultData = SegmentDecrypt(engine, byteData);
                return System.Text.Encoding.UTF8.GetString(resultData);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string EncryptByPublicKey(string s, string key)
        {
            //非对称加密算法，加解密用
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());

            //加密
            try
            {
                engine.Init(true, GetPublicKeyParameter(key));
                byte[] byteData = System.Text.Encoding.UTF8.GetBytes(s);
                //var resultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                var resultData = SegmentEncrypt(engine, byteData);

                return Convert.ToBase64String(resultData);
                //Console.WriteLine("密文（base64编码）:" + Convert.ToBase64String(testData) + Environment.NewLine);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string DecryptByPrivateKey(string s, string key)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            //非对称加密算法，加解密用
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());

            //解密
            try
            {
                engine.Init(false, GetPrivateKeyParameter(key));
                byte[] byteData = Convert.FromBase64String(s);
                //var resultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                var resultData = SegmentDecrypt(engine, byteData);
                return System.Text.Encoding.UTF8.GetString(resultData);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static Dictionary<string, object> InitKey()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            RSACryptoServiceProvider rcsp = new RSACryptoServiceProvider();
            rcsp.KeySize = 1024;
            RSAParameters param = rcsp.ExportParameters(true);
            byte[] publicKey = (new byte[0]).Concat(param.Modulus).Concat(param.Exponent).ToArray();
            byte[] privateKey = param.D;
            // TO DO...
            dict.Add(PRIVATE_KEY, privateKey);
            dict.Add(PUBLIC_KEY, publicKey);
            return dict;
        }

        public static void InitKey(out string publicKey, out string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            publicKey = rsa.ToXmlString(false);
            privateKey = rsa.ToXmlString(true);
        }


        private static byte[] SegmentEncrypt(IAsymmetricBlockCipher cipher, byte[] data)
        {
            int len = data.Length;
            MemoryStream ms = new MemoryStream();
            int offset = 0;
            byte[] buf;
            int i = 0;
            while (len - offset > 0)
            {
                if (len - offset > MAX_ENCRYPT_BLOCK)
                {
                    buf = cipher.ProcessBlock(data, offset, MAX_ENCRYPT_BLOCK);
                } 
                else
                {
                    buf = cipher.ProcessBlock(data, offset, len - offset);
                }
                ms.Write(buf, 0, buf.Length);
                i++;
                offset = i * MAX_ENCRYPT_BLOCK; 
            }
            byte[] res = ms.ToArray();
            ms.Close();
            return res;
            
        }

        private static byte[] SegmentDecrypt(IAsymmetricBlockCipher cipher, byte[] data)
        {
            int len = data.Length;
            MemoryStream ms = new MemoryStream();
            int offset = 0;
            byte[] buf;
            int i = 0;
            while (len - offset > 0)
            {
                if (len - offset > MAX_DECRYPT_BLOCK)
                {
                    buf = cipher.ProcessBlock(data, offset, MAX_DECRYPT_BLOCK);
                }
                else
                {
                    buf = cipher.ProcessBlock(data, offset, len - offset);
                }
                ms.Write(buf, 0, buf.Length);
                i++;
                offset = i * MAX_DECRYPT_BLOCK;
            }
            byte[] res = ms.ToArray();
            ms.Close();
            return res;
        }

        public static Dictionary<string, object> BCInitKey()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
            RsaKeyGenerationParameters keyGenParams = new RsaKeyGenerationParameters(Org.BouncyCastle.Math.BigInteger.ValueOf(3), new SecureRandom(), 1024, 25);
            keyPairGenerator.Init(keyGenParams);
            AsymmetricCipherKeyPair keyPair = keyPairGenerator.GenerateKeyPair();
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;
            dict.Add(PRIVATE_KEY, (privateKey as RsaPrivateCrtKeyParameters));
            dict.Add(PUBLIC_KEY, (publicKey as RsaKeyParameters));
            return dict;
        }

        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <param name="keyMap"></param>
        /// <returns></returns>
        public static string GetPrivateKey2(Dictionary<string, object> keyMap)
        {
            if (keyMap == null)
            {
                return null;
            }
            RsaPrivateCrtKeyParameters key = null;
            string privateKeyStr = null;
            try
            {
                key = keyMap[PRIVATE_KEY] as RsaPrivateCrtKeyParameters;
                privateKeyStr = Base64.Encode(new byte[] { });
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return privateKeyStr;
        } 

        public static string GetPublicKey2(Dictionary<string, object> keyMap)
        {

            if (keyMap == null)
            {
                return null;
            }
            byte[] key = null;
            string publicKeyStr = null;
            try
            {
                key = keyMap[PUBLIC_KEY] as byte[];
                publicKeyStr = Base64.Encode(key);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return publicKeyStr;
        }

        /// <summary>
        /// RSA加密，根据公钥
        /// </summary>
        /// <param name="plaintext"></param>
        /// <param name="publicKeyStr"></param>
        /// <returns></returns>
        public static string EncryptByPublicKey2(string plaintext, string publicKeyStr)
        {
            byte[] plaintextByteArray = null;
            byte[] ciphertextByteArray = null;
            string res;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKeyStr);
            plaintextByteArray = Encoding.UTF8.GetBytes(plaintext);
            ciphertextByteArray = rsa.Encrypt(plaintextByteArray, false);
            res = Base64.Encode(ciphertextByteArray);
            return res;
        }

        /// <summary>
        /// RSA解密，根据私钥
        /// </summary>
        /// <param name="ciphertext"></param>
        /// <param name="privateKeyStr">C# Formatted Key Text</param>
        /// <returns></returns>
        public static string DecryptByPrivateKey2(string ciphertext, string privateKeyStr)
        {
            byte[] ciphertextByteArray = null;
            byte[] plaintextByteArray = null;
            string res;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKeyStr);
            ciphertextByteArray = Base64.Decode(ciphertext);
            plaintextByteArray = rsa.Decrypt(ciphertextByteArray, false);
            res = Encoding.UTF8.GetString(plaintextByteArray);
            return res;
        }

        public static string EncryptByPrivateKey2(string text, string keyStr)
        {
            throw new NotImplementedException();
        }


        public static string DecryptByPublicKey2(string text, string keyStr)
        {
            throw new NotImplementedException();
        }


    }
}
