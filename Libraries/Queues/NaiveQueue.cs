using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.Queues
{
    public class NaiveQueue
    {
        private List<object> _jobs = new List<object>();
        public NaiveQueue()
        {
            Task.Run(() => { OnStart(); });
        }
        public void Enqueue(object job)
        {
            _jobs.Add(job);
        }
        private void OnStart()
        {
            while (true)
            {
                if(_jobs.Count > 0)
                {
                    object job = _jobs.First();
                    _jobs.RemoveAt(0);
                    Console.WriteLine(job);
                }
            }
        }
    }
}
