using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace YD.SP.Util
{
    public class FileUtil
    {

        public static void Save(string data, string fileName)
        {
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YPStore"));
            if (!dir.Exists)
            {
                dir.Create();
            }
            string destFile = Path.Combine(dir.FullName, fileName);
            File.WriteAllText(destFile, data);

        }

        public static string ReadAllText(string fileName) 
        {
            string text = null;
            try
            {
                text = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "YPStore", fileName));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return text;
        } 

    }
}
