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
        private QueueClient queue;
        private SecretClient secret;

        private string connectionStorageString;
        public AzureServices() { }

        private async Task AzureQueueConnection(string queueName)
        {
            queue = new QueueClient(connectionStorageString, queueName);
            await queue.CreateAsync();
        }
        private void AzureKeyVaultConnection(string clientId, string clientSecret, string tenantId, Uri keyVaultUri)
        {
            secret = new SecretClient(keyVaultUri, new ClientSecretCredential(tenantId, clientId, clientSecret));
        }
        
        public string EnQueueMessage(string message, string queueName)
        {
            if (queue == null)
            {
                AzureKeyVaultConnection(SecureSecrets.GetSecret("AzureKeyVaultClientId"), SecureSecrets.GetSecret("AzureKeyVaultClientSecret"), SecureSecrets.GetSecret("AzureTranslatorTenantId"), new Uri(SecureSecrets.GetSecret("AzureKeyVaultUrl")));
                AzureQueueConnection(queueName).GetAwaiter();
            }
            var enqueueMessage = queue.EnqueueMessage(message);
        }
    }
}
