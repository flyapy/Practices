using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace Practices.Cryptography
{
    public class UrlEncode
    {

        /// <summary>
        /// WebSocket URL Encode, Encode Twice
        /// </summary>
        /// <param name="paramStr"></param>
        /// <returns></returns>
        public static string WebSocketUrlEncode(string paramStr)
        {
            string encodedUrl = null;
            try
            {
                encodedUrl = HttpUtility.UrlEncode(paramStr, Encoding.UTF8);
                //encodedUrl = HttpUtility.UrlEncode(encodedUrl, Encoding.UTF8);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return encodedUrl;
        }

        /// <summary>
        /// Http URL Encode, Normal Encode
        /// </summary>
        /// <param name="paramStr"></param>
        /// <returns></returns>
        public static string HttpUrlEncode(string paramStr)
        {
            string encodedUrl = null;
            try
            {
                encodedUrl = HttpUtility.UrlEncode(paramStr, Encoding.UTF8);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return encodedUrl;
        }

    }
}
