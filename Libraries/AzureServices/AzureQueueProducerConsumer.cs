using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Libraries.AzureServices
{
    public class AzureQueueProducerConsumer<T> : IProducerConsumerCollection<T>
    {
        private CloudStorageAccount storageAccount = null;
        public CloudQueue queue = null;
        private string _storageConnectionString;
        private TimeSpan? _timeToMessageLive;
        private string _queueName;
        public AzureQueueProducerConsumer(string storageConnectionString, string queueName, TimeSpan? timeToMessageLive = null)
        {
            this._storageConnectionString = storageConnectionString;
            this._queueName = queueName;
            this._timeToMessageLive = timeToMessageLive ?? new TimeSpan(7, 0, 0, 0);
            GetQueueReferenceAsync().Wait();
        }
        private async Task GetQueueReferenceAsync()
        {
            if (CloudStorageAccount.TryParse(_storageConnectionString, out storageAccount))
            {
                var cloudQueueClient = storageAccount.CreateCloudQueueClient();
                queue = cloudQueueClient.GetQueueReference(_queueName);
                await queue.CreateAsync();
            }
        }
        public TimeSpan TimeToMessageLive
        {
            get { return _timeToMessageLive.Value; }
            set
            {
                _timeToMessageLive = value;
            }
        }
        public int Count
        {
            get
            {
                queue.FetchAttributes();
                return queue.ApproximateMessageCount.Value;
            }
        }
        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public void CopyTo(T[] array, int index)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        public bool TryAdd(T item)
        {
            if (item is string)
                return AddToQueueAsync(item.ToString()).GetAwaiter().GetResult();
            return false;
        }
        public bool TryTake(out T item)
        {
            item = (T)Convert.ChangeType(TakeToQueueAsync().GetAwaiter().GetResult(), typeof(T));
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        private async Task<bool> AddToQueueAsync(string text)
        {
            if (queue != null)
            {
                CloudQueueMessage message = new CloudQueueMessage(text);
                await queue.AddMessageAsync(message, TimeToMessageLive, null, null, null);
                return true;
            }
            return false;
        }
        private async Task<string> TakeToQueueAsync()
        {
            if (queue != null)
            {
                CloudQueueMessage retrievedMessage = await queue.GetMessageAsync();
                await queue.DeleteMessageAsync(retrievedMessage);
                return retrievedMessage.AsString;
            }
            return string.Empty;
        }
    }
}
