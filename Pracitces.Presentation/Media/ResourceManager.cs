using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Practices.Presentation.Media
{
    public class ResourceManager
    {
        static Stream Get(Assembly assembly, string path)
        {
            return assembly.GetManifestResourceStream(assembly.GetName().Name + "." + path);
        }

        public static string GetString(Assembly assembly, string path)
        {
            try
            {
                return StreamHelper.ToString(Get(assembly, path));
            }
            catch
            {
                return null;
            }
        }

        public static ImageSource GetImageSource(Assembly assembly, string path)
        {
            try
            {
                return StreamHelper.ToImageSource(Get(assembly, path));
            }
            catch
            {
                return null;
            }
        }
    }
}
