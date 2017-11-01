using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace YD.SP.Util
{
    /// <summary>
    /// JSON工具类
    /// </summary>
    public class JsonUtil
    {
        public static string ToJson(object obj)
        {
            string json = null;
            try
            {
                json = JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return json;
        }


        public static object ToObject(string json)
        {
            object obj = null;
            try
            {
                obj = JsonConvert.DeserializeObject(json);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return obj;
        }

        public static T ToObject<T>(string json)
        {
            T obj = default(T);
            try
            {
                obj = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return obj;
        }


    }
}
