using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.CSharp
{
    //class Cat : IFeed, Animal
    //{
    //}

    class Cat : Animal, IFeed
    {
        public Cat(string name) : base(name)
        {
            //Codes goes here
        }

        public Cat(string name, string gender) : this(name)
        {

        }

        public void Feed()
        {
            Console.WriteLine($"I'm {name}, i'm feeding myself, don't bother me.");
        }
    }
}
