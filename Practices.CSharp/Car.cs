using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.CSharp
{
    
    class Car : Commodity
    {
        public override double Price { get; } = 1024;

        //public override double Volumn => 2048;

        public double Volumn => 2048;

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
