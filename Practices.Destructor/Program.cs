using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.Destructor
{
    class Program
    {
        static void Main(string[] args)
        {
            new Mock();

        }

        class Mock
        {
            public Mock()
            {
                Console.WriteLine($"{GetType().Name} Contructing...");
            }

            ~Mock()
            {
                Console.WriteLine($"{GetType().Name} Destructing...");
            }
        }
    }
}
