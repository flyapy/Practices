using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.CSharp
{
    class Commodity
    {
        
        public virtual double Price { get; } = 2;

        public double Volume => 4;

        public double Length() => 8;

        //public abstract string Description { get; set; }
    }
}
