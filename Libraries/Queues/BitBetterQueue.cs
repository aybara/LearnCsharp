using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Libraries.Queues
{
    public class BitBetterQueue
    {
        private ConcurrentQueue<object> _jobs = new ConcurrentQueue<object>();
        public BitBetterQueue()
        {
            var worker = new Thread(new ThreadStart(OnStart));
            worker.IsBackground = true;
            worker.Start();
        }
        public void Enqueue(object job)
        {
            _jobs.Enqueue(job);
        }
        private void OnStart()
        {
            while (true)
            {
                if(_jobs.TryDequeue(out object job))
                {
                    Console.WriteLine(job);
                }
            }
        }
    }
}
