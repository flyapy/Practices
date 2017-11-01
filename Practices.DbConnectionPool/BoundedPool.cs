using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.DbConnectionPool
{
    public sealed class BoundedPool<T> : AbstractPool<T>
    {
        private int capacity; // 可持有对象的总数

        private int size = 0; // 已创建的对象数目

        private ConcurrentQueue<T> objects; 

        private IValidator<T> validator;

        private IFactory<T> objectFactory;

        private volatile bool shutdownCalled;

        public BoundedPool(int capacity, IFactory<T> objectFactory, IValidator<T> validator)
        {
            this.objectFactory = objectFactory;
            this.capacity = capacity;
            this.validator = validator;
            this.objects = new ConcurrentQueue<T>(new Queue<T>(capacity));
            this.shutdownCalled = false;
        }

        public override T Acquire()
        {
            T t = default(T);
            if (!shutdownCalled)
            {
                objects.TryDequeue(out t);
            }
            return t;
        }

        public override void Shutdown()
        {
            this.shutdownCalled = true;
            ClearResources();
        }

        private void ClearResources()
        {
            if (validator == null)
            {
                return;
            }
            foreach(var obj in objects)
            {
                validator.Invalidate(obj);
            }
            objects = null;
        }

        protected override void HandleInvalidReturn(T t)
        {
            throw new NotImplementedException();
        }

        protected override void ReturnToPool(T o)
        {
            if (objects == null)
            {
                return;
            }
            Task.Run(() => 
            {
                objects.Enqueue(o);
            });
        }

        protected override bool Validate(T o)
        {
            if (validator == null)
            {
                return true;
            }
            return validator.Validate(o);
        }
    }
}
