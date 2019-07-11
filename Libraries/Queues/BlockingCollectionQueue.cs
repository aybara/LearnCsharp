using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Libraries.Queues
{
    public class BlockingCollectionQueue
    {
        private BlockingCollection<object> _jobs = new BlockingCollection<object>();
        public BlockingCollectionQueue()
        {
            var worker = new Thread(new ThreadStart(OnStart)) { IsBackground = true };
            worker.Start();
        }
        public void Enqueue(object job)
        {
            _jobs.Add(job);
        }
        private void OnStart()
        {
            foreach(object job in _jobs.GetConsumingEnumerable(CancellationToken.None))
            {
                Console.WriteLine(job);
            }
        }
    }
}
