using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.DbConnectionPool
{
    public interface IValidator<T>
    {
        bool Validate(T o);

        void Invalidate(T o);
    }
}
