using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace MyClassLibrary.AzureServices
{
    public class AzureServices
    {
        public QueueClient queue;
        public AzureServices() { }
    }
}
