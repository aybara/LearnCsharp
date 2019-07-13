using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Libraries.AzureServices;
using Libraries.AzureServices.AzureTranslatorText;
using Libraries.Cache;
using Libraries.Queues;
using Libraries.Math;

namespace Libraries
{
    class Program
    {
        static void Main(string[] args)
        {
            AzureKeyVaultHandler azureKeyVault = new AzureKeyVaultHandler(
                ConfigurationManager.AppSettings.Get("AzureKeyVaultClientId"),
                ConfigurationManager.AppSettings.Get("AzureKeyVaultClientSecret"),
                ConfigurationManager.AppSettings.Get("AzureKeyVaultUrl"));

            string secret = azureKeyVault.GetSecretAsync("HelloWorld").GetAwaiter().GetResult();
        }
    }
}
