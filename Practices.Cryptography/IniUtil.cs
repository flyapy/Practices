using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YD.SP.Util
{
    public class IniUtil
    {
        #region  变量

         
        #endregion

        #region  私有方法

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);


        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);


        #endregion

        #region  公共方法



        public static string GetValue(string section, string key, string filename)
        {
            try
            {
                int size = 2048;
                StringBuilder temp = new StringBuilder(size);
                GetPrivateProfileString(section, key, "", temp, size, filename);
                return temp.ToString();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            

        }

        public static bool WriteValue(string section, string key, string value, string filename)
        {
            try
            {
                long length = WritePrivateProfileString(section, key, value, filename);
                return length > 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

    }
}
