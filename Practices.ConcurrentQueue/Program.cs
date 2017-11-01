using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Practices.ConcurrentQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<int> queue = new ConcurrentQueue<int>(new Queue<int>(2));
            queue.Enqueue(0);
            queue.Enqueue(1);
            queue.Enqueue(2);
            // 队列的大小并不受限于2
            Console.WriteLine(queue.Count);

        }
    }
}
