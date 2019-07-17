using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyClassLibrary.Queues
{
    public class MultiThreadQueue
    {
        BlockingCollection<object> _jobs = new BlockingCollection<object>();
        public MultiThreadQueue(int numThreads)
        {
            for(int i = 0; i < numThreads; i++)
            {
                var thread = new Thread(OnHandlerStart) { IsBackground = true };
                thread.Start();
            }
        }
        public void Enqueue(object job)
        {
            if (!_jobs.IsAddingCompleted)
            {
                _jobs.Add(job);
            }
        }
        public void Stop()
        {
            _jobs.CompleteAdding();
        }
        private void OnHandlerStart()
        {
            foreach(object job in _jobs.GetConsumingEnumerable(CancellationToken.None))
            {
                Console.WriteLine(job);
                Thread.Sleep(10);
            }
        }
    }
}
