using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.DbConnectionPool
{
    public interface IPool<T>
    {
        T Acquire();

        void Return(T o);

        void Shutdown();
    }

   
}
