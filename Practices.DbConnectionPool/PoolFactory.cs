using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.DbConnectionPool
{
    public sealed class PoolFactory
    {
        private PoolFactory() { }

        public static IPool<T> CreateBoundedPool<T>(int capacity, IFactory<T> factory, IValidator<T> validator)
        {
            return new BoundedPool<T>(capacity, factory, validator);
        }
    }
}
