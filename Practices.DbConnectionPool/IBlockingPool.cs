using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.DbConnectionPool
{
    public interface IBlockingPool<T> : IPool<T>
    {
        T Acquire(long timeout);
    }
}
