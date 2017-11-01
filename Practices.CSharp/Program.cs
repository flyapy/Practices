using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Practices.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //DoVirtualFunctionTest();
            //DoDateTimeToStringTest();
            string s = HttpUtility.UrlDecode("Gx5w%2B4BJXOP5fUCQ2D0wS4aYmoewwl8%2FPNe5YCZ7QY0Npfo6xTzjSp8aQdaDLQ%2BqZgwzzZ9l39l7zEvk4o0RGZ4Qov7dzRpFGiVXAL4Z4%2FS2kvNOA53LC%2FAIGNU8cZ2GVKh6MyHeor8NahtvNo8XN5N7MQ2miRQXWNuQTtPacNipyWhbXOxyxTaG9RZuwbE2291DiTaYNZnsjURo34cjmzgGPig4O6acsRDoK7FMkfkUdNAgYDdWnJkjh97y0ZbWGaZX0hXstrRR1EK%2FjnkEeUhRD8i9BdxcraQ313ZYYyXdEtjCYzFQceDfEW19%2FTfmGFZcHV0T085Ceue02PClYBksaRFjE5YNx3PZhU1ka2Qv3bM%2F6KOJ08wckBD4b2UJ94wGuPjZjgA6xKPw9y5DvRcR9b%2BaDMJHo8aUyzqLl6jFGOzJ0TCUqaca3JfigI9Y%2BSKhWmIVuRXydDc8TyVUAIY%3D");
            Console.WriteLine(Encoding.UTF8.GetString(Base64Decode("khyu6sanfXQ3wUWd", s)));

            Console.ReadKey();
        }

        private static void DoDateTimeToStringTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN", true)
            {
                DateTimeFormat = { ShortDatePattern = "yyyy-MM-dd", FullDateTimePattern = "yyyy-MM-dd HH:mm:ss", LongTimePattern = "HH:mm:ss" }
            };
            Console.WriteLine("" + DateTime.Now);
        }

        private static void DoVirtualFunctionTest()
        {
            Console.WriteLine(new Commodity().Price);
            Commodity car = new Car();
            Console.WriteLine(car.Price);
            //Console.WriteLine(((Car)car).Price);

            new Cat("mimi").Feed();

            Size size = new Size();
            size.cat = new Cat("mii");
            Size size2 = size;
            Cat cat = new Cat("mimi");
            Cat cat2 = cat;
            Console.WriteLine(Object.ReferenceEquals(size, size2));
            Console.WriteLine(object.ReferenceEquals(cat, cat2));

            EnumTest.ParseTest();
        }

        private static byte[] Base64Decode(string key, string s)
        {
            return RC4(key, Convert.FromBase64String(s));
        }

        private static byte[] RC4(string key, byte[] buf)
        {
            int[] box = new int[256];
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            int i = 0, x = 0, t = 0, len = keyBytes.Length;
            for (i = 0; i < 256; i++)
            {
                box[i] = i;
            }

            for (i = 0; i < 256; i++)
            {
                x = (x + box[i] + keyBytes[i % len]) % 256;
                // swap
                Swap(box, x, i);
            }

            t = 0;
            i = 0;
            len = buf.Length;
            int o = 0, j = 0;
            byte[] res = new byte[len];
            int[] ibox = new int[256];
            Array.Copy(box, 0, ibox, 0, 256);

            for (int k = 0; k < len; k++)
            {
                i = (i + 1) % 256;
                j = (j + ibox[i]) % 256;
                Swap(ibox, i, j);

                o = ibox[(ibox[i] + ibox[j]) % 256];
                res[k] = (byte)(buf[k] ^ o);
            }
            return res;
        }

        private static void Swap(int[] arr, int i, int j)
        {
            int t = arr[i];
            arr[i] = arr[j];
            arr[j] = t;
        }
    }



    struct Size
    {
        public int length;
        public int width;
        public int height;
        public Cat cat;
    }
}
