using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.CSharp
{
    class Animals : System.Collections.CollectionBase
    {
        public Animal this[int index]
        {
            get { return (Animal)List[index]; }
            set { List[index] = value; }
        }

        public void Add(Animal animal)
        {
            List.Add(animal);
        }

        public void Remove(Animal animal)
        {
            List.Remove(animal);
        }
    }
}
