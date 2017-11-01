using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.DbConnectionPool
{
    public abstract class AbstractPool<T> : IPool<T>
    {


        public abstract T Acquire();

        public void Return(T o)
        {
            if (Validate(o))
            {
                ReturnToPool(o);
            }
            else
            {
                HandleInvalidReturn(o);
            }
        }
        protected abstract void ReturnToPool(T o);

        protected abstract void HandleInvalidReturn(T t);

        protected abstract bool Validate(T o);
        public abstract void Shutdown();
    }
}
