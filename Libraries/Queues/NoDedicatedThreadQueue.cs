using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Libraries.Queues
{
    public class NoDedicatedThreadQueue
    {
        private Queue<object> _jobs = new Queue<object>();
        private bool _delegateQueuedOrRunning = false;
        public void Enqueue(object job)
        {
            lock (_jobs)
            {
                _jobs.Enqueue(job);
                if(!_delegateQueuedOrRunning)
                {
                    _delegateQueuedOrRunning = true;
                    ThreadPool.UnsafeQueueUserWorkItem(ProcessQueuedItems, null);
                }
            }
        }
        private void ProcessQueuedItems(object ignored)
        {
            while (true)
            {
                object job;
                lock (_jobs)
                {
                    if(_jobs.Count == 0)
                    {
                        _delegateQueuedOrRunning = false;
                        break;
                    }
                    job = _jobs.Dequeue();
                }

                try
                {
                    Console.WriteLine(job);
                }
                catch
                {
                    ThreadPool.UnsafeQueueUserWorkItem(ProcessQueuedItems, null);
                    throw;
                }
            }
        }
    }
}
