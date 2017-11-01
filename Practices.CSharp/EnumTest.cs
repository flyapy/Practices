using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Practices.CSharp
{
    class EnumTest
    {
        public static void ParseTest()
        {
            Console.WriteLine((StopBits)Enum.Parse(typeof(StopBits), "1"));
            Console.WriteLine((Parity)Enum.Parse(typeof(Parity), "Odd"));
        }
    }
}
