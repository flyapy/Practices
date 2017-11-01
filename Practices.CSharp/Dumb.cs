using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.CSharp
{
    partial class Dumb
    {
        partial void MyMethod();
    }

    partial class Dumb
    {
        partial void MyMethod()
        {
            Console.WriteLine("Implementation goes here");
        }
    }
}
