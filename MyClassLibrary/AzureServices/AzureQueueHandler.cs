using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace MyClassLibrary.AzureServices
{
    public class AzureQueueHandler : QueueClient
    {
        public AzureQueueHandler(string queueName, string connectionStorageStringName = "MyConnectionStorageString") : base(SecureSecrets.GetSecret(connectionStorageStringName), queueName)
        {
            this.CreateAsync().GetAwaiter();
        }
    }
}
